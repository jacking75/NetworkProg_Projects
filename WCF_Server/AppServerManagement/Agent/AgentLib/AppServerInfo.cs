using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentLib
{
    public class AppServerInfo
    {
        public static bool IPCUseHttp;
        public static string AppServerName;
        public static string AppServerFullPathDir;
        public static string AppServerExeFileName;
        public static System.Diagnostics.Process AppServerProcess;
        static APP_SERVER_STATUS 서버상태 = APP_SERVER_STATUS.STOP;


        public static void 서버상태_설정하기(APP_SERVER_STATUS status) { 서버상태 = status; }
        public static APP_SERVER_STATUS 현재_서버상태() { return 서버상태; }

        static public bool App서버_정보_설정(AppServerConfig appServerConfig)
        {
            IPCUseHttp = appServerConfig.IPCUseHttp;
            AppServerFullPathDir = appServerConfig.AppServerFullPathDir;
            AppServerName = appServerConfig.AppServerName;
            AppServerExeFileName = appServerConfig.AppServerExeFileName;
                        
            CommonLib.DevLog.Write(string.Format("App서버 이름:{0}", AppServerInfo.AppServerName));
            CommonLib.DevLog.Write(string.Format("App서버 full path:{0}", AppServerInfo.AppServerFullPathDir));
            CommonLib.DevLog.Write(string.Format("App서버 실행 파일 이름:{0}", AppServerInfo.AppServerExeFileName));
            return true;
        }

        public static void App서버_실행하기()
        {
            string exeFilePath = System.IO.Path.Combine(AppServerFullPathDir, AppServerExeFileName);

            AppServerProcess = System.Diagnostics.Process.Start(exeFilePath);

            if (AppServerProcess != null)
            {
                서버상태_설정하기(APP_SERVER_STATUS.RUN);

                CommonLib.DevLog.Write(string.Format("App서버: {0}. 실행 성공", exeFilePath), CommonLib.LOG_LEVEL.INFO);
            }
            else
            {
                CommonLib.DevLog.Write(string.Format("App서버: {0}. 실행 실패", exeFilePath), CommonLib.LOG_LEVEL.ERROR);
            }
        }

        public static void App서버_종료하기()
        {
            string exeFilePath = System.IO.Path.Combine(AppServerFullPathDir, AppServerExeFileName);

            if (AppServerProcess != null && AppServerProcess.CloseMainWindow())
            {
                서버상태_설정하기(APP_SERVER_STATUS.STOP);
                AppServerProcess = null;
                
                CommonLib.DevLog.Write(string.Format("App서버: {0}. 종료 성공", exeFilePath), CommonLib.LOG_LEVEL.INFO);
            }
            else
            {
                CommonLib.DevLog.Write(string.Format("App서버: {0}. 실행 실패", exeFilePath), CommonLib.LOG_LEVEL.ERROR);
            }
        }

        

    } // End Class


    public enum APP_SERVER_STATUS
    {
        STOP            = 0,
        RUN             = 1,
        ABNORMAL        = 2,
    }
}
