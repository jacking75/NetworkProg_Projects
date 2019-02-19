using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonServer;
using Loader = CommonServer.DB.GameContents.Loader;
using CommonServer.DB.GameContents;
using CommonServer.DB;

namespace ServerLogic.Quest
{
    public class Adder
    {
        public bool CheckAndAddQuest(int userLevel, Int64 영업턴, DBUserQuests userQuestInfo, Int64 traceId)
        {
            var userID = userQuestInfo._id;

            if (userQuestInfo.CQID == 0 && userQuestInfo.Quest.ID == 0)
            {
                var firstQuestData = Loader.First일반퀘스트(userLevel);
                if (firstQuestData == null)
                {
                    OPLogger.Error(LOG_TYPE.NEW_NORMAL_QUEST, traceId, string.Format("첫 퀘스트가 없음. UserID:{0}", userID));
                    return false;
                }

                AddQuest(영업턴, firstQuestData, userQuestInfo);
                return true;
            }

            // 현재 진행 중인 것이 있으면 새로 받지 못한다.
            if (userQuestInfo.Quest.ID != 0)
            {
                return false;
            }

            // 다음 퀘스트 조건에 부합하면 퀘스트를 받는다.
            var questData = Loader.Next일반퀘스트(userLevel, userQuestInfo.CQID, traceId);
            if (questData == null)
            {
                OPLogger.Trace(LOG_TYPE.T_NEW_QUEST, traceId, string.Format("{0} 다음의 일반 퀘스트가 없다. UserID:{1}, Level:{2}", userQuestInfo.CQID, userID, userLevel));
                return false;
            }
                       
            AddQuest(영업턴, questData, userQuestInfo);
            return true;
        }

        public int CheckAndAdd일일Quest(int userLevel, ref DBUserQuests userQuestInfo)
        {
            if (Manager.Is일일퀘스트_시간지남(userQuestInfo.DQTick) == false)
            {
                return 0;
            }
            var dayileQuestInfo = Loader.SelectNew일일퀘스트(userLevel);
            if (dayileQuestInfo == null)
            {
                return 0;
            }

            var count = Add일일Quest(dayileQuestInfo, ref userQuestInfo);
            return count;
        }



        
                     
        void AddQuest(Int64 영업턴, QuestData questData, DBUserQuests userQuestInfo)
        {
            userQuestInfo.Quest.ID = questData.ID;
            userQuestInfo.Quest.IsC = false;
            userQuestInfo.Quest.RT = 영업턴;
            userQuestInfo.Quest.ST = 0;
            userQuestInfo.Quest.PR = 0;
        }

        int Add일일Quest(DailyQuestData questData, ref DBUserQuests userQuestInfo)
        {
            userQuestInfo.DQTick = Manager.일일퀘스트_기준시간().Ticks;
            userQuestInfo.DQID = questData.ID;
            userQuestInfo.DQIsC = false;

            for (int i = 0; i < userQuestInfo.DQList.Count; ++i)
            {
                userQuestInfo.DQList[i].ID = questData.QuestIDs[i];
                userQuestInfo.DQList[i].IsC = false;
                userQuestInfo.DQList[i].RT = 1;
                userQuestInfo.DQList[i].ST = 1;
                userQuestInfo.DQList[i].PR = 0;
            }

            return userQuestInfo.DQList.Count;
        }
                
        

        
        
       
                
    }
}
