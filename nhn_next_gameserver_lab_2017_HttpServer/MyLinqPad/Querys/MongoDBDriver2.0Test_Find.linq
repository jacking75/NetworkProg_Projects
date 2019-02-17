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

// 데이터 하나만 가져온다
try
{
	var collection = MyExtensions.GetMongoDBCollectionVer2<DBBasic>("mongodb://172.20.60.221", "TestDB", "Basic");
	
	// 기본으로는 Find 메소드는 없다. Find는 확장 메소드로 사용하고 싶다면
 	//using MongoDB.Driver.Core.Misc;
 	//using MongoDB.Driver;
 	//을 선언해야 한다.

 	// 첫 번째 값 또는 null
 	var document = await collection.Find(x => x._id == "jacking").FirstOrDefaultAsync();
 	document.Dump();
}
catch (Exception ex)
{
	ex.Message.Dump();
}


// BsonDocument를 사용한 조건 쿼리
try
{
	var userID = "jacking";
	int userLevel = 0;
	
	var collection = MyExtensions.GetMongoDBCollectionVer2<BsonDocument>("mongodb://172.20.60.221", "TestDB", "Basic");
 	var filter = new BsonDocument("_id", userID);
 	var documents = await collection.Find(filter).ToListAsync();

 	if (documents.Count > 0)
 	{
    	userLevel = documents[0]["Level"].AsInt32;
	}
	
	userLevel.Dump();
}
catch (Exception ex)
{
 	ex.Message.Dump();
}


// BsonDocument를 사용한 조건 쿼리 2
try
{
 	var collection = MyExtensions.GetMongoDBCollectionVer2<BsonDocument>("mongodb://172.20.60.221", "TestDB", "Basic");

 	var filter = new BsonDocument("Level", new BsonDocument("$gte", 2));
 	var documents = await collection.Find(filter).ToListAsync();
 	documents.Dump();
}
catch (Exception ex)
{
 	ex.Message.Dump();
}


// BsonDocument를 사용한 조건 쿼리 3
try
{
	// Builders를 사용할 때는 Collection은 BsonDocument를 사용해야 한다.

 	var collection = MyExtensions.GetMongoDBCollectionVer2<BsonDocument>("mongodb://172.20.60.221", "TestDB", "Basic");

 	var builder = Builders<BsonDocument>.Filter;
 	var filter = builder.Gte("Level", 2) & builder.Eq("Money", 1000);
 	var documents = await collection.Find(filter).ToListAsync();

 	var IDList = new List<string>();

 	foreach (var document in documents)
 	{
    	IDList.Add(document["_id"].AsString);
 	}
	
 	IDList.Dump();
}
catch (Exception ex)
{
 	ex.Message.Dump();
}


// 클래스를 사용한 조건 쿼리
try
{
 	var collection = MyExtensions.GetMongoDBCollectionVer2<DBBasic>("mongodb://172.20.60.221", "TestDB", "Basic");
 	var documents = await collection.Find(x=> x.Level >= 2).ToListAsync();
 	documents.Dump();
}
catch (Exception ex)
{
 	ex.Message.Dump();
}


// 정렬
try
{
 	var collection = MyExtensions.GetMongoDBCollectionVer2<DBBasic>("mongodb://172.20.60.221", "TestDB", "Basic");
 	
	// 내림차순
	//var documents = await collection.Find(x=> x.Level >= 1).SortByDescending(d => d.Level).FirstOrDefaultAsync();
	// or
	//var documents = await collection.Find(x=> x.Level >= 1).SortByDescending(d => d.Level).FirstOrDefaultAsync();
	// or
	//var documents = await collection.Find(x=> x.Level >= 1).SortByDescending(d => d.Level).ToListAsync();
	// or 조건 없이
	var documents = await collection.Find(x => true).SortByDescending(d => d.Level).ToListAsync();
 	documents.Dump();
}
catch (Exception ex)
{
 	ex.Message.Dump();
}


// 정렬 2
try
{
 	var collection = MyExtensions.GetMongoDBCollectionVer2<BsonDocument>("mongodb://172.20.60.221", "TestDB", "Basic");
 	
	// 올림 차순
	//var documents = await collection.Find(new BsonDocument()).Sort(new BsonDocument("$Level", 1)).ToListAsync();
	
	// 내림 차순
	//var documents = await collection.Find(new BsonDocument()).Sort(new BsonDocument("Level", -1)).ToListAsync();
	// or
	var documents = await collection.Find(new BsonDocument()).Sort("{Level: -1}").ToListAsync();
 	documents.Dump();
}
catch (Exception ex)
{
 	ex.Message.Dump();
}


// 정렬 3
var sort = Builders<BsonDocument>.Sort.Ascending("borough").Ascending("address.zipcode");
var result = await collection.Find(filter).Sort(sort).ToListAsync();

			

// 특정 필드의 데이터만 가져오기
try
{
 	var collection = MyExtensions.GetMongoDBCollectionVer2<BsonDocument>("mongodb://172.20.60.221", "TestDB", "Basic");
	
	// Level만
	var documents = await collection.Find(new BsonDocument()).Project(BsonDocument.Parse("{Level:1}")).ToListAsync();
	// Level, Money만
 	//var documents = await collection.Find(new BsonDocument()).Project(BsonDocument.Parse("{Level:1, Money:1}")).ToListAsync();
 	
	documents.Dump();
}
catch (Exception ex)
{
 	ex.Message.Dump();
}

// 특정 필드를 가져오기
try
{
 	var collection = MyExtensions.GetMongoDBCollectionVer2<DBBasic>("mongodb://172.20.60.221", "TestDB", "Basic");
	
	var projection = Builders<DBBasic>.Projection.Include("Exp").Include("Level");
	var documents = await collection.Find(x => true).Project(projection).ToListAsync();
	documents.Dump();
}
catch (Exception ex)
{
 	ex.Message.Dump();
}


// 특정 필드를 제외한 데이터만 가져오기
try
{
 	var collection = MyExtensions.GetMongoDBCollectionVer2<BsonDocument>("mongodb://172.20.60.221", "TestDB", "Basic");
	
	// Level만
	var documents = await collection.Find(new BsonDocument()).Project(BsonDocument.Parse("{Level:0}")).ToListAsync();
	documents.Dump();
}
catch (Exception ex)
{
 	ex.Message.Dump();
}


// Expression
// 지정된 필드만, 필드는 다른 이름으로
try
{
 	var collection = MyExtensions.GetMongoDBCollectionVer2<DBBasic>("mongodb://172.20.60.221", "TestDB", "Basic");
	
	var projection = Builders<DBBasic>.Projection.Expression(x => new { X = x.Level, Y = x.Exp });
	var documents = await collection.Find(x => true).Project(projection).ToListAsync();
	documents.Dump();
}
catch (Exception ex)
{
 	ex.Message.Dump();
}
//var projection = Builders<Widget>.Projection.Expression(x => new { X = x.X, Y = x.Y });
//var projection = Builders<Widget>.Projection.Expression(x => new { Sum = x.X + x.Y });
//var projection = Builders<Widget>.Projection.Expression(x => new { Avg = (x.X + x.Y) / 2 });
//var projection = Builders<Widget>.Projection.Expression(x => (x.X + x.Y) / 2);




// sort + projection + skip + limt
try
{
	//var userID = "jacking";
	//int userLevel = 0;
	
	var projection = BsonDocument.Parse("{Level:1, Momey:1, Exp:1}");
    var sort = BsonDocument.Parse("{Level:1}");
    var options = new FindOptions
    {
        AllowPartialResults = true,
        BatchSize = 20,
        Comment = "funny",
        //CursorType = CursorType.TailableAwait,
        MaxTime = TimeSpan.FromSeconds(3),
        NoCursorTimeout = true
    };

    var collection = MyExtensions.GetMongoDBCollectionVer2<DBBasic>("mongodb://172.20.60.221", "TestDB", "Basic");
 	var documents = collection.Find(x => x.Exp >= 0, options).Project(projection)
												.Sort(sort)
												.Limit(30)
												.Skip(0).ToListAsync();

 	documents.Dump();
}
catch (Exception ex)
{
 	ex.Message.Dump();
}


// Builders 사용 
try
{
	var collection = MyExtensions.GetMongoDBCollectionVer2<DBBasic>("mongodb://172.20.60.221", "TestDB", "Basic");
	
	var builder = Builders<DBBasic>.Filter;
	var filter = builder.Eq(x => x._id, "jacking") & builder.Lt(x => x.Money, 1100);
	// or
	//var filter = builder.Eq("X", 10) & builder.Lt("Y", 20);
	// or
	//var filter = builder.Eq("x", 10) & builder.Lt("y", 20);
	// or
	//var filter = Builders<DBBasic>.Filter.Where(x => x._id == "jacking" && x.Money < 1100);

 	// 첫 번째 값 또는 null
 	var document = await collection.Find(filter).FirstOrDefaultAsync();
 	document.Dump();
}
catch (Exception ex)
{
	ex.Message.Dump();
}


// 배열 요소 조건 검색
try
{
 	var collection = MyExtensions.GetMongoDBCollectionVer2<DBBasic>("mongodb://172.20.60.221", "TestDB", "Basic");
 	
	var filter = Builders<DBBasic>.Filter.AnyGt(x => x.Costume, 0);
	var documents = await collection.Find(filter).ToListAsync();
 	documents.Dump();
}
catch (Exception ex)
{
 	ex.Message.Dump();
}


// 비동기로 검색 후 데이터를 비동기로 바로 사용
var filter = new BsonDocument("x", new BsonDocument("$gte", 100));
await collection.Find(filter).ForEachAsync(async document =>
{
	await ProcessDocumentAsync(document);
});	