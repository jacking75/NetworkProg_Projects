using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentLib
{
    public enum ERROR_CODE : int
    {
        NONE = 0,        // 에러가 아니다

        ADMIN_INVALID_SECURE_STRING = 71,
    }

    public class ADMIN_INFO
    {
        public static string REQUEST_SECURE_KET = "fdsfsdfdsr454345645654tgftg3we23";
    }


    public class REQ_BASE_DATA
    {
        public string ID;
        public string AuthToken;
    }

    public class RES_BASE_DATA
    {
        public ERROR_CODE Result = ERROR_CODE.NONE; // 요청에 대한 결과
        public int ServerCheckTime = 0;             // 서버 점검을 할 시간(초단위)
    }


    public class REQ_ADMIN_SERVER_STATUS : REQ_BASE_DATA
    {
        public string SecureKey = ADMIN_INFO.REQUEST_SECURE_KET;
    }

    public class RES_ADMIN_SERVER_STATUS : RES_BASE_DATA
    {
    }
}
