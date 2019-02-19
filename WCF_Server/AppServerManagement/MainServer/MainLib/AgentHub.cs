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
    public class AgentHub : Hub
    {
        public override Task OnConnected()
        {
            string connectionID = Context.ConnectionId;
            CommonLib.DevLog.Write(string.Format("Agent:{0}. 접속", connectionID), CommonLib.LOG_LEVEL.INFO);
                        
            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected()
        {
            string connectionID = Context.ConnectionId;
            CommonLib.DevLog.Write(string.Format("Agent:{0}. 접속이 끊어짐", connectionID), CommonLib.LOG_LEVEL.INFO);

            CommonLib.InnerMessageManager.AddMsg(new CommonLib.InnerMsg() { type = CommonLib.InnerMsgType.AGENT_DISCONNECT, connectionID = connectionID });

            return base.OnDisconnected();
        }

        public override Task OnReconnected()
        {
            string connectionID = Context.ConnectionId;
            CommonLib.DevLog.Write(string.Format("Agent:{0}. 재 접속", connectionID), CommonLib.LOG_LEVEL.INFO);

            if (MainLib.GetAgent(connectionID) == null)
            {
                CommonLib.DevLog.Write(string.Format("Agent:{0}. Agent에 재접속 요청", connectionID), CommonLib.LOG_LEVEL.INFO);

                Clients.Client(connectionID).관리서버로부터_재접속_요청();
            }

            return base.OnReconnected();
        }


        

        //////////////////////////////////////////////////////////////////////////////////////////
        //<<< 받는 메시지 정의
        public void Agent로부터_정보통보(string jsonFormatMsg)
        {
            string connectionID = Context.ConnectionId;
            var agentInfo = JsonConvert.DeserializeObject<CommonLib.MsgAgentInfo>(jsonFormatMsg);

            CommonLib.DevLog.Write(string.Format("Agent:{0}. AppServerName:{1}", connectionID, agentInfo.appServerName), CommonLib.LOG_LEVEL.INFO);


            var agentStatus = new CommonLib.InnerMsgAgentStatus()
            {
                connectID = connectionID,
                에이전트_허트비트_문제발생 = false,
                IP = agentInfo.ipAddress,
                AppServerName = agentInfo.appServerName,
                AppServerFullPath = agentInfo.appServerFullPathDir,
                AppServer실행중 = false,
                전체_CPU_사용량 = "0",
                프로세스_CPU_사용량 = "0",
                AppServer메모리_사용량 = "0",
                Agent와AppServer통신가능_여부 = false,
                AppServer접속인원수 = "0",
            };

            
            string json = JsonConvert.SerializeObject(agentStatus, Formatting.Indented);
            
            CommonLib.InnerMessageManager.AddMsg(new CommonLib.InnerMsg()
            {
                type = CommonLib.InnerMsgType.AGENT_CONNECT,
                connectionID = connectionID,
                value1 = json
            });
            
            //System.Threading.Thread.Sleep(10000);
            //CommonLib.DevLog.Write("호출 완료");
        }

        public void Agent로부터_App서버_상태(string jsonFormatMsg)
        {
            CommonLib.InnerMessageManager.AddMsg(new CommonLib.InnerMsg()
            {
                type = CommonLib.InnerMsgType.AGENT_CHANGE_STATUS,
                connectionID = Context.ConnectionId,
                value1 = jsonFormatMsg
            });
        }

        public void Agent로부터_SVNUpdate결과(string result)
        {
            CommonLib.DevLog.Write(string.Format("Agent:{0}.", Context.ConnectionId), CommonLib.LOG_LEVEL.INFO);

            CommonLib.InnerMessageManager.AddMsg(new CommonLib.InnerMsg()
            {
                type = CommonLib.InnerMsgType.AGENT_SVN_UPDATE_RESULT,
                connectionID = Context.ConnectionId,
                value1 = result
            });
        }

        public void Agent로부터_허트비트()
        {
            CommonLib.InnerMessageManager.AddMsg(new CommonLib.InnerMsg()
            {
                type = CommonLib.InnerMsgType.AGENT_RECEIVE_HEARTBEAT,
                connectionID = Context.ConnectionId,
                value1 = ""
            });
        }
        //>>> 받는 메시지 정의
        //////////////////////////////////////////////////////////////////////////////////////////
              
    }
}
