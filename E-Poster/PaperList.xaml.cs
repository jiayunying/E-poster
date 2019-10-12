using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public PaperList()
        {
            InitializeComponent();
            //初始化paperdata数据

            
            ImageInit();
            this.typeList.ItemsSource = CommonData.PaperTypes;
            ButtomAnimation();
            if (CommonData.Papers.Count==0) {
                ServiceRequest.RefreshList();
            }
            this.paperList.ItemsSource = new ObservableCollection<Paper>(CommonData.Papers);

        }

        /// <summary>
        /// 刷新论文列表
        /// </summary>
        //public void RefreshList() {
        //    CommonData.Papers.Clear();
        //    string json_req = JsonConvert.SerializeObject(
        //        CommonData.jsonFilters
        //    );
        //    string response = ServiceRequest.HttpPost(CommonData.pre_url + "/paperlist", json_req);

        //    //            string str_papers = "{\"paper_list\": [{\"paper_id\": 1,\"paper_title\":\"电针对膝盖骨关节炎大鼠软骨细胞caspase-1表达的影响电针对膝盖骨关节炎大鼠软骨细胞caspase-1表达的影响电针对膝盖骨关节炎大鼠软骨细胞caspase-1表达的影响电针对膝盖骨关节炎大鼠软骨细胞caspase-1表达的影响\",\"first_author\": \"张春萍\",\"first_author_org\": \"北京大学医学部\",\"keyword\": \"关节，疼痛\"," +
        //    //"\"filename\": \"电针对膝盖骨关节炎大鼠软骨细胞caspase-1表达的影响.jpg\",\"hot\": 34}," +
        //    //        "{\"paper_id\": 2,\"paper_title\": \"冲击波治疗关节疼痛的疗效观察\",\"first_author\": \"薛毅\",\"first_author_org\": \"北京大学医学部北京大学医学部北京大学医学部\",\"keyword\": \"冲击波，疼痛\",\"filename\": \"冲击波治疗关节疼痛的疗效观察.jpg\",\"hot\": 5}," +
        //    //        "{\"paper_id\": 1,\"paper_title\": \" 浅谈自闭症儿童正面干预的策略\",\"first_author\": \"刘锡\",\"first_author_org\": \"北京大学医学部\",\"keyword\": \"自闭症\",\"filename\": \"浅谈自闭症儿童正面干预的策略.jpg\",\"hot\": 2}]}";

        //    JObject jo = (JObject)JsonConvert.DeserializeObject(response);
        //    //TODO：调接口查询论文列表
        //    if (jo["code"].ToString().Equals("0")) {
            
        //        String record = jo["papers"].ToString();
        //        JArray array = (JArray)JsonConvert.DeserializeObject(record);


        //            foreach (JToken token in array)
        //            {
        //                string first_author = ((JObject)((JObject)token["firstAuthor"])["author"])["uName"].ToString();
        //                string first_author_org = ((JObject)((JObject)token["firstAuthor"])["author"])["uOrg"].ToString();
        //                string filename = ((JObject)((JArray)token["files"])[0])["fileName"].ToString();
        //                Paper p = new Paper()
        //                {
        //                    paper_id = (int)token["paperId"],
        //                    paper_title = (string)token["paperTitle"],
        //                    first_author = first_author,
        //                    first_author_org = first_author_org,
        //                    keyword = token["paperKeyword"].ToString(),
        //                    filename = filename,
        //                    paper_title_en = token["paperTitleEn"].ToString(),
        //                    hot = (int)token["paperEposterHot"]

        //                };
        //            CommonData.Papers.Add(p);
        //            }
    
        //        //this.paperList.ItemsSource = null;
        //        this.paperList.ItemsSource = new ObservableCollection<Paper>(CommonData.Papers);
        //    }
        //}

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

        /// <summary>
        /// 选中某壁报信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void paperList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //1、获取当前选中的壁报信息
            if (paperList.SelectedIndex >= 0)
            {
                CommonData.CurrentIndex = paperList.SelectedIndex;
                CommonData.CurrentPaper = CommonData.Papers[paperList.SelectedIndex];
            }
                //TODO：根据选择项跳转详情页

                this.NavigationService.Navigate(new Uri("/PaperDetail.xaml", UriKind.Relative));

        }

        /// <summary>
        /// 输入框获得焦点时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_GotFocus(object sender, RoutedEventArgs e)
        {
            InputPanel.ShowInputPanel();
        }
        /// <summary>
        /// 输入框失去焦点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_LostFocus(object sender, RoutedEventArgs e)
        {
            InputPanel.HideInputPanel();
        }

        /// <summary>
        /// 中英文切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CnEn_Click(object sender, RoutedEventArgs e) {
            //切换字典资源
            var btn = sender as RadioButton;
            string requestedCulture= @"Resources\zh-cn.xaml";
            if (btn.Name.Equals("btn_en"))
            {
                requestedCulture = @"Resources\en-us.xaml";
                CommonData.jsonFilters.language = "en";
            }
            if(btn.Name.Equals("btn_cn"))
            {
                requestedCulture = @"Resources\zh-cn.xaml";
                CommonData.jsonFilters.language = "cn";

            }

            List<ResourceDictionary> dictionaries = new List<ResourceDictionary>();
            foreach (ResourceDictionary dictionary in Application.Current.Resources.MergedDictionaries)
            {
                dictionaries.Add(dictionary);
            }
            
            ResourceDictionary resourceDictionary = dictionaries.FirstOrDefault(d => d.Source.OriginalString.Equals(requestedCulture));
            Application.Current.Resources.MergedDictionaries.Remove(resourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);

            //TODO:切换论文类型控件的数据源

            //TODO:调接口获取论文列表（按中/英文排序）
            CommonData.jsonFilters.offset = 1;
            ServiceRequest.RefreshList();
            this.paperList.ItemsSource = new ObservableCollection<Paper>(CommonData.Papers);

        }

        private void APP_Closed(object sender, RoutedEventArgs e) {
            //TODO:需输入会议校验码校验正确方可关闭程序

            DependencyObject currParent = VisualTreeHelper.GetParent(this);
            Window mainwindow = null;
            //循环取节点树中this的父节点直到取到window
            while (currParent != null && mainwindow == null)
            {
                mainwindow = currParent as Window;
                currParent = VisualTreeHelper.GetParent(currParent);
            }
            // Change the page of the frame.
            mainwindow.Close();

        }
        #region 抽屉效果
        private bool _Expand = false;

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Expand = !_Expand;
        }

        public bool Expand
        {
            get { return _Expand; }
            set
            {
                _Expand = value;

                Duration duration = new Duration(TimeSpan.FromSeconds(0.5));
                FillBehavior behavior = FillBehavior.HoldEnd;

                DoubleAnimation translateAnim = new DoubleAnimation();
                translateAnim.Duration = duration;
                translateAnim.FillBehavior = behavior;

                RectAnimation clipAnim = new RectAnimation();
                clipAnim.Duration = duration;
                clipAnim.FillBehavior = behavior;

                double delta = this.ActualWidth/2; //收缩的大小  +shadow.ShadowDepth

                if (_Expand) // Expand
                {
                    translateAnim.From = -550;
                    translateAnim.To = 0;

                    clipAnim.From = new Rect(550, 0, Thumb1.ActualWidth, Thumb1.ActualHeight);
                    clipAnim.To = new Rect(0, 0, Thumb1.ActualWidth, Thumb1.ActualHeight);
                    mengban.Visibility = Visibility.Visible;
                }
                else  //Shrink
                {
                    translateAnim.From = 0;
                    translateAnim.To = -550;

                    clipAnim.From = new Rect(0, 0, Thumb1.ActualWidth, Thumb1.ActualHeight);
                    clipAnim.To = new Rect(550, 0, Thumb1.ActualWidth, Thumb1.ActualHeight);
                    mengban.Visibility = Visibility.Hidden;
                }

                spt1.BeginAnimation(TranslateTransform.XProperty, translateAnim);
                spc1.BeginAnimation(RectangleGeometry.RectProperty, clipAnim);

            }
        }
        #endregion

        /// <summary>
        /// 选择类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TypeList_GotFocus(object sender, RoutedEventArgs e)
        {
            //TODO:调接口获取论文列表并赋值给全局静态变量
            CommonData.jsonFilters.type = CommonData.PaperTypes[typeList.SelectedIndex].t_id;
            ServiceRequest.RefreshList();
            this.paperList.ItemsSource = new ObservableCollection<Paper>(CommonData.Papers);

            Button_Click_1(sender, e);
        }

        /// <summary>
        /// 上一页列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Left_Click(object sender, RoutedEventArgs e)
        {
            if (CommonData.jsonFilters.offset > 1)
            {
                CommonData.jsonFilters.offset -= 1;
                ServiceRequest.RefreshList();
                this.paperList.ItemsSource = new ObservableCollection<Paper>(CommonData.Papers);

            }
            else {
                MessageBox.Show("已经是第一页了!");
            }
            
        }

        /// <summary>
        /// 下一页列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Right_Click(object sender, RoutedEventArgs e)
        {
                CommonData.jsonFilters.offset += 1;
            ServiceRequest.RefreshList();
            this.paperList.ItemsSource = new ObservableCollection<Paper>(CommonData.Papers);

            if (CommonData.Papers.Count < 1) {
                MessageBox.Show("没有数据了!");
            }
                

        }

        private void TxtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            InputPanel.ShowInputPanel();
        }

        private void TxtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            InputPanel.HideInputPanel();
        }

        private void Btn_Search_Click(object sender, RoutedEventArgs e)
        {
            CommonData.jsonFilters.keyword = txt_keyword.Text.Trim();
            ServiceRequest.RefreshList();
            this.paperList.ItemsSource = new ObservableCollection<Paper>(CommonData.Papers);

            Button_Click_1(sender, e);
        }
    }
}
