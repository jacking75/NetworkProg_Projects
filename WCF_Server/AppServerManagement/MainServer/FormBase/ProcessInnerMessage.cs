using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBase
{
    class ProcessInnerMessage
    {
        MainLib.MainLib ServerLogic;


        public void Init(MainLib.MainLib serverLogic)
        {
            ServerLogic = serverLogic;
        }

        public void Process(CommonLib.InnerMsg msg)
        {
            switch (msg.type)
            {
                // Agent
                case CommonLib.InnerMsgType.AGENT_CONNECT:
                    ProcessInnerMsgAgentStatus(isNew: true, connectionID: msg.connectionID, jsonFormatMsg: msg.value1);
                    break;

                case CommonLib.InnerMsgType.AGENT_DISCONNECT:
                    ProcessInnerMsgRemoveAgent(msg.connectionID);
                    break;

                case CommonLib.InnerMsgType.AGENT_CHANGE_STATUS:
                    ProcessInnerMsgAgentStatus(isNew: false, connectionID: msg.connectionID, jsonFormatMsg: msg.value1);
                    break;

                case CommonLib.InnerMsgType.AGENT_RECEIVE_HEARTBEAT:
                    MainLib.MainLib.에이전트_허트비트_받은시간_갱신(connectionID: msg.connectionID);
                    break;

                case CommonLib.InnerMsgType.AGENT_SVN_UPDATE_RESULT:
                    ProcessInnerMsgSvnPatchResult(msg.connectionID, msg.value1);
                    break;

                case CommonLib.InnerMsgType.AGENT_NOTIFY_ABNORNAL:
                    ProcessInnerMsgNotifyAgentAbNormal(connectionID: msg.connectionID, notifyMsg: msg.value1);
                    break;


                // 클라이언트
                case CommonLib.InnerMsgType.CLIENT_CONNECT:
                    ProcessInnerMsgClientStatus(isNew: true, connectionID: msg.connectionID, jsonFormatMsg: msg.value1);
                    break;

                case CommonLib.InnerMsgType.CLIENT_DISCONNECT:
                    ProcessInnerMsgRemoveClient(msg.connectionID);
                    break;

                case CommonLib.InnerMsgType.CLIENT_START_APP_SERVER:
                    ProcessInnerMsgStartAppServerFromClient(msg.connectionID, msg.value1);
                    break;

                case CommonLib.InnerMsgType.CLIENT_STOP_APP_SERVER:
                    ProcessInnerMsgStopAppServerFromClient(msg.connectionID, msg.value1);
                    break;

                case CommonLib.InnerMsgType.CLIENT_SVN_PATCH_APP_SERVER:
                    ProcessInnerMsgSvnPatchAppServerFromClient(msg.connectionID, msg.value1);
                    break;


                case CommonLib.InnerMsgType.REDIS_CHECK_RESULT:
                    ProcessInnerMsgRedisCheckResult(msg.value1);
                    break;
            }
        }

        void ProcessInnerMsgAgentStatus(bool isNew, string connectionID, string jsonFormatMsg)
        {
            if (isNew)
            {
                var agentStatus = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonLib.InnerMsgAgentStatus>(jsonFormatMsg);

                FormUIManager.AddAgentListView(agentStatus);

                MainLib.MainLib.AddAgent(agentStatus);

                CommonLib.DevLog.Write(string.Format("Client:{0}. Agent 새로 등록", connectionID), CommonLib.LOG_LEVEL.INFO);
            }
            else
            {
                var msg = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonLib.MsgAppServerStatus>(jsonFormatMsg);

                FormUIManager.ChangeAgentListView(connectionID, msg);

                MainLib.MainLib.ChangeAgentStatus(connectionID, msg);

                CommonLib.DevLog.Write(string.Format("Client:{0}. Agent 상태 갱신", connectionID));
            }


            MainLib.MainLib.NotifyAgentInfoListToAllClients();
        }

        void ProcessInnerMsgRemoveAgent(string connectionID)
        {
            FormUIManager.RemoveAgentListView(connectionID);

            MainLib.MainLib.RemoveAgent(connectionID);

            ServerLogic.NotifyRemoveAgentToAllClients(connectionID);
        }

        void ProcessInnerMsgSvnPatchResult(string connectionID, string notifyMsg)
        {
            var tokens = CommonLib.Util.패킷_문자열_파싱하기(notifyMsg);

            if (tokens.Count() == 2)
            {
                CommonLib.DevLog.Write(string.Format("Client:{0}.SVN Update 결과:{1}, 리비전:{2}", connectionID, tokens[0], tokens[1]), CommonLib.LOG_LEVEL.INFO);

                ServerLogic.NotifySVNPatchResultToAllClients(connectionID, tokens[0], tokens[1], "없음");
            }
            else
            {
                CommonLib.DevLog.Write(string.Format("Client:{0}.SVN Update 결과:{1}, 리비전:{2}, 에러:{3}", connectionID, tokens[0], tokens[1], tokens[2]), CommonLib.LOG_LEVEL.ERROR);

                ServerLogic.NotifySVNPatchResultToAllClients(connectionID, tokens[0], tokens[1], tokens[2]);
            }
        }

        void ProcessInnerMsgNotifyAgentAbNormal(string connectionID, string notifyMsg)
        {
            FormUIManager.ChangeAgentListView(connectionID, notifyMsg);

            CommonLib.DevLog.Write(string.Format("Client:{0}. Agent에 이상 발생: {1}", connectionID, notifyMsg), CommonLib.LOG_LEVEL.WARN);
        }

        void ProcessInnerMsgClientStatus(bool isNew, string connectionID, string jsonFormatMsg)
        {
            if (isNew)
            {
                var clientStatus = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonLib.InnerMsgClientStatus>(jsonFormatMsg);

                MainLib.MainLib.AddClient(clientStatus);

                CommonLib.DevLog.Write(string.Format("Client:{0}. 클라이언트 새로 등록", connectionID), CommonLib.LOG_LEVEL.INFO);
            }
            else
            {
                CommonLib.DevLog.Write(string.Format("Client:{0}. 클라이언트 상태 갱신", connectionID));
            }
        }

        void ProcessInnerMsgRemoveClient(string connectionID)
        {
            MainLib.MainLib.RemoveClient(connectionID);
        }

        void ProcessInnerMsgStartAppServerFromClient(string clientConnectionID, string agentConnectionID)
        {
            var client = MainLib.MainLib.GetClient(clientConnectionID);
            if (client == null)
            {
                CommonLib.DevLog.Write(string.Format("Client:{0}. 클라이언트 찾지 못함", clientConnectionID), CommonLib.LOG_LEVEL.ERROR);
            }
            CommonLib.DevLog.Write(string.Format("Client:{0}. Agent:{1}", clientConnectionID, agentConnectionID), CommonLib.LOG_LEVEL.INFO);

            ServerLogic.RequestStartAppServer(agentConnectionID);
        }

        void ProcessInnerMsgStopAppServerFromClient(string clientConnectionID, string agentConnectionID)
        {
            var client = MainLib.MainLib.GetClient(clientConnectionID);
            if (client == null)
            {
                CommonLib.DevLog.Write(string.Format("Client:{0}. 클라이언트 찾지 못함", clientConnectionID), CommonLib.LOG_LEVEL.ERROR);
            }
            CommonLib.DevLog.Write(string.Format("Client:{0}. Agent:{1}", clientConnectionID, agentConnectionID), CommonLib.LOG_LEVEL.INFO);

            ServerLogic.RequestTerminateAppServer(agentConnectionID);
        }

        void ProcessInnerMsgSvnPatchAppServerFromClient(string clientConnectionID, string agentConnectionID)
        {
            var client = MainLib.MainLib.GetClient(clientConnectionID);
            if (client == null)
            {
                CommonLib.DevLog.Write(string.Format("Client:{0}. 클라이언트 찾지 못함", clientConnectionID), CommonLib.LOG_LEVEL.ERROR);
            }
            CommonLib.DevLog.Write(string.Format("Client:{0}. Agent:{1}", clientConnectionID, agentConnectionID), CommonLib.LOG_LEVEL.INFO);

            ServerLogic.RequestSVNPatchAppServer(agentConnectionID);
        }

        void ProcessInnerMsgRedisCheckResult(string jsonFormatMsg)
        {
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Tuple<string, string, string>>(jsonFormatMsg);
            FormUIManager.ChangeRedisStatus(result);
           
            ServerLogic.NotifyRedisStatus(jsonFormatMsg);
        }






    }
}
