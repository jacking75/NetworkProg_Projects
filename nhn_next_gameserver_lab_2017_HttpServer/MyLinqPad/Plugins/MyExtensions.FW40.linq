<Query Kind="Program">
  <Reference>D:\SVN\BProject\trunk\MobileGameServer2\Bin\CommonServer.dll</Reference>
  <Reference>D:\SVN\BProject\trunk\MobileGameServer2\Bin\ServerLogic.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <NuGetReference>AutoMapper</NuGetReference>
  <NuGetReference>mongocsharpdriver</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>MongoDB.Bson</Namespace>
  <Namespace>MongoDB.Bson.Serialization.Attributes</Namespace>
  <Namespace>MongoDB.Driver</Namespace>
  <Namespace>MongoDB.Driver.Builders</Namespace>
  <Namespace>MongoDB.Driver.Core.Misc</Namespace>
  <Namespace>MongoDB.Driver.Linq</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	// Write code to test your extensions here. Press F5 to compile and run.
	MyExtensions.SaveAuthToke("jacking1", "ddd");
	MyExtensions.SaveAuthToke("jacking2", "xxx");
	
}

public static class MyExtensions
{
	public const short NO_ERROR = 30000;
	
	public static async Task<RESULT_T> RequestHttpAESEncry<REQUEST_T, RESULT_T>(
													string address,
													string reqAPI, 
                                                    string loginSeq,
                                                    string userID,
                                                    REQUEST_T reqPacket) where RESULT_T : new() 
    {		 
        var resultData = new RESULT_T();
		
		var api = "http://" + address + "/GameService/" + reqAPI;
   		var jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(reqPacket);
   
        var encryData = AESEncrypt(loginSeq, jsonText);
        var sendData = new REQ_DATA { LSeq = loginSeq, ID = userID, Data = encryData };
        var requestJson = Newtonsoft.Json.JsonConvert.SerializeObject(sendData);

        var content = new ByteArrayContent(Encoding.UTF8.GetBytes(requestJson));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
		
		var Network = new System.Net.Http.HttpClient();
        var response = await Network.PostAsync(api, content).ConfigureAwait(false);

        if (response.IsSuccessStatusCode == false)
        {
	   		Console.WriteLine("[Error] RequestHttpAESEncry Network");
            return resultData;
        }

        var responeString = await response.Content.ReadAsStringAsync();
        var responseJson = Newtonsoft.Json.JsonConvert.DeserializeObject<RES_DATA>(responeString);

        if (responseJson.Result != NO_ERROR)
        {
	   		Console.WriteLine("[Error] RequestHttpAESEncry Response Error: {0}", responseJson.Result);
            return resultData;
        }

        var decryptData = AESDecrypt(loginSeq, responseJson.Data);
        resultData = Newtonsoft.Json.JsonConvert.DeserializeObject<RESULT_T>(decryptData);
        return resultData;
    }
	public static async Task<RESULT_T> RequestHttp<REQUEST_T, RESULT_T>(
													string address,
													string reqAPI, 
                                                    string loginSeq,
                                                    string userID,
                                                    REQUEST_T reqPacket) where RESULT_T : new() 
    {		 
        var resultData = new RESULT_T();
		
		var api = "http://" + address + "/GameService/" + reqAPI;
   		//var jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(reqPacket);
   
        //var encryData = AESEncrypt(loginSeq, jsonText);
        //var sendData = new REQ_DATA { LSeq = loginSeq, ID = userID, Data = encryData };
        var requestJson = Newtonsoft.Json.JsonConvert.SerializeObject(reqPacket);

        var content = new ByteArrayContent(Encoding.UTF8.GetBytes(requestJson));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
		
		var Network = new System.Net.Http.HttpClient();
        var response = await Network.PostAsync(api, content).ConfigureAwait(false);

        if (response.IsSuccessStatusCode == false)
        {
	   		Console.WriteLine("[Error] RequestHttpAESEncry Network");
            return resultData;
        }

        var responeString = await response.Content.ReadAsStringAsync();
        var responseJson = Newtonsoft.Json.JsonConvert.DeserializeObject<RESULT_T>(responeString);
		return responseJson;
    }
		
	#region Crypto
	static string AesIV = @"!QAZ2WSX#EDC4RFV";  //16
    static string AesKey = @"5TGB&7U(IK<";      // 11
    public static string AesLoginDynamicKey = @"d_&^t";      // 5
		
	static string AESEncrypt(string dynamincKey, string text)
    {
       var comleteAesKey = dynamincKey.Substring(0, 5) + AesKey;

       System.Security.Cryptography.AesCryptoServiceProvider aes = new System.Security.Cryptography.AesCryptoServiceProvider();
       aes.BlockSize = 128;
       aes.KeySize = 128;
       aes.IV = Encoding.UTF8.GetBytes(AesIV);
       aes.Key = Encoding.UTF8.GetBytes(comleteAesKey);
       aes.Mode = CipherMode.CBC;
       aes.Padding = PaddingMode.PKCS7;

       // 문자열을 바이트형 배열로 변환
       byte[] src = Encoding.Unicode.GetBytes(text);

       // 암호화 한다
       using (ICryptoTransform encrypt = aes.CreateEncryptor())
       {
           byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);

           // 바이트형 배열에서 Base64형식의 문자열로 변환
           return Convert.ToBase64String(dest);
       }
    }

    static string AESDecrypt(string dynamincKey, string text)
    {
       var comleteAesKey = dynamincKey.Substring(0, 5) + AesKey;

       AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
       aes.BlockSize = 128;
       aes.KeySize = 128;
       aes.IV = Encoding.UTF8.GetBytes(AesIV);
       aes.Key = Encoding.UTF8.GetBytes(comleteAesKey);
       aes.Mode = CipherMode.CBC;
       aes.Padding = PaddingMode.PKCS7;

       // Base64 형식의  문자열에서 바이트형 배열로 변환
       byte[] src = System.Convert.FromBase64String(text);

       // 복호화 한다
       using (ICryptoTransform decrypt = aes.CreateDecryptor())
       {
           byte[] dest = decrypt.TransformFinalBlock(src, 0, src.Length);
           return Encoding.Unicode.GetString(dest);
       }
    }
	#endregion Crypto
	
	
	#region CACHE
	static Dictionary<string, string> UserLoingCache = Util.Cache(() => new Dictionary<string, string>());
	
	public static void SaveAuthToke(string id, string at)
	{
		var key = id+"_at";
		UserLoingCache[key] = at;
	}

	public static string GetAuthToke(string id)
	{
		var key = id+"_at";
		return UserLoingCache[key];
	}
	
	public static void SaveLoginSeq(string id, string loginSeq)
	{
		var key = id+"_seq";
		UserLoingCache[key] = loginSeq;
	}

	public static string GetLoginSeq(string id)
	{
		var key = id+"_seq";
		return UserLoingCache[key];
	}
	#endregion CACHE
	
	#region DB설정
	public static void DBSetting(string server)
	{
		if(server == "223")
		{
			CommonServer.ServerConfig.DevToolInit("mongodb://172.20.60.223", 
													"mongodb://172.20.60.223", 
													"mongodb://172.20.60.223");
		}
		else
		{
			Console.WriteLine("등록 되어 있지 않은 서버 주소");
		}
	}
	
	public static void DBSettingAndLoadGameContents(string server)
	{
		CommonServer.UniqueNumberManager.Init(21, 21);
        
		if(server == "223")
		{
			CommonServer.ServerConfig.DevToolInit("mongodb://172.20.60.223", 
													"mongodb://172.20.60.223", 
													"mongodb://172.20.60.223");
			//CommonServer.DB.DBLibDev.AccountDBConnectString = "mongodb://172.20.60.223";
		}
		else
		{
			Console.WriteLine("등록 되어 있지 않은 서버 주소");
			return;
		}
				
		CommonServer.DB.GameContents.Loader.Load();
	}
	
	public static MongoCollection<T> GetMongoDBCollection<T>(string connectString, string dataBase, string collection)
	{
		var cli1 = new MongoClient(connectString);
		var db1 = cli1.GetServer().GetDatabase(dataBase);
		return db1.GetCollection<T>(collection);
	}	
	#endregion DB설정
	
	
	#region MongoDB 2.0 테스트
	public static IMongoCollection<T> GetMongoDBCollectionVer2<T>(string connectString, string dbName, string collectionName)
    {
       var mongoClient = new MongoClient(connectString);
       if (mongoClient == null)
       {
           return null;
       }

       var collection = mongoClient.GetDatabase(dbName).GetCollection<T>(collectionName);
       return collection;
    }    
	#endregion MongoDB 2.0 테스트
}

// You can also define non-static classes, enums, etc.
public enum MARKET_TYPE : short
{
   ANDROID = 29,
   iOS = 30,
}

#region PACKET_DEFINE
public struct REQ_DATA
{
   public string LSeq;
   public string ID;
   public string Data;
}

public struct RES_DATA
{
   public short Result;
   public string Data;
}

// 로그인 요청
public struct REQ_LOGIN_DATA
{
   public string ID;
   public string PW;
   public short ClientVer;
   public short DataVer;
   public short MarketType;
}

public struct RES_LOGIN_DATA
{
   public short Result;   // 요청에 대한 결과
   public string AT;           // AuthToken
   public string LSeq;          // LoginSeq;
}

// 기본 게임데이터 요청
public struct REQ_USER_DATA
{
   public string LSeq;
   public string ID;
   public string AT;
}

public struct RES_USER_DATA
{
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
}


// 튜토리얼 단계 완료 요청
public struct REQ_TUTORIAL_STEP
{
   public string LSeq;
   public string ID;
   public string AT;
}

public struct RES_TUTORIAL_STEP
{
   public short Result;   
   public int TSeq;  // 현재 완료된 단계
}


///<<< 영업 관련 패킷
public struct REQ_PREPARE_BUSINESS_SIMULATION
{
	public string LSeq;
	public string ID;
	public string AT;
}

public struct RES_PREPARE_BUSINESS_SIMULATION
{
	public short Result;   // 요청에 대한 결과
	public int GWCount; // GoodWillCount. 영업권
	public int GWWaitTime; 
}

public struct REQ_BUSINESS_SIMULATION
{
	public string LSeq;
	public string ID;
	public string AT;
}

public struct BUSINESS_RESULT
{
	public string StoreUID;
	public string Name;
	public int FoodType;
	public int BlockPos;                 // 상점 위치 ( Play-1~12, AI-13~24)
	public int TotalIncomeMoney;        // 1일 총 매출
	public int TotalExpense;            // 1일 총 지출

	public int VisitCountReserve;       // 방문예정자 수
	public int OneDayTotalVistor;       // 음식 먹고간 손님
	public int OneDayTotalReturn;       // 비싸서 돌아간 손님 수
	public int OneDayNoSit;             // 자리 없어 돌아간 손님 수

	public int FoodScore;               // 음식 점수(MAX 65)
	public int ServiceScore;            // 서비스 점수(MAX 35)
	public int PriceScore;              // 가격점수(-15 ~ 15)
	public int FamousScore;             // 평판
}
public struct RES_BUSINESS_SIMULATION
{
   public short Result;   // 요청에 대한 결과
   public int RealVisitCustomer;   // 상권 전체 연출을 위한 손님 수
   public int KnowHowCount;        // 경영 노하우 발생수
   public int GroupGuestID1;       // 1wave 단체 손님 ID
   public int GroupGuestID2;       // 2wave 단체 손님 ID
   public int GroupGuestID3;       // 3wave 단체 손님 ID
   public int EventLandMark;       // 이벤트 랜드마크 ID
   public List<BUSINESS_RESULT> BusinessResultList;
}



public struct REQ_BUSINESS_RESULT
{
	public string LSeq;
	public string ID;
	public string AT;
	public int KnowHowCount;    // 영업 시뮬레이션 도중 클릭하여 획득한 영업노하우 수
	public int GroupGuestID1;       // 클릭 성공한 1wave 단체 손님 ID
	public int GroupGuestID2;       // 클릭 성공한 2wave 단체 손님 ID
	public int GroupGuestID3;       // 클릭 성공한 3wave 단체 손님 ID
}

// 미션 보상
public struct MISSION_REWARD
{
	public string Money;   // 미션 보상 Money
	public string Cash;    // 미션 보상 Cash
	public int Exp;     // 미션 보상 경험치
}
public struct MISSION_RESULT
{
   public int LandMarkID; //랜드마크 ID
   public string CreateTurn;   // 생성 Turn
   public string CurScore; // 현재 진행 상태
}
public struct RES_BUSINESS_RESULT
{
	public short Result;   // 요청에 대한 결과

	public string BusinessTurn;                // 영업턴
	public int UserStoreVisitor;          // 유저매장 방문 손님
	public int AIStoreVisitor;            // AI매장 방문 손님
	public int ReturnVisitor;             // 돌아간 전체 손님
	public int NoFavorCustomer;             // 선호 업종 없어 방문하지 않은 손님
	public int UserIncome;                // 유저매장 전체 매출
	public int UserExpense;               // 유저매장 전체 지출
	public int UserFamousScore;             // 유저매장 평균 평판
	public int UserFoodScore;               // 유저매장 평균 음식 점수
	public int UserServiceScore;            // 유저매장 평균 서비스 점수
	public int UserPriceScore;              // 유저매장 평균 가격 점수
	public int UserStoreNotSitReturn;       // 유저매장 자리 없어 돌아감
	public int UserStorePriceReturn;        // 유저매장 비싸서 돌아감
	public int UserPromotionVisitor;        // 홍보효과 방문 손님 수

	public int CommentID;                   // 영업결과 코멘트 ID

	public int OccupationExp;               // 점유율 승리 경험치
	public int KnowhowCount;                // 획득 경영 노하우

	public int GroupGuestMoney;             // 단체 손님으로 획득한 Money
	public int EventLandMoney;              // 이벤트 랜드마크로 획득한 Money

	public MISSION_REWARD MissionReward;    // 미션 보상
	public List<MISSION_RESULT> MissionResultList;  // 미션 결과 정보 
}
///>>>> 영업 관련 패킷


public struct REQ_LOAD_USER_VIP
{
   public string LSeq;
   public string ID;
   public string AT;
}

public struct RES_LOAD_USER_VIP
{
   public short Result;

   public int VIPGrade;        // 현재 VIP 등급
   public int VIPFree;         // Free VIP. VIPGrade + VIPFree 이 실질적인 현재 VIP 등급
   public int VIPWaitTime;     // 다음 VIP 충전까지 남은 시간 초
   public string VIPBuyDiamond; // VIP 누적 구입 다이아몬드 수
}


public struct REQ_DEV_BUY_DIAMOND
{
   public string LSeq;
   public string ID;
   public string AT;
}

public struct RES_DEV_BUY_DIAMOND
{
   public short Result;
   public int CurDiamond;  // 현재 다이아몬드 수
   public int CurVipGrade; // 현재 VIP 등급
   public int CurVipPrice; // 현재 VIP 다이아몬드 누적 구입 수
}

#region 비지니스 모델
public struct REQ_BM_BUY_DIAMOND
{
   public string LSeq;
   public string ID;
   public string AT;

   public int SID;
}

public struct RES_BM_BUY_DIAMOND
{
   public short Result;
   public string StoreUID;   // 구입 고유 ID. 정액or횟수제한이 아닌 경우에는 "0"
   public string CurDiamond;
   public int VipGrade;
   public string VipPrice;
}


public struct REQ_BM_LOAD_BOUGHT_LIST 
{
   public string LSeq;
   public string ID;
   public string AT;
}
   
public struct RES_BM_LOAD_BOUGHT_LIST
{
   public short Result;
   public List<string> StoreUID;   // 구입 고유 ID
   public List<int> StoreID;
   public List<int> BonusLY; // 보너스 요청 가능 마지막 년
   public List<int> BonusLM; // 보너스 요청 가능 마지막 월
   public List<int> BonusLD; // 보너스 요청 가능 마지막 일
   public List<int> BonusRY; // 최근 보너스 요청한 년
   public List<int> BonusRM; // 최근 보너스 요청한 월
   public List<int> BonusRD; // 최근 보너스 요청한 일
   public List<int> BuyCount; // 지금까지 구입한 횟수
}


public struct REQ_BM_BONUS_DIAMOND
{
   public string LSeq;
   public string ID;
   public string AT;

   public string StoreUID;
}

public struct RES_BM_BONUS_DIAMOND
{
   public short Result;
   public string CurDiamond; // 보너스까지 받은 후의 총 다이아몬드 수
   public int VipGrade;
   public string VipPrice;
}
#endregion

#endregion PACKET_DEFINE


#region MongoDB2.0 테스트용 클래스
public class DBBasic
{
   public string _id; // 유저ID
   public int Level;
   public int Exp;
   public Int64 Money;
   public List<int> Costume; // 캐릭터 복장 아이템ID. 개수는 무조건 12
}

public class DBUserItem
{
   public Int64 _id; // Unique ID

   public string UserID; 

   public int ItemID;

   [MongoDB.Bson.Serialization.Attributes.BsonElement("AD")]
   public DateTime AcquireDateTime; // 아이템 입수 시간
}


public class SkillItemInfo
{
	public int Value;
}
public class SkillItem
{
    public int ID;
    public SkillItemInfo Info;
}
public class DBUserSkill
{
    public string _id;
	public int Value;
    public List<SkillItem> SkillItems;
}


public class DBTimeData
{
	public DateTime CurTime;
}

#endregion MongoDB2.0 테스트용 클래스