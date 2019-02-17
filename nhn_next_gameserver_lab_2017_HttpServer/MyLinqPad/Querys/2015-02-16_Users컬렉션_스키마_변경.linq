<Query Kind="Program">
  <NuGetReference>mongocsharpdriver</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>MongoDB.Bson</Namespace>
  <Namespace>MongoDB.Driver</Namespace>
  <Namespace>MongoDB.Driver.Builders</Namespace>
</Query>

void Main()
{
	var server = MongoServer.Create("mongodb://172.20.60.221:27018");
	var db = server.GetDatabase("Account");
	var collection = db.GetCollection<User>("Users");
	
	var list = collection.FindAll();
	var oldList = list.ToList();
	
	collection.RemoveAll();
	
	
	var collection2 = db.GetCollection<User2>("Users");
	foreach(var oldUser in oldList)
	{
		var newUser = new User2()
		{
			_id = oldUser.UserID,
			PW = oldUser.PW,
			UID = oldUser._id,
		};
		
		collection2.Insert<User2>(newUser);
	}
	
	var list2 = collection2.FindAll();
	list2.Dump();
}


public class User
{
	public Int64  _id;
	public string UserID;
	public string PW;
} 

public class User2
{
	public string _id;
   public string PW;
   public Int64 UID; 
}