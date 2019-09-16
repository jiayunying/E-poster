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
using System.Windows.Threading;
using CommonUtil;
namespace E_Poster
{
    /// <summary>
    /// PaperList.xaml 的交互逻辑
    /// </summary>
    public partial class PaperList : Page
    {
        DataSet paperListData = new DataSet();

        DispatcherTimer timer;

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
            SystemConfig sc = new SystemConfig();
            string uri_banner = sc.GetValue("image.banner");
            string uri_bottom = sc.GetValue("image.bottom");

            BitmapImage bi_banner = new BitmapImage();
            // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
            bi_banner.BeginInit();
            bi_banner.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + uri_banner, UriKind.Absolute);
            bi_banner.EndInit();
            this.banner.Source = bi_banner;

            BitmapImage bi_bottom = new BitmapImage();
            // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
            bi_bottom.BeginInit();
            bi_bottom.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + uri_bottom, UriKind.Absolute);
            bi_bottom.EndInit();
            this.bottom.Source = bi_bottom;

            //相对路径:有问题！！
            //Uri uri_re_bottom = new Uri(uri_bottom, UriKind.Relative);
            //this.bottom.Source = new BitmapImage(uri_re_bottom);

        }

        private void ListBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TODO:调接口进行权限校验
            DependencyObject currParent = VisualTreeHelper.GetParent(this);
            Window mainwindow = null;
            //循环取节点树中this的父节点直到取到window
            while (currParent != null && mainwindow == null)
            {
                mainwindow = currParent as Window;
                currParent = VisualTreeHelper.GetParent(currParent);
            }
            // Change the page of the frame.
            if (mainwindow != null)
            {
                mainwindow.Content = new PaperDetail();
            }

            //mainwindow.Content = new PaperList();

            //NavigationService.GetNavigationService(this).Navigate(new Uri("../PaperList.xaml", UriKind.RelativeOrAbsolute));
            //NavigationService.GetNavigationService(this).GoForward(); 向后转
            //NavigationService.GetNavigationService(this).GoBack(); 向前转

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += timer1_Tick;
            timer.Start();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            Ellipse rec = (Ellipse)_btn.Template.FindName("ButtonEllipse", _btn);
            if (rec.Fill == System.Windows.Media.Brushes.Gray)
            {
                rec.Fill = System.Windows.Media.Brushes.LightGreen;
            }
            else
            {
                rec.Fill = System.Windows.Media.Brushes.Gray;
            }

            //Polygon pol = (Polygon)_btn.Template.FindName("ButtonPolygon", _btn);
            //if (pol.Fill.Opacity ==0.4)
            //{
            //    pol.Fill.Opacity =0.8;
            //}
            //else
            //{
            //    pol.Fill.Opacity = 0.4;
            //}
        }
    }
}
