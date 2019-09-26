using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CommonUtil
{
    public static class CommonData
    {
        public static List<Paper> PaperList=new List<Paper>() { };

        public static Paper CurrentPaper=new Paper();

        public static List<PaperType> PaperTypes = new List<PaperType>
        {
           new PaperType(){
               t_id=-1,
               t_name="全部",
               p_count=0
           }
        };

        public static object JsonFilters = new
        {
            language = "",
            page = 1,
            p_type = 2,
            keyword = ""
        };       
        //public static string JsonFilters = JsonConvert.SerializeObject(new
        //{
        //    language = "",
        //    page=1,
        //    p_type=2,
        //    keyword=""
        //});

    }
}
