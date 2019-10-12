using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonUtil
{
    public class PaperType
    {
        public PaperType()
        {
            //初始化
        }
        public int t_id { get; set; }

        public string t_name { get; set; }

        public int p_count { get; set; }
        public string t_en_name { get; set; }

    }

    public  class Paper{
        public Paper() {

        }

        public int paper_id { get; set; }

        public string paper_title { get; set; }

        public string first_author { get; set; }

        public string first_author_org { get; set; }

        public string keyword { get; set; }

        public string filename { get; set; }
        public string paper_title_en { get; set; }

        public int hot { get; set; }
    }

    public class JsonFilters
    {
        public int cid { get; set; }
        public int poster_result_id { get; set; }
        /// <summary>
        /// 语言
        /// </summary>
        public string language { get; set; }

        public int limit { get; set; }

        public int offset { get; set; }

        public int type { get; set; }

        public string keyword { get; set; }

    }
}
