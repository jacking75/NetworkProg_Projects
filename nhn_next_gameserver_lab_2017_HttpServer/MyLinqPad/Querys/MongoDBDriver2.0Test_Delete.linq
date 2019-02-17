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

// 삭제. 한번에 복수개를 지울 때는 DeleteManyAsync
try
{
 	var collection = MyExtensions.GetMongoDBCollectionVer2<DBBasic>("mongodb://172.20.60.221", "TestDB", "Basic");
	
	var userID = "jacking4";
 	var result = await collection.DeleteOneAsync(x=> x._id == userID);
 	result.Dump();
}
catch (Exception ex)
{
 	ex.Message.Dump();
}