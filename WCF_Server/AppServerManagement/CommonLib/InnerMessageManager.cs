using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public class InnerMessageManager
    {
        static bool Enable = true;

        static System.Collections.Concurrent.ConcurrentQueue<InnerMsg> msgQueue = new System.Collections.Concurrent.ConcurrentQueue<InnerMsg>();

        public static void SetEnable(bool enable)
        {
            Enable = enable;
        }

        static public void AddMsg(InnerMsg msg)
        {
            if (Enable)
            {
                msgQueue.Enqueue(msg);
            }
        }

        static public bool GetMsg(out InnerMsg msg)
        {
            return msgQueue.TryDequeue(out msg);
        }
    }


    public enum InnerMsgType
    {
        // 메인 관리 서버에서 사용
        AGENT_CONNECT           = 1,
        AGENT_DISCONNECT        = 2,
        AGENT_CHANGE_STATUS     = 11,
        AGENT_RECEIVE_HEARTBEAT = 12,
        AGENT_NOTIFY_ABNORNAL   = 14,
        AGENT_SVN_UPDATE_RESULT = 15,
        
        CLIENT_CONNECT              = 101,
        CLIENT_DISCONNECT           = 102,
        CLIENT_START_APP_SERVER     = 111,
        CLIENT_STOP_APP_SERVER      = 112,
        CLIENT_SVN_PATCH_APP_SERVER = 114,


        // Agent
        APP_SERVER_STATUS_RUNNING       = 151,
        APP_SERVER_STATUS_COMMUNICATE   = 152,

        // 클라이언트에서 사용
        AGENT_INFO_LIST             = 201,
        AGENT_REMOVED               = 202,
        RECEIVE_REDIS_CHECK_RESULT  = 203,

        // Redis
        REDIS_CHECK_RESULT      = 301,
    }

    public class InnerMsg
    {
        public InnerMsgType type;
        public string connectionID;
        public string value1;
    }

    public class InnerMsgAgentStatus
    {
        public string connectID;

        public bool 에이전트_허트비트_문제발생;

        public string IP;
        public string AppServerName;
        public string AppServerFullPath;

        public bool AppServer실행중;
        public string 프로세스_CPU_사용량;
        public string 전체_CPU_사용량;
        public string AppServer메모리_사용량;

        public bool Agent와AppServer통신가능_여부;
        public string AppServer접속인원수;
    }

   


    // 클라이언트
    public class InnerMsgClientStatus
    {
        public string ConnectID;

        public string IP;
        public string UserName;
    }
}
