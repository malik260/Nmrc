﻿using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Newtonsoft.Json;
using System.Data;
using System.Net;
using System.Text;
using System.Xml;

namespace Mortgage.Ecosystem.DataAccess.Layer.Helpers
{
    // IP Location Tool
    public class IpLocationHelper
    {
        #region IP location query

        // Get the IP location
        // <param name="ipAddress"></param>
        // <returns></returns>
        public static string GetIpLocation(string ipAddress)
        {
            string ipLocation = string.Empty;
            try
            {
                if (!IsInnerIP(ipAddress))
                {
                    ipLocation = GetIpLocationFromTaoBao(ipAddress);
                    if (string.IsNullOrEmpty(ipLocation))
                    {
                        ipLocation = GetIpLocationFromPCOnline(ipAddress);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            return ipLocation;
        }

        private static string GetIpLocationFromTaoBao(string ipAddress)
        {
            string url = "http://ip.taobao.com/service/getIpInfo2.php";
            string postData = string.Format("ip={0}", ipAddress);
            string result = HttpHelper.HttpPost(url, postData);
            string ipLocation = string.Empty;
            //if (!string.IsNullOrEmpty(result))
            //{
            //    var json = JsonHelper.ToJObject(result);
            //    var jsonData = json["data"];
            //    if (jsonData != null)
            //    {
            //        ipLocation = jsonData["region"] + " " + jsonData["city"];
            //        ipLocation = ipLocation.Trim();
            //    }
            //}
            var json = JsonHelper.ToJObject(result);
            var jsonData = json["data"];
            if (jsonData != null)
            {
                ipLocation = jsonData["region"] + " " + jsonData["city"];
                ipLocation = ipLocation.Trim();
            }
            return ipLocation;
        }

        private static string GetIpLocationFromPCOnline(string ipAddress)
        {
            HttpResult httpResult = new HttpHelper().GetHtml(new HttpItem
            {
                URL = "http://whois.pconline.com.cn/ip.jsp?ip=" + ipAddress,
                ContentType = "text/html; charset=gb2312"
            });

            string ipLocation = string.Empty;
            if (!string.IsNullOrEmpty(httpResult.Html))
            {
                var resultArr = httpResult.Html.Split(' ');
                ipLocation = resultArr[0].Replace("Province", " ").Replace("City", "");
                ipLocation = ipLocation.Trim();
            }
            return ipLocation;
        }

        public static string? GetLocation(string ipAddress)
        {
            string? jsonString = null;
            string url = $"http://freegeoip.net/xml/{ipAddress}";
            var request = (HttpWebRequest)WebRequest.Create(url);
            var proxy = new WebProxy(url, true);
            request.Proxy = proxy;
            request.Timeout = 2000;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader responseStream = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string returnValue = responseStream.ReadToEnd();
                response.Close();
                responseStream.Close();
                XmlReader xmlReader = XmlReader.Create(new StringReader(returnValue));
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(returnValue.ToString());
                jsonString = JsonConvert.SerializeXmlNode(xmlDoc);
            }
            catch (Exception)
            {
                jsonString = null;
            }

            //if (string.IsNullOrEmpty(jsonString))
            //{
            //    WebRequest webRequest = WebRequest.Create("http://freegeoip.net/xml/" + ipAddress);
            //    WebProxy px = new WebProxy("http://freegeoip.net/xml/" + ipAddress, true);
            //    webRequest.Proxy = px;
            //    webRequest.Timeout = 2000;
            //    try
            //    {
            //        WebResponse rep = webRequest.GetResponse();
            //        XmlTextReader xtr = new XmlTextReader(rep.GetResponseStream());
            //        DataSet ds = new DataSet();
            //        ds.ReadXml(xtr);
            //        return ds.Tables[0];
            //    }
            //    catch
            //    {
            //        return null;
            //    }
            //}
            return jsonString;

            //WebRequest webRequest = WebRequest.Create("http://freegeoip.net/xml/" + ipAddress);
            //WebProxy px = new WebProxy("http://freegeoip.net/xml/" + ipAddress, true);
            //webRequest.Proxy = px;
            //webRequest.Timeout = 2000;
            //try
            //{
            //    WebResponse rep = webRequest.GetResponse();
            //    XmlTextReader xtr = new XmlTextReader(rep.GetResponseStream());
            //    DataSet ds = new DataSet();
            //    ds.ReadXml(xtr);
            //    return ds.Tables[0];
            //}
            //catch
            //{
            //    return null;
            //}
        }

        public static string GetMachineNameUsingIPAddress(string varIpAdress)
        {
            string machineName = string.Empty;
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(varIpAdress);

                machineName = hostEntry.HostName;
            }
            catch (Exception ex)
            {
                // Machine not found...    
            }
            return machineName;
        }

        #endregion

        #region Determine whether it is an external network IP

        // Whether the intranet IP
        // <param name="ipAddress"></param>
        // <returns></returns>
        public static bool IsInnerIP(string ipAddress)
        {
            bool isInnerIp = false;
            long ipNum = GetIpNum(ipAddress);
            /**
                Private IP: Class A 10.0.0.0-10.255.255.255
                            Class B 172.16.0.0-172.31.255.255
                            Class C 192.168.0.0-192.168.255.255
                Of course, the network segment 127 is the loopback address.
           **/
            long aBegin = GetIpNum("10.0.0.0");
            long aEnd = GetIpNum("10.255.255.255");
            long bBegin = GetIpNum("172.16.0.0");
            long bEnd = GetIpNum("172.31.255.255");
            long cBegin = GetIpNum("192.168.0.0");
            long cEnd = GetIpNum("192.168.255.255");
            isInnerIp = IsInner(ipNum, aBegin, aEnd) || IsInner(ipNum, bBegin, bEnd) || IsInner(ipNum, cBegin, cEnd) || ipAddress.Equals("127.0.0.1");
            return isInnerIp;
        }

        // Convert the IP address to a Long number
        // <param name="ipAddress">IP address string</param>
        // <returns></returns>
        private static long GetIpNum(string ipAddress)
        {
            string[] ip = ipAddress.Split('.');
            long a = int.Parse(ip[0]);
            long b = int.Parse(ip[1]);
            long c = int.Parse(ip[2]);
            long d = int.Parse(ip[3]);

            long ipNum = a * 256 * 256 * 256 + b * 256 * 256 + c * 256 + d;
            return ipNum;
        }

        // Intranet
        // <param name="userIp"></param>
        // <param name="begin"></param>
        // <param name="end"></param>
        // <returns></returns>
        private static bool IsInner(long userIp, long begin, long end)
        {
            return (userIp >= begin) && (userIp <= end);
        }

        #endregion
    }
}