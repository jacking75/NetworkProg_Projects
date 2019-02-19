using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFGameServerLib
{
    public enum ERROR_CODE : short
    {
        SERVER_ERROR = 0,

        // 서버 초기 실행 에러 
        LOAD_CONFIG_FILE            = 1,
        LOAD_CONFIG_DB              = 2,
        DUPLICATION_DATACENTER_ID   = 3,
        SERVER_INFO_NOT_FOUND       = 4,
        LOAD_CONFIG_DB_EXCEPTION    = 5,
        INIT_REDIS_DB               = 11,
        INTT_REDIS_PARSE_DB_INFO    = 12,

        // 서버 에러
        SERVER_STATUS_PATCHING = 101,    // 서버 상태 패치 중
        SERVER_STATUS_STOP = 102,
        DBGAMEDATA_NULL = 111,
        MONGODB_UNKNOWN = 112,    // 몽고디비 에러
        REDIS_UNKNOWN = 114,        // 레디스 에러
        UNKNOWN = 116,

        NONE = 30000,                            // 에러가 아니다. struct에서 사용해야 하므로 0은 사용하지 않는다.
    }
}
