﻿using System;
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
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string retString = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlChar);
            request.KeepAlive = false;
            request.Method = "POST";
            request.ContentType = "application/json";


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

        /// <summary>
        /// 刷新论文列表
        /// </summary>
        public static void RefreshList () 
        {
            try {
                CommonData.Papers.Clear();
                string json_req = JsonConvert.SerializeObject(
                    CommonData.jsonFilters
                );
                string response = ServiceRequest.HttpPost(CommonData.pre_url + "/paperlist", json_req);

                JObject jo = (JObject)JsonConvert.DeserializeObject(response);
                //TODO：调接口查询论文列表
                if (jo["code"].ToString().Equals("0"))
                {

                    String record = jo["papers"].ToString();
                    JArray array = (JArray)JsonConvert.DeserializeObject(record);


                    foreach (JToken token in array)
                    {
                        Paper p = new Paper()
                        {
                            paper_id = (int)token["paperId"],
                            paper_title = (string)token["paperTitle"],
                            first_author = (string)token["firstUName"],
                            first_author_org = ((string)token["firstUOrg"]).Length>18? ((string)token["firstUOrg"]).Substring(0,18)+"...": (string)token["firstUOrg"],    
                            keyword = (string)token["paperKeyword"],
                            filename = (string)token["fileName"],
                            paper_title_en = (string)token["paperTitleEn"],
                            hot = (int)token["paperEposterHot"],
                        };
                        CommonData.Papers.Add(p);
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }      
    }
}
