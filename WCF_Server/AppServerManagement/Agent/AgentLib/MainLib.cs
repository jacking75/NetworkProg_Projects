using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR.Client;

namespace AgentLib
{
    public class MainLib
    {
        public static string MyIP = "127.0.0.1";

        HubConnection hubConnection = null;
        IHubProxy agentHubProxy = null;
        
        MsgProcess MessageProcess = new MsgProcess();

        ProcessCommunicate.ProcessCommu IPCCommu = new ProcessCommunicate.ProcessCommu();

        static DateTime 가장_최근_서버자동접속_시도_시간 = DateTime.Now;
        static DateTime 가장_최근_Update_호출_시간 = DateTime.Now;


        public void Init(bool isEnableInnerMsg, string serverAddress, AppServerConfig appServerConfig, IPCCommuConfig ipcConfig)
        {
            CommonLib.InnerMessageManager.SetEnable(isEnableInnerMsg);
            
            MyIP = appServerConfig.IPAddress;

            AppServerInfo.App서버_정보_설정(appServerConfig);

            hubConnection = new HubConnection(serverAddress);

            RegistHubProxy(hubConnection);

            SendMessages.Init(agentHubProxy);

            IPCCommu.Init(ipcConfig.MyPort, ipcConfig.OtherPort, ipcConfig.MaxPacketSize, ipcConfig.MaxPacketBufferSize);
            IPCCommu.InitClient(appServerConfig.AppServerName);

            ComputerStatus.Init();
        }
                
        void RegistHubProxy(HubConnection hubConn)
        {
            agentHubProxy = hubConn.CreateHubProxy("AgentHub");

            agentHubProxy.On("관리서버로부터_재접속_요청", 관리서버로부터_재접속_요청);

            agentHubProxy.On("관리서버로부터_App서버_실행", MessageProcess.관리서버로부터_App서버_실행);
            agentHubProxy.On("관리서버로부터_App서버_종료", MessageProcess.관리서버로부터_App서버_종료);
            agentHubProxy.On("관리서버로부터_SVN_패치", MessageProcess.관리서버로부터_SVN_패치);
        }


        // 접속
        public bool Connect()
        {
            try
            {
                if (관리서버와연결중인가())
                {
                    return false;
                }

                // 접속 시작
                hubConnection.Start().Wait();

                SendMessages.관리서버에_Agent정보통보();
            }
            catch(Exception ex)
            {
                CommonLib.DevLog.Write(ex.ToString(), CommonLib.LOG_LEVEL.TRACE);
                return false;
            }

            return true;
        }

        // 접속 끊기
        public bool Disconnect()
        {
            if (관리서버와연결중인가() == false)
            {
                return false;
            }

            try
            {
                hubConnection.Stop();
            }
            catch
            {
                return false;
            }

            return true;
        }
                
        public bool 관리서버와연결중인가()
        {
            if (hubConnection.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Disconnected)
            {
                return false;
            }

            return true;
        }

        public bool 서버와_자동_접속_시도()
        {
            TimeSpan diffTime = DateTime.Now.Subtract(가장_최근_서버자동접속_시도_시간);

            if (관리서버와연결중인가() || diffTime.Seconds < 5)
            {
                return false;
            }
            else
            {
                CommonLib.DevLog.Write(string.Format("관리서버에 접속하기"), CommonLib.LOG_LEVEL.INFO);
            }

            가장_최근_서버자동접속_시도_시간 = DateTime.Now;
            
            var connectResult = Connect();
            if (connectResult)
            {
                CommonLib.DevLog.Write(string.Format("관리서버에 접속 성공"), CommonLib.LOG_LEVEL.INFO);
            }

            return connectResult;
        }
        
        public void 관리서버로부터_재접속_요청()
        {
            CommonLib.DevLog.Write(string.Format("관리서버로부터 재접속 요청을 받음"), CommonLib.LOG_LEVEL.INFO);

            Disconnect();
            Connect();
        }

        // 주기적으로 해야할 일을 처리한다. 최소 1초 이상으로 호출해야 한다.
        public void Update()
        {
            try
            {
                TimeSpan diffTime = DateTime.Now.Subtract(가장_최근_Update_호출_시간);
                if (diffTime.Seconds < 2)
                {
                    return;
                }


                IPCAppServer.CheckAppServerStatus.ProcessAlive();

                IPCAppServer.CheckAppServerStatus.통신하기_AppServer(관리서버와연결중인가(), IPCCommu);

                관리서버에_허트비트_보내기();

                ProcessIPCMessage();

                가장_최근_Update_호출_시간 = DateTime.Now;
            }
            catch (Exception ex)
            {
                CommonLib.DevLog.Write(string.Format("[Update()] Exception:{0}", ex.ToString()), CommonLib.LOG_LEVEL.ERROR);
            }
        }

        void 관리서버에_허트비트_보내기()
        {
            if (관리서버와연결중인가())
            {
                SendMessages.관리서버에_허트비트_보내기();
            }
        }

        void ProcessIPCMessage()
        {
            var packet = IPCCommu.GetPacketData();

            var result = MessageProcess.ProcessIPCMessage(관리서버와연결중인가(), packet);
            if (result)
            {
            }
        }
        void ProcessIPCMessage2()
        {
            var packet = IPCCommu.ClientReceive();
            if (packet.PacketIndex == 0 || packet.PacketIndex == ProcessCommunicate.ProcessCommu.PACKET_INDEX_DISCONNECT)
            {
                return;
            }
            
            MessageProcess.ProcessIPCMessage(관리서버와연결중인가(), new Tuple<short,string>(packet.PacketIndex, packet.JsonFormat));
        }

        // IPC 메시지 보내기
        public bool IPCClientSendTest(short packetIndex, string message)
        {
            var sendData = new ProcessCommunicate.IPCTestMsg { N1 = packetIndex, S1 = message };
            var jsonFormat = Newtonsoft.Json.JsonConvert.SerializeObject(sendData);
            var packet = new ProcessCommunicate.IPCPacket { PacketIndex = packetIndex, JsonFormat = jsonFormat };
            
            return IPCCommu.ClientSend(packet);
        }
        // IPC 메시지 받기
        public Tuple<int,string> IPCClientReceiveTest()
        {
            var packet = IPCCommu.ClientReceive();
            var responData = Newtonsoft.Json.JsonConvert.DeserializeObject<ProcessCommunicate.IPCTestMsg>(packet.JsonFormat);
            return new Tuple<int, string>(responData.N1, responData.S1);
        }
        
    }

    public class AppServerConfig
    {
        public bool IPCUseHttp;
        public string IPAddress;
        public string AppServerName;
        public string AppServerFullPathDir;
        public string AppServerExeFileName;
    }

    public class IPCCommuConfig
    {
        public int MyPort           = 0;
        public int OtherPort		= 0;
        public int MaxPacketSize    = 0;
        public int MaxPacketBufferSize = 0;
    }
}
