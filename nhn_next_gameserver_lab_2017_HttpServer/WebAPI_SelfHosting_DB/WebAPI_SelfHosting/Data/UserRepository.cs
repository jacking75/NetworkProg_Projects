using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver;

namespace WebAPI_SelfHosting.Data
{
    public static class UserRepository
    {
        public static async Task<long> AddUser(string userID, string pw)
        {
            try
            {
                var uid = UniqueSeqNumberGenerator.채번_받아오기();

                var user = new DB.DBUser()
                {
                    UID = uid,
                    _id = userID,
                    PW = pw,
                };

                var collection = DB.MongoDBLib.GetAccountDBUserCollection<DB.DBUser>();
                var option = new InsertOneOptions();
                option.BypassDocumentValidation = true;
                await collection.InsertOneAsync(user, option);

                return uid;
            }
            catch(Exception)
            {
                return 0;
            }            
        }

        public static async Task<DB.DBUser> GetUser(string userID)
        {
            var collection = DB.MongoDBLib.GetAccountDBUserCollection<DB.DBUser>();
            var data = await collection.Find(x => x._id == userID).FirstOrDefaultAsync();
            return data;
        }
    }
        
}
