using System;

using System.Web.Http;

namespace GameServerLib.Request
{
    public class DevEchoController : ApiController
    {
        [Route("Request/DevEcho")]
        [HttpPost]
        public RES_DEV_ECHO RequestDevEcho(REQ_DEV_ECHO request)
        {
            Console.WriteLine($"RequestDevEcho: {request.ReqData}");

            return new RES_DEV_ECHO { Result = true, ResData = request.ReqData };
        }


      
               
    }
}
