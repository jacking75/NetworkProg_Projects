using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json;


namespace MainLib
{
    public class MainLib
    {
        //string serverAddress;
        SeverConfig SeverConfig = new SeverConfig();
        IDisposable SignalR;

        static System.Collections.Concurrent.ConcurrentDictionary<string, Agent> AgentMap = new System.Collections.Concurrent.ConcurrentDictionary<string, Agent>();
        static System.Collections.Concurrent.ConcurrentDictionary<string, Client> ClientMap = new System.Collections.Concurrent.ConcurrentDictionary<string, Client>();

        static public Dictionary<string, string> SVNAddressMap = new Dictionary<string, string>();

        public bool IsCheckRedisStatus;
        RedisChecker RedisStatusChecker = new RedisChecker();


        public void Init(SeverConfig severConfig)
        {
            SeverConfig = severConfig;

            CommonLib.DevLog.Init(CommonLib.LOG_LEVEL.TRACE);
            CommonLib.DevLog.Write(string.Format("Server running on {0}", SeverConfig.ServerAddress), CommonLib.LOG_LEVEL.INFO);

            SetSchedule();

            SignalR = WebApp.Start(SeverConfig.ServerAddress);
        }

        public void Init_Redis(List<Tuple<string, int>> addressList, bool isCheckStatus)
        {
            RedisStatusChecker.Init(addressList);
            IsCheckRedisStatus = isCheckStatus;
        }

        public void Destory()
        {
            SignalR.Dispose();

            ComLib.Scheduling.Scheduler.ShutDown();
        }

        public void ConnectRedis(int redisOfIndex)
        {
            RedisStatusChecker.Connect(redisOfIndex);
        }

        void SetSchedule()
        {
            ComLib.Scheduling.Scheduler.Schedule("CheckRedisStatus",
                                    new ComLib.Scheduling.Trigger().Every(((int)1).Seconds()),
                                        () =>
                                        {
                                            BGCheckRedisStatus();
                                        }
                                    );

            ComLib.Scheduling.Scheduler.Schedule("SaveUserCountToDB",
                                    new ComLib.Scheduling.Trigger().Every(((int)5).Minutes()),
                                        () =>
                                        {
                                            BGSaveUserCountToDB();
                                        }
                                    );
            
        }
                
        void BGCheckRedisStatus()
        {
            if (IsCheckRedisStatus == false)
            {
                return;
            }

            var result = RedisStatusChecker.Check();
            string json = JsonConvert.SerializeObject(result, Formatting.Indented);

            CommonLib.InnerMessageManager.AddMsg(new CommonLib.InnerMsg()
            {
                type = CommonLib.InnerMsgType.REDIS_CHECK_RESULT,
                value1 = json
            });
        }

        void BGSaveUserCountToDB()
        {
            foreach (var agent in AgentMap.Values)
            {
                var AppStatus = agent.GetAgentStatus();

                // 채팅서버가 아닌 것은 무시한다. 채팅서버는 이름이 "ChatServer"로 시작
                if (AppStatus.AppServerName.IndexOf("ChatServer") < 0)
                {
                    continue;
                }

                //CommonLib.DevLog.Write(string.Format("BGSaveUserCountToDB: 동접수{0}", AppStatus.AppServer접속인원수));

                var uvData = new ChatServerUserCount { ChattingServerName = AppStatus.AppServerName, CCU = AppStatus.AppServer접속인원수.ToInt32() };
                MainLib.SendMessageHttp(uvData, SeverConfig.SaveCCUAPIAddress);
            }
        }

        static RestSharp.IRestResponse SendMessageHttp(object reqData, string serverAddressapi)
        {
            var client = new RestSharp.RestClient(serverAddressapi);
            var request = new RestSharp.RestRequest(RestSharp.Method.POST);
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddBody(reqData);

            var queryResult = client.Execute(request);
            return queryResult;
        }

        static public Agent GetAgent(string connectID)
        {
            Agent agent = null;

            AgentMap.TryGetValue(connectID, out agent);

            return agent;
        }

        static public CommonLib.MsgAgentInfoToClientList GetAgentInfoList()
        {
            var agentList = new CommonLib.MsgAgentInfoToClientList();

            foreach (var agent in AgentMap.Values)
            {
                var status = new CommonLib.MsgAgentInfoToClient
                {
                    ConnectionID = agent.connectionID,
                    IPAddress = agent.IPAddress,
                    AppServer실행중 = agent.GetAgentStatus().AppServer실행중,
                    전체_CPU_사용량 = agent.GetAgentStatus().전체_CPU_사용량,
                    프로세스_CPU_사용량 = agent.GetAgentStatus().프로세스_CPU_사용량,
                    AppServer메모리_사용량 = agent.GetAgentStatus().AppServer메모리_사용량,
                    Agent와AppServer통신가능_여부 = agent.GetAgentStatus().Agent와AppServer통신가능_여부,
                    AppServer접속인원수 = agent.GetAgentStatus().AppServer접속인원수
                };

                agentList.list.Add(status);
            }

            return agentList;
        }

        static public bool AddAgent(CommonLib.InnerMsgAgentStatus status)
        {
            var agent = new Agent() { connectionID = status.connectID, IPAddress = status.IP };

            if (AgentMap.TryAdd(status.connectID, agent))
            {
                agent.SetAgentStatus(status);
                CommonLib.DevLog.Write(string.Format("Agent:{0}. 추가", status.connectID), CommonLib.LOG_LEVEL.INFO);
            }
            else
            {
                CommonLib.DevLog.Write(string.Format("Agent:{0}. 추가 실패", status.connectID), CommonLib.LOG_LEVEL.ERROR);
                return false;
            }
                        
            return true;
        }

        static public void ChangeAgentStatus(string connectionID, CommonLib.MsgAppServerStatus msg)
        {
            var agent = GetAgent(connectionID);

            if (agent != null)
            {
                agent.ChangeAgentStatus(msg);
            }
        }

        static public void RemoveAgent(string connectionID)
        {
            Agent agent;

            if (AgentMap.TryRemove(connectionID, out agent))
            {
                CommonLib.DevLog.Write(string.Format("Agent:{0}. 삭제", connectionID), CommonLib.LOG_LEVEL.INFO);
            }
            else
            {
                CommonLib.DevLog.Write(string.Format("Agent:{0}. 삭제 실패", connectionID), CommonLib.LOG_LEVEL.ERROR);
            }
        }

        public static void 에이전트_허트비트_받은시간_갱신(string connectionID)
        {
            var agent = GetAgent(connectionID);

            if (agent != null)
            {
                agent.최근에_허트비트_받은시간 = DateTime.Now;
            }
        }

        public void RequestStartAppServer(string connectID)
        {
            var agentHub = GlobalHost.ConnectionManager.GetHubContext<AgentHub>();
            
            var agent = GetAgent(connectID);

            if (agent != null)
            {
                agent.App서버_실행_요청(agentHub.Clients);
            }
        }

        public void RequestTerminateAppServer(string connectID)
        {
            var agentHub = GlobalHost.ConnectionManager.GetHubContext<AgentHub>();
            
            var agent = GetAgent(connectID);

            if (agent != null)
            {
                agent.App서버_종료_요청(agentHub.Clients);
            }
        }

        public void RequestSVNPatchAppServer(string connectionID)
        {
            var agentHub = GlobalHost.ConnectionManager.GetHubContext<AgentHub>();

            var agent = GetAgent(connectionID);

            if (agent != null)
            {
                agent.App서버_SVN패치_요청(agentHub.Clients);
            }
        }

        public static void 주기적으로_모든_에이전트_접속상태_갱신()
        {
            foreach (var agent in AgentMap.Values)
            {
                agent.에이전트_접속상태_갱신();
            }
        }

        



        //
        public static Client GetClient(string connectID)
        {
            Client client = null;

            ClientMap.TryGetValue(connectID, out client);

            return client;
        }

        public static bool AddClient(CommonLib.InnerMsgClientStatus status)
        {
            var client = new Client() { ConnectionID = status.ConnectID };

            if (ClientMap.TryAdd(status.ConnectID, client))
            {
                CommonLib.DevLog.Write(string.Format("Client:{0}. 추가", status.ConnectID), CommonLib.LOG_LEVEL.INFO);
            }
            else
            {
                CommonLib.DevLog.Write(string.Format("Client:{0}. 추가 실패", status.ConnectID), CommonLib.LOG_LEVEL.ERROR);
                return false;
            }

            return true;
        }

        public static void RemoveClient(string connectionID)
        {
            Client client;

            if (ClientMap.TryRemove(connectionID, out client))
            {
                CommonLib.DevLog.Write(string.Format("Client:{0}. 삭제", connectionID), CommonLib.LOG_LEVEL.INFO);
            }
            else
            {
                CommonLib.DevLog.Write(string.Format("Client:{0}. 삭제 실패", connectionID), CommonLib.LOG_LEVEL.ERROR);
            }
        }

        //<<< 보내기 메시지
        public static void NotifyAgentInfoListToAllClients()
        {
            var msgAgentList = MainLib.GetAgentInfoList();
            string json = JsonConvert.SerializeObject(msgAgentList, Formatting.Indented);

            var clientHub = GlobalHost.ConnectionManager.GetHubContext<ClientHub>();

            foreach(var client in ClientMap.Values)
            {
                clientHub.Clients.Client(client.ConnectionID).관리서버로부터_에이전트_리스트(json);
            }
        }

        public void NotifyRemoveAgentToAllClients(string AgentConnectionID)
        {
            var clientHub = GlobalHost.ConnectionManager.GetHubContext<ClientHub>();

            foreach (var client in ClientMap.Values)
            {
                clientHub.Clients.Client(client.ConnectionID).관리서버로부터_에이전트_삭제(AgentConnectionID);
            }
        }

        public void NotifySVNPatchResultToAllClients(string agentConnectionID, string result, string revision, string error)
        {
            var clientHub = GlobalHost.ConnectionManager.GetHubContext<ClientHub>();

            foreach (var client in ClientMap.Values)
            {
                clientHub.Clients.Client(client.ConnectionID).관리서버로부터_SVNPatch_결과(agentConnectionID, result, revision, error);
            }
        }

        public void NotifyRedisStatus(string jsonFormat)
        {
            var clientHub = GlobalHost.ConnectionManager.GetHubContext<ClientHub>();

            foreach (var client in ClientMap.Values)
            {
                clientHub.Clients.Client(client.ConnectionID).관리서버로부터_Redis_상태(jsonFormat);
            }
        }

        //>>> 보내기 메시지

        public void testapi(int ccu)
        {
            var uvData = new ChatServerUserCount { ChattingServerName = "chatServer_1", CCU = ccu };
            MainLib.SendMessageHttp(uvData, "http://114.52.72.56:8000/MMS/api/StatFromServer");
        }

        class ChatServerUserCount
        {
            public string ChattingServerName;
            public int CCU;
        }
    } // End Class

    public class SeverConfig
    {
        public string ServerAddress;

        public string SaveCCUAPIAddress;
    }
}
