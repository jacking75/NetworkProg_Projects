<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
</Query>

var serverAddress = "172.20.60.17:10301";
var userID = "jacking";
var pw = "123qwe";

var request = new REQ_LOGIN_DATA()
{
  ID = userID,
  PW = pw,
  ClientVer = 0,
  DataVer = 339,
  MarketType = (short)MARKET_TYPE.ANDROID
};

var result = MyExtensions.RequestHttpAESEncry<REQ_LOGIN_DATA, RES_LOGIN_DATA>(
										serverAddress,
										"RequestLogin",
                                      	MyExtensions.AesLoginDynamicKey,
                                      	request.ID, request);

if(result.Result.Result == MyExtensions.NO_ERROR)
{
	Console.WriteLine("성공 : {0}", result.Result.Dump());
	MyExtensions.SaveAuthToke(request.ID, result.Result.AT);
	MyExtensions.SaveLoginSeq(request.ID, result.Result.LSeq);
}
else
{
	Console.WriteLine("에러 : {0}", result.Result.Result);
}


// < 게임 기본 데이터 로딩 >
var userID = "jacking";
var serverAddress = "172.20.60.17:10301";

var request = new REQ_USER_DATA();
request.LSeq = MyExtensions.GetLoginSeq(userID);
request.ID = userID;
request.AT = MyExtensions.GetAuthToke(userID);

var result = MyExtensions.RequestHttpAESEncry<REQ_USER_DATA, RES_USER_DATA>(
										serverAddress,
										"RequestLoadUserData",
                                      	request.LSeq,
                                      	request.ID, 
										request);

result.Result.Dump();



// 튜토리얼 진행 요청
var userID = "dummy3";
var serverAddress = "172.20.60.17:10301";

var request = new REQ_TUTORIAL_STEP();
request.LSeq = MyExtensions.GetLoginSeq(userID);
request.ID = userID;
request.AT = MyExtensions.GetAuthToke(userID);

var result = MyExtensions.RequestHttpAESEncry<REQ_TUTORIAL_STEP, RES_TUTORIAL_STEP>(
										serverAddress,
										"RequestTutorialStep",
                                      	request.LSeq,
                                      	request.ID, 
										request);
result.Result.Dump();



// 영업 준비 요청
var userID = "jin111";
var serverAddress = "172.20.60.221:10301";

var request = new REQ_PREPARE_BUSINESS_SIMULATION();
request.LSeq = MyExtensions.GetLoginSeq(userID);
request.ID = userID;
request.AT = MyExtensions.GetAuthToke(userID);

var result = MyExtensions.RequestHttpAESEncry<REQ_PREPARE_BUSINESS_SIMULATION, RES_PREPARE_BUSINESS_SIMULATION>(
										serverAddress,
										"RequestPrepareBusinessSimulation ",
                                      	request.LSeq,
                                      	request.ID, 
										request);
result.Result.Dump();


// 영업 요청
var userID = "jin111";
var serverAddress = "172.20.60.221:10301";

var request = new REQ_BUSINESS_SIMULATION();
request.LSeq = MyExtensions.GetLoginSeq(userID);
request.ID = userID;
request.AT = MyExtensions.GetAuthToke(userID);

var result = MyExtensions.RequestHttpAESEncry<REQ_BUSINESS_SIMULATION, RES_BUSINESS_SIMULATION>(
										serverAddress,
										"RequestBusinessSimulation ",
                                      	request.LSeq,
                                      	request.ID, 
										request);
result.Result.Dump();


// 영업 결과 요청
var userID = "jin111";
var serverAddress = "172.20.60.221:10301";

var request = new REQ_BUSINESS_RESULT();
request.LSeq = MyExtensions.GetLoginSeq(userID);
request.ID = userID;
request.AT = MyExtensions.GetAuthToke(userID);

var result = MyExtensions.RequestHttpAESEncry<REQ_BUSINESS_RESULT, RES_BUSINESS_RESULT>(
										serverAddress,
										"RequestBusinessResult ",
                                      	request.LSeq,
                                      	request.ID, 
										request);
result.Result.Dump();


// 개발 - 캐시 구입
var userID = "31yh215";
var serverAddress = "172.20.60.17:10301";

var request = new REQ_DEV_BUY_DIAMOND();
request.LSeq = MyExtensions.GetLoginSeq(userID);
request.ID = userID;
request.AT = MyExtensions.GetAuthToke(userID);

var result = MyExtensions.RequestHttpAESEncry<REQ_DEV_BUY_DIAMOND, RES_DEV_BUY_DIAMOND>(
										serverAddress,
										"RequestDevBuyDiamond",
                                      	request.LSeq,
                                      	request.ID, 
										request);

result.Result.Dump();


// VIP 정보 로딩
var userID = "31yh215";
var serverAddress = "172.20.60.17:10301";

var request = new REQ_LOAD_USER_VIP();
request.LSeq = MyExtensions.GetLoginSeq(userID);
request.ID = userID;
request.AT = MyExtensions.GetAuthToke(userID);

var result = MyExtensions.RequestHttpAESEncry<REQ_LOAD_USER_VIP, RES_LOAD_USER_VIP>(
										serverAddress,
										"RequestLoadUserVIPInfo ",
                                      	request.LSeq,
                                      	request.ID, 
										request);
result.Result.Dump();


//<<<<<< BM - 상점
// < 데이터 로딩 >
var userID = "jacking";
var serverAddress = "172.20.60.17:10301";

var request = new REQ_BM_LOAD_BOUGHT_LIST();
request.LSeq = MyExtensions.GetLoginSeq(userID);
request.ID = userID;
request.AT = MyExtensions.GetAuthToke(userID);

var result = MyExtensions.RequestHttpAESEncry<REQ_BM_LOAD_BOUGHT_LIST, RES_BM_LOAD_BOUGHT_LIST>(
										serverAddress,
										"RequestBMLoadBoughtList",
                                      	request.LSeq,
                                      	request.ID, 
										request);

result.Result.Dump();


// < 상점 구매 >
var userID = "jacking";
var serverAddress = "172.20.60.17:10301";

var request = new REQ_BM_BUY_DIAMOND();
request.LSeq = MyExtensions.GetLoginSeq(userID);
request.ID = userID;
request.AT = MyExtensions.GetAuthToke(userID);
request.SID = 10101;

var result = MyExtensions.RequestHttpAESEncry<REQ_BM_BUY_DIAMOND, RES_BM_BUY_DIAMOND>(
										serverAddress,
										"RequestBMBuyDiamond",
                                      	request.LSeq,
                                      	request.ID, 
										request);

result.Result.Dump();
//>>>>> BM - 상점