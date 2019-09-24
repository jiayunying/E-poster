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

        public int hot { get; set; }
    }
}
