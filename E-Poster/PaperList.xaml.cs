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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using CommonUtil;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace E_Poster
{
    /// <summary>
    /// PaperList.xaml 的交互逻辑
    /// </summary>
    public partial class PaperList : Page
    {


        DispatcherTimer timer;

        public PaperList()
        {
            InitializeComponent();
            //初始化paperdata数据

            InitPaperList();
            
            ImageInit();
            TypeListInit();
            ButtomAnimation();
        }

        /// <summary>
        /// 初始化论文列表
        /// </summary>
        private void InitPaperList() {
            string str_papers = "{\"paper_list\": [{\"paper_id\": 1,\"paper_title\":\"电针对膝盖骨关节炎大鼠软骨细胞caspase-1表达的影响\",\"first_author\": \"张春萍\",\"first_author_org\": \"北京大学医学部\",\"keyword\": \"关节，疼痛\"," +
"\"filename\": \"电针对膝盖骨关节炎大鼠软骨细胞caspase-1表达的影响.jpg\",\"hot\": 34}," +
        "{\"paper_id\": 2,\"paper_title\": \"冲击波治疗关节疼痛的疗效观察\",\"first_author\": \"薛毅\",\"first_author_org\": \"北京大学医学部\",\"keyword\": \"冲击波，疼痛\",\"filename\": \"冲击波治疗关节疼痛的疗效观察.jpg\",\"hot\": 5}," +
        "{\"paper_id\": 1,\"paper_title\": \" 浅谈自闭症儿童正面干预的策略\",\"first_author\": \"刘锡\",\"first_author_org\": \"北京大学医学部\",\"keyword\": \"自闭症\",\"filename\": \"浅谈自闭症儿童正面干预的策略.jpg\",\"hot\": 2}]}";

                JObject jo = (JObject)JsonConvert.DeserializeObject(str_papers);
                //TODO：调接口查询论文类型
               
                String record = jo["paper_list"].ToString();
                JArray array = (JArray)JsonConvert.DeserializeObject(record);

                if (array.Count< 1) return;
                else
                {
                    foreach (JToken token in array)
                    {
                        Paper p = new Paper()
                        {
                            paper_id = (int)token["paper_id"],
                            paper_title = (string)token["paper_title"],
                            first_author = (string)token["first_author"],
                            first_author_org=(string)token["first_author_org"],
                            keyword=(string)token["keyword"],
                            filename=(string)token["filename"],
                            hot=(int)token["hot"]
                        };
                    MainWindow.paperlist.Add(p);
                    }
                 }
            this.listBox1.ItemsSource = MainWindow.paperlist;
         }

        /// <summary>
        /// 初始化论文类型列表
        /// </summary>
        private void TypeListInit()
            {
            //这个方法应该放到登录事件中
                string Typelist = "{\"code\": 0,\"msg\": \"\",\"paper_type\": [{\"t_id\": 1,\"t_name\": \"康复医学基础研究\",\"p_count\": 39}, {\"t_id\": 2,\"t_name\": \"康复医学临床研究\",\"p_count\": 78}, {\"t_id\": 3,\"t_name\": \"骨关节疼痛研究\",\"p_count\": 113}]}";

                JObject jo = (JObject)JsonConvert.DeserializeObject(Typelist);
                //TODO：调接口查询论文类型
                List<PaperType> typelist = new List<PaperType>();
                String record = jo["paper_type"].ToString();
                JArray array = (JArray)JsonConvert.DeserializeObject(record);

                if (array.Count < 1) return;
                else
                {
                    foreach (JToken token in array)
                    {
                        PaperType pt = new PaperType()
                        {
                            t_id = (int)token["t_id"],
                            t_name = (string)token["t_name"] + "(" + (string)token["p_count"] + "篇)",
                            p_count = (int)token["p_count"]
                        };
                        typelist.Add(pt);
                    }
                }

                this.typeList.ItemsSource = typelist;
            }

        /// <summary>
        /// 翻页按钮闪烁动画
        /// </summary>
        private void ButtomAnimation() {
            Storyboard s_left = new Storyboard();
            DoubleAnimation da_opacity = new DoubleAnimation();
            da_opacity.From = 0.3;
            da_opacity.To = 1;

            //DoubleAnimation da_width= new DoubleAnimation();
            //da_width.From = 40;
            //da_width.To = 95;

            //DoubleAnimation da_height = new DoubleAnimation();
            //da_height.From = 40;
            //da_height.To = 95;

            Storyboard.SetTarget(da_opacity, Left);
            //Storyboard.SetTarget(da_width, Left);
            //Storyboard.SetTarget(da_height, Left);
            Storyboard.SetTargetProperty(da_opacity, new PropertyPath("Opacity", new object[] { }));
            //Storyboard.SetTargetProperty(da_width, new PropertyPath("Width", new object[] { }));
            //Storyboard.SetTargetProperty(da_height, new PropertyPath("Height", new object[] { }));


            s_left.Duration = new Duration(TimeSpan.FromSeconds(2));
            s_left.Children.Add(da_opacity);
            //s_left.Children.Add(da_width);
            //s_left.Children.Add(da_height);
            s_left.AutoReverse = true;
            s_left.RepeatBehavior = RepeatBehavior.Forever;
            s_left.Begin();

            Storyboard s_right = new Storyboard();

            Storyboard.SetTarget(da_opacity, Right);
            //Storyboard.SetTarget(da_width, Right);
            //Storyboard.SetTarget(da_height, Right);
            Storyboard.SetTargetProperty(da_opacity, new PropertyPath("Opacity", new object[] { }));
            //Storyboard.SetTargetProperty(da_width, new PropertyPath("Width", new object[] { }));
            //Storyboard.SetTargetProperty(da_height, new PropertyPath("Height", new object[] { }));
            s_right.Duration = new Duration(TimeSpan.FromSeconds(2));
            s_right.Children.Add(da_opacity);
            //s_right.Children.Add(da_width);
            //s_right.Children.Add(da_height);
            s_right.AutoReverse = true;
            s_right.RepeatBehavior = RepeatBehavior.Forever;
            s_right.Begin();

            //da_opacity.Duration = new Duration(TimeSpan.FromSeconds(1.5));
            //Right.BeginAnimation(Button.OpacityProperty, da_opacity);

            //DoubleAnimation da_width = new DoubleAnimation();
            //da_width.From = Right.Width;
            //da_width.To = 30;
            //da_width.AutoReverse = true;
            //da_width.Duration = new Duration(TimeSpan.FromSeconds(3));
            //da_width.RepeatBehavior = RepeatBehavior.Forever;
            //Right.BeginAnimation(Button.WidthProperty, da_width);

        }

        /// <summary>
        /// 初始化banner图和bottom图
        /// </summary>
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
            //TODO：根据选择项跳转详情页

            //this.NavigationService.Navigate(new PaperDetail());
            this.NavigationService.Navigate(new Uri("/PaperDetail.xaml", UriKind.Relative));

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //开启定时器
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += timer1_Tick;
            timer.Start();
        }
        /// <summary>
        /// 定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e) {

        }
        /// <summary>
        /// 输入框获得焦点时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_GotFocus(object sender, RoutedEventArgs e)
        {
            txt_search.Text = "";
            //System.Diagnostics.Process.Start("osk.exe");
            InputPanel.ShowInputPanel();
        }
        /// <summary>
        /// 输入框失去焦点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txt_search.Text))
            {
                txt_search.Text = "搜索";
            }
            InputPanel.HideInputPanel();
        }

        /// <summary>
        /// 中英文切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cn_Click(object sender, RoutedEventArgs e) {
            var btn = sender as RadioButton;
            string requestedCulture= @"Resources\zh-cn.xaml";
            if (btn.Name.Equals("btn_en"))
            {
                requestedCulture = @"Resources\en-us.xaml";
            }
            if(btn.Name.Equals("btn_cn"))
            {
                requestedCulture = @"Resources\zh-cn.xaml";
            }

            List<ResourceDictionary> dictionaries = new List<ResourceDictionary>();
            foreach (ResourceDictionary dictionary in Application.Current.Resources.MergedDictionaries)
            {
                dictionaries.Add(dictionary);
            }
            
            ResourceDictionary resourceDictionary = dictionaries.FirstOrDefault(d => d.Source.OriginalString.Equals(requestedCulture));
            Application.Current.Resources.MergedDictionaries.Remove(resourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);

            //TODO:调接口获取论文列表（按中/英文排序）
        }

        #region 抽屉效果
        private bool _Expand = false;

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Expand = !_Expand;
            if (this.typeList.SelectedItem != null) { 
            this.typeList.SelectedItem.ToString();
            }
        }

        public bool Expand
        {
            get { return _Expand; }
            set
            {
                _Expand = value;

                Duration duration = new Duration(TimeSpan.FromSeconds(1));
                FillBehavior behavior = FillBehavior.HoldEnd;

                DoubleAnimation translateAnim = new DoubleAnimation();
                translateAnim.Duration = duration;
                translateAnim.FillBehavior = behavior;

                RectAnimation clipAnim = new RectAnimation();
                clipAnim.Duration = duration;
                clipAnim.FillBehavior = behavior;

                double delta = this.ActualWidth/2+shadow.ShadowDepth; //收缩的大小

                if (_Expand) // Expand
                {
                    translateAnim.From = -delta;
                    translateAnim.To = 0;

                    clipAnim.From = new Rect(delta, 0, Thumb1.ActualWidth, Thumb1.ActualHeight);
                    clipAnim.To = new Rect(0, 0, Thumb1.ActualWidth, Thumb1.ActualHeight);
                    mengban.Visibility = Visibility.Visible;
                }
                else  //Shrink
                {
                    translateAnim.From = 0;
                    translateAnim.To = -delta;

                    clipAnim.From = new Rect(0, 0, Thumb1.ActualWidth, Thumb1.ActualHeight);
                    clipAnim.To = new Rect(delta, 0, Thumb1.ActualWidth, Thumb1.ActualHeight);
                    mengban.Visibility = Visibility.Hidden;
                }

                spt1.BeginAnimation(TranslateTransform.XProperty, translateAnim);
                spc1.BeginAnimation(RectangleGeometry.RectProperty, clipAnim);

            }
        }
        #endregion

    }
}
