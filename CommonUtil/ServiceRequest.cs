using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        public void GetListData(string url, object obj) {
            string jsonstr = JsonConvert.SerializeObject(obj);
            string response = HttpPost(url, jsonstr);

            string Typelist = "{\"code\": 0,\"msg\": \"\",\"paper_type\": [{\"t_id\": 1,\"t_name\": \"康复医学基础研究\",\"p_count\": 39}, {\"t_id\": 2,\"t_name\": \"康复医学临床研究\",\"p_count\": 78}, {\"t_id\": 3,\"t_name\": \"骨关节疼痛研究\",\"p_count\": 113}]}";

            JObject jo = (JObject)JsonConvert.DeserializeObject(response);
            //TODO：调接口查询论文类型

            String record = jo["paper_type"].ToString();
            JArray array = (JArray)JsonConvert.DeserializeObject(record);

            if (array.Count < 1) return;
        }
    }
}
