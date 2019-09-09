using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace CommonUtil
{
    public class ServiceRequest
    {
        public static string HttpPost(string urlChar, string postDataStr)
        {
            //SystemConfig conf = new SystemConfig();
            //string url = conf.GetValue("service.request." + urlChar);
            string retString = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlChar);
            request.KeepAlive = false;
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("log_entid", "1");
            request.Headers.Add("log_phone","13231128007");
            //request.ContentLength = postDataStr.Length;
            //StreamWriter writer = new StreamWriter(request.GetRequestStream(),Encoding.ASCII); 

            byte[] postBytes = Encoding.UTF8.GetBytes(postDataStr);
            request.ContentLength = Encoding.UTF8.GetBytes(postDataStr).Length;
            Stream writer = request.GetRequestStream();
            writer.Write(postBytes, 0, postBytes.Length);

            writer.Flush();
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                string encoding = response.ContentEncoding;
                if ((encoding == "") || (encoding.Length < 1))
                {
                    encoding = "UTF-8"; //默认编码  
                }


                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                retString = reader.ReadToEnd();
                response.Close();
                reader.Close();
            }
            catch(Exception e)
            {
                retString = e.ToString();
            }
            return retString;
        }
    }
}
