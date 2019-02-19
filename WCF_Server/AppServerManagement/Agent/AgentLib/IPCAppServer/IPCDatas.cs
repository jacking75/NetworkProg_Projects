using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentLib.IPCAppServer
{
    public enum IPC_MSG_TYPE : short
    {
        // 1 ~ 100 사이는 사용 불가

        REQUEST_HEALTH_CHECK = 101,
        RESPONSE_HEALTH_CHECK = 102,
    }


    class IPCMsgRequestHealthCheck
    {
        public string CurrentDateTime = DateTime.Now.ToString("yyyyMMddHHmss");
    }

    class IPCMsgReSponseHealthCheck
    {
        public string CurrentDateTime = DateTime.Now.ToString("yyyyMMddHHmss");
        public string CurUserCount = "0";
    }
}
