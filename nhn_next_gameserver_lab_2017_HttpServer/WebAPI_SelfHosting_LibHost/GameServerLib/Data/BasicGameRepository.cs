using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver;

namespace GameServerLib.Data
{
    public static class BasicGameRepository
    {
        public static async Task AddGameData(string userID)
        {
            var rand = new Random();

            var gameData = new DB.DBBasicGameData()
            {
                _id = userID,
                Level = rand.Next(1, 11),
                Money = rand.Next(1000, 100000),
            };

            var collection = DB.MongoDBLib.GetGameDBUserBasicDataCollection<DB.DBBasicGameData>();
            await collection.InsertOneAsync(gameData);
        }

        public static async Task<DB.DBBasicGameData> GetGameData(string userID)
        {
            var collection = DB.MongoDBLib.GetGameDBUserBasicDataCollection<DB.DBBasicGameData>();
            var data = await collection.Find(x => x._id == userID).FirstOrDefaultAsync();
            return data;
        }
    }
       
}
