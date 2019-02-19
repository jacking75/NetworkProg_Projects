using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Net;
using System.Runtime.InteropServices;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;

namespace ProcessCommunicate
{
    public class ProcessCommu
    {
        public const short IPC_NETWORK_ERROR = 1;

        public int MyPort = 0;
        public int OtherPort = 0;
                
        UdpClient IPCSocket;
        //PacketBufferManager PacketBuffer = new PacketBufferManager();

        // 
        Int64 HearbeatFailCount = 0;

        ConcurrentQueue<Tuple<short, string>> ReceivePacketQueue = new ConcurrentQueue<Tuple<short, string>>();


        public void Init(int myPort, int otherPort, int maxPakcetSize, int maxPacketBufferSize)
        {
            MyPort = myPort;
            OtherPort = otherPort;

            IPCSocket = new UdpClient(MyPort);
            //PacketBuffer.Init(maxPacketBufferSize, DefineData.PacketHeaderSize, DefineData.BodySizePosAtHeader, maxPakcetSize);
            PostReceiveMessages();
        }

        public Tuple<short, string> GetPacketData()
        {
            Tuple<short, string> packet;

            if (ReceivePacketQueue.TryDequeue(out packet) == false)
            {
                return null;
            }

            return packet;
        }

        public void SendMessage(short packetID, string jsonString)
        {
            try
            {
                byte[] bodyData = Encoding.UTF8.GetBytes(jsonString);

                List<byte> sendBytes = new List<byte>();
                sendBytes.AddRange(BitConverter.GetBytes((short)packetID));
                sendBytes.AddRange(BitConverter.GetBytes((short)bodyData.Length));
                sendBytes.AddRange(bodyData);

                IPCSocket.Send(sendBytes.ToArray(), sendBytes.Count(), "127.0.0.1", OtherPort);

                System.Threading.Interlocked.Increment(ref HearbeatFailCount);
            }
            catch (Exception ex)
            {
                ReceivePacketQueue.Enqueue(new Tuple<short, string>(IPC_NETWORK_ERROR, ex.ToString()));
            }
        }

        public Int64 HearbeatFailCount_ThreadSafe() { return System.Threading.Interlocked.Read(ref HearbeatFailCount); }

        void PostReceiveMessages()
        {
            try
            {
                IPCSocket.BeginReceive(new AsyncCallback(ReceiveCallback), null);
            }
            catch(Exception ex)
            {
                ReceivePacketQueue.Enqueue(new Tuple<short, string>(IPC_NETWORK_ERROR, ex.ToString()));
            }
        }

        void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                var remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                Byte[] receiveBytes = IPCSocket.EndReceive(ar, ref remoteEndPoint);

                if (receiveBytes.Count() < DefineData.PacketHeaderSize)
                {
                    return;
                }

                var packetID = BitConverter.ToInt16(receiveBytes, 0);
                var packetBodySize = BitConverter.ToInt16(receiveBytes, DefineData.BodySizePosAtHeader);
                var packetBody = new byte[packetBodySize];
                Buffer.BlockCopy(receiveBytes, DefineData.PacketHeaderSize, packetBody, 0, packetBodySize);
                string jsonstring = System.Text.Encoding.GetEncoding("utf-8").GetString(packetBody);

                ReceivePacketQueue.Enqueue(new Tuple<short, string>(packetID, jsonstring));

                System.Threading.Interlocked.Exchange(ref HearbeatFailCount, 0);
                
                //var remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                //Byte[] receiveBytes = IPCSocket.EndReceive(ar, ref remoteEndPoint);
                //PacketBuffer.Write(receiveBytes, 0, receiveBytes.Count());

                //while (true)
                //{
                //    var recvData = PacketBuffer.Read();
                //    if (recvData.Count < 1)
                //    {
                //        break;
                //    }

                //    var packetID = BitConverter.ToInt16(recvData.Array, recvData.Offset);
                //    var packetBodySize = BitConverter.ToInt16(recvData.Array, recvData.Offset + DefineData.BodySizePosAtHeader);
                //    var packetBody = new byte[packetBodySize];
                //    Buffer.BlockCopy(recvData.Array, (recvData.Offset + DefineData.PacketHeaderSize), packetBody, 0, packetBodySize);
                //    string jsonstring = System.Text.Encoding.GetEncoding("utf-8").GetString(packetBody);

                //    ReceivePacketQueue.Enqueue(new Tuple<short, string>(packetID, jsonstring));

                //    System.Threading.Interlocked.Exchange(ref HearbeatFailCount, 0);
                //}
            }
            catch (Exception ex)
            {
                ReceivePacketQueue.Enqueue(new Tuple<short, string>(IPC_NETWORK_ERROR, ex.ToString()));
            }
            finally
            {
                PostReceiveMessages();
            }
        }
        



       

        IpcServerChannel ServerChannel; // AppServer
        IpcClientChannel ClientChannel; // Agent
        RemoteObject IPCObject;

        public const short PACKET_INDEX_DISCONNECT = 10001;


        public void InitServer(string serverName)
        {
            ServerChannel = new IpcServerChannel(serverName);
            ChannelServices.RegisterChannel(ServerChannel, false);

            RemotingConfiguration.RegisterWellKnownServiceType(typeof(RemoteObject), "IPCDataManager", WellKnownObjectMode.Singleton);

            IPCObject = new RemoteObject();
        }

        public void InitClient(string serverName)
        {
            try
            {
                ClientChannel = new IpcClientChannel();
                ChannelServices.RegisterChannel(ClientChannel, false);

                RemotingConfiguration.RegisterWellKnownClientType(typeof(RemoteObject),
                            string.Format("ipc://{0}/IPCDataManager", serverName));

                IPCObject = new RemoteObject();
            }
            catch (System.Security.SecurityException ex)
            {
            }
        }

        
        /// 서버는 클라이언트가 없어도 데이터를 보내고 받을 수 있다.
        /// 그러므로 클라이언트는 받은 메시지가 시간 상 오랜된 것이라면 무시해야 한다.
        /// 물론 무조건 처리해야 하는 데이터라면 다 처리한다..
        public bool ServerSend(IPCPacket packet)
        {
            try
            {
                IPCObject.AppServerToAgent(packet);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IPCPacket ServerReceive()
        {
            try
            {
                var packet = IPCObject.GetAppServerMessage();
                return packet;
            }
            catch
            {
                var packet = new IPCPacket { PacketIndex = PACKET_INDEX_DISCONNECT };
                return packet;
            }
        }

        public bool ClientSend(IPCPacket packet)
        {
            try
            {
                IPCObject.AgentToAppServer(packet);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IPCPacket ClientReceive()
        {
            try
            {
                var packet = IPCObject.GetAgentMessage();
                return packet;
            }
            catch
            {
                var packet = new IPCPacket { PacketIndex = PACKET_INDEX_DISCONNECT };
                return packet;
            }
        }
    }


    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct IPCPacket
    {
        public short PacketIndex;
        public string JsonFormat;
    }

    public class RemoteObject : MarshalByRefObject
    {
        static Queue<IPCPacket> AgentMessage = new Queue<IPCPacket>();
        static Queue<IPCPacket> AppServerMessage = new Queue<IPCPacket>();


        public void AppServerToAgent(IPCPacket packet)
        {
            AgentMessage.Enqueue(packet);
        }

        public void AgentToAppServer(IPCPacket packet)
        {
            AppServerMessage.Enqueue(packet);
        }

        public IPCPacket GetAgentMessage()
        {
            if(AgentMessage.Count() < 1)
            {
                return default(IPCPacket);
            }

            var packet = AgentMessage.Dequeue();
            return packet;
        }

        public IPCPacket GetAppServerMessage()
        {
            if (AppServerMessage.Count() < 1)
            {
                return default(IPCPacket);
            }

            var packet = AppServerMessage.Dequeue();
            return packet;
        }
    }

    public struct IPCTestMsg
    {
        public int N1;
        public string S1;
    }
}
