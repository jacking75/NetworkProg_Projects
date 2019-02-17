using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace WebApi_Basic.RequestAPI
{
    public class LoginController : Controller
    {
        [Route("Request/Login")]
        [HttpPost]
        public LoginResponse RequestLogin(LoginRequest request)
        {
            if(request.UserPW != "123qwe")
            {
                return new LoginResponse { Result = "Error" };
            }

            return new LoginResponse { Result = "Success", Sequence = System.DateTime.Now.Ticks.ToString(), UserSeq = request.UserSeq };
        }


        public class LoginRequest
        {
            public int UserSeq { get; set; }
            public string UserID { get; set; }
            public string UserPW { get; set; }
        }

        public class LoginResponse
        {
            public string Result { get; set; }
            public string Sequence { get; set; }
            public int UserSeq { get; set; }
        }
               
    }
}
