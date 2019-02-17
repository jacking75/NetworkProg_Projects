using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_SelfHosting
{
    public static class Main
    {
        public static bool EnableRequestHeathCheck { get; private set; }


        public static ERROR_CODE Init()
        {
            var error = ERROR_CODE.NONE;

            UniqueSeqNumberGenerator.Init(1, 1);

            error = InitDB();
            if (error != ERROR_CODE.NONE)
            {
                Console.WriteLine(string.Format("Starting. Fail DB:{0}", error));
                return error;
            }

            EnableRequestHeathCheck = true ;
            return ERROR_CODE.NONE;
        }

        static ERROR_CODE InitDB()
        {
            var redisList = "localhost:6379"; //"17.10.60.21:6379,17.10.60.21:6380"
            var error = DB.Redis.Init(redisList);
            if (error != ERROR_CODE.NONE)
            {
                return error;
            }

            return error;
        }

       
    }
    

}
