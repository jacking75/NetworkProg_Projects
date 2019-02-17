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

// FindOneAndReplaceAsync
try
{
 	var collection = MyExtensions.GetMongoDBCollectionVer2<DBBasic>("mongodb://172.20.60.221", "TestDB", "Basic");
 	
	var newData = new DBBasic()
	{
		_id = "jacking3",
		Money = 3333,
		Costume = new List<int>(Enumerable.Repeat(0, 12))
	};
	
	// 변경 되기 이전 값을 반환한다. 실패하면 null
	var documents = await collection.FindOneAndReplaceAsync(x => x._id == "jacking5", newData);
 	documents.Dump();
}
catch (Exception ex)
{
 	ex.Message.Dump();
}


// FindOneAndReplaceAsync
try
{
 	var collection = MyExtensions.GetMongoDBCollectionVer2<BsonDocument>("mongodb://172.20.60.221", "TestDB", "Basic");
 	
	var filter = new BsonDocument("_id", "jacking3");
	
	// _id와 Level 필드만 도큐먼트에 남게된다.
  	var replacement = BsonDocument.Parse("{Level: 12}");
  	//var projection = BsonDocument.Parse("{x: 1}");
  	//var sort = BsonDocument.Parse("{a: -1}");
  	var options = new FindOneAndReplaceOptions<BsonDocument, BsonDocument>()
  	{
    	IsUpsert = false,
      	//Projection = projection,
      	//ReturnDocument = returnDocument,
      	//Sort = sort,
      	MaxTime = TimeSpan.FromSeconds(2)
  	};

  	var documents = await collection.FindOneAndReplaceAsync<BsonDocument>(filter, replacement, options, CancellationToken.None);
	documents.Dump();
}
catch (Exception ex)
{
 	ex.Message.Dump();
}


//FindOneAndUpdateAsync
try
{
 	var collection = MyExtensions.GetMongoDBCollectionVer2<BsonDocument>("mongodb://172.20.60.221", "TestDB", "Basic");
 	
	var filter = new BsonDocument("_id", "jacking3");
  	var update = BsonDocument.Parse("{$set: {Level: 3}}");
  	//var projection = BsonDocument.Parse("{x: 1}");
  	//var sort = BsonDocument.Parse("{a: -1}");
  	var options = new FindOneAndUpdateOptions<BsonDocument, BsonDocument>()
  	{
      	//IsUpsert = isUpsert,
      	//Projection = projection,
      	//ReturnDocument = returnDocument,
      	//Sort = sort,
      	MaxTime = TimeSpan.FromSeconds(2)
  	};

  
  var document = await collection.FindOneAndUpdateAsync<BsonDocument>(filter, update, options, CancellationToken.None);
  document.Dump();
}
catch (Exception ex)
{
 	ex.Message.Dump();
}