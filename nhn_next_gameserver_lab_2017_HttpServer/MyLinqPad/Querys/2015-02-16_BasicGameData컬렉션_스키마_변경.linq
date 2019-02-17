<Query Kind="Program">
  <Reference>D:\SVN\BProject\trunk\MobileGameServer2\Bin\CommonServer.dll</Reference>
  <Reference>D:\SVN\BProject\trunk\MobileGameServer2\Bin\ServerLogic.dll</Reference>
  <NuGetReference>AutoMapper</NuGetReference>
  <NuGetReference>mongocsharpdriver</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>MongoDB.Bson</Namespace>
  <Namespace>MongoDB.Driver</Namespace>
  <Namespace>MongoDB.Driver.Builders</Namespace>
</Query>

void Main()
{
	AutoMapper.Mapper.CreateMap<DBUserBasicGameData, DBUserBasicGameData2>(); // Nuget으로 추가

    var collection = MyExtensions.GetMongoDBCollection<DBUserBasicGameData>("mongodb://172.20.60.223:27017","GameDB", "UserBasicData");
    var list = collection.FindAll();
    
	var oldList = list.ToList();
    collection.RemoveAll();
	

    var collection2 = MyExtensions.GetMongoDBCollection<DBUserBasicGameData2>("mongodb://172.20.60.223:27017","GameDB", "UserBasicData");
    foreach(var oldUser in oldList)
    {
        var newUser = AutoMapper.Mapper.Map<DBUserBasicGameData2>(oldUser);
		newUser.BGWC = 1;
		newUser.BGWT = 0;
        collection2.Insert<DBUserBasicGameData2>(newUser);
    }

    var list2 = collection2.FindAll();
    list2.Dump();

}


public class DBUserBasicGameData
{
   public string _id; // 유저아이디 혹은 이름
   public Int64 UID; // 이 데이터를 만들 때의 UserIndex
   public int DBI; // Default Building Index
   public int Lv;
   public int Exp;
   public Int64 Money;
   public Int64 Cash;
   public Int64 CurTurn;   // 현재 진행중인 턴 수
   public short MaxBuild;  // 지을 수 있는 최대 건물 수
   public short TSeq;      // TutorialSeq. 튜토리얼 진행 단계
   public int GWCount; // GoodWillCount. 영업권
   public Int64 GWCST; // GoodWillChargeStartTime. 영업권 충전 시작 시간. yyyyMMddHHmmss
   public List<bool> UseBPosList; // 빌딩 사용 여부 
   public Int64 PBSTurn; // PrepareBusinessSimulation을 요청했을 때의 턴 수
   public Int64 RASTurn;   // Refresh AI Store 처리 턴 - 현재 턴과 동일 할 경우 AI Store에 대한 갱신을 하지 않는다.
   public int RBID;
   public Int64 WESTime; // 월드 확장 시작 시간. "" 이면 확장 중이 아니다.
   public string NewsID; // 뉴스 인덱스 총 7자리의 숫자(2, 2, 2, 1)
   public List<Int64> NewsTurn; // 요소는 2개만. 첫 번째는 시작, 두 번째는 종료될 턴
   public int Weather; // 날씨
   public Int64 WeatherEndT;   // 날씨 종료될 턴
   public int CSDay; // CLOSE SUPPORT DAY(소상공인 지원 일) 0이 될 때까지 계속 받을 수 있음
   public int Knowhow; // 보유 경영노하우
   public bool LvUpReward; // 레벨업 보상 있음
   public Int64 EmployeeResetTime;    // 직원 뽑기 갱신 시간
   public int RMCount;                 // 고용시장 갱신 누적 카운트
   public int HHCount;                 // 헤드헌팅 갱신 누적 카운트
   public int RMFreeTicket;            // 고용시장 무료 갱신권
   public int HHFreeTicket;            // 헤드헌팅 무료 갱신권
   public int ELGTurn;                 // 이벤트랜드마크 생성 누적 Turn
   public int Guide;					// 현재까지 진행한 가이드 번호
}

public class DBUserBasicGameData2
{
   public string _id; // 유저아이디 혹은 이름
   public Int64 UID; // 이 데이터를 만들 때의 UserIndex
   public int DBI; // Default Building Index
   public int Lv;
   public int Exp;
   public Int64 Money;
   public Int64 Cash;
   public Int64 CurTurn;   // 현재 진행중인 턴 수
   public short MaxBuild;  // 지을 수 있는 최대 건물 수
   public short TSeq;      // TutorialSeq. 튜토리얼 진행 단계
   public int GWCount; // GoodWillCount. 영업권
   public Int64 GWCST; // GoodWillChargeStartTime. 영업권 충전 시작 시간. yyyyMMddHHmmss
   public List<bool> UseBPosList; // 빌딩 사용 여부 
   public Int64 PBSTurn; // PrepareBusinessSimulation을 요청했을 때의 턴 수
   public Int64 RASTurn;   // Refresh AI Store 처리 턴 - 현재 턴과 동일 할 경우 AI Store에 대한 갱신을 하지 않는다.
   public int RBID;
   public Int64 WESTime; // 월드 확장 시작 시간. "" 이면 확장 중이 아니다.
   public string NewsID; // 뉴스 인덱스 총 7자리의 숫자(2, 2, 2, 1)
   public List<Int64> NewsTurn; // 요소는 2개만. 첫 번째는 시작, 두 번째는 종료될 턴
   public int Weather; // 날씨
   public Int64 WeatherEndT;   // 날씨 종료될 턴
   public int CSDay; // CLOSE SUPPORT DAY(소상공인 지원 일) 0이 될 때까지 계속 받을 수 있음
   public int Knowhow; // 보유 경영노하우
   public bool LvUpReward; // 레벨업 보상 있음
   public Int64 EmployeeResetTime;    // 직원 뽑기 갱신 시간
   public int RMCount;                 // 고용시장 갱신 누적 카운트
   public int HHCount;                 // 헤드헌팅 갱신 누적 카운트
   public int RMFreeTicket;            // 고용시장 무료 갱신권
   public int HHFreeTicket;            // 헤드헌팅 무료 갱신권
   public int ELGTurn;                 // 이벤트랜드마크 생성 누적 Turn
   public int Guide;					// 현재까지 진행한 가이드 번호
   
   public int BGWC; // 영업권 충전 상품을 살 수 있는 횟수. 0 이면 살 수 없다.
   public Int64 BGWT;	// 최근에 영업권 충전 상품을 산 날짜. Ticks
}