using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonServer.DB;
using Loader = CommonServer.DB.GameContents.Loader;
using DBGameContents = CommonServer.DB.GameContents;
using CommonServer;

namespace ServerLogic.Quest
{
    public class CountUpdater
    {
        public int Update(SimulationQuestCountValue simulCountValue, ref DBUserQuests userQuestInfo, Int64 traceId)
        {
            var userID = userQuestInfo._id;
            var hitCount = 0;

            if (simulCountValue != null)
            {
                if (userQuestInfo.Quest.ID != 0 && userQuestInfo.Quest.IsC == false && userQuestInfo.Quest.ST > 0)
                {
                    var quest = Loader.GetQuestData(userQuestInfo.Quest.ID);
                    if (quest == null)
                    {
                        OPLogger.Error(LOG_TYPE.QUEST_PR_UPDATE, traceId, string.Format("UserID:{0}, 퀘스트ID:{1} 정보가 없음", userID, userQuestInfo.Quest.ID));
                        return hitCount;
                    }

                    hitCount += simulCountValue.Update(quest.SubjectClass, quest.SubjectID, 
                                                       quest.SubjectCountOpenStart, 
                                                       ref userQuestInfo.Quest.PR);

                    CheckUpdateComplete(quest.SubjectCount, userQuestInfo.Quest.PR, ref userQuestInfo.Quest.IsC);
                }

                for (int i = 0; i < userQuestInfo.DQList.Count; ++i)
                {
                    if (userQuestInfo.DQList[i].ID != 0 && userQuestInfo.DQList[i].IsC == false && userQuestInfo.DQList[i].ST >0)
                    {
                        var quest = Loader.GetQuestData(userQuestInfo.DQList[i].ID);
                        if (quest == null)
                        {
                            OPLogger.Error(LOG_TYPE.QUEST_PR_UPDATE, traceId, string.Format("UserID:{0}, 퀘스트ID:{1} 정보가 없음", userID, userQuestInfo.DQList[i].ID));
                            continue;
                        }

                        hitCount += simulCountValue.Update(quest.SubjectClass, quest.SubjectID, 
                                                           quest.SubjectCountOpenStart, 
                                                           ref userQuestInfo.DQList[i].PR);

                        CheckUpdateComplete(quest.SubjectCount, userQuestInfo.DQList[i].PR, ref userQuestInfo.DQList[i].IsC);
                    }
                }
            }

            return hitCount;
        }

        public int Update(NoneSimulationQuestCountValue noneSimulCountValue, ref DBUserQuests userQuestInfo, Int64 traceId)
        {
            var userID = userQuestInfo._id;
            var hitCount = 0;

            if (noneSimulCountValue != null)
            {
                if (userQuestInfo.Quest.ID != 0 && userQuestInfo.Quest.IsC == false && userQuestInfo.Quest.ST > 0)
                {
                    var quest = Loader.GetQuestData(userQuestInfo.Quest.ID);
                    if (quest == null)
                    {
                        OPLogger.Error(LOG_TYPE.QUEST_PR_UPDATE, traceId, string.Format("UserID:{0}, 퀘스트ID:{1} 정보가 없음", userID, userQuestInfo.Quest.ID));
                        return hitCount;
                    }

                    hitCount += noneSimulCountValue.Update(quest.SubjectID, 
                                                            quest.SubjectCountOpenStart,
                                                            ref userQuestInfo.Quest.PR);

                    CheckUpdateComplete(quest.SubjectCount, userQuestInfo.Quest.PR, ref userQuestInfo.Quest.IsC);
                }

                for (int i = 0; i < userQuestInfo.DQList.Count; ++i)
                {
                    if (userQuestInfo.DQList[i].ID != 0 && userQuestInfo.DQList[i].IsC == false && userQuestInfo.DQList[i].ST > 0)
                    {
                        var quest = Loader.GetQuestData(userQuestInfo.DQList[i].ID);
                        if (quest == null)
                        {
                            OPLogger.Error(LOG_TYPE.QUEST_PR_UPDATE, traceId, string.Format("UserID:{0}, 퀘스트ID:{1} 정보가 없음", userID, userQuestInfo.DQList[i].ID));
                            continue;
                        }

                        hitCount += noneSimulCountValue.Update(quest.SubjectID, 
                                                                quest.SubjectCountOpenStart, 
                                                                ref userQuestInfo.DQList[i].PR);

                        CheckUpdateComplete(quest.SubjectCount, userQuestInfo.DQList[i].PR, ref userQuestInfo.DQList[i].IsC);
                    }
                }
            }

            return hitCount;
        }


        void CheckUpdateComplete(int subjectCount, Int64 questPR, ref bool isComplete)
        {
            if (subjectCount <= questPR)
            {
                isComplete = true;
            }
        }
        
    }

    
}
