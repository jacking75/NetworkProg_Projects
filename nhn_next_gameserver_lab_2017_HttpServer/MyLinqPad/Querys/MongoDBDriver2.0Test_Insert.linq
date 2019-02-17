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

try
{
	var newData = new DBBasic()
	{
		_id = "jacking3",
		Level = 1,
		Exp = 0,
		Money = 1000,
		Costume = new List<int>(Enumerable.Repeat(0, 12)),
	};
	
	var collection = MyExtensions.GetMongoDBCollectionVer2<DBBasic>("mongodb://172.20.60.221", "TestDB", "Basic");
	await collection.InsertOneAsync(newData);
}
catch(Exception ex)
{
	ex.Message.Dump();
}


// 복수의 데이터를 넣기
try
{
	var newItemList = new List<DBUserItem>();
	
	var itemIDList = new List<int>() { 1001, 1002, 1003 };
	int i = 0;
 	foreach (var itemID in itemIDList)
 	{
		++i;
		
    	var newData = new DBUserItem()
     	{
        	_id = DateTime.Now.Ticks + i,
        	UserID = "jacking",
        	ItemID = itemID,
        	AcquireDateTime = DateTime.Now,
     	};

     	newItemList.Add(newData);
 	}

 	var collection = MyExtensions.GetMongoDBCollectionVer2<DBUserItem>("mongodb://172.20.60.221", "TestDB", "Item");
 	await collection.InsertManyAsync(newItemList);
}
catch (Exception ex)
{
	ex.Message.Dump();
}



// 동적 기능 사용하기
try
{
	dynamic person = new System.Dynamic.ExpandoObject();
	person.FirstName = "Jane";
	person.Age = 12;
	person.PetNames = new List<dynamic> { "Sherlock", "Watson" };
	
	var collection = MyExtensions.GetMongoDBCollectionVer2<dynamic>("mongodb://172.20.60.221", "TestDB", "Persion");
	await collection.InsertOneAsync(person);
}
catch (Exception ex)
{
	ex.Message.Dump();
}



try
{
	var newData = new DBTimeData()
	{
		CurTime = DateTime.Now.AddHours(9)
	};
	
	var collection = MyExtensions.GetMongoDBCollectionVer2<DBTimeData>("mongodb://172.20.60.221", "TestDB", "TimeData");
	await collection.InsertOneAsync(newData);
}
catch(Exception ex)
{
	ex.Message.Dump();
}