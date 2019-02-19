using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 참조에 System.Net.Http 어셈블리를 추가해야 한다.
using System.Net.Http;
using System.Net.Http.Headers;

namespace APILogicLib
{
	class HttpRequester
	{
		public static async Task<RES_T> RequestReturnObjetAsyn<REQ_T, RES_T>(string apiUrl, REQ_T requestPkt) where RES_T : IRES_DATA, new()
		{
			try
			{
				var content = CreateHttpContentFromObject<REQ_T>(requestPkt);

				HttpClient httpClient = new HttpClient();

				var response = await httpClient.PostAsync(apiUrl, content);

				if (response.IsSuccessStatusCode == false)
				{
					var returnPkt = new RES_T();
					returnPkt.SetResult(ERROR_ID.FAIL_NETWORK_HTTP_REQUEST);
					return returnPkt;
				}

				var result = await response.Content.ReadAsStringAsync();
				var responsePkt = Jil.JSON.Deserialize<RES_T>(result);
				return responsePkt;
			}
			catch (Exception ex)
			{
				Logger.Exception(string.Format("API Url:{0}. {1}", apiUrl, ex.ToString()));

				var returnPkt = new RES_T();
				returnPkt.SetResult(ERROR_ID.EXCEPTION_HTTP_REQUEST);
				return returnPkt;
			}
		}

		public static async Task<string> RequestReturnStringAsyn<REQ_T>(string apiUrl, REQ_T requestPkt)
		{
			try
			{
				var content = CreateHttpContentFromObject<REQ_T>(requestPkt);

				HttpClient httpClient = new HttpClient();

				var response = await httpClient.PostAsync(apiUrl, content);

				if (response.IsSuccessStatusCode == false)
				{
					return "";
				}

				var result = await response.Content.ReadAsStringAsync();
				return result;
			}
			catch (Exception ex)
			{
				Logger.Exception(string.Format("API Url:{0}. {1}", apiUrl, ex.ToString()));
				return "";
			}
		}

		public static async Task<bool> RequestReturnNoneAsyn<REQ_T>(string apiUrl, REQ_T requestPkt)
		{
			try
			{
				var content = CreateHttpContentFromObject<REQ_T>(requestPkt);

				HttpClient httpClient = new HttpClient() { Timeout = TimeSpan.FromMilliseconds(1000) };

				var response = await httpClient.PostAsync(apiUrl, content);

				if (response.IsSuccessStatusCode == false)
				{
					return false;
				}

				return true;
			}
			catch (Exception ex)
			{
				Logger.Exception(string.Format("API Url:{0}. {1}", apiUrl, ex.ToString()));
				return false;
			}
		}

		static HttpContent CreateHttpContentFromObject<T>(T obj)
		{
			var jsonText = Jil.JSON.Serialize(obj);
            var utf8Bytes = Encoding.UTF8.GetBytes(jsonText);
            var content = new ByteArrayContent(utf8Bytes);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }



        public static Tuple<bool, RES_T> Request<REQ_T, RES_T>(string apiUrl, REQ_T requestData)
        {
            try
            {
                var httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(apiUrl);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new System.IO.StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    var jsonText = Jil.JSON.Serialize(requestData);

                    streamWriter.Write(jsonText);
                    streamWriter.Flush();
                }

                var httpResponse = (System.Net.HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var jsonResObject = Jil.JSON.Deserialize<RES_T>(result);
                    return Tuple.Create(true, jsonResObject);
                }
            }
            catch (Exception ex)
            {
                //FileLogger.Exception(ex.ToString());
                return Tuple.Create(false, default(RES_T));
            }
        }
    }
}
