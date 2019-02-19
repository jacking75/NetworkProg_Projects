using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CloudStructures.Redis;

namespace MainLib
{
    public class RedisChecker
    {
        List<RedisSettings> redisList = new List<RedisSettings>();
        int 현재_조사_인덱스 = -1;

        public void Init(List<Tuple<string, int>> addressList)
        {
            foreach (var address in addressList)
            {
                var setting = new RedisSettings(address.Item1, port: address.Item2, ioTimeout:3);
                redisList.Add(setting);
            }
        }

        public Tuple<string, string, string> Check()
        {
            ++현재_조사_인덱스;

            if (현재_조사_인덱스 >= redisList.Count())
            {
                현재_조사_인덱스 = 0;
            }

            Int64 실행시간 = 0;
            var redisIndex = 현재_조사_인덱스;

            try
            {
                var redisConnect = redisList[redisIndex].GetConnection(false);
                if (redisConnect.State != BookSleeve.RedisConnectionBase.ConnectionState.Open)
                {
                    return new Tuple<string, string, string>(redisList[redisIndex].Host, redisList[redisIndex].Port.ToString(), "접속하지 않았음");
                }

                var startTime = DateTime.Now;

                var redis = new CloudStructures.Redis.RedisString<string>(redisList[redisIndex], "check_redis");
                redis.TryGet().Wait();

                실행시간 = (DateTime.Now - startTime).Milliseconds;
            }
            catch (TimeoutException ex)
            {
                return new Tuple<string, string, string>(redisList[redisIndex].Host, redisList[redisIndex].Port.ToString(), ex.ToString());
            }
            catch(Exception ex)
            {
                return new Tuple<string, string, string>(redisList[redisIndex].Host, redisList[redisIndex].Port.ToString(), ex.ToString());
            }
            
            return new Tuple<string, string, string>(redisList[redisIndex].Host, redisList[redisIndex].Port.ToString(), string.Format("{0} MillSec", 실행시간));
        }

        public void Connect(int redisOfIndex)
        {
            if (redisOfIndex < 0 || redisOfIndex >= redisList.Count())
            {
                return;
            }


            var redisConnect = redisList[redisOfIndex].GetConnection();
            
            try
            {
                redisConnect.Open();

                CommonLib.DevLog.Write(string.Format("Success Req Redis Connect. IP:{0}, Port:{1}", redisConnect.Host, redisConnect.Port), CommonLib.LOG_LEVEL.INFO);
            }
            catch
            {
                CommonLib.DevLog.Write(string.Format("Fail Req Redis Connect. IP:{0}, Port:{1}", redisConnect.Host, redisConnect.Port), CommonLib.LOG_LEVEL.ERROR);
            }
        }


    }
}
