using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InnerMsgType = CommonLib.InnerMsgType;


namespace AgentLib.IPCAppServer
{
    class CheckAppServerStatus
    {
        public static void ProcessAlive()
        {
            //CommonLib.DevLog.Write(string.Format("App 서버가 살아 있는지 알아보자"));
            try
            {
                var exename = AppServerInfo.AppServerExeFileName.Replace(".exe", "");

                if (AppServerInfo.AppServerProcess == null)
                {
                    var localByName = System.Diagnostics.Process.GetProcessesByName(exename);

                    if (localByName.Length == 1)
                    {
                        AppServerInfo.AppServerProcess = localByName[0];
                        SendMessageToHostProgram(InnerMsgType.APP_SERVER_STATUS_RUNNING, "AppServer 실행 중");
                    }
                }
                else
                {
                    var localByName = System.Diagnostics.Process.GetProcessesByName(exename);

                    if (localByName.Length != 1)
                    {
                        AppServerInfo.AppServerProcess = null;
                        SendMessageToHostProgram(InnerMsgType.APP_SERVER_STATUS_RUNNING, "AppServer 실행 중단");
                    }
                }
            }
            catch (Exception ex)
            {
                CommonLib.DevLog.Write(ex.ToString(), CommonLib.LOG_LEVEL.ERROR);
            }
        }

        public static void 통신하기_AppServer(bool 관리서버와_연결중, ProcessCommunicate.ProcessCommu IPCCommu)
        {
            if (AppServerInfo.AppServerProcess == null)
            {
                관리서버에_통보하기(관리서버와_연결중, "", "0");
                return;
            }

            bool successAppServerHeathcheck = false;
            if (AppServerInfo.IPCUseHttp)
            {
                successAppServerHeathcheck = HTTP로AppServer와_주기적통신(관리서버와_연결중);
            }
            else
            {
                successAppServerHeathcheck = IPCLib으로AppServer와_주기적통신(관리서버와_연결중, IPCCommu);
            }

            if (successAppServerHeathcheck == false)
            {
                관리서버에_통보하기(관리서버와_연결중, "", "0");
            }
        }

        static bool IPCLib으로AppServer와_주기적통신(bool 관리서버와_연결중, ProcessCommunicate.ProcessCommu IPCCommu)
        {
            bool App서버_상태OK = false;

            try
            {
                App서버_상태OK = true;
                var requestHealtCheck = new IPCMsgRequestHealthCheck();
                var jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(requestHealtCheck);

                IPCCommu.SendMessage((short)IPC_MSG_TYPE.REQUEST_HEALTH_CHECK, jsonstring);

                var message = string.Format("[IPCLib으로AppServer와_주기적통신] Count:{0}, To:{1}", IPCCommu.HearbeatFailCount_ThreadSafe(), IPCCommu.OtherPort);
                CommonLib.DevLog.Write(message, CommonLib.LOG_LEVEL.INFO);

                /// 버전 2
                //var packet = new ProcessCommunicate.IPCPacket { PacketIndex = (short)IPC_MSG_TYPE.REQUEST_HEALTH_CHECK, JsonFormat = jsonstring };
                //var result = IPCCommu.ClientSend(packet);
                //var message2 = string.Format("[IPCLib으로AppServer와_주기적통신] Send Result:{0}", result.ToString());
                //CommonLib.DevLog.Write(message, CommonLib.LOG_LEVEL.INFO);
                /// 버전 2
            }
            catch (Exception ex)
            {
                var message = string.Format("[IPCLib으로AppServer와_주기적통신] Exception:{0}", ex.ToString());
                CommonLib.DevLog.Write(message, CommonLib.LOG_LEVEL.ERROR);
            }
            
            return App서버_상태OK;
        }

        static bool HTTP로AppServer와_주기적통신(bool 관리서버와_연결중)
        {
            var reqData = new REQ_ADMIN_SERVER_STATUS();
            RES_ADMIN_SERVER_STATUS ResponData = null;

            try
            {
                var queryResult = SendMessageHttp(reqData, "RequestAdminServerStatus");
                ResponData = Newtonsoft.Json.JsonConvert.DeserializeObject<RES_ADMIN_SERVER_STATUS>(queryResult.Content);

                var response = new IPCMsgReSponseHealthCheck();
                관리서버에_통보하기(관리서버와_연결중, response.CurrentDateTime, "0");
                
                SendMessageToHostProgram(InnerMsgType.APP_SERVER_STATUS_COMMUNICATE, "AppServer 통신 성공");
                return true;
            }
            catch
            {
                SendMessageToHostProgram(InnerMsgType.APP_SERVER_STATUS_COMMUNICATE, "AppServer 통신 실패");
                return false;
            }
        }

        static void 관리서버에_통보하기(bool 관리서버와_연결중, string time, string userCount)
        {
            if (관리서버와_연결중)
            {
                SendMessages.관리서버에_App서버_상태통보(time, userCount);
            }
        }

        static RestSharp.IRestResponse SendMessageHttp(object reqData, string api)
        {
            var serverAddress = "http://localhost:10301/GameService";

            var client = new RestSharp.RestClient(serverAddress);
            string RestAPI = string.Format("{0}", api);

            var request = new RestSharp.RestRequest(RestAPI, RestSharp.Method.POST);
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddBody(reqData);

            var queryResult = client.Execute(request);
            return queryResult;
        }


        public static void SendMessageToHostProgram(InnerMsgType msgType, string message)
        {
            CommonLib.InnerMessageManager.AddMsg(new CommonLib.InnerMsg()
            {
                type = msgType,
                value1 = message
            });
        }
        
    }
}
