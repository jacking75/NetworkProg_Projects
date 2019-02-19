using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APILogicLib
{
    class RequestServiceAPI
    {
        /*
        public static Tuple<bool, string> RegistToLoginServer()
        {
            var serverInfo = ServerConfig.GetServerInfo();

            try
            {
                var loginServerAPI = ServerConfig.LoginServerAPIAddress() + RestAPIAddress.RequestRegistGameServer;
                var request = new REQ_LS_REGIST_GAMESERVER_DATA()
                {
                    SN = serverInfo.ServiceName,
                    WorkID = serverInfo.WorkID,
                    DataCenterID = serverInfo.DataCenterID,
                    BaseAPIUrl = string.Format("http://{0}:10301/GameService/", serverInfo.IP),
                };

                var response = NetHttpRequester.Request<REQ_LS_REGIST_GAMESERVER_DATA>(loginServerAPI, request);
                if (response.Result.IsSuccessStatusCode == false)
                {
                    FileLogger.Error("LoginServer Network Error");
                    return new Tuple<bool, string>(false, "");
                }

                var result = response.Result.Content.ReadAsStringAsync();
                var jsonResObject = Jil.JSON.Deserialize<RES_LS_REGIST_GAMESERVER_DATA>(result.Result);

                if (jsonResObject.Result != (short)ERROR_CODE.NONE)
                {
                    FileLogger.Error("LoginServer Request Error");
                    return new Tuple<bool, string>(false, "");
                }

                FileLogger.Info(string.Format("Success Regist GameServer To LoginServer. SecureKey:{0}", jsonResObject.SecureKey));

                return new Tuple<bool, string>(true, jsonResObject.SecureKey);
            }
            catch (Exception ex)
            {
                FileLogger.Exception(ex.ToString());
                return new Tuple<bool, string>(false, "");
            }
        }

        public static async Task<RES_LS_CREATE_ACCOUNT_DEV_DATA> RequsetCreateAccountDevToLoginServer(string loginServerAPI, REQ_LS_CREATE_ACCOUNT_DEV_DATA loginRequest)
        {
            var response = await NetHttpRequester.Request<REQ_LS_CREATE_ACCOUNT_DEV_DATA>(loginServerAPI, loginRequest);
            if (response.IsSuccessStatusCode == false)
            {
                return new RES_LS_CREATE_ACCOUNT_DEV_DATA { Result = (short)ERROR_CODE.HTTP_REQUEST_LOGIN_ERRROR };
            }

            var result = await response.Content.ReadAsStringAsync();
            var jsonResObject = Jil.JSON.Deserialize<RES_LS_CREATE_ACCOUNT_DEV_DATA>(result);
            return jsonResObject;
        }

        public static async Task<RES_LS_LOGIN_DEV_DATA> RequsetLoginDevToLoginServer(string loginServerAPI, REQ_LS_LOGIN_DEV_DATA loginRequest)
        {
            var response = await NetHttpRequester.Request<REQ_LS_LOGIN_DEV_DATA>(loginServerAPI, loginRequest);
            if (response.IsSuccessStatusCode == false)
            {
                return new RES_LS_LOGIN_DEV_DATA { Result = (short)ERROR_CODE.HTTP_REQUEST_LOGIN_ERRROR };
            }

            var result = await response.Content.ReadAsStringAsync();
            var jsonResObject = Jil.JSON.Deserialize<RES_LS_LOGIN_DEV_DATA>(result);
            return jsonResObject;
        }

        public static async Task<MemoryDBUserAuth> RequsetAuthCheckToLoginServer(string loginServerAPI,
                                                                                string sn,
                                                                                string userID)
        {
            var userAuthInfo = new MemoryDBUserAuth();

            var authCheckRequest = new REQ_LS_AUTH_CHECK()
            {
                SN = sn,
                UserID = userID,
            };


            var response = await NetHttpRequester.Request<REQ_LS_AUTH_CHECK>(loginServerAPI, authCheckRequest);
            if (response.IsSuccessStatusCode == false)
            {
                userAuthInfo.err = ERROR_CODE.HTTP_REQUEST_AUTH_CHECK_ERRROR;
                return userAuthInfo;
            }

            var result = await response.Content.ReadAsStringAsync();
            var jsonResObject = Jil.JSON.Deserialize<RES_LS_AUTH_CHECK>(result);

            if (jsonResObject.Result != (short)ERROR_CODE.NONE)
            {
                userAuthInfo.err = (ERROR_CODE)jsonResObject.Result;
                return userAuthInfo;
            }

            userAuthInfo.err = ERROR_CODE.NONE;
            userAuthInfo.LSeq = jsonResObject.LSeq.ToInt64();
            userAuthInfo.UID = jsonResObject.DBID;
            userAuthInfo.UserID = jsonResObject.UserID;
            userAuthInfo.Token = jsonResObject.Token;
            userAuthInfo.CV = jsonResObject.CV;
            userAuthInfo.CDV = jsonResObject.CDV;
            return userAuthInfo;
        }


        public static async Task<ERROR_CODE> RequestLockCheck(string apiUrl,
                                                            string sn,
                                                            string userID,
                                                            string authToken)
        {
            var request = new REQ_LS_REQUEST_LOCK_DATA()
            {
                SN = sn,
                UserID = userID,
                Token = authToken,
            };


            var response = await NetHttpRequester.Request<REQ_LS_REQUEST_LOCK_DATA>(apiUrl, request);
            if (response.IsSuccessStatusCode == false)
            {
                FileLogger.Error(string.Format("Netowrk Error RequestLockCheck. {0}", apiUrl));
                return ERROR_CODE.REQUEST_LOCK_NETWORK_ERROR;
            }

            var result = await response.Content.ReadAsStringAsync();
            var jsonResObject = Jil.JSON.Deserialize<RES_LS_REQUEST_LOCK_DATA>(result);

            if (jsonResObject.Result != (short)ERROR_CODE.NONE)
            {
                return (ERROR_CODE)jsonResObject.Result;
            }

            return ERROR_CODE.NONE;
        }

        public static async Task NotifyLockCheckUnLock(string apiUrl,
                                                        string sn,
                                                        string userID)
        {
            var request = new NTF_LS_REQUEST_UNLOCK_DATA()
            {
                SN = sn,
                UserID = userID,
            };


            var response = await NetHttpRequester.Request<NTF_LS_REQUEST_UNLOCK_DATA>(apiUrl, request);
            if (response.IsSuccessStatusCode == false)
            {
                FileLogger.Error(string.Format("Netowrk Error NotifyLockCheckUnLock. {0}", apiUrl));
            }
        }
        */
    }
}
