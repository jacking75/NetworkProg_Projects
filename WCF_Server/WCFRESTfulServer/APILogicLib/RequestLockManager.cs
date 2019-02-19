using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Concurrent;

namespace ServerLogic
{
    static class RequestLockManager
    {
        static System.Threading.SpinLock SLock = new System.Threading.SpinLock();
        static HashSet<string> RequestLockSet = new HashSet<string>();

        public static bool Lock(string userID)
        {
            bool isEnable = false;
            bool gotLock = false;
            
            try
            {
                SLock.Enter(ref gotLock);

                if (RequestLockSet.Add(userID))
                {
                    isEnable = true;
                }
            }
            finally
            {
                if (gotLock)
                {
                    SLock.Exit();
                }
            }

            return isEnable;
        }

        public static void UnLock(string userID)
        {
            bool gotLock = false;

            try
            {
                SLock.Enter(ref gotLock);
                RequestLockSet.Remove(userID);
            }
            finally
            {
                if (gotLock)
                {
                    SLock.Exit();
                }
            }
        }


    }

    /*
    class RequestLockManager
    {
        static ConcurrentDictionary<string, RequestLockObject> RequestLockObjectDic = new ConcurrentDictionary<string, RequestLockObject>();

        static ConcurrentQueue<Int64> SequenceQueue = new ConcurrentQueue<Int64>();


        static int MaxCount = 12800000;
        static int RemoveCount = 10000;
        


        public static void Clear()
        {
            RequestLockObjectDic.Clear();

            while (true)
            {
                Int64 removeItem = 0;
                if (SequenceQueue.TryDequeue(out removeItem) == false)
                {
                    return;
                }
            }
        }
        
        public static void Add(Int64 uid)
        {
            RequestLockObject RLObject;
            var newRLObject = new RequestLockObject();

            if (RequestLockObjectDic.TryGetValue(uid, out RLObject) == false)
            {
                IfMaxThenRemove();

                SequenceQueue.Enqueue(uid);

                RLObject = new RequestLockObject();
                RequestLockObjectDic.TryAdd(uid, newRLObject);
            }
            else
            {
                RequestLockObjectDic.TryUpdate(uid, newRLObject, RLObject);
            }
        }

        public static bool EnableCheckOrUpdate(Int64 uid)
        {
            bool isEnable = false;
            RequestLockObject RLObject;

            if (RequestLockObjectDic.TryGetValue(uid, out RLObject) == false)
            {
                return isEnable;
            }

            isEnable = RLObject.EnableCheckOrUpdate();
            return isEnable;
        }

        static void IfMaxThenRemove()
        {
            if (RequestLockObjectDic.Count() >= MaxCount)
            {
                RequestLockObject tempRLObject;

                for (int i = 0; i < RemoveCount; ++i)
                {
                    Int64 uid = 0;

                    if (SequenceQueue.TryDequeue(out uid))
                    {
                        RequestLockObjectDic.TryRemove(uid, out tempRLObject);
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
    }


    class RequestLockObject
    {
        System.Threading.SpinLock SLock = new System.Threading.SpinLock();
        Int64 RequestedSecond = 0;

        string AuthToken;

        public void Init(string authToken)
        {
            RequestedSecond = 0;
            AuthToken = authToken;
        }

        public bool EnableCheckOrUpdate()
        {
            bool isEnable = false;

            var curSecond = Util.TimeTickToSec(DateTime.Now.Ticks);

            var prevRequestedSecond = System.Threading.Interlocked.CompareExchange(ref RequestedSecond,
                                                                                    curSecond,
                                                                                    0);
            if (RequestedSecond == curSecond)
            {
                isEnable = true;
            }

            return isEnable;
        }

        /// <summary>
        /// spinlock 사용 버전
        /// </summary>
        /// <param name="waitSecond"></param>
        /// <returns></returns>
        public bool EnableCheckOrUpdateVer2(int waitSecond)
        {
            bool isEnable = false;
            bool gotLock = false;
            
            gotLock = false;
            try
            {
                var curSecond = Util.TimeTickToSec(DateTime.Now.Ticks);
                
                SLock.Enter(ref gotLock);
                
                var diff = curSecond - RequestedSecond;
                if (diff == 0 || diff < waitSecond)
                {
                    isEnable = true;
                    RequestedSecond = curSecond;
                }
            }
            finally
            {
                // Only give up the lock if you actually acquired it
                if (gotLock)
                {
                    SLock.Exit();
                }
            }

            return isEnable;
        }

        
    }
     * */
}
