using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;

namespace Client
{
    class Communicator
    {
        HubConnection HubConn = null;
        IHubProxy ClientHubProxy = null;

        string UserName = "모름";

        DateTime 가장_최근_서버자동접속_시도_시간 = DateTime.Now;


        public void Init(string serverAddress)
        {
            HubConn = new HubConnection(serverAddress);

            RegistHubProxy(HubConn);
        }

        void RegistHubProxy(HubConnection hubConn)
        {
            ClientHubProxy = hubConn.CreateHubProxy("ClientHub");

            ClientHubProxy.On("관리서버로부터_재접속_요청", 관리서버로부터_재접속_요청);

            ClientHubProxy.On<string>("관리서버로부터_에이전트_리스트", 관리서버로부터_에이전트_리스트);
            ClientHubProxy.On<string>("관리서버로부터_에이전트_삭제", 관리서버로부터_에이전트_삭제);
            ClientHubProxy.On<string, string, string, string>("관리서버로부터_SVNPatch_결과", 관리서버로부터_SVNPatch_결과);
            ClientHubProxy.On<string>("관리서버로부터_Redis_상태", 관리서버로부터_Redis_상태);
        }


        // 접속
        public bool Connect(string userName)
        {
            UserName = userName;

            try
            {
                if (서버와연결중인가())
                {
                    return false;
                }

                // 접속 시작
                HubConn.Start().Wait();

                관리서버에_클라이언트_정보통보(userName);
            }
            catch (Exception ex)
            {
                CommonLib.DevLog.Write(ex.ToString());
                return false;
            }

            return true;
        }

        // 접속 끊기
        public bool Disconnect()
        {
            if (서버와연결중인가() == false)
            {
                return false;
            }

            try
            {
                HubConn.Stop();
            }
            catch
            {
                return false;
            }

            return true;
        }

        // 접속 끊고 다시 접속 요청
        bool 서버와연결중인가()
        {
            if (HubConn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Disconnected)
            {
                return false;
            }

            return true;
        }



        public void 관리서버에_클라이언트_정보통보(string userName)
        {
            var myIPAddress = CommonLib.Util.IPString(true);

            var msgAgentInfo = new CommonLib.MsgClientInfo()
            {
                UserName = userName,
                IPAddress = myIPAddress,
            };

            string json = JsonConvert.SerializeObject(msgAgentInfo, Formatting.Indented);

            ClientHubProxy.Invoke("Client로부터_정보통보", json);
        }

        public void 관리서버에_AppServer실행요청(string agentConnectionID)
        {
            ClientHubProxy.Invoke("Client로부터_AppServer실행요청", agentConnectionID);
        }

        public void 관리서버에_AppServer종료요청(string agentConnectionID)
        {
            ClientHubProxy.Invoke("Client로부터_AppServer종료요청", agentConnectionID);
        }

        public void 관리서버에_AppServerSVN패치요청(string agentConnectionID)
        {
            ClientHubProxy.Invoke("Client로부터_AppServerSVN패치요청", agentConnectionID);
        }



        public void 관리서버로부터_재접속_요청()
        {
            CommonLib.DevLog.Write(string.Format("관리서버로부터 재접속 요청을 받음"));

            Disconnect();
            Connect(UserName);
        }

        public void 관리서버로부터_에이전트_리스트(string jsonFormat)
        {
            CommonLib.DevLog.Write(string.Format("에이전트 정보 받았음."), CommonLib.LOG_LEVEL.TRACE);

            CommonLib.InnerMessageManager.AddMsg(new CommonLib.InnerMsg() { type = CommonLib.InnerMsgType.AGENT_INFO_LIST, value1 = jsonFormat });
        }

        public void 관리서버로부터_에이전트_삭제(string agentConnectionID)
        {
            CommonLib.DevLog.Write(string.Format("Agnet:{0}. 삭제 통보", agentConnectionID), CommonLib.LOG_LEVEL.INFO);

            CommonLib.InnerMessageManager.AddMsg(new CommonLib.InnerMsg() { type = CommonLib.InnerMsgType.AGENT_REMOVED, connectionID = agentConnectionID });
        }

        public void 관리서버로부터_SVNPatch_결과(string agentConnectionID, string result, string revision, string error)
        {
            CommonLib.DevLog.Write(string.Format("Agnet:{0}.SVN Update 결과:{1}, 리비전:{2}, 에러:{3}", agentConnectionID, result, revision, error), CommonLib.LOG_LEVEL.INFO);
        }

        public void 관리서버로부터_Redis_상태(string jsonFormat)
        {
            CommonLib.InnerMessageManager.AddMsg(new CommonLib.InnerMsg() { type = CommonLib.InnerMsgType.RECEIVE_REDIS_CHECK_RESULT, value1 = jsonFormat });
        }
    }
}
