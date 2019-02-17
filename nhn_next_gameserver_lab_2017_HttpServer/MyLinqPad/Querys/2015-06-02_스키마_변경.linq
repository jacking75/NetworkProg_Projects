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
	AutoMapper.Mapper.CreateMap<DBUserLandMark, DBUserLandMark2>(); // Nuget으로 추가

    var collection = MyExtensions.GetMongoDBCollection<DBUserLandMark>("mongodb://172.20.60.223:27017","GameDB", "UserLandMark");
    var list = collection.FindAll();
    
	var oldList = list.ToList();
    collection.RemoveAll();
	

    var collection2 = MyExtensions.GetMongoDBCollection<DBUserLandMark2>("mongodb://172.20.60.223:27017","GameDB", "UserLandMark");
    foreach(var oldUser in oldList)
    {
        var newUser = AutoMapper.Mapper.Map<DBUserLandMark2>(oldUser);
        collection2.Insert<DBUserLandMark2>(newUser);
    }

    var list2 = collection2.FindAll();
    list2.Dump();

}


public class UserLandmark
{
   public int ID; // 랜드마크ID
   public Int64 TurnNum; // 랜드마크 생성했을 때의 턴 수 
   public int Favor1P; // 선호 업정 1 인구수
   public int Favor2P; // 선호 업정 2 인구수
   public int Favor3P; // 선호 업정 3 인구수
   public Int64 Score;   // 미션 진행 값

   public void Clear()
   {
       ID = Favor1P = Favor2P = Favor3P = 0;
       TurnNum = 0;
       Score = 0;
   }

   public void Set(UserLandmark other)
   {
       ID = other.ID;
       TurnNum = other.TurnNum;
       Favor1P = other.Favor1P;
       Favor2P = other.Favor2P;
       Favor3P = other.Favor3P;
       Score = other.Score;
   }

   public bool IsMissionComplete(Int64 expectScore)
   {
       return Score >= expectScore ? true : false;
   }                
}    
public class DBUserLandMark
{
   public string _id; // UserID or NickName
   public List<UserLandmark> LMList = new List<UserLandmark>();
   public int CUL;     // 랜드마크 생성 때의 유저 레벨
   public int CULS;    // CUL 값이 달라질 때는 0, 그 이외는 랜드마크 생성 때마다 1씩 증가한다
   public int LandMarkCount() { return LMList.Count(x => x.ID != 0); }

}

public class DBUserLandMark2
{
   public string _id; // UserID or NickName
   public List<UserLandmark> LMList = new List<UserLandmark>();
   public int CUL;     // 랜드마크 생성 때의 유저 레벨
   
   public int LandMarkCount() { return LMList.Count(x => x.ID != 0); }

}