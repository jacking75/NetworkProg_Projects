using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

using WebAPI_SelfHosting.Data;

namespace WebAPI_SelfHosting.Request
{
    public class LoginController : ApiController
    {
        [Route("Request/Login")]
        [HttpPost]
        public async Task<RES_LOGIN> Process(REQ_LOGIN requestPacket)
        {
            var responseResult = new RES_LOGIN();

            var userObject = await UserRepository.GetUser(requestPacket.UserID);
            if (userObject == null)
            {
                return responseResult.Return(ERROR_CODE.REQ_LOGIN_INVALID_USER);
            }

            if (userObject.PW != requestPacket.PW)
            {
                return responseResult.Return(ERROR_CODE.REQ_LOGIN_PW);
            }

            var authToken = System.DateTime.Now.Ticks.ToString();
            await AuthTokenRepository.Add(requestPacket.UserID, authToken);

            responseResult.SetResult(ERROR_CODE.NONE);
            responseResult.AuthToken = authToken;
            return responseResult;
        }
              
    }
}
