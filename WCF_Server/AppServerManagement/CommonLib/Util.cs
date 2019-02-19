using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public class Util
    {
        public static string IPString(bool isIP4)
        {
            string myIPAddress = "";

            try
            {
                var ipentry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());

                foreach (var ip in ipentry.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        myIPAddress = ip.ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return myIPAddress;
        }

        public static string[] 패킷_문자열_파싱하기(string packetData)
        {
            string[] stringSeparators = new string[] { "#*#" };
            var TokenString = packetData.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

            return TokenString;
        }

        public static int TimeTickToSec(Int64 curTimeTick)
        {
            var sec = (int)(curTimeTick / TimeSpan.TicksPerSecond);
            return sec;
        }
    }
}
