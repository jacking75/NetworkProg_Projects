using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CommonServer;
using Loader = CommonServer.DB.GameContents.Loader;
using CommonServer.DB.GameContents;
using CommonServer.DB;
using DBGameContents = CommonServer.DB.GameContents;
using DBUserGameData = CommonServer.DB.UserGameData;

namespace ServerLogic.Quest
{
    public static class Manager
    {
        public static Adder AdderInst = new Adder();
        static CountUpdater CountUpdaterInst = new CountUpdater();

        
        public static async Task<int> Update(string userID, SimulationQuestCountValue simulCountValue, Int64 traceId = 0)
        {
            int updateCount = 0;

            var userQuestInfo = await DBUserGameData.GetQuestData(userID);
            if (userQuestInfo != null)
            {
                updateCount = CountUpdaterInst.Update(simulCountValue, ref userQuestInfo, traceId);
                if (updateCount > 0)
                {
                    await DBUserGameData.SaveQuestData(userQuestInfo);
                }
            }
            else
            {
                OPLogger.Error(LOG_TYPE.QUEST_PR_UPDATE, traceId, string.Format("{0} 유저의 퀘스트 정보가 없습니다", userID));
            }

            return updateCount;
        }

        public static async Task<int> Update(string userID, NoneSimulationQuestCountValue noneSimulCountValue, Int64 traceId = 0)
        {
            int updateCount = 0;

            var userQuestInfo = await DBUserGameData.GetQuestData(userID);
            if (userQuestInfo != null)
            {
                updateCount = CountUpdaterInst.Update(noneSimulCountValue, ref userQuestInfo, traceId);
                if (updateCount > 0)
                {
                    await DBUserGameData.SaveQuestData(userQuestInfo);
                }
            }
            else
            {
                OPLogger.Error(LOG_TYPE.QUEST_PR_UPDATE, traceId, string.Format("{0} 유저의 퀘스트 정보가 없습니다", userID));
            }

            return updateCount;
        }
                        
        public static bool SetComplete(int questID, ref DBUserQuests userQuestInfo)
        {
            if (userQuestInfo.Quest.ID == questID)
            {
                userQuestInfo.Quest.IsC = true;
                return true;
            }

            return false;
        }

        public static bool IfCompletedThenClear(int questID, ref DBUserQuests userQuestInfo)
        {
            if (userQuestInfo.Quest.ID == questID && userQuestInfo.Quest.IsC)
            {
                userQuestInfo.CQID = userQuestInfo.Quest.ID;
                userQuestInfo.Quest.Clear();
                return true;
            }

            for (int i = 0; i < userQuestInfo.DQList.Count; ++i)
            {
                if (userQuestInfo.DQList[i].ID == questID && userQuestInfo.DQList[i].IsC)
                {
                    userQuestInfo.DQList[i].Clear();
                    return true;
                }
            }

            return false;
        }


        public static bool Is일일퀘스트_시간지남(Int64 생성시간Tick)
        {
            if (생성시간Tick == 0)
            {
                return true;
            }

            return 일일퀘스트_남은시간_초(생성시간Tick) < 0 ? true : false;
        }

        public static int 일일퀘스트_남은시간_초(Int64 생성시간Tick)
        {
            var 일퀘_만료시간 = new DateTime(생성시간Tick).AddDays(1);
            var 남은시간 = 일퀘_만료시간 - DateTime.Now;
            return (int)남은시간.TotalSeconds;
        }

        public static DateTime 일일퀘스트_기준시간()
        {
            // 일퀘 생성시간이 0시가 아니라면 지금 시간이 일퀘 생성시간 보다 작다면 어제 생성한 것으로 시간을 설정한다.
            if (DateTime.Now.Hour < Loader.DBDefineDataInst.DAILY_QUEST_SET_TIME)
            {
                return DateTime.Now.SetTime(Loader.DBDefineDataInst.DAILY_QUEST_SET_TIME, 0, 0).AddDays(-1);
            }

            return DateTime.Now.SetTime(Loader.DBDefineDataInst.DAILY_QUEST_SET_TIME, 0, 0);
        }
    }

    
}
