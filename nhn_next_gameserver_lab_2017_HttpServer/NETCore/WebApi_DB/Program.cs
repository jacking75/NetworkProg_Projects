using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApi_Basic
{
    public class Program
    {
        static string baseAddress = "http://localhost:19000/"; // 내부에서만 접속하는 경우는 localhost 사용
        //string baseAddress = "http://10.73.44.51:19000/"; // 외부에서 접속할 때 서버 실행 머신의 IP를 사용
        //string baseAddress = "http://*:19000/"; // 외부에서 접속할 때 할당된 IP를 자동으로 사용

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel() // 기본에서 추가
                .UseStartup<Startup>()
                .UseUrls(baseAddress) // 기본에서 추가
                .Build();
    }
}
