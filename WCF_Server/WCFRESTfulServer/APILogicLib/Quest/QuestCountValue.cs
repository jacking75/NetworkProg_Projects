using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Quest
{
    public class SimulationQuestCountValue
    {
        public void 총매출(int value)
        {
            ProcessChecker.ValueUpdate(5001, value, ref 진행결과Dic);
        }

        public void 총수익(int value)
        {
            ProcessChecker.ValueUpdate(5002, value, ref 진행결과Dic);
        }

        public void 총식사_완료_손님(int value)
        {
            ProcessChecker.ValueUpdate(5003, value, ref 진행결과Dic);
        }

        public void 총평판_점수(int value)
        {
            ProcessChecker.ValueUpdate(5004, value, ref 진행결과Dic);
        }

        public void 총음식_판매(int value)
        {
            ProcessChecker.ValueUpdate(5005, value, ref 진행결과Dic);
        }

        public void 매장_판매량(int shopID, int value)
        {
            ProcessChecker.ValueUpdate(shopID, value, ref 업종별판매량Dic);
        }

        public void 매장_매출액(int shopID, int value)
        {
            ProcessChecker.ValueUpdate(shopID, value, ref 업종별매출액Dic);
        }

        public void 음식_판매량(int foodID, int value)
        {
            ProcessChecker.ValueUpdate(foodID, value, ref 음식별판매량Dic);
        }

        public void 음식_매출액(int foodID, int value)
        {
            ProcessChecker.ValueUpdate(foodID, value, ref 음식별매출액Dic);
        }

        public void 전체매장_음식_점수(int value)
        {
            ProcessChecker.ValueUpdate(5101, value, ref 진행결과Dic);
        }

        public void 전체매장_서비스_점수(int value)
        {
            ProcessChecker.ValueUpdate(5102, value, ref 진행결과Dic);
        }

        public void 전체매장_점유율(int value)
        {
            ProcessChecker.ValueUpdate(5103, value, ref 진행결과Dic);
        }

        public void 업종별매장수(int shopID, int value)
        {
            ProcessChecker.ValueUpdate(shopID, value, ref 업종별매장수Dic);
        }

        public void 업종별음식보유수(int shopID, int value)
        {
            ProcessChecker.ValueUpdate(shopID, value, ref 업종별음식보유수Dic);
        }
                
        public void 획득_노하우_수(int value)
        {
            ProcessChecker.ValueUpdate(5607, value, ref 진행결과Dic);
        }

        public void 랜드마크_공략(int value)
        {
            ProcessChecker.ValueUpdate(5901, value, ref 진행결과Dic);
        }

        public void 영업진행(int value)
        {
            ProcessChecker.ValueUpdate(6101, value, ref 진행결과Dic);
        }
        
        public void 전체매장_특가음식판매량(int value)
        {
            ProcessChecker.ValueUpdate(5006, value, ref 진행결과Dic);
        }
                
        public void 단체손님_방문_성공_수(int value)
        {
            ProcessChecker.ValueUpdate(6201, value, ref 진행결과Dic);
        }

        public void 이벤트_랜드마크_출연_수(int value)
        {
            ProcessChecker.ValueUpdate(6202, value, ref 진행결과Dic);
        }
                       

        public int Update(int type, int subjectID, bool isUpdate, ref Int64 result)
        {
            int hitCount = 0;

            switch (type)
            {
                case 2:
                    {
                        hitCount = ProcessChecker.GetResultValue(업종별판매량Dic, subjectID, isUpdate, ref result);
                    }
                    break;
                case 3:
                    {
                        hitCount = ProcessChecker.GetResultValue(업종별매출액Dic, subjectID, isUpdate, ref result);
                    }
                    break;
                case 4:
                    {
                        hitCount = ProcessChecker.GetResultValue(음식별판매량Dic, subjectID, isUpdate, ref result);
                    }
                    break;
                case 5:
                    {
                        hitCount = ProcessChecker.GetResultValue(음식별매출액Dic, subjectID, isUpdate, ref result);
                    }
                    break;
                case 7:
                    {
                        hitCount = ProcessChecker.GetResultValue(업종별매장수Dic, subjectID, isUpdate, ref result);
                    }
                    break;
                case 11:
                    {
                        hitCount = ProcessChecker.GetResultValue(업종별음식보유수Dic, subjectID, isUpdate, ref result);
                    }
                    break;
                default:
                    {
                        hitCount = ProcessChecker.GetResultValue(진행결과Dic, subjectID, isUpdate, ref result);
                    }
                    break;
            }

            // 순이익 결과는 0 보다 작으면 안된다.
            if (subjectID == 5002 && result < 0 )
            {
                result = 0;
            }

            return hitCount;
        }
        

        Dictionary<int, int> 진행결과Dic = new Dictionary<int, int>();    

        Dictionary<int, int> 업종별판매량Dic = new Dictionary<int, int>(); // Type 2
        Dictionary<int, int> 업종별매출액Dic = new Dictionary<int, int>(); // Type 3
        Dictionary<int, int> 업종별매장수Dic = new Dictionary<int, int>(); // Type 7
        Dictionary<int, int> 업종별음식보유수Dic = new Dictionary<int, int>(); // Type 11

        Dictionary<int, int> 음식별판매량Dic = new Dictionary<int, int>(); // Type 4
        Dictionary<int, int> 음식별매출액Dic = new Dictionary<int, int>(); // Type 5

    }

    public class NoneSimulationQuestCountValue
    {
        public void 업종_변경() { ProcessChecker.ValueUpdate(5401, 1, ref 진행결과Dic); }
        public void 신규_직원() { ProcessChecker.ValueUpdate(5601, 1, ref 진행결과Dic); }
        public void 꾸미기_구매(int value) { ProcessChecker.ValueUpdate(5701, value, ref 진행결과Dic); }
        public void 꾸미기_설치() { ProcessChecker.ValueUpdate(5702, 1, ref 진행결과Dic); }
        public void 홍보_구매() { ProcessChecker.ValueUpdate(5703, 1, ref 진행결과Dic); }
        public void 월드_확장() { ProcessChecker.ValueUpdate(5801, 1, ref 진행결과Dic); }
        public void 경쟁_매장_폐업() { ProcessChecker.ValueUpdate(6001, 1, ref 진행결과Dic); }
        public void 서빙_직원채용() { ProcessChecker.ValueUpdate(5608, 1, ref 진행결과Dic); }
        public void 요리사_직원채용() { ProcessChecker.ValueUpdate(5609, 1, ref 진행결과Dic); }
        public void 매니저_직원채용() { ProcessChecker.ValueUpdate(5610, 1, ref 진행결과Dic); }
        public void 등급2_이상_직원채용() { ProcessChecker.ValueUpdate(5611, 1, ref 진행결과Dic); }
        public void 등급3_이상_직원채용() { ProcessChecker.ValueUpdate(5612, 1, ref 진행결과Dic); }
        public void 등급4_이상_직원채용() { ProcessChecker.ValueUpdate(5613, 1, ref 진행결과Dic); }
        public void 식재료_구매_수() { ProcessChecker.ValueUpdate(6203, 1, ref 진행결과Dic); }
        public void 매입_매장_수(int value) { ProcessChecker.ValueUpdate(5201, value, ref 진행결과Dic); }
        public void 현재_1차증축_매장_수(int value) { ProcessChecker.ValueUpdate(5301, value, ref 진행결과Dic); }
        public void 현재_2차증축_매장_수(int value) { ProcessChecker.ValueUpdate(5302, value, ref 진행결과Dic); }

        public void 음식_업그레이드(int grade) 
        {
            switch (grade)
            {
                case 2: ProcessChecker.ValueUpdate(5501, 1, ref 진행결과Dic); break;
                case 3: ProcessChecker.ValueUpdate(5502, 1, ref 진행결과Dic); break;
                case 4: ProcessChecker.ValueUpdate(5503, 1, ref 진행결과Dic); break;
                case 5: ProcessChecker.ValueUpdate(5504, 1, ref 진행결과Dic); break;
            }

            ProcessChecker.ValueUpdate(5505, 1, ref 진행결과Dic);
        }

        public void 직원_레벨업그레이드(int level)
        {
            if (level >= 8)
            {
                ProcessChecker.ValueUpdate(5605, 1, ref 진행결과Dic);
            }

            if (level >= 6)
            {
                ProcessChecker.ValueUpdate(5604, 1, ref 진행결과Dic);
            }

            if (level >= 4)
            {
                ProcessChecker.ValueUpdate(5603, 1, ref 진행결과Dic);
            }

            if (level >= 2)
            {
                ProcessChecker.ValueUpdate(5602, 1, ref 진행결과Dic);
            }
                        
            ProcessChecker.ValueUpdate(5606, 1, ref 진행결과Dic);
        }
      


        public int Update(int subjectID, bool isUpdate, ref Int64 result)
        {
            int hitCount = 0;
            hitCount = ProcessChecker.GetResultValue(진행결과Dic, subjectID, isUpdate, ref result);
            return hitCount;
        }       

        Dictionary<int, int> 진행결과Dic = new Dictionary<int, int>();
    }

    static class ProcessChecker
    {
        public static int GetResultValue(Dictionary<int, int> valueDic, int subjectID, bool isUpdate, ref Int64 result)
        {
            int value = 0;
            if (valueDic.TryGetValue(subjectID, out value))
            {
                if (isUpdate)
                {
                    result += value;
                }
                else
                {
                    result = value;
                }

                return 1;
            }

            return 0;
        }

        public static void ValueUpdate(int key, int value, ref Dictionary<int, int> dic)
        {
            int curValue = 0;
            int newValue = 0;

            if(dic.TryGetValue(key, out curValue))
            {
                newValue = curValue + value;
                dic.AddOrUpdate(key, newValue);
            }
            else
            {
                newValue = value;
                dic.Add(key, newValue);
            }

            // 서비스 할 때는 출력하면 안 된다.
            if (CommonServer.ServerConfig.IsServiceVersion() == false)
            {
                //CommonServer.FileLogger.Info(string.Format("퀘스트 진행 현황. 퀘스트대상ID:{0}, 값:{1}", key, newValue));
            }
        }
    }
}
