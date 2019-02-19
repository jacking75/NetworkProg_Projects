using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFGameServerLib
{
    public struct REQ_DATA
    {
        public string LSeq;
        public string ID;
        public string Data;
    }

    public interface IRES_DATA
    {
        void SetResult(ERROR_CODE error);
    }

    public struct RES_DATA : IRES_DATA
    {
        public short Result;
        public string Data;

        public void SetResult(ERROR_CODE error) { Result = (short)error; }
    }

    // (개발)계정 생성 요청
    public struct REQ_CREATE_ACCOUNT_DEV_DATA
    {
        public string ID;
        public string PW;
    }

    public struct RES_CREATE_ACCOUNT_DEV_DATA : IRES_DATA
    {
        public void SetResult(ERROR_CODE error) { Result = (short)error; }
        public RES_CREATE_ACCOUNT_DEV_DATA Return(ERROR_CODE error)
        {
            Result = (short)error; return this;
        }

        public short Result;
    }




    // 유저 데이터 요청
    public struct REQ_USER_DATA
    {
        public string LSeq;
        public string ID;
        public string AT;
    }

    public struct RES_USER_DATA : IRES_DATA
    {
        public void SetResult(ERROR_CODE error) { Result = (short)error; }
        public RES_USER_DATA Return(ERROR_CODE error)
        {
            Result = (short)error; return this;
        }

        public short Result;   // 요청에 대한 결과
        public int DBI; // Default Building Index
        public int Lv;
        public int Exp;
        public string Money;
        public string Cash;
        public string CurTurn;   // 현재 진행중인 턴 수
        public short MaxBuild;  // 지을 수 있는 최대 건물 수
        public short TSeq;      // 튜토리얼 진행 번호.
        public int GWCount;     // GoodWillCount. 영업권
        public int GWWaitTime;  // 영업권 충전 남은 시간(초)
        public int WEWaitTime;  // 월드 확장 남은 시간(초)
        public int CSDay;       // CLOSE SUPPORT DAY(소상공인 지원 일) 0이 될 때까지 계속 받을 수 있음
        public int Knowhow;     // 경영 노하우 수
        public bool IsLevelReward;  // 레벨업 보상 있음
        public int VIPGrade;        // 현재 VIP 등급
        public int Guide;
    }




    // 월드 확장 
    public struct REQ_WORLD_EXTENSION
    {
        public string LSeq;
        public string ID;
        public string AT;

        public bool IsUseDiamond; // 월드 확장에 다이아몬드 사용
    }

    public struct RES_WORLD_EXTENSION : IRES_DATA
    {
        public void SetResult(ERROR_CODE error) { Result = (short)error; }
        public RES_WORLD_EXTENSION Return(ERROR_CODE error)
        {
            Result = (short)error; return this;
        }

        public short Result;
        public string CurMoney;
        public string CurCash;
        public int WaitingTime;
        public int MaxBuild;
    }
}
