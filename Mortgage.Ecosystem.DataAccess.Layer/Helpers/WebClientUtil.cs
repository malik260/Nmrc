using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Helpers
{
    public class WebClientUtil
    {
        public static string GetResponse(String BaseURL, String APIMethod, Headers _Headers)
        {
            Console.WriteLine("+++++++++ URL: " + $"{BaseURL}{APIMethod}");

            String response = string.Empty;
            try
            {
                var client = new WebClient();

                foreach (var i in _Headers.headers)
                    client.Headers.Add(i.header, i.value);
                response = client.DownloadString($"{BaseURL}{APIMethod}");
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }

            return response;
        }


        public static string GetResponse2(String BaseURL, String APIMethod, List<Header2> headers)
        {

            Console.WriteLine("+++++++++ URL: " + $"{BaseURL}{APIMethod}");
            Console.WriteLine(" ");
            String response = string.Empty;
            try
            {
                var client = new WebClient();

                foreach (var i in headers)
                    client.Headers.Add(i.header, i.value);
                response = client.DownloadString($"{BaseURL}{APIMethod}");
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="POST"></param>
        /// <param name="BaseURL"></param>
        /// <param name="APIMethod"></param>
        /// <param name="_Headers"></param>
        /// <returns></returns>
        public static string PostResponse(String BaseURL, String APIMethod, String body, Headers _Headers)
        {
            Console.WriteLine("+++++++++ URL: " + $"{BaseURL}{APIMethod}");

            Console.WriteLine();
            Console.WriteLine("++++++++++++++Body: " + body);
            Console.WriteLine();

            String response = string.Empty;
            try
            {
                var client = new WebClient();

                foreach (var i in _Headers.headers)
                    client.Headers.Add(i.header, i.value);

                client.Encoding = System.Text.Encoding.UTF8;
                string method = "POST";

                response = client.UploadString($"{BaseURL}{APIMethod}", method, body);
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }

            return response;
        }


        public static string PostResponse2(String BaseURL, String body, List<Header2> headers)
        {
            Console.WriteLine("+++++++++ URL: " + $"{BaseURL}");

            Console.WriteLine();
            Console.WriteLine("++++++++++++++Body: " + body);
            Console.WriteLine();

            String response = string.Empty;
            try
            {
                var client = new WebClient();

                foreach (var i in headers)
                    client.Headers.Add(i.header, i.value);

                client.Encoding = System.Text.Encoding.UTF8;
                string method = "POST";

                //response = client.UploadString($"{BaseURL}{APIMethod}", method, body);
                response = client.UploadString($"{BaseURL}", method, body);
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }

            return response;
        }



        public class Header2
        {

            /// <summary>
            /// header
            /// </summary>
            public string header { get; set; }
            /// <summary>
            /// value
            /// </summary>
            public string value { get; set; }
        }

        /// <summary>
        /// API Header Object
        /// </summary>
        public class Header
        {
            /// <summary>
            /// header
            /// </summary>
            public string header { get; set; }
            /// <summary>
            /// value
            /// </summary>
            public string value { get; set; }
        }

        /// <summary>
        /// API Headers Object 
        /// </summary>
        public class Headers
        {
            /// <summary>
            /// Header List Object
            /// </summary>
            public List<Header>? headers { get; set; }
        }

        public static string SHA512(string hash_string)
        {
            System.Security.Cryptography.SHA512Managed sha512 = new System.Security.Cryptography.SHA512Managed();
            Byte[] EncryptedSHA512 = sha512.ComputeHash(System.Text.Encoding.UTF8.GetBytes(hash_string));
            sha512.Clear();
            string hashed = BitConverter.ToString(EncryptedSHA512).Replace("-", "").ToLower();
            return hashed;
        }


        public static string CreateMD5Hash(string input)
        {
            // Use input string to calculate MD5 hash
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));

            }
            return sb.ToString();
        }
    }

}
