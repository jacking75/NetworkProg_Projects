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

// 기본 업데이트
// UpdateOneAsync 는 하나만, 복수는 UpdateManyAsync
try
{
	var collection = MyExtensions.GetMongoDBCollectionVer2<DBUserSkill>("mongodb://172.20.60.221", "TestDB", "Skill");

	var userID = "jacking";
	
	var result = await collection.UpdateOneAsync(x => x._id == userID,
									Builders<DBUserSkill>.Update.Set(x => x.Value, 14));
									
	result.Dump();
	// result.MatchedCount 가 1 보다 작으면 업데이트 한 것이 없음
}
catch (Exception ex)
{
 	ex.Message.Dump();
}


// 도큐먼트의 내부 도큐먼트를 변경할 때
try
{
	var collection = MyExtensions.GetMongoDBCollectionVer2<DBUserSkill>("mongodb://172.20.60.221", "TestDB", "Skill");

	var userID = "jacking";
	int skillItemID = 1;
	var newInfo = new SkillItemInfo() { Value = 101 };
	
 	
	var filter = Builders<DBUserSkill>.Filter.And(Builders<DBUserSkill>.Filter.Eq(x => x._id, userID),
    											Builders<DBUserSkill>.Filter.ElemMatch(x => x.SkillItems, x => x.ID == skillItemID));
	
	var update = Builders<DBUserSkill>.Update.Set("SkillItems.$.Info", newInfo);
	//or
	//var update = Builders<DBUserSkill>.Update.Set(x => x.SkillItems.ElementAt(-1).Info, newInfo);
	
	collection.UpdateOneAsync(filter, update);
}
catch (Exception ex)
{
 	ex.Message.Dump();
}



// Replace
try
{
 	var collection = MyExtensions.GetMongoDBCollectionVer2<DBBasic>("mongodb://172.20.60.221", "TestDB", "Basic");
	
	var userID = "jacking";
 	var documents = await collection.Find(x=> x._id == userID).SingleAsync();
 	
	if( documents != null)
	{
		documents.Level = documents.Level + 3;
		var result = await collection.ReplaceOneAsync(x => x._id == documents._id, documents);
		result.Dump();
	}
	else
	{
		Console.WriteLine("없음");
	}
}
catch (Exception ex)
{
 	ex.Message.Dump();
}

var tom = await collection.Find(x => x.Id == ObjectId.Parse("550c4aa98e59471bddf68eef"))
	.SingleAsync();

tom.Name = "Thomas";
tom.Age = 43;
tom.Profession = "Hacker";

var result = await collection.ReplaceOneAsync(x => x.Id == tom.Id, tom);