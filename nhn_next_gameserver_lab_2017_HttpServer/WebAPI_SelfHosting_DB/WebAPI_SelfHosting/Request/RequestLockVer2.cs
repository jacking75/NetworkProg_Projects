using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.CompilerServices;

// 클라이언트의 요청을 순차적으로 처리하도록 한다.
namespace WebAPI_SelfHosting.Request
{
    public struct RequestLockVer2 : IDisposable
    {
        ERROR_CODE 요청을_할수있다;
        //ERROR_CODE 요청을_할수있다 = ERROR_CODE.PREV_REQUEST_NOT_COMPLETE;
        string key;

        public RequestLockVer2(string id, ERROR_CODE errorCode= ERROR_CODE.PREV_REQUEST_NOT_COMPLETE)
        {
            key = string.Format("{0}:RLock", id);
            요청을_할수있다 = errorCode;
        }

        public void Release()
        {
            DB.Redis.DeleteStringNoReturn<int>(key);
        }

        public async Task<ERROR_CODE> 요청_처리중인가([System.Runtime.CompilerServices.CallerFilePath] string fileName = "",
                                [System.Runtime.CompilerServices.CallerMemberName] string methodName = "",
                                [System.Runtime.CompilerServices.CallerLineNumber] int lineNumber = 0)
        {
            try
            {
                var result = await DB.Redis.SetStringAsyncWhenNotExists<int>(key, 12345);

                if (result == false)
                {
                    return 요청을_할수있다;
                }
               
                요청을_할수있다 = ERROR_CODE.NONE;
                return 요청을_할수있다;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[RequestLock] 예외발생: " + ex.Message);
                return ERROR_CODE.PREV_REQUEST_FAIL_REDIS;
            }            
        }

        public void Dispose()
        {
            if (요청을_할수있다 == ERROR_CODE.NONE)
            {
                Release();
            }
        }       
    }
}
