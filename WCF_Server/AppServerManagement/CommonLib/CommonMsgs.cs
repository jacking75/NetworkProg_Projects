using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public class MsgAgentInfo
    {
        public string ipAddress;
        public string appServerName;
        public string appServerFullPathDir;
    }
        
    public class MsgAppServerStatus
    {
        public bool AppServer실행중;
        public string 프로세스_CPU_사용량;
        public string 전체_CPU_사용량;
        public string AppServer메모리_사용량;

        public bool Agent와AppServer통신가능_여부;
        public string UserCount;
    }



    public class MsgClientInfo
    {
        public string IPAddress;
        public string UserName;
    }

    public class MsgAgentInfoToClient
    {
        public string ConnectionID;
        public string IPAddress;

        public bool AppServer실행중;
        public string 프로세스_CPU_사용량;
        public string 전체_CPU_사용량;
        public string AppServer메모리_사용량;

        public bool Agent와AppServer통신가능_여부;
        public string AppServer접속인원수;
    }

    public class MsgAgentInfoToClientList
    {
        public List<MsgAgentInfoToClient> list = new List<MsgAgentInfoToClient>();
    }
}
