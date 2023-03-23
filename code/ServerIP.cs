using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace LinkHome
{
    static class ServerIP
    {
        static Func<string>[] getIpFuncs = new Func<string>[] {
            getServerIP_Ctrip,
            getServerIP_IPCN,
            //getServerIPIfconfig,
            //getServerIPTrackip
        };


        /// <summary>
        /// 获得当前服务器ip
        /// </summary>
        /// <param name="errorCallback"></param>
        /// <returns></returns>
        public static string Get(ErrorHandler errorCallback = null)
        { 
            while (true)
            {
                foreach (var func in getIpFuncs)
                {
                    try
                    {
                        return func();
                    }
                    catch (Exception ex)
                    {
                        errorCallback?.Invoke(ex.Message);
                        Thread.Sleep(100);
                    }
                }
            }
        }

        static string getResponse(string url)
        {
            var http = WebRequest.CreateHttp(url);
            http.UserAgent = "Mozilla/5.0";
            //http.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7";
            using var response = http.GetResponse();
            using var stream = response.GetResponseStream();
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
        //static string getServerIPTrackip()
        //{
        //    var json = getResponse(@"http://www.trackip.net/ip?json");
        //    var serializer = new JavaScriptSerializer();
        //    var data = (Dictionary<string, object>)serializer.DeserializeObject(json);
        //    return (string)data["IP"];
        //}

        //static string getServerIPIfconfig()
        //{
        //    return getResponse(@"http://ifconfig.me/ip");
        //}

   

        static string getServerIP_Ctrip() {
            return getResponse(@"https://cdid.c-ctrip.com/model-poc2/h");
        }

        static string getServerIP_IPCN()
        {
            var json = getResponse(@"https://www.ip.cn/api/index?type=0");
            var serializer = new JavaScriptSerializer();
            var data = (Dictionary<string, object>)serializer.DeserializeObject(json);
            return (string)data["ip"]; 
        }
    }
}
