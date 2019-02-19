using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.CompilerServices;
using System.Collections.Concurrent;
using SmartFormat;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ServerLogic
{
    // 서버 개발/운영 관련 로그를 MongoDB에 남긴다.
    public static class OPLogger
    {
        static bool IsEnable = false;
        const string ServerType = "Login";
        static string ServerIP;
        static int ShortPathFileRemovePos;
        static Int64 ServerStartTime;

        static bool IsEnableConsoleWrite = false;
        static List<bool> EnableTraceTypeIndexList = new List<bool>();
        static List<string> EnableTraceTypeNameList = new List<string>();

        static string DBConnectString;
        const string DBDatabaseName = "OPLog";
        const string ServerSettingCollectionName = "ServerSetting";

        static ConcurrentBag<DBOperationLog> InfoLogs = new ConcurrentBag<DBOperationLog>();
        static ConcurrentBag<DBOperationLog> ErrorLogs = new ConcurrentBag<DBOperationLog>();
        static ConcurrentBag<DBOperationLog> ExceptionLogs = new ConcurrentBag<DBOperationLog>();
        static ConcurrentBag<DBOperationLog> TraceLogs = new ConcurrentBag<DBOperationLog>();

        static bool IsThreadRunning = false;
        static System.Threading.Thread WorkerThread = null;
        const int TIME_TO_SLEEP_BETWEEN_BATCHES_MILLSEC = 1000;
        const int MAX_LOG_COUNT_AT_IME_WRITE = 100;


        public static void Init(string ip, string dbConnectString, 
                                [CallerFilePath] string callerFilePath = null)
        {
            ServerIP = ip;
            DBConnectString = dbConnectString;
            ServerStartTime = Util.DateTimeToyyyyMMddHHmmss(DateTime.Now).ToInt64();

            var serverFoldName = @"\LoginServer";
            var pos = callerFilePath.IndexOf(serverFoldName);
            ShortPathFileRemovePos = pos + serverFoldName.Length;

            
            IsThreadRunning = true;
            WorkerThread = new System.Threading.Thread(OPLogger.WorkerThreadFunc);
            WorkerThread.Start();


            if (LoadLogConfig(ServerType))
            {
                IsEnable = true;
            }
            else
            {
                IsEnable = false;
            }


            WriteInitLog();
        }

        public static void Destory()
        {
            if (IsEnable == false)
            {
                return;
            }


            FileLogger.Info("로그인 서버 종료");
            
            var 채번 = UniqueNumberManager.채번_받아오기();
            Info(LOG_TYPE.SERVER_TERMINATE, 채번, "로그인 서버 종료");
            
            // 로그 출력을 위해 잠시 대기
            System.Threading.Thread.Sleep(1000);


            IsThreadRunning = false;
            if (WorkerThread.IsAlive)
            {
                WorkerThread.Join();
            }
        }

        static void WriteInitLog([CallerFilePath] string callerFilePath = null)
        {
            var STR_Enable_ServerType = Smart.Format("[OPLogger Init] IsEnable:{0}, ServerType:{1}, StartTime:{2}", IsEnable, ServerType, ServerStartTime);
            var STR_IP_ShortFilePath = Smart.Format("[OPLogger Init] IP:{0}, ShortFilePathName:{1}", ServerIP, ShortFilePathName(callerFilePath));
            var STR_EnableTraceType = Smart.Format("[EnableTraceTypeNameList] {0:list:{}|, |,}", EnableTraceTypeNameList);
            var STR_DB = Smart.Format("DBConnectString:{0}, DBDatabaseName:{1}, ServerSettingCollectionName:{2}", DBConnectString, DBDatabaseName, ServerSettingCollectionName);
            var STR_Thread = Smart.Format("TIME_TO_SLEEP_BETWEEN_BATCHES_MILLSEC: {0}, MAX_LOG_COUNT_AT_IME_WRITE: {1}", TIME_TO_SLEEP_BETWEEN_BATCHES_MILLSEC, MAX_LOG_COUNT_AT_IME_WRITE);


            FileLogger.Info(STR_Enable_ServerType);

            var groupId = UniqueNumberManager.채번_받아오기();
            Info(LOG_TYPE.SERVER_INIT, groupId, STR_Enable_ServerType);
            Info(LOG_TYPE.SERVER_INIT, groupId, STR_IP_ShortFilePath);
            Info(LOG_TYPE.SERVER_INIT, groupId, STR_EnableTraceType);
            Info(LOG_TYPE.SERVER_INIT, groupId, STR_DB);
            Info(LOG_TYPE.SERVER_INIT, groupId, STR_Thread);
        }

        public static void Trace(LOG_TYPE logType, Int64 groupId, string message, 
                                [CallerMemberName] string callerName = null, 
                                [CallerFilePath] string callerFilePath = null, 
                                [CallerLineNumber] int lineNumber = 0)
        {
            var index = (int)logType - (int)LOG_TYPE.START_TRACE;
            if (EnableTraceTypeIndexList[index] == false)
            {
                return;
            }

            AddLog(OP_LOG_LEVEL.TRACE, logType, groupId, message,
                   callerName, ShortFilePathName(callerFilePath), lineNumber);
        }

        public static void Info(LOG_TYPE logType, Int64 groupId, string message,
                                [CallerMemberName] string callerName = null,
                                [CallerFilePath] string callerFilePath = null,
                                [CallerLineNumber] int lineNumber = 0)
        {
            AddLog(OP_LOG_LEVEL.INFO, logType, groupId, message,
                   callerName, ShortFilePathName(callerFilePath), lineNumber);            
        }

        public static void Error(LOG_TYPE logType, Int64 groupId, string message,
                                [CallerMemberName] string callerName = null,
                                [CallerFilePath] string callerFilePath = null,
                                [CallerLineNumber] int lineNumber = 0)
        {
            AddLog(OP_LOG_LEVEL.ERROR, logType, groupId, message,
                   callerName, ShortFilePathName(callerFilePath), lineNumber);
        }

        public static void Exception(LOG_TYPE logType, Int64 groupId, string message, string stackTrace,
                                [CallerMemberName] string callerName = null,
                                [CallerFilePath] string callerFilePath = null,
                                [CallerLineNumber] int lineNumber = 0)
        {
            var msg = string.Format("Msg:{0} || stackTrace: {1}", message, stackTrace);
            AddLog(OP_LOG_LEVEL.EXCEPTION, logType, groupId, msg,
                  callerName, ShortFilePathName(callerFilePath), lineNumber);

        }
        


        static string ShortFilePathName(string fullPathFileName)
        {
            var pathName = fullPathFileName.Remove(0, ShortPathFileRemovePos);
            return pathName;
        }

        static void AddLog(OP_LOG_LEVEL logLevel, LOG_TYPE logType, Int64 groupId, string message, 
                            string callerName, string callerFilePath, int lineNumber)
        {
            if (IsEnable == false)
            {
                return;
            }

            var log = new DBOperationLog()
            {
                STime = ServerStartTime,
                GroupID = groupId,
                Lv = (int)logLevel,
                LT = (int)logType,
                IP = ServerIP,
                SType = ServerType,
                Time = DateTime.Now,
                CF = callerName,
                FL = callerFilePath,
                Line = lineNumber,
                Msg = message,
            };

            switch (logLevel)
            {
                case OP_LOG_LEVEL.TRACE:
                    TraceLogs.Add(log);
                    break;
                case OP_LOG_LEVEL.INFO:
                    InfoLogs.Add(log);
                    break;
                case OP_LOG_LEVEL.ERROR:
                    ErrorLogs.Add(log);
                    break;
                case OP_LOG_LEVEL.EXCEPTION:
                    ExceptionLogs.Add(log);
                    break;
            }

            ConsoleWrite(log);
        }

        static void ConsoleWrite(DBOperationLog log)
        {
            if (IsEnableConsoleWrite)
            {
                var logLevel = ((OP_LOG_LEVEL)log.Lv).ToString();
                Console.WriteLine(Smart.Format("{1} | Caller:{CF} | Line:{Line} | {FL} | {Msg}", log, logLevel));
            }
        }

        static void WorkerThreadFunc()
        {
            DateTime 최근에_저장_시간 = DateTime.Now;
            
            while (IsThreadRunning)
            {
                if ((DateTime.Now - 최근에_저장_시간).TotalMilliseconds >= TIME_TO_SLEEP_BETWEEN_BATCHES_MILLSEC)
                {
                    WriteLog(OP_LOG_LEVEL.TRACE, TraceLogs);
                    WriteLog(OP_LOG_LEVEL.INFO, InfoLogs);
                    WriteLog(OP_LOG_LEVEL.ERROR, ErrorLogs);
                    WriteLog(OP_LOG_LEVEL.EXCEPTION, ExceptionLogs);

                    최근에_저장_시간 = DateTime.Now;
                }

                System.Threading.Thread.Sleep(TIME_TO_SLEEP_BETWEEN_BATCHES_MILLSEC);
            }
        }

        static void WriteLog(OP_LOG_LEVEL logLevel, ConcurrentBag<DBOperationLog> collectedLogs)
        {
            try
            {
                if (collectedLogs.IsEmpty())
                {
                    return;
                }

                var inertLogs = new List<DBOperationLog>();

                for (int i = 0; i < MAX_LOG_COUNT_AT_IME_WRITE; ++i)
                {
                    DBOperationLog log;
                    if (collectedLogs.TryTake(out log))
                    {
                        inertLogs.Add(log);
                    }
                    else
                    {
                        break;
                    }
                }

                switch (logLevel)
                {
                    case OP_LOG_LEVEL.TRACE:
                        GetMongoDBCollection<DBOperationLog>("Trace").InsertManyAsync(inertLogs).Wait();
                        break;
                    case OP_LOG_LEVEL.INFO:
                        GetMongoDBCollection<DBOperationLog>("Info").InsertManyAsync(inertLogs).Wait();
                        break;
                    case OP_LOG_LEVEL.ERROR:
                        GetMongoDBCollection<DBOperationLog>("Error").InsertManyAsync(inertLogs).Wait();
                        break;
                    case OP_LOG_LEVEL.EXCEPTION:
                        GetMongoDBCollection<DBOperationLog>("Exception").InsertManyAsync(inertLogs).Wait();
                        break;
                }
            }
            catch (Exception ex)
            {
                FileLogger.Exception(ex.Message);
            }
        }

        static IMongoCollection<T> GetMongoDBCollection<T>(string collectionName)
        {
            var mongoClient = new MongoClient(DBConnectString);
            if (mongoClient == null)
            {
                return null;
            }

            var collection = mongoClient.GetDatabase(DBDatabaseName).GetCollection<T>(collectionName);
            return collection;
        }

        #pragma warning disable 649
        class DBOperationLog
        {
            public ObjectId _id;
            public Int64 STime;       //시작 시간
            public Int64 GroupID;   // 그룹핑 ID
            public int Lv;
            public int LT; // LogType
            public string IP;
            public string SType; // 서버 타입
            public DateTime Time; // DateTime
            public string CF; // call stack
            public string FL; // FilePathFilePath
            public int Line;
            public string Msg; // Message
        }
        #pragma warning restore 649

        static bool LoadLogConfig(string serverType)
        {
            var allLogTypeStringList = new List<string>();

            EnableTraceTypeIndexList.Clear();

            for (int i = (int)LOG_TYPE.START_TRACE; i < (int)LOG_TYPE.END_TRACE; ++i)
            {
                allLogTypeStringList.Add(((LOG_TYPE)i).ToString());
                EnableTraceTypeIndexList.Add(false);
            }


            try
            {
                var key = ServerIP + "_" + ServerType;
                var collection = GetMongoDBCollection<DBConfig>(ServerSettingCollectionName);
                var document = collection.Find(x => x._id == key).FirstAsync();

                if (document.Result != null)
                {
                    SetConfig(document.Result, allLogTypeStringList);
                }
                else
                {
                    FileLogger.Error("DB에 로그 설정 정보가 없음 !!!");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                FileLogger.Exception(ex.Message);
                return false;
            }
        }

        static void SetConfig(DBConfig dbConfig, List<string> allLogTypeStringList)
        {
            IsEnableConsoleWrite = dbConfig.IsConsole;

            if (dbConfig.TraceTypeList.Count < 0)
            {
                return;
            }

            // trace 설정 중 첫 번째에 ALL이 있으면 모든 trace 다 나오도록 한다
            if (dbConfig.TraceTypeList[0] == "ALL")
            {
                for (int i = 0; i < EnableTraceTypeIndexList.Count; ++i)
                {
                    EnableTraceTypeIndexList[i] = true;
                }

                EnableTraceTypeNameList.Add("ALL TRACE");
            }
            else
            {
                var traceTypeList = dbConfig.TraceTypeList;
                for (int i = 0; i < traceTypeList.Count; ++i)
                {
                    var index = allLogTypeStringList.FindIndex(x => x == traceTypeList[i]);
                    if (index < 0)
                    {
                        continue;
                    }

                    EnableTraceTypeIndexList[index] = true;
                    EnableTraceTypeNameList.Add(((LOG_TYPE)index).ToString());
                }
            }
        }

        public class DBConfig
        {
            public string _id; // 서버 IP + ServerType
            public bool IsConsole;
            public List<string> TraceTypeList;

        }
        
    }

    enum OP_LOG_LEVEL
    {
        TRACE = 0,
        INFO = 1,
        ERROR = 2,
        EXCEPTION = 3,
    }
           
    public enum LOG_TYPE
    {
        // 0 ~ 1000
        START_INFO_ERROR = 0,
            SERVER_INIT = 1,
            SERVER_TERMINATE = 2,
            CONSOLE_KEY_INPUT = 3,
            DEV_CREATE_ACCOUNT = 11,
            AUTH_TO_LOGINSERVER = 12,
            AUTH_TO_GAMESERVER = 13,
            AUTH_GAME_TO_LOGIN = 14,
            DB_WORK = 15,
            HBO_CREATE_ACCOUNT = 16,
            REGIST_GAME_SERVER = 17,
            UN_REGIST_GAME_SERVER = 18,
            NOTIFY_USER_AUTH_INFO = 19,
            REQUEST_EXCEPTION = 20,
            GAME_SERVER_MANAGER_WORK_PROCESS_EXCEPTION = 21,
            GAME_SERVER_MANAGER_INSERT_MSG_EXCEPTION = 22,
            HTTP_REQUESTER_EXCEPTION = 23,
        END_INFO_ERROR,
        
        // 1001 ~ 2000
        START_TRACE = 1001,
            T_DEV_CREATE_ACCOUNT = 1001,
            T_DEV_LOGIN = 1002,
            T_AUTH_TO_LOGINSERVER = 1011,
            T_AUTH_TO_GAMESERVER = 1012,
            T_AUTH_ADD_UPDATE = 1013,
            T_HBO_CREATE_ACCOUNT = 1014,
            T_HBO_MEMBER_CHECK = 1015,
            T_HBO_LOGIN = 1016,
            T_HTTP_REQUESTER = 1017,
        END_TRACE,
    }

    
}
