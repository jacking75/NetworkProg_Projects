<Query Kind="Statements">
  <Reference>D:\SVN\BProject\trunk\MobileGameServer2\Bin\CommonServer.dll</Reference>
  <Reference>D:\SVN\BProject\trunk\MobileGameServer2\Bin\ServerLogic.dll</Reference>
  <NuGetReference>AutoMapper</NuGetReference>
  <NuGetReference>AWSSDK</NuGetReference>
  <NuGetReference>mongocsharpdriver</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>MongoDB.Bson</Namespace>
  <Namespace>MongoDB.Driver</Namespace>
  <Namespace>MongoDB.Driver.Builders</Namespace>
</Query>

//http://docs.aws.amazon.com/sdkfornet/latest/apidocs/Index.html

// 토픽에 메시지 보내기(전체 메시지)
var Endpoint = "ap-northeast-1";
var AwsAccessKey = "AK54WXWQ";
var AwsSecretKey = "paU4T4Nu";
var subject = "test343";
var logMessage= "fdfdsfsdfsd";
var TopicArn = "arn:aws:sns:ap-northeast-1:473637008560:fs_dev_topic";

try
{
	var client = new Amazon.SimpleNotificationService.AmazonSimpleNotificationServiceClient(AwsAccessKey, AwsSecretKey, Amazon.RegionEndpoint.GetBySystemName(Endpoint));
	//client.Dump();
	
	client.Publish(new Amazon.SimpleNotificationService.Model.PublishRequest()
					{
						Message = logMessage,
						Subject = subject,
						TopicArn = TopicArn
					});
}
catch(Exception ex)
{
	ex.Dump();
}



// 한 디바이스에만 보내기
var Endpoint = "ap-northeast-1";
var AwsAccessKey = "AKIAXWQ";
var AwsSecretKey = "pT4Nu";
var targetArn = "arn:aws:sns:ap-northeast-1:473637008560:endpoint/GCM/fs_dev_and/952bd2a6-0c18-3469-a2dd-8071e16fa7a9";

try
{
	var client = new Amazon.SimpleNotificationService.AmazonSimpleNotificationServiceClient(AwsAccessKey, AwsSecretKey, Amazon.RegionEndpoint.GetBySystemName(Endpoint));
	//client.Dump();
	
	var result = client.Publish(new Amazon.SimpleNotificationService.Model.PublishRequest()
					{
						Message = "434324523",
						Subject = "TEST - 123",
						TargetArn = targetArn, 
					});
	result.Dump();
}
catch(Exception ex)
{
	ex.Dump();
}


// Application에 추가하기(엔드 포인트 추출하기)
var Endpoint = "ap-northeast-1";
var AwsAccessKey = "AKIWXWQ";
var AwsSecretKey = "pBaU4T4Nu";
var deviceRegistID = "APA91bE952T7PPK4NM3tnzPeoIbUc2WaaxK0NvZU7fvGw3OcrmG4zM-NSJ41N6nRNPn0_R7X7gLUMMcyQFNPW5YoVXrr0MRCwaNfNfTNelsACcXEsnQlrghUmfQHx46qJRqDMYHsvjKaTOIYjVUK_V_ha3UYQ7fMNg";

try
{
	var client = new Amazon.SimpleNotificationService.AmazonSimpleNotificationServiceClient(AwsAccessKey, AwsSecretKey, Amazon.RegionEndpoint.GetBySystemName(Endpoint));
		
	var result = client.CreatePlatformEndpoint(new Amazon.SimpleNotificationService.Model.CreatePlatformEndpointRequest()
					{
						PlatformApplicationArn = "arn:aws:sns:ap-northeast-1:473637008560:app/GCM/fs_dev_and",
						Token = deviceRegistID,
					});
					
	result.EndpointArn.Dump();
}
catch(Exception ex)
{
	ex.Dump();
}


// 토픽에 엔드포인트 추가하기
var Endpoint = "ap-northeast-1";
var AwsAccessKey = "AKIWXWQ";
var AwsSecretKey = "pBpU4T4Nu";
var topicArn = "arn:aws:sns:ap-northeast-1:473637008560:fs_dev_topic";
var endpoint = "arn:aws:sns:ap-northeast-1:473637008560:endpoint/GCM/fs_dev_and/952bd2a6-0c18-3469-a2dd-8071e16fa7a9";

try
{
	var client = new Amazon.SimpleNotificationService.AmazonSimpleNotificationServiceClient(AwsAccessKey, AwsSecretKey, Amazon.RegionEndpoint.GetBySystemName(Endpoint));
		
	var result = client.Subscribe(topicArn, "application", endpoint);
	result.SubscriptionArn.Dump();
}
catch(Exception ex)
{
	ex.Dump();
}


// 토픽에서 엔드포인트 삭제하기
var Endpoint = "ap-northeast-1";
var AwsAccessKey = "AKIXWQ";
var AwsSecretKey = "pBp4Nu";
var subscriptionArn = "arn:aws:sns:ap-northeast-1:473637008560:fs_dev_topic:25959ad0-03af-4c16-a8ed-bb6082ec9c34"; // 토픽에 추가할 때 반환 받는다

try
{
	var client = new Amazon.SimpleNotificationService.AmazonSimpleNotificationServiceClient(AwsAccessKey, AwsSecretKey, Amazon.RegionEndpoint.GetBySystemName(Endpoint));
		
	var result = client.Unsubscribe(subscriptionArn);
	result.Dump();
}
catch(Exception ex)
{
	ex.Dump();
}


// 애플리케이션에서 엔드포인트 삭제하기(만약 토픽에 등록 되어 있다면 토픽에는 남아 있다)
var Endpoint = "ap-northeast-1";
var AwsAccessKey = "AKIAXWQ";
var AwsSecretKey = "pBpT4Nu";

try
{
	var client = new Amazon.SimpleNotificationService.AmazonSimpleNotificationServiceClient(AwsAccessKey, AwsSecretKey, Amazon.RegionEndpoint.GetBySystemName(Endpoint));
		
	var request = new Amazon.SimpleNotificationService.Model.DeleteEndpointRequest
	{
		EndpointArn = "arn:aws:sns:ap-northeast-1:473637008560:endpoint/GCM/fs_dev_and/952bd2a6-0c18-3469-a2dd-8071e16fa7a9"
	};
	
	var result = client.DeleteEndpoint(request);
	result.Dump();
}
catch(Exception ex)
{
	ex.Dump();
}


// 엔드포인트가 유효한지 조사한다
var Endpoint = "ap-northeast-1";
var AwsAccessKey = "AKXWQ";
var AwsSecretKey = "pBU4T4Nu";

try
{
	var client = new Amazon.SimpleNotificationService.AmazonSimpleNotificationServiceClient(AwsAccessKey, AwsSecretKey, Amazon.RegionEndpoint.GetBySystemName(Endpoint));
		
	var request = new Amazon.SimpleNotificationService.Model.GetEndpointAttributesRequest 
	{
		EndpointArn = "arn:aws:sns:ap-northeast-1:473637008560:endpoint/GCM/fs_dev_and/952bd2a6-0c18-3469-a2dd-8071e16fa7a9"
	};
	
	var result = client.GetEndpointAttributes(request);
	result.Dump();
}
catch(Exception ex)
{
	ex.Dump();
}