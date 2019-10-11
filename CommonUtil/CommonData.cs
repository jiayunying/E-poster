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

        public static List<Paper> Papers;
        public static Paper CurrentPaper = new Paper();

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

        public static string pre_url = (new SystemConfig()).GetValue("http.url");


    }

    public  class JsonFilters
    {
        public int cid { get; set; }
        public int poster_result_id { get; set; }
        /// <summary>
        /// 语言
        /// </summary>
        public string language { get; set; }

        public  int limit { get; set; }

        public  int offset { get; set; }

        public  int type { get; set; }

        public  string keyword { get; set; }

    }
}
