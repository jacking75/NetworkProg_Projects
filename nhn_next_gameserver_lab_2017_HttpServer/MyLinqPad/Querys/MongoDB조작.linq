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

var cli1 = new MongoClient("mongodb://172.20.60.223");
var db1 = cli1.GetServer().GetDatabase("GameContentsData");
var collection1 = db1.GetCollection<BsonDocument>("RankingReward");

var cli2 = new MongoClient("mongodb://172.20.60.221");
var db2 = cli2.GetServer().GetDatabase("GameContentsData");
var collection2 = db2.GetCollection<BsonDocument>("RankingReward");
collection2.RemoveAll();
            
collection2.InsertBatch(collection1.FindAll());