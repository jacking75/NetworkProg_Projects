using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

using LOG_LEVEL = CommonLib.LOG_LEVEL;

namespace MainLib
{
    public class ClientHub : Hub
    {
        public override Task OnConnected()
        {
            string connectionID = Context.ConnectionId;
            CommonLib.DevLog.Write(string.Format("클라이언트:{0}. 접속", connectionID), CommonLib.LOG_LEVEL.INFO);

            //CommonLib.InnerMessageManager.AddMsg(new CommonLib.InnerMsg() { type = CommonLib.InnerMsgType.CLIENT_CONNECT, connectionID = connectionID });

            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected()
        {
            string connectionID = Context.ConnectionId;
            CommonLib.DevLog.Write(string.Format("클라이언트:{0}. 접속이 끊어짐", connectionID), CommonLib.LOG_LEVEL.INFO);

            CommonLib.InnerMessageManager.AddMsg(new CommonLib.InnerMsg() { type = CommonLib.InnerMsgType.CLIENT_DISCONNECT, connectionID = connectionID });

            return base.OnDisconnected();
        }

        public override Task OnReconnected()
        {
            string connectionID = Context.ConnectionId;
            CommonLib.DevLog.Write(string.Format("클라이언트:{0}. 재 접속", connectionID), CommonLib.LOG_LEVEL.INFO);

            if (MainLib.GetClient(connectionID) == null)
            {
                CommonLib.DevLog.Write(string.Format("Client:{0}. 클라이언트에 재접속 요청", connectionID), CommonLib.LOG_LEVEL.INFO);

                Clients.Client(connectionID).관리서버로부터_재접속_요청();
            }

            return base.OnReconnected();
        }



        


        //<<< 받는 메시지 정의
        public void Client로부터_정보통보(string jsonFormatMsg)
        {
            string connectionID = Context.ConnectionId;
            var clientInfo = JsonConvert.DeserializeObject<CommonLib.MsgClientInfo>(jsonFormatMsg);

            CommonLib.DevLog.Write(string.Format("Client:{0}. AppServerName:{1}", connectionID, clientInfo.UserName), CommonLib.LOG_LEVEL.INFO);


            var clientStatus = new CommonLib.InnerMsgClientStatus()
            {
                ConnectID = connectionID,
                UserName = clientInfo.UserName,
                IP = clientInfo.IPAddress,
            };
            
            string json = JsonConvert.SerializeObject(clientStatus, Formatting.Indented);

            CommonLib.InnerMessageManager.AddMsg(new CommonLib.InnerMsg()
            {
                type = CommonLib.InnerMsgType.CLIENT_CONNECT,
                connectionID = connectionID,
                value1 = json
            });
        }

        public void Client로부터_AppServer실행요청(string agentConnectionID)
        {
            CommonLib.InnerMessageManager.AddMsg(new CommonLib.InnerMsg()
            {
                type = CommonLib.InnerMsgType.CLIENT_START_APP_SERVER,
                connectionID = Context.ConnectionId,
                value1 = agentConnectionID
            });
        }

        public void Client로부터_AppServer종료요청(string agentConnectionID)
        {
            CommonLib.InnerMessageManager.AddMsg(new CommonLib.InnerMsg()
            {
                type = CommonLib.InnerMsgType.CLIENT_STOP_APP_SERVER,
                connectionID = Context.ConnectionId,
                value1 = agentConnectionID
            });
        }

        public void Client로부터_AppServerSVN패치요청(string agentConnectionID)
        {
            CommonLib.InnerMessageManager.AddMsg(new CommonLib.InnerMsg()
            {
                type = CommonLib.InnerMsgType.CLIENT_SVN_PATCH_APP_SERVER,
                connectionID = Context.ConnectionId,
                value1 = agentConnectionID
            });
        }
        //>>> 받는 메시지 정의
              
    }
}
