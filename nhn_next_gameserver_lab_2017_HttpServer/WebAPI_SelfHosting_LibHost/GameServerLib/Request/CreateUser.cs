using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Http;

namespace GameServerLib.Request
{
    public class CreateUserController : ApiController
    {
        [Route("Request/CreateUser")]
        [HttpPost]
        public async Task<RES_CREATE_USER> Process(REQ_CREATE_USER requestPacket)
        {
            var responseResult = new RES_CREATE_USER();

            if (string.IsNullOrEmpty(requestPacket.UserID))
            {
                return responseResult.Return(ERROR_CODE.REQ_CREATE_USER_INVALID_ID);
            }

            var findUser = await Data.UserRepository.GetUser(requestPacket.UserID);
            if (findUser != null)
            {
                return responseResult.Return(ERROR_CODE.REQ_CREATE_USER_DUPLICATE_USER_ID);
            }

            await Data.UserRepository.AddUser(requestPacket.UserID, requestPacket.PW);
            await Data.BasicGameRepository.AddGameData(requestPacket.UserID);

            responseResult.SetResult(ERROR_CODE.NONE);
            return responseResult;
        }

    }
}
