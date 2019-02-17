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


MyExtensions.DBSettingAndLoadGameContents("223");
var userQuest = await CommonServer.DB.UserGameData.GetQuestData("jacking");
userQuest.Dump();


var simulCountValue = new ServerLogic.Quest.SimulationQuestCountValue(); 
simulCountValue.총매출(500000);
await ServerLogic.Quest.Manager.Update("jacking", simulCountValue);