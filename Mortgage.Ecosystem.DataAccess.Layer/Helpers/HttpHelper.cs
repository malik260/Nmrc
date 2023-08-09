using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using System.IO.Compression;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace Mortgage.Ecosystem.DataAccess.Layer.Helpers
{
    // Http connection operation helper class
    public class HttpHelper
    {
        #region Is it a URL

        // Is it a URL
        // <param name="url"></param>
        // <returns></returns>
        public static bool IsUrl(string url)
        {
            url = url.ToStr().ToLower();
            if (url.StartsWith("http://") || url.StartsWith("https://"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Simulate GET

        // GET request
        // <param name="url">The URL.</param>
        // <param name="postDataStr">The post data string.</param>
        // <returns>System.String.</returns>
        public static string HttpGet(string url, int timeout = 10 * 1000)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            request.Timeout = timeout;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        #endregion

        #region Simulate POST

        // POST request
        // <param name="posturl">The posturl.</param>
        // <param name="postData">The post data.</param>
        // <returns>System.String.</returns>
        public static string HttpPost(string posturl, string postData, string contentType = "application/x-www-form-urlencoded", int timeout = 10 * 1000)
        {
            Stream? outstream = null;
            Stream? instream = null;
            StreamReader? sr = null;
            HttpWebResponse? response = null;
            HttpWebRequest? request = null;
            Encoding encoding = Encoding.GetEncoding("utf-8");
            byte[] data = encoding.GetBytes(postData);
            // prepare request...
            try
            {
                // Setting parameters
                request = WebRequest.Create(posturl) as HttpWebRequest;
                CookieContainer cookieContainer = new();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = contentType;
                request.Timeout = timeout;
                request.ContentLength = data.Length;
                outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();
                //Send the request and get the corresponding response data
                response = request.GetResponse() as HttpWebResponse;
                //Until the request.GetResponse() program starts to send a Post request to the target web page
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                //return the result web page (html) code
                string content = sr.ReadToEnd();
                string err = string.Empty;
                return content;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return string.Empty;
            }
        }

        // Simulate httpPost to submit the form
        // <param name="url">URL of POS request</param>
        // <param name="data">parameters and values in the form</param>
        // <param name="encoder">page encoding</param>
        // <returns></returns>
        public static string CreateAutoSubmitForm(string url, Dictionary<string, string> data, Encoding encoder)
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine("<html>");
            html.AppendLine("<head>");
            html.AppendFormat("<meta http-equiv=\"Content-Type\" content=\"text/html; charset={0}\" />", encoder.BodyName);
            html.AppendLine("</head>");
            html.AppendLine("<body onload=\"OnLoadSubmit();\">");
            html.AppendFormat("<form id=\"pay_form\" action=\"{0}\" method=\"post\">", url);
            foreach (KeyValuePair<string, string> kvp in data)
            {
                html.AppendFormat("<input type=\"hidden\" name=\"{0}\" id=\"{0}\" value=\"{1}\" />", kvp.Key, kvp.Value);
            }
            html.AppendLine("</form>");
            html.AppendLine("<script type=\"text/javascript\">");
            html.AppendLine("<!--");
            html.AppendLine("function OnLoadSubmit()");
            html.AppendLine("{");
            html.AppendLine("document.getElementById(\"pay_form\").submit();");
            html.AppendLine("}");
            html.AppendLine("//-->");
            html.AppendLine("</script>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");
            return html.ToString();
        }

        #endregion

        #region Predefined methods or changes

        // Default encoding
        private Encoding encoding = Encoding.Default;

        // The HttpWebRequest object is used to initiate a request
        private HttpWebRequest request = null;

        // Get the data object that affects the stream
        private HttpWebResponse response = null;

        // According to the incoming data, get the corresponding page data
        // <param name="strPostdata">Incoming data Post method, get method can pass NULL or empty string</param>
        // <returns>string type response data</returns>
        private HttpResult GetHttpRequestData(HttpItem httpItem)
        {
            //return parameter
            HttpResult result = new HttpResult();
            try
            {
                #region Get the requested response
                using (response = (HttpWebResponse)request.GetResponse())
                {
                    result.Header = response.Headers;
                    if (response.Cookies != null)
                    {
                        result.CookieCollection = response.Cookies;
                    }
                    if (response.Headers["set-cookie"] != null)
                    {
                        result.Cookie = response.Headers["set-cookie"];
                    }

                    MemoryStream _stream = new MemoryStream();
                    //GZIP processing
                    if (response.ContentEncoding != null && response.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                    {
                        //Start reading the stream and set the encoding method
                        //new GZipStream(response.GetResponseStream(), CompressionMode.Decompress).CopyTo(_stream, 10240);
                        //.net4.0 the following writing
                        _stream = GetMemoryStream(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress));
                    }
                    else
                    {
                        //Start reading the stream and set the encoding method
                        //response.GetResponseStream().CopyTo(_stream, 10240);
                        //.net4.0 the following writing
                        _stream = GetMemoryStream(response.GetResponseStream());
                    }
                    //get Byte
                    byte[] RawResponse = _stream.ToArray();
                    //Whether to return Byte type data
                    if (httpItem.ResultType == ResultType.Byte)
                    {
                        result.ResultByte = RawResponse;
                    }
                    //From here we have to ignore encoding
                    if (encoding == null)
                    {
                        string temp = Encoding.Default.GetString(RawResponse, 0, RawResponse.Length);
                        //<meta(.*?)charset([\s]?)=[^>](.*?)>
                        Match meta = Regex.Match(temp, "<meta([^<]*)charset=([^<]*)[\"']", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string charter = (meta.Groups.Count > 2) ? meta.Groups[2].Value : string.Empty;
                        charter = charter.Replace("\"", string.Empty).Replace("'", string.Empty).Replace(";", string.Empty);
                        if (charter.Length > 0)
                        {
                            charter = charter.ToLower().Replace("iso-8859-1", "gbk");
                            encoding = Encoding.GetEncoding(charter);
                        }
                        else
                        {
                            if (response.CharacterSet != null && response.CharacterSet.ToLower().Trim() == "iso-8859-1")
                            {
                                encoding = Encoding.GetEncoding("gbk");
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(response.CharacterSet.Trim()))
                                {
                                    encoding = Encoding.UTF8;
                                }
                                else
                                {
                                    encoding = Encoding.GetEncoding(response.CharacterSet);
                                }
                            }
                        }
                    }
                    // get the returned HTML
                    result.Html = encoding.GetString(RawResponse);
                    //Finally release the stream
                    _stream.Close();
                }
                #endregion
            }
            catch (WebException ex)
            {
                //here is the error message returned when an exception occurs
                result.Html = "String Error";
                response = (HttpWebResponse)ex.Response;
            }
            if (httpItem.IsToLower)
            {
                result.Html = result.Html.ToLower();
            }
            return result;
        }

        // The .net version below 4.0 takes data and uses it
        // <param name="streamResponse">stream</param>
        private static MemoryStream GetMemoryStream(Stream streamResponse)
        {
            MemoryStream _stream = new MemoryStream();
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = streamResponse.Read(buffer, 0, Length);
            // write the required bytes  
            while (bytesRead > 0)
            {
                _stream.Write(buffer, 0, bytesRead);
                bytesRead = streamResponse.Read(buffer, 0, Length);
            }
            return _stream;
        }

        // Prepare parameters for the request
        //<param name="httpItem">Parameter list</param>
        // <param name="_Encoding">Encoding method when reading data</param>
        private void SetRequest(HttpItem httpItem)
        {
            // Verify the certificate
            SetCer(httpItem);
            // Set proxy
            SetProxy(httpItem);
            //Request method Get or Post
            request.Method = httpItem.Method;
            request.Timeout = httpItem.Timeout;
            request.ReadWriteTimeout = httpItem.ReadWriteTimeout;
            //Accept
            request.Accept = httpItem.Accept;
            //ContentType return type
            request.ContentType = httpItem.ContentType;
            // Access type of UserAgent client, including browser version and operating system information
            request.UserAgent = httpItem.UserAgent;
            // Encode
            SetEncoding(httpItem);
            // Set cookies
            SetCookie(httpItem);
            // Source address
            request.Referer = httpItem.Referer;
            // Whether to execute the jump function
            request.AllowAutoRedirect = httpItem.Allowautoredirect;
            // Set Post data
            SetPostData(httpItem);
            // Set max connection
            if (httpItem.Connectionlimit > 0)
            {
                request.ServicePoint.ConnectionLimit = httpItem.Connectionlimit;
            }
        }

        // Set the certificate
        // <param name="httpItem"></param>
        private void SetCer(HttpItem httpItem)
        {
            if (!string.IsNullOrEmpty(httpItem.CerPath))
            {
                //This sentence must be written before the connection is created. Use the callback method for certificate verification.
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                //Initialize the object and set the requested URL
                request = (HttpWebRequest)WebRequest.Create(GetUrl(httpItem.URL));
                //create certificate file
                X509Certificate objx509 = new X509Certificate(httpItem.CerPath);
                // add to the request
                request.ClientCertificates.Add(objx509);
            }
            else
            {
                //Initialize the object and set the requested URL
                request = (HttpWebRequest)WebRequest.Create(GetUrl(httpItem.URL));
            }
        }

        // Set the encoding
        // <param name="httpItem">Http parameter</param>
        private void SetEncoding(HttpItem httpItem)
        {
            if (string.IsNullOrEmpty(httpItem.Encoding) || httpItem.Encoding.ToLower().Trim() == "null")
            {
                //Encoding method when reading data
                encoding = null;
            }
            else
            {
                //Encoding method when reading data
                encoding = System.Text.Encoding.GetEncoding(httpItem.Encoding);
            }
        }

        // Set cookies
        // <param name="httpItem">Http parameter</param>
        private void SetCookie(HttpItem httpItem)
        {
            if (!string.IsNullOrEmpty(httpItem.Cookie))
            {
                //Cookie
                request.Headers[HttpRequestHeader.Cookie] = httpItem.Cookie;
            }
            // set cookies
            if (httpItem.CookieCollection != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(httpItem.CookieCollection);
            }
        }

        // Set Post data
        // <param name="httpItem">Http parameter</param>
        private void SetPostData(HttpItem httpItem)
        {
            // Verify that there is incoming data when getting the result
            if (request.Method.Trim().ToLower().Contains("post"))
            {
                //Write Byte type
                if (httpItem.PostDataType == PostDataType.Byte)
                {
                    // Verify that there is incoming data when getting the result
                    if (httpItem.PostdataByte != null && httpItem.PostdataByte.Length > 0)
                    {
                        request.ContentLength = httpItem.PostdataByte.Length;
                        request.GetRequestStream().Write(httpItem.PostdataByte, 0, httpItem.PostdataByte.Length);
                    }
                }//Write to file
                else if (httpItem.PostDataType == PostDataType.FilePath)
                {
                    StreamReader r = new StreamReader(httpItem.Postdata, encoding);
                    byte[] buffer = Encoding.Default.GetBytes(r.ReadToEnd());
                    r.Close();
                    request.ContentLength = buffer.Length;
                    request.GetRequestStream().Write(buffer, 0, buffer.Length);
                }
                else
                {
                    // Verify that there is incoming data when getting the result
                    if (!string.IsNullOrEmpty(httpItem.Postdata))
                    {
                        byte[] buffer = Encoding.Default.GetBytes(httpItem.Postdata);
                        request.ContentLength = buffer.Length;
                        request.GetRequestStream().Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }

        // Set the proxy
        // <param name="httpItem">parameter object</param>
        private void SetProxy(HttpItem httpItem)
        {
            if (string.IsNullOrEmpty(httpItem.ProxyUserName) && string.IsNullOrEmpty(httpItem.ProxyPwd) && string.IsNullOrEmpty(httpItem.ProxyIp))
            {
                // no need to set
            }
            else
            {
                //set proxy server
                WebProxy myProxy = new WebProxy(httpItem.ProxyIp, false);
                // suggest connection
                myProxy.Credentials = new NetworkCredential(httpItem.ProxyUserName, httpItem.ProxyPwd);
                //Give the current request object
                request.Proxy = myProxy;
                //set security credentials
                request.Credentials = CredentialCache.DefaultNetworkCredentials;
            }
        }

        // Callback to verify the certificate problem
        // <param name="sender">Stream object</param>
        // <param name="certificate">Certificate</param>
        // <param name="chain">X509Chain</param>
        // <param name="errors">SslPolicyErrors</param>
        // <returns>bool</returns>
        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            // always accept
            return true;
        }
        #endregion

        #region common type

        // Pass in a correct or incorrect URI and return the correct URL
        // <param name="URL">url</param>
        // <returns>
        // </returns>
        public static string GetUrl(string URL)
        {
            if (!(URL.Contains("http://") || URL.Contains("https://")))
            {
                URL = "http://" + URL;
            }
            return URL;
        }

        // Use the https protocol to access the network, and get the response data string according to the incoming URI address.
        //</summary>
        //<param name="httpItem">Parameter list</param>
        //<returns>String type data</returns>
        public HttpResult GetHtml(HttpItem httpItem)
        {
            // prepare parameters
            SetRequest(httpItem);
            //call the class that reads data
            return GetHttpRequestData(httpItem);
        }

        #endregion
    }

    // Http request reference class
    public class HttpItem
    {
        string _URL;

        // The request URL must be filled in
        public string URL
        {
            get { return _URL; }
            set { _URL = value; }
        }

        string _Method = "GET";

        // The request method defaults to the GET method
        public string Method
        {
            get { return _Method; }
            set { _Method = value; }
        }

        int _Timeout = 100000;

        // Default request timeout
        public int Timeout
        {
            get { return _Timeout; }
            set { _Timeout = value; }
        }

        int _ReadWriteTimeout = 30000;

        // Default write Post data timeout
        public int ReadWriteTimeout
        {
            get { return _ReadWriteTimeout; }
            set { _ReadWriteTimeout = value; }
        }

        string _Accept = "text/html, application/xhtml+xml, */*";

        // The request header value defaults to text/html, application/xhtml+xml, */*
        public string Accept
        {
            get { return _Accept; }
            set { _Accept = value; }
        }
        string _ContentType = "text/html";

        // The request return type default text/html
        public string ContentType
        {
            get { return _ContentType; }
            set { _ContentType = value; }
        }

        string _UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";

        // Client access information defaults to Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)
        public string UserAgent
        {
            get { return _UserAgent; }
            set { _UserAgent = value; }
        }

        string _Encoding = string.Empty;

        // The return data encoding defaults to NULL, which can be automatically recognized
        public string Encoding
        {
            get { return _Encoding; }
            set { _Encoding = value; }
        }
        private PostDataType _PostDataType = PostDataType.String;

        // The data type of Post
        public PostDataType PostDataType
        {
            get { return _PostDataType; }
            set { _PostDataType = value; }
        }
        string _Postdata;

        // String Post data to be sent when Post request
        public string Postdata
        {
            get { return _Postdata; }
            set { _Postdata = value; }
        }

        private byte[] _PostdataByte = null;

        // Post data of Byte type to be sent during Post request
        public byte[] PostdataByte
        {
            get { return _PostdataByte; }
            set { _PostdataByte = value; }
        }

        CookieCollection cookiecollection = null;

        // Cookie object collection
        public CookieCollection CookieCollection
        {
            get { return cookiecollection; }
            set { cookiecollection = value; }
        }

        string _Cookie = string.Empty;

        // Cookie on request
        public string Cookie
        {
            get { return _Cookie; }
            set { _Cookie = value; }
        }

        string _Referer = string.Empty;

        // Source address, last visited address
        public string Referer
        {
            get { return _Referer; }
            set { _Referer = value; }
        }

        string _CerPath = string.Empty;

        // Certificate absolute path
        public string CerPath
        {
            get { return _CerPath; }
            set { _CerPath = value; }
        }

        private Boolean isToLower = true;

        // Whether it is set to full text lowercase
        public Boolean IsToLower
        {
            get { return isToLower; }
            set { isToLower = value; }
        }

        private Boolean allowautoredirect = true;

        // Support jumping pages, the query result will be the page after jumping
        public Boolean Allowautoredirect
        {
            get { return allowautoredirect; }
            set { allowautoredirect = value; }
        }

        private int connectionlimit = 1024;

        // Maximum number of connections
        public int Connectionlimit
        {
            get { return connectionlimit; }
            set { connectionlimit = value; }
        }
        private string proxyusername = string.Empty;

        // Proxy server username
        public string ProxyUserName
        {
            get { return proxyusername; }
            set { proxyusername = value; }
        }

        private string proxypwd = string.Empty;

        // Proxy server password
        public string ProxyPwd
        {
            get { return proxypwd; }
            set { proxypwd = value; }
        }

        private string proxyip = string.Empty;

        // Proxy service IP
        public string ProxyIp
        {
            get { return proxyip; }
            set { proxyip = value; }
        }

        private ResultType resulttype = ResultType.String;

        // Set the return type String and Byte
        public ResultType ResultType
        {
            get { return resulttype; }
            set { resulttype = value; }
        }
    }

    // Http return parameter class
    public class HttpResult
    {
        string _Cookie = string.Empty;

        // Cookie returned by Http request
        public string Cookie
        {
            get { return _Cookie; }
            set { _Cookie = value; }
        }

        CookieCollection cookiecollection = null;

        // Cookie object collection
        public CookieCollection CookieCollection
        {
            get { return cookiecollection; }
            set { cookiecollection = value; }
        }

        private string html = string.Empty;

        // The returned String type data is only returned when ResultType.String, otherwise it is empty
        public string Html
        {
            get { return html; }
            set { html = value; }
        }

        private byte[] resultbyte = null;

        // The returned Byte array returns data only when ResultType.Byte, otherwise it is empty
        public byte[] ResultByte
        {
            get { return resultbyte; }
            set { resultbyte = value; }
        }

        private WebHeaderCollection header = new WebHeaderCollection();

        // Header object
        public WebHeaderCollection Header
        {
            get { return header; }
            set { header = value; }
        }
    }

    // Return type
    public enum ResultType
    {
        // Indicates that only a string is returned
        String,

        // Represents a return string and byte stream
        Byte,
    }

    // The data format of Post defaults to string
    public enum PostDataType
    {
        // String
        String,

        // String and byte streams
        Byte,

        // Indicates that the incoming file is a file
        FilePath,
    }
}
