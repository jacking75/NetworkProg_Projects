<Query Kind="Statements">
  <Reference>D:\SVN\BProject\trunk\MobileGameServer2\Bin\CommonServer.dll</Reference>
  <Reference>D:\SVN\BProject\trunk\MobileGameServer2\Bin\ServerLogic.dll</Reference>
  <NuGetReference>AutoMapper</NuGetReference>
  <NuGetReference>mongocsharpdriver</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>MongoDB.Bson</Namespace>
  <Namespace>MongoDB.Driver</Namespace>
  <Namespace>MongoDB.Driver.Builders</Namespace>
</Query>

// 로딩 테스트 하기
MyExtensions.DBSettingAndLoadGameContents("223");
var mailList = await CommonServer.DB.UserGameData.GetUserMailListAsync("jacking", 0, 12);
mailList.Dump();


// 메일 하나만 로딩
MyExtensions.DBSetting("223");
var mail = await CommonServer.DB.UserGameData.GetUserMailAsync(635646078812791630);
mail.Dump();


// 메일 추가하기
MyExtensions.DBSetting("223");
var userID = "hotm12";

for(int i = 0; i < 14; ++i)
{
	var newmail = new CommonServer.DB.DBUserMail()
    {
        _id = DateTime.Now.Ticks + i,
        UserID = userID,
        STID = 20001,
        CTID = 30001,
        AT = 1,
        Value = 100+i,
        Tick = DateTime.Now.AddDays(7).Ticks,
    };
	
	var result = await CommonServer.DB.UserGameData.InsertMailAsync(newmail);
	result.Dump();
}


// 메일 첨부 받기
MyExtensions.DBSetting("223");
var userID = "jacking";
var curUserData1 = await CommonServer.DB.UserGameData.GetBasicData(userID);
curUserData1.Dump();

var result = await CommonServer.DB.UserGameData.UpdateMailGetData(userID, incKnowhow:2);
result.Dump();

var curUserData2 = await CommonServer.DB.UserGameData.GetBasicData(userID);
curUserData2.Dump();