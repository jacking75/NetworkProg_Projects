using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace AgentLib
{
    class MsgProcess
    {
        
        public void 관리서버로부터_App서버_실행()
        {
            CommonLib.DevLog.Write(string.Format("App 서버 실행 하자~~"));

            AppServerInfo.App서버_실행하기();
        }

        public void 관리서버로부터_App서버_종료()
        {
            CommonLib.DevLog.Write(string.Format("App 서버 종료 하자~~"));

            AppServerInfo.App서버_종료하기();
        }

        public void 관리서버로부터_SVN_패치()
        {
            CommonLib.DevLog.Write(string.Format("SVN 패치 요청 받음. 위치"), CommonLib.LOG_LEVEL.INFO);

            if (AppServerInfo.AppServerProcess != null)
            {
                CommonLib.DevLog.Write(string.Format("App서버가 실행 중이므로 SVN 업데이트 실패"), CommonLib.LOG_LEVEL.ERROR);
                return;
            }

            var result = SVNManage.Update(AppServerInfo.AppServerFullPathDir);

            SendMessages.관리서버에_SVNUpdate결과_보내기(result);
        }

        public bool ProcessIPCMessage(bool 관리서버와연결중인가, Tuple<short, string> packet)
        {
            if (packet == null)
            {
                return false;
            }

            switch ((IPCAppServer.IPC_MSG_TYPE)packet.Item1)
            {
                case IPCAppServer.IPC_MSG_TYPE.RESPONSE_HEALTH_CHECK:
                    {
                        try
                        {
                            var resData = Newtonsoft.Json.JsonConvert.DeserializeObject<IPCAppServer.IPCMsgReSponseHealthCheck>(packet.Item2);

                            if (관리서버와연결중인가)
                            {
                                SendMessages.관리서버에_App서버_상태통보(resData.CurrentDateTime, resData.CurUserCount);
                            }

                            IPCAppServer.CheckAppServerStatus.SendMessageToHostProgram(CommonLib.InnerMsgType.APP_SERVER_STATUS_COMMUNICATE, "AppServer 통신 성공");

                            var message = string.Format("[ProcessIPCMessage] AppServer 통신 성공. UserCount:{0}", resData.CurUserCount);
                            CommonLib.DevLog.Write(message, CommonLib.LOG_LEVEL.INFO);
                        }
                        catch (Exception ex)
                        {
                            var message = string.Format("[ProcessIPCMessage] Exception:{0}", ex.ToString());
                            CommonLib.DevLog.Write(message, CommonLib.LOG_LEVEL.ERROR);
                            var jsonMsg = string.Format("[ProcessIPCMessage] Exception. Json:{0}", packet.Item2);
                            CommonLib.DevLog.Write(jsonMsg, CommonLib.LOG_LEVEL.ERROR);
                        }
                    }
                    break;

                default:
                    {
                        if(packet.Item1 == ProcessCommunicate.ProcessCommu.IPC_NETWORK_ERROR)
                        {
                            CommonLib.DevLog.Write(packet.Item2, CommonLib.LOG_LEVEL.ERROR);
                        }
                    }
                    break;
            }

            return true;
        }

      

        
    }
}
