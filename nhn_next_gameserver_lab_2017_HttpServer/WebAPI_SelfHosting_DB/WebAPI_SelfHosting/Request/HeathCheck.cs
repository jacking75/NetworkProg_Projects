using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using System.Web.Http;

namespace WebAPI_SelfHosting.Request
{
    public class HeathCheckController : ApiController
    {
        [Route("Request/HeathCheck")]
        [HttpGet]
        public HttpResponseMessage Process()
        {
            if (Main.EnableRequestHeathCheck)
            {
                //Logger.Info("RequestHeathCheck");
                return Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.Forbidden);
            }
        }

    }
}
