using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using InnerMsgAgentStatus = CommonLib.InnerMsgAgentStatus;

namespace MainLib
{
    public class Agent
    {
        public string connectionID;
        public string IPAddress;

        InnerMsgAgentStatus agentStatus;
        
        DateTime AppServer_종료_실행_시간 = DateTime.Now;

        public DateTime 최근에_허트비트_받은시간 = DateTime.Now;
        public DateTime 최근에_허트비트_보낸시간 = DateTime.Now;
        

        public CommonLib.InnerMsgAgentStatus GetAgentStatus()
        {
            return agentStatus;
        }

        public void SetAgentStatus(CommonLib.InnerMsgAgentStatus status)
        {
            agentStatus = status;
        }

        public void ChangeAgentStatus(CommonLib.MsgAppServerStatus msg)
        {
            agentStatus.AppServer실행중 = msg.AppServer실행중;
            agentStatus.전체_CPU_사용량 = msg.전체_CPU_사용량;
            agentStatus.프로세스_CPU_사용량 = msg.프로세스_CPU_사용량;
            agentStatus.AppServer메모리_사용량 = msg.AppServer메모리_사용량;
            agentStatus.Agent와AppServer통신가능_여부 = msg.Agent와AppServer통신가능_여부;
            agentStatus.AppServer접속인원수 = msg.UserCount;
        }

        public bool 정상_AppServer_종료_여부(DateTime nowTime)
        {
            var diff = nowTime - AppServer_종료_실행_시간;

            if (diff.Seconds > 10)
            {
                return false;
            }

            return true;
        }


        
        public void App서버_실행_요청(Microsoft.AspNet.SignalR.Hubs.IHubConnectionContext Clients)
        {
            CommonLib.DevLog.Write(string.Format("Agent:{0}", connectionID), CommonLib.LOG_LEVEL.INFO);
            
            Clients.Client(connectionID).관리서버로부터_App서버_실행();
        }

        public void App서버_종료_요청(Microsoft.AspNet.SignalR.Hubs.IHubConnectionContext Clients)
        {
            AppServer_종료_실행_시간 = DateTime.Now;

            CommonLib.DevLog.Write(string.Format("Agent:{0}", connectionID), CommonLib.LOG_LEVEL.INFO);

            Clients.Client(connectionID).관리서버로부터_App서버_종료();
        }

        public void App서버_SVN패치_요청(Microsoft.AspNet.SignalR.Hubs.IHubConnectionContext Clients)
        {
            CommonLib.DevLog.Write(string.Format("SVN patch 요청. Client:{0}.", connectionID), CommonLib.LOG_LEVEL.INFO);

            Clients.Client(connectionID).관리서버로부터_SVN_패치();
        }

        //public void App서버_상태_요청(Microsoft.AspNet.SignalR.Hubs.IHubConnectionContext Clients)
        //{
        //    //CommonLib.DevLog.Write(string.Format("Agent:{0}", connectionID));
        //    Clients.Client(connectionID).관리서버로부터_App서버_상태();
        //}

        //public void App서버_HeartBeat(Microsoft.AspNet.SignalR.Hubs.IHubConnectionContext Clients)
        //{
        //    //CommonLib.DevLog.Write(string.Format("Agent:{0}", connectionID));
        //    //Clients.Client(connectionID).관리서버로부터_HeartBeat();
        //}

        public void 에이전트_접속상태_갱신()
        {
            var diffTime = DateTime.Now.Subtract(최근에_허트비트_받은시간);

            if (diffTime.Seconds >= 10)
            {
                if (agentStatus.에이전트_허트비트_문제발생 == false)
                {
                    agentStatus.에이전트_허트비트_문제발생 = true;

                    CommonLib.InnerMessageManager.AddMsg(new CommonLib.InnerMsg()
                    {
                        type = CommonLib.InnerMsgType.AGENT_NOTIFY_ABNORNAL,
                        connectionID = connectionID,
                        value1 = "에이전트와 통신에 이상 발생",
                    });
                }
            }
            else
            {
                agentStatus.에이전트_허트비트_문제발생 = false;
            }
        }
    }
}
