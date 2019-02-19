	



using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.ServiceModel.Web;
using System.Threading.Tasks;


namespace WCFGameServerLib
{
	// 참고: "리팩터링" 메뉴에서 "이름 바꾸기" 명령을 사용하여 코드 및 config 파일에서 클래스 이름 "Service1"을 변경할 수 있습니다.
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
	public class ServerService : IServerService
	{
		ServerService()
		{
			//ServerLogic.ServerInit.Setting();
		}

		
		// 
		// http://localhost:10301/GameService/RequestCreateAccountDev 
		[WebInvoke(Method = "POST",
					RequestFormat = WebMessageFormat.Json,
					ResponseFormat = WebMessageFormat.Json,
					UriTemplate = "RequestCreateAccountDev")]
		public async Task<RES_DATA> RequestCreateAccountDev(REQ_DATA request)
		{
			try
			{
				//var preCheck = ServerLogic.ClientRequestPrepare.CheckServerStatus();
				//if(preCheck != ERROR_CODE.NONE)
				//{
					//return new RES_DATA { Result = (short)preCheck };
				//}

				var jsonObject = DecryptRequestData<REQ_CREATE_ACCOUNT_DEV_DATA>(request.LSeq, request.Data);
				if (string.IsNullOrEmpty(jsonObject.ID) || string.IsNullOrEmpty(request.ID))
				{
					return new RES_DATA { Result = (short)ERROR_CODE.REQUEST_JSON_PARSE_OR_ID_NULL };
				}

				var result = await Request.LoginServer.CreateAccountDev.Process(jsonObject);
				return EncryotResponseData<RES_CREATE_ACCOUNT_DEV_DATA>(AESEncrypt.AesLoginDynamicKey, result);
			}
			catch (Exception ex)
			{
				return ResponseException<RES_DATA>(ex.ToString());
			}
		}  
		
		// 유저의 기본 게임 데이터 로딩 요청
		// http://localhost:10301/GameService/RequestLoadUserData 
		[WebInvoke(Method = "POST",
					RequestFormat = WebMessageFormat.Json,
					ResponseFormat = WebMessageFormat.Json,
					UriTemplate = "RequestLoadUserData")]
		public async Task<RES_DATA> RequestLoadUserData(REQ_DATA request)
		{
			try
			{
				//var preCheck = ServerLogic.ClientRequestPrepare.CheckServerStatus();
				//if(preCheck != ERROR_CODE.NONE)
				//{
					//return new RES_DATA { Result = (short)preCheck };
				//}

				var jsonObject = DecryptRequestData<REQ_USER_DATA>(request.LSeq, request.Data);
				if (string.IsNullOrEmpty(jsonObject.ID) || string.IsNullOrEmpty(request.ID))
				{
					return new RES_DATA { Result = (short)ERROR_CODE.REQUEST_JSON_PARSE_OR_ID_NULL };
				}

				var userID = jsonObject.ID;
				var loginSeq = jsonObject.LSeq;
				var authToken = jsonObject.AT;

				if (userID != request.ID || loginSeq != request.LSeq)
				{
					return new RES_DATA { Result = (short)ERROR_CODE.DISCORDANCE_ID_LSEQ };
				}


				var authUserInfo = await CommonServer.LocalCache.UserAuthCache.GetAuthInfo(userID);
				if(authUserInfo.err != ERROR_CODE.NONE)
				{
					return new RES_DATA { Result = (short)authUserInfo.err };
				}

				var checkResult = authUserInfo.Check(loginSeq, userID, authToken);
				if (checkResult != ERROR_CODE.NONE)
				{
					return new RES_DATA { Result = (short)checkResult };
				}

				if(ServerLogic.ClientRequestPrepare.CompareClientDataVer(authUserInfo.CDV) == false)
				{
					return new RES_DATA { Result = (short)ERROR_CODE.FAIL_CLIENT_DATA_VERSION };
				}

				{
					checkResult = await RequestLock.Lock(userID, authToken);
					if (checkResult != ERROR_CODE.NONE)
					{
						return new RES_DATA { Result = (short)checkResult };
					}

							var result = await Request.LoadUserData.Process(jsonObject, authUserInfo);
							await RequestLock.UnLock(userID);

					return EncryotResponseData<RES_USER_DATA>(request.LSeq.ToString(), result);
				} 
			}
			catch (Exception ex)
			{
				return ResponseException<RES_DATA>(ex.ToString());
			}
		}  
		
		// 월드 확장 요청
		// http://localhost:10301/GameService/RequestWorldExtension 
		[WebInvoke(Method = "POST",
					RequestFormat = WebMessageFormat.Json,
					ResponseFormat = WebMessageFormat.Json,
					UriTemplate = "RequestWorldExtension")]
		public async Task<RES_DATA> RequestWorldExtension(REQ_DATA request)
		{
			try
			{
				//var preCheck = ServerLogic.ClientRequestPrepare.CheckServerStatus();
				//if(preCheck != ERROR_CODE.NONE)
				//{
					//return new RES_DATA { Result = (short)preCheck };
				//}

				var jsonObject = DecryptRequestData<REQ_WORLD_EXTENSION>(request.LSeq, request.Data);
				if (string.IsNullOrEmpty(jsonObject.ID) || string.IsNullOrEmpty(request.ID))
				{
					return new RES_DATA { Result = (short)ERROR_CODE.REQUEST_JSON_PARSE_OR_ID_NULL };
				}

				var userID = jsonObject.ID;
				var loginSeq = jsonObject.LSeq;
				var authToken = jsonObject.AT;

				if (userID != request.ID || loginSeq != request.LSeq)
				{
					return new RES_DATA { Result = (short)ERROR_CODE.DISCORDANCE_ID_LSEQ };
				}


				var authUserInfo = await CommonServer.LocalCache.UserAuthCache.GetAuthInfo(userID);
				if(authUserInfo.err != ERROR_CODE.NONE)
				{
					return new RES_DATA { Result = (short)authUserInfo.err };
				}

				var checkResult = authUserInfo.Check(loginSeq, userID, authToken);
				if (checkResult != ERROR_CODE.NONE)
				{
					return new RES_DATA { Result = (short)checkResult };
				}

				if(ServerLogic.ClientRequestPrepare.CompareClientDataVer(authUserInfo.CDV) == false)
				{
					return new RES_DATA { Result = (short)ERROR_CODE.FAIL_CLIENT_DATA_VERSION };
				}

				{
					checkResult = await RequestLock.Lock(userID, authToken);
					if (checkResult != ERROR_CODE.NONE)
					{
						return new RES_DATA { Result = (short)checkResult };
					}

						var userGameData = await DBUserGameData.GetSmallBasicGameData(authUserInfo.UserID);
					if (userGameData.IsValid == false)
					{
						await RequestLock.UnLock(userID);
						return new RES_DATA { Result = (short)ERROR_CODE.FAIL_LOAD_SMALL_USER_BASIC_GAME_DATA };
					}

					if (userGameData.TSeq != Loader.DBTutorialDataInst.TUTORIAL_COUNT)
					{
						await RequestLock.UnLock(userID);
						return new RES_DATA { Result = (short)ERROR_CODE.TUTORIAL_DONT_COMPLETED };
					}

					if (Request.Function.IsBusinessSimulationing(userGameData.PBSTurn))
					{
						await RequestLock.UnLock(userID);
						return new RES_DATA { Result = (short)ERROR_CODE.ALREADY_BUSINESS_SIMULATIONING };
					}
			
					var result = await Request.WorldExtension.Process(jsonObject, authUserInfo, userGameData);
							await RequestLock.UnLock(userID);

					return EncryotResponseData<RES_WORLD_EXTENSION>(request.LSeq.ToString(), result);
				} 
			}
			catch (Exception ex)
			{
				return ResponseException<RES_DATA>(ex.ToString());
			}
		}  


		//[[ 관리 기능 관련 ]]
		// AWS의 ELB에서 서버가 살아 있는지 조사할 때 사용. 일종의 Heartbeat
		// http://localhost:10301/GameService/RequestHeathCheck
		[WebInvoke(Method = "GET",
					UriTemplate = "RequestHeathCheck")]
		public void RequestHeathCheck()
		{
			if (ServerLogic.ServerInit.EnableRequestHeathCheck)
			{
				FileLogger.Info("RequestHeathCheck");
			}
			else
			{
				OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
				response.StatusCode = System.Net.HttpStatusCode.Forbidden;
				response.StatusDescription = "Server Spot Check";
			}
		}
		
		
		
		REQUSET_T DecryptRequestData<REQUSET_T>(string dynamicKey, string request)
		{
			try
			{
				var decryptData = AESEncrypt.Decrypt(dynamicKey, request);
				var jsonObject = Jil.JSON.Deserialize<REQUSET_T>(decryptData);
				return jsonObject;
			}
			catch(Exception ex)
			{
				CommonServer.FileLogger.Error(ex.ToString());
				
				return default(REQUSET_T);
			}
		}

		RES_DATA EncryotResponseData<RESULT_T>(string dynamicKey, RESULT_T result)
		{
			var response = new RES_DATA { Result = (short)ERROR_CODE.NONE };

			var jsonResObject = Jil.JSON.Serialize<RESULT_T>(result);
			var encryData = AESEncrypt.Encrypt(dynamicKey, jsonResObject);

			response.Result = (short)ERROR_CODE.NONE;
			response.Data = encryData;

			return response;
		}             

		RESULT_T ResponseException<RESULT_T>(string exceptionMsg) where RESULT_T : IRES_DATA, new() 
		{
			CommonServer.FileLogger.Exception(exceptionMsg);
			
			var result = new RESULT_T();
			result.SetResult(ERROR_CODE.SERVER_ERROR);
			return result;  
		}

		string RemoteClientIP()
		{
			var context = OperationContext.Current;
			var mp = context.IncomingMessageProperties;
			var propName = System.ServiceModel.Channels.RemoteEndpointMessageProperty.Name;
			var prop = (System.ServiceModel.Channels.RemoteEndpointMessageProperty)mp[propName];
			string remoteIP = prop.Address;
			return remoteIP;
		}


		
	} 
}