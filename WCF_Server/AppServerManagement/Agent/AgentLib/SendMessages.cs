using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;

namespace AgentLib
{
    public class SendMessages
    {
        static bool isInit = false;
        static IHubProxy agentHubProxy = null;

        public static void Init(IHubProxy hubProxy)
        {
            if (isInit == false)
            {
                isInit = true;
                agentHubProxy = hubProxy;
            }
        }

        public static void 관리서버에_Agent정보통보()
        {
            var myIPAddress = MainLib.MyIP;
            var msgAgentInfo = new CommonLib.MsgAgentInfo() { appServerName = AppServerInfo.AppServerName,
                                                               ipAddress = myIPAddress,
                                                              appServerFullPathDir = AppServerInfo.AppServerFullPathDir };
            string json = JsonConvert.SerializeObject(msgAgentInfo, Formatting.Indented);

            agentHubProxy.Invoke("Agent로부터_정보통보", json);
        }

        public static void 관리서버에_App서버_상태통보(string currentDateTime, string userCount)
        {
            try
            {
                //if (string.IsNullOrEmpty(currentDateTime))
                //{
                //    return;
                //}

                var exename = AppServerInfo.AppServerExeFileName.Replace(".exe", "");
                ComputerStatus.GetStatus(AppServerInfo.AppServerProcess, exename);

                bool 서버실행중 = true;
                if (AppServerInfo.AppServerProcess == null)
                {
                    서버실행중 = false;
                }

                bool agent와AppServer통신가능_여부 = true;
                if (string.IsNullOrEmpty(currentDateTime))
                {
                    agent와AppServer통신가능_여부 = false;
                }

                var msgAsppServerStatus = new CommonLib.MsgAppServerStatus()
                {
                    AppServer실행중 = 서버실행중,
                    전체_CPU_사용량 = ComputerStatus.전체_CPU_사용량.ToString(),
                    프로세스_CPU_사용량 = ComputerStatus.프로세스_CPU_사용량.ToString(),
                    AppServer메모리_사용량 = ComputerStatus.메모리_사용량.ToString(),
                    Agent와AppServer통신가능_여부 = agent와AppServer통신가능_여부,
                    UserCount = userCount,
                };


                string json = JsonConvert.SerializeObject(msgAsppServerStatus, Formatting.Indented);
                agentHubProxy.Invoke("Agent로부터_App서버_상태", json);
            }
            catch(Exception ex)
            {
                CommonLib.DevLog.Write(ex.ToString(), CommonLib.LOG_LEVEL.ERROR);
            }
        }

        public static void 관리서버에_SVNUpdate결과_보내기(SVNUpdateResult result)
        {
            var resultData = string.Format("{0}#*#{1}#*#{2}", result.IsSuccess, result.Revision, result.ErrorMsg);

            agentHubProxy.Invoke("Agent로부터_SVNUpdate결과", resultData);
        }

        public static void 관리서버에_허트비트_보내기()
        {
            agentHubProxy.Invoke("Agent로부터_허트비트");
        }
    }

    
}
