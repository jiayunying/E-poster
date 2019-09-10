using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace E_Poster
{
    /// <summary>
    /// PaperList.xaml 的交互逻辑
    /// </summary>
    public partial class PaperList : Page
    {
        DataSet paperListData = new DataSet();
        
        public PaperList()
        {
            InitializeComponent();
            //初始化paperdata数据
            DataTable dt = new DataTable();
            dt.Columns.Add("ProvinceName");
            dt.Columns.Add("DateCreated");
            dt.Columns.Add("DataTest");
            dt.Columns.Add("DateUpdated");

            DataRow dr = dt.NewRow();
            dr["ProvinceName"] = "北京";
            dr["DateCreated"] = "论文一论文一论文一论文一论文一论文一论文一论文一";
            dr["DataTest"] = "测试数据";
            dr["DateUpdated"] = "第一作者";
            dt.Rows.Add(dr.ItemArray);

            DataRow dr1 = dt.NewRow();
            dr1["ProvinceName"] = "上海";
            dr1["DateCreated"] = "论文一论文一论文一论文一论文一论文一论文一论文一";
            dr1["DataTest"] = "测试数据";
            dr1["DateUpdated"] = "第二作者";
            dt.Rows.Add(dr1.ItemArray);

            DataRow dr2 = dt.NewRow();
            dr2["ProvinceName"] = "广州";
            dr2["DateCreated"] = "论文一论文一论文一论文一论文一论文一论文一论文一";
            dr2["DataTest"] = "测试数据";
            dr2["DateUpdated"] = "第一作者";
            dt.Rows.Add(dr2.ItemArray);

            DataRow dr3 = dt.NewRow();
            dr3["ProvinceName"] = "深圳";
            dr3["DateCreated"] = "论文一论文一论文一论文一论文一论文一论文一论文一";
            dr3["DataTest"] = "测试数据";
            dr3["DateUpdated"] = "第一作者";
            dt.Rows.Add(dr3.ItemArray);

            DataRow dr4 = dt.NewRow();
            dr4["ProvinceName"] = "南京";
            dr4["DateCreated"] = "论文一论文一论文一论文一论文一论文一论文一论文一";
            dr4["DataTest"] = "测试数据";
            dr4["DateUpdated"] = "第一作者";
            dt.Rows.Add(dr4.ItemArray);

            DataRow dr5 = dt.NewRow();
            dr5["ProvinceName"] = "海口";
            dr5["DateCreated"] = "论文一论文一论文一论文一论文一论文一论文一论文一";
            dr["DataTest"] = "测试数据";
            dr5["DateUpdated"] = "第一作者";
            dt.Rows.Add(dr5.ItemArray);

            paperListData.Tables.Add(dt);
            listBox1.DataContext = paperListData;

           ImageInit();

        }

        private void ImageInit() {
            //BitmapImage bi_banner = new BitmapImage();
            //// BitmapImage.UriSource must be in a BeginInit/EndInit block.  
            //bi_banner.BeginInit();
            //bi_banner.UriSource = new Uri(@"D:\Product\e-poster电子壁报\E-Poster\bin\Debug\ydt-mettings\1103\banner.png", UriKind.RelativeOrAbsolute);
            //bi_banner.EndInit();
            //this.banner.Source = bi_banner;

            //相对路径

            Uri uri = new Uri(@"ydt-mettings\1103\logo.png", UriKind.Relative);
            this.bottom.Source = new BitmapImage(uri);

        }

        private void ListBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
