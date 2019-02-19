using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBase
{
    class Program
    {
        static void Main(string[] args)
        {
            AgentLib.MainLib mainLib = new AgentLib.MainLib();

            try
            {
                var serverAddress = Properties.Settings.Default.ServerAddress;
                
                var appServerConfig = new AgentLib.AppServerConfig
                {
                    IPCUseHttp = Properties.Settings.Default.IPC_USE_HTTP,
                    IPAddress = Properties.Settings.Default.MyIP,
                    AppServerName = Properties.Settings.Default.AppServerName,
                    AppServerFullPathDir = Properties.Settings.Default.AppServerDirFullPath,
                    AppServerExeFileName = Properties.Settings.Default.AppServerExeName,
                };

                var ipcConfog = new AgentLib.IPCCommuConfig
                {
	                MyPort           = Properties.Settings.Default.IPCMyPort,
                    OtherPort = Properties.Settings.Default.IPCOtherPort,
                    MaxPacketSize = Properties.Settings.Default.IPCMaxPacketSize,
                    MaxPacketBufferSize = Properties.Settings.Default.IPCMaxPacketBufferSize,
                };

                bool is개발로그출력 = false;
                mainLib.Init(is개발로그출력, serverAddress, appServerConfig, ipcConfog);
                CommonLib.DevLog.Write(string.Format("Agent 시작!. 이 머신의 IP: {0}", appServerConfig.IPAddress));
                

                if (mainLib.Connect())
                {
                    CommonLib.DevLog.Write("메인 관리 서버에 접속 성공", CommonLib.LOG_LEVEL.INFO);
                }


                while (true)
                {
                    string msg;

                    if (CommonLib.DevLog.GetLog(out msg))
                    {
                        Console.WriteLine(msg);
                    }

                    
                    mainLib.서버와_자동_접속_시도();
                    mainLib.Update();


                    CommonLib.InnerMsg innerMsg;
                    if (CommonLib.InnerMessageManager.GetMsg(out innerMsg))
                    {
                        ProcessInnerMsg(innerMsg);
                    }

                    System.Threading.Thread.Sleep(32);
                }
            }
            catch (Exception ex)
            {
                CommonLib.DevLog.Write("Exception: " + ex.ToString(), CommonLib.LOG_LEVEL.ERROR);
            }
        }

        static void ProcessInnerMsg(CommonLib.InnerMsg msg)
        {
            switch (msg.type)
            {
                case CommonLib.InnerMsgType.APP_SERVER_STATUS_RUNNING:
                    CommonLib.DevLog.Write(msg.value1, CommonLib.LOG_LEVEL.INFO);
                    break;

                case CommonLib.InnerMsgType.APP_SERVER_STATUS_COMMUNICATE:
                    CommonLib.DevLog.Write(msg.value1, CommonLib.LOG_LEVEL.INFO);
                    break;
            }
        }
        
    }
}
