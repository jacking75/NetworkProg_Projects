using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Concurrent;

using CommonServer.DB;

namespace CommonServer.LocalCache
{
    //TODO: 특정 시간이 지나면 메모리 상에 있는 인증 정보는 무시한다.
    //      인증 정보 유효시간 정책을 정하면 적용한다.
    /// <summary>
    /// 유저 인증 정보를 관리한다. 
    /// 1유저당 인증 정보가 100바이트라면 100만명 기준으로 10메가의 메모리를 점유
    /// 로컬 캐시는 단순하게 접속 순서로 오래된 것은 제거한다. 
    /// 만약 오랜된 유저가 아직 게임 중이라도 레디스에 정보가 아직 있으니 문제될 것은 없다.
    /// </summary>
    public static class UserAuthCache
    {
        static ConcurrentDictionary<string, MemoryDBUserAuth> Cache = new ConcurrentDictionary<string, MemoryDBUserAuth>();

        static ConcurrentQueue<string> LoginSeqQueue = new ConcurrentQueue<string>();


        static int MaxCount = 320000;
        static int RemoveCount = 100;


        public static void ChangeMaxCount(int maxCount, int removeCount)
        {
            MaxCount = maxCount;
            RemoveCount = removeCount;
        }

        public static void AddAuthInfo(MemoryDBUserAuth newAuthInfo, Int64 traceId)
        {
            var userID = newAuthInfo.UserID;
            MemoryDBUserAuth oldAuthInfo;

            if (Cache.TryGetValue(userID, out oldAuthInfo) == false)
            {
                IfMaxThenRemove();

                LoginSeqQueue.Enqueue(userID);
                Cache.TryAdd(userID, newAuthInfo);

                OPLogger.Trace(LOG_TYPE.T_AUTH_TO_GAMESERVER, traceId, string.Format("AddAuthInfo:New. UserID:{0}, Token:{1}", userID, newAuthInfo.GameAuthToken));
            }
            else
            {
                var oldToken = oldAuthInfo.GameAuthToken;
                Cache.TryUpdate(userID, newAuthInfo, oldAuthInfo);

                OPLogger.Trace(LOG_TYPE.T_AUTH_TO_GAMESERVER, traceId, string.Format("AddAuthInfo:Update. UserID:{0}, New-Token:{1}, Old-Token:{2}", userID, newAuthInfo.GameAuthToken, oldToken));
            }
        }

        public static async Task<MemoryDBUserAuth> GetAuthInfo(string userID)
        {
            MemoryDBUserAuth userInfo;

            if (Cache.TryGetValue(userID, out userInfo))
            {
                return userInfo;
            }

            var traceId = UniqueNumberManager.채번_받아오기();
            userInfo = await RequestServiceAPILoginServer.RequsetAuthCheckAsync(ServerConfig.ServiceName(), userID, traceId);
            if (userInfo.err == ERROR_CODE.NONE)
            {
                AddAuthInfo(userInfo, traceId);
            }
            
            return userInfo;
        }

        public static void Clear()
        {
            Cache.Clear();

            while (true)
            {
                string removeItem;
                if (LoginSeqQueue.TryDequeue(out removeItem) == false)
                {
                    return;
                }
            }
        }
        
        static void IfMaxThenRemove()
        {
            if (Cache.Count() >= MaxCount)
            {
                for (int i = 0; i < RemoveCount; ++i)
                {
                    string removeItem;
                    
                    if (LoginSeqQueue.TryDequeue(out removeItem))
                    {
                        RemoveAuthInfo(removeItem);
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        static void RemoveAuthInfo(string userID)
        {
            MemoryDBUserAuth userInfo;
            
            Cache.TryRemove(userID, out userInfo);
        }


        
        
   
    }
}