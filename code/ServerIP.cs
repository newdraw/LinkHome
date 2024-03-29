﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
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
              
            var uri = new Uri(url);
            var addrs = Dns.GetHostAddresses(uri.Host);
            var addr = addrs.FirstOrDefault(i => i.AddressFamily == AddressFamily.InterNetwork);
            if (addr == null) {
                throw new Exception("本机或服务器不支持IPv4");
            }
            
            url = url.Replace(uri.Host, addr.ToString()); //确保用ipv4访问服务器，防止返回ipv6地址。
            var http = WebRequest.CreateHttp(url); 
            http.Host = uri.Host;
            //减小获取IP地址的超时时间，防止系统卡。
            http.Timeout = 10 * 1000;
            http.ContinueTimeout = 1000;
            http.ReadWriteTimeout = 10 * 1000;
            http.UserAgent = "Mozilla/5.0";
            http.Proxy = null; //防止系统代理改变IP
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
