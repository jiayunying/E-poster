using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CommonUtil
{
    public static class CommonData
    {

        public static List<Paper> Papers=new List<Paper>();

        private static int? currentIndex = null;
        public static int? CurrentIndex {
            get {
                return currentIndex;
            }
            set {
                //这是自动循环
                //    if (value == Papers.Count)
                //    {
                //        //该向下翻页了
                //        //先判断是最后一页 最后一条；
                //        //向后翻页
                //        if (Papers.Count == PageSize)
                //        {
                //            //列表页翻页,刷新列表
      
                //            jsonFilters.offset += 1;
                //            ServiceRequest.RefreshList();
                //            currentIndex = 0;
                //      //  翻页后无数据了
                //            if (CommonData.Papers.Count == 0)
                //        {
               
                //        }
                //    }
                //}
                //    else if (value >= 0 && value < Papers.Count)
                //    {
                //        currentIndex = value;
                //    }
                //    else 

                if (value == -1)
                {
                    //列表第一条基础上点击上一条
                    //向前翻页

                    //列表页翻页,刷新列表
                    jsonFilters.offset -= 1;
                    ServiceRequest.RefreshList();
                    currentIndex = Papers.Count-1;
                }
                else {
                    currentIndex = value;
                }

                //  currentIndex = value;
            }
        }
        private static Paper currentPaper;
        public static Paper CurrentPaper
        {
            get
            {
                return currentPaper;
            }
            set
            {
                if (currentPaper != value)
                {
                    currentPaper = value;
                    //current值改变之后要增加当前电子壁报的浏览量
                    if (currentPaper != null)
                    {
                        //调接口增加电子壁报浏览量
                        string json_req = JsonConvert.SerializeObject(
                          new
                          {
                              cid = CommonData.cid,
                              paperid = currentPaper.paper_id
                          }
                         );
                        string response = ServiceRequest.HttpPost(CommonData.pre_url + "/sethot", json_req);
                        JObject jo = (JObject)JsonConvert.DeserializeObject(response);
                        if (!jo["code"].ToString().Equals("0"))
                        {
                            //增加浏览量失败怎么办
                        }
                    }
                }
            }
        }

        public static Boolean langFl = true;



        public static List<PaperType> PaperTypes = new List<PaperType>
        {
           new PaperType(){
               t_id=-1,
               t_name="全部",
               p_count=0,
               t_en_name="ALL"
           }
        };

        public static JsonFilters jsonFilters = new JsonFilters
        {
            cid = -1,
            poster_result_id = -1,
            language = "cn",
            keyword = null,
            limit = 8,
            offset = 1,
            type= -1

        };


        public static int cid = -1;
        //从配置文件中读取本次会议配置的评审结果为电子壁报的结果id:case_result_config.crc_id
        public static int poster_result_id = int.Parse((new SystemConfig()).GetValue("result.eposter"));
        public static int PageSize = int.Parse((new SystemConfig()).GetValue("page.size"));

        public static string pre_url = (new SystemConfig()).GetValue("http.url");


    }

    
}
