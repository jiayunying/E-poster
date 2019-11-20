using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
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

                    //TODO：调接口查询论文列表
                    ServiceRequest.RefreshList();
                this.nodata.Visibility = CommonData.Papers.Count > 0 ? Visibility.Hidden : Visibility.Visible;
            }
            //设置语言切换按钮是否可见
            if (CommonData.langFl)
            {
                cn_en_btn.Visibility = Visibility.Visible;
                switch (CommonData.jsonFilters.language) {
                    case "cn":
                        btn_cn.IsChecked = true;
                        typeList.DisplayMemberPath = "t_name";

                        break;
                    case "en":
                        btn_en.IsChecked = true;
                        typeList.DisplayMemberPath = "t_en_name";

                        break;
                }
            }
            else {
                cn_en_btn.Visibility = Visibility.Collapsed; 
            }

            if (!string.IsNullOrEmpty(CommonData.jsonFilters.keyword)) {
                txt_keyword.Text = CommonData.jsonFilters.keyword;
            }

            this.typeList.SelectedValue = CommonData.jsonFilters.type;
            CommonData.isReturn = false;
            this.paperList.ItemsSource = new ObservableCollection<Paper>(CommonData.Papers);

        }



        /// <summary>
        /// 翻页按钮闪烁动画
        /// </summary>
        private void ButtomAnimation() {
            Storyboard s_left = new Storyboard();
            DoubleAnimation da_opacity = new DoubleAnimation();
            da_opacity.From = 0.4;
            da_opacity.To = 0.7;

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
            try {
                SystemConfig sc = new SystemConfig();
                string uri_banner = sc.GetValue("image.banner");
                string uri_bottom = sc.GetValue("image.bottom");


                BitmapImage bi_banner = new BitmapImage();
                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                bi_banner.BeginInit();
                bi_banner.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + uri_banner, UriKind.Absolute);
                bi_banner.EndInit();
                this.banner.Source = bi_banner;

                if (!string.IsNullOrEmpty(uri_bottom)) {
                    BitmapImage bi_bottom = new BitmapImage();
                    // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                    bi_bottom.BeginInit();
                    bi_bottom.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + uri_bottom, UriKind.Absolute);
                    bi_bottom.EndInit();
                    this.bottom.Source = bi_bottom;
                }
                //相对路径:有问题！！
                //Uri uri_re_bottom = new Uri(uri_bottom, UriKind.Relative);
                //this.bottom.Source = new BitmapImage(uri_re_bottom);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 选中某壁报信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void paperList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try {
                //1、获取当前选中的壁报信息
                if (paperList.SelectedIndex >= 0)
                {
                    CommonData.CurrentIndex = paperList.SelectedIndex;
                    CommonData.CurrentPaper = CommonData.Papers[paperList.SelectedIndex];
                }
                //TODO：根据选择项跳转详情页

                this.NavigationService.Navigate(new Uri("/PaperDetail.xaml", UriKind.Relative));
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 点击标题响应选中壁报事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try {
                DataTemplate obj = (sender as Label).ContentTemplate;

                DependencyObject currParent = VisualTreeHelper.GetParent(sender as Label);
                ListBoxItem listBoxitem = null;
                //循环取节点树中this的父节点直到取到window
                while (currParent != null && listBoxitem == null)
                {
                    listBoxitem = currParent as ListBoxItem;
                    currParent = VisualTreeHelper.GetParent(currParent);
                }
                int index = paperList.ItemContainerGenerator.IndexFromContainer(listBoxitem);
                paperList.SelectedIndex = index;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
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
            try {
                //切换字典资源
                var btn = sender as RadioButton;
                string requestedCulture = @"Resources\zh-cn.xaml";
                if (btn.Name.Equals("btn_en"))
                {
                    requestedCulture = @"Resources\en-us.xaml";
                    CommonData.jsonFilters.language = "en";
                    //TODO:切换论文类型控件的数据源
                    typeList.DisplayMemberPath = "t_en_name";

                }
                if (btn.Name.Equals("btn_cn"))
                {
                    requestedCulture = @"Resources\zh-cn.xaml";
                    CommonData.jsonFilters.language = "cn";
                    //TODO:切换论文类型控件的数据源
                    typeList.DisplayMemberPath = "t_name";
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
                CommonData.jsonFilters.offset = 1;
                CommonData.jsonFilters.keyword = null;
                ServiceRequest.RefreshList();
                this.nodata.Visibility = CommonData.Papers.Count > 0 ? Visibility.Hidden : Visibility.Visible;

                this.paperList.ItemsSource = new ObservableCollection<Paper>(CommonData.Papers);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }

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
        private void TypeList_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try {
                //监视搜索框内容变化之前使用
                //if (!txt_keyword.Text.Trim().Equals(App.Current.FindResource("txt_search").ToString()))
                //{
                //    CommonData.jsonFilters.keyword = txt_keyword.Text.Trim();
                //}
                if (CommonData.isReturn)
                {
                    e.Handled = false;

                }else { 
                    CommonData.jsonFilters.offset = 1;

                    //TODO:调接口获取论文列表并赋值给全局静态变量
                    CommonData.jsonFilters.type = CommonData.PaperTypes[typeList.SelectedIndex].t_id;
                    txt_keyword.Text = App.Current.FindResource("txt_search").ToString();

                    ServiceRequest.RefreshList();
                    this.nodata.Visibility = CommonData.Papers.Count > 0 ? Visibility.Hidden : Visibility.Visible;

                    this.paperList.ItemsSource = new ObservableCollection<Paper>(CommonData.Papers);
                    //缩回后置为_Expand=false 否则页面刷新时展开分类会有问题
                    Button_Click_1(sender, e);
                    _Expand = false;
                }

            }
            catch (Exception ex) {
                ex.ToString();
            }
        }

        /// <summary>
        /// 上一页列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Left_Click(object sender, RoutedEventArgs e)
        {
            try { 
                if (CommonData.jsonFilters.offset > 1)
                {
                    CommonData.jsonFilters.offset -= 1;
                   ServiceRequest.RefreshList();
                    this.paperList.ItemsSource = new ObservableCollection<Paper>(CommonData.Papers);
                }
                else
                {
                    MessageBox.Show(App.Current.FindResource("startPage").ToString());
                   
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }

        /// <summary>
        /// 下一页列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Right_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CommonData.Papers.Count == CommonData.PageSize)
                {
                    CommonData.jsonFilters.offset += 1;
                    ServiceRequest.RefreshList();
                    if (CommonData.Papers.Count > 0)
                    {
                        this.paperList.ItemsSource = new ObservableCollection<Paper>(CommonData.Papers);
                    }
                    else
                    {
                        //如果没有数据了则回退
                        CommonData.jsonFilters.offset -= 1;
                        ServiceRequest.RefreshList();
                        MessageBox.Show(App.Current.FindResource("endPage").ToString());

                    }
                }
                else if(CommonData.Papers.Count < CommonData.PageSize)
                {
                    MessageBox.Show(App.Current.FindResource("endPage").ToString());

                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }

        private void TxtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_keyword.Text.Trim().Equals(App.Current.FindResource("txt_search"))) {
                txt_keyword.Text = "";
            }
           
            InputPanel.ShowInputPanel();
        }

        private void TxtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            InputPanel.HideInputPanel();
        }

        private void Btn_Search_Click(object sender, RoutedEventArgs e)
        {
            try {
                //监视搜索框内容变化之前使用
                //if (!txt_keyword.Text.Trim().Equals(App.Current.FindResource("txt_search").ToString())) {
                //    CommonData.jsonFilters.keyword = txt_keyword.Text.Trim();                   
                //}
                CommonData.jsonFilters.offset = 1;
                CommonData.jsonFilters.type = -1;
                ServiceRequest.RefreshList();
                this.nodata.Visibility = CommonData.Papers.Count > 0 ? Visibility.Hidden : Visibility.Visible;

                this.paperList.ItemsSource = new ObservableCollection<Paper>(CommonData.Papers);
                if (string.IsNullOrEmpty(txt_keyword.Text.Trim())) {
                    txt_keyword.Text = App.Current.FindResource("txt_search").ToString();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void Txt_keyword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!txt_keyword.Text.Equals(App.Current.FindResource("txt_search").ToString()))
            {
                CommonData.jsonFilters.keyword = txt_keyword.Text.Trim();
                if (!string.IsNullOrEmpty(txt_keyword.Text.Trim()))
                {
                    btn_clear.Visibility = Visibility.Visible;
                }
                else
                {
                    btn_clear.Visibility = Visibility.Hidden;
                }
            }

        }

        private void Txt_clear_Click(object sender, RoutedEventArgs e)
        {
            txt_keyword.Text = "";
        }
    }
    public static class ScrollViewerBehavior
    {
        public static readonly DependencyProperty HorizontalOffsetProperty = DependencyProperty.RegisterAttached("HorizontalOffset", typeof(double), typeof(ScrollViewerBehavior), new UIPropertyMetadata(0.0, OnHorizontalOffsetChanged));
        public static void SetHorizontalOffset(FrameworkElement target, double value)
        {
            target.SetValue(HorizontalOffsetProperty, value);
        }
        public static double GetHorizontalOffset(FrameworkElement target)
        {
            return (double)target.GetValue(HorizontalOffsetProperty);
        }
        private static void OnHorizontalOffsetChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            var view = target as ScrollViewer;
            if (view != null)
            {
                view.ScrollToHorizontalOffset((double)e.NewValue+50);
                
            }
        }

        //public static readonly DependencyProperty VerticalOffsetProperty = DependencyProperty.RegisterAttached("VerticalOffset", typeof(double), typeof(ScrollViewerBehavior), new UIPropertyMetadata(0.0, OnVerticalOffsetChanged));
        //public static void SetVerticalOffset(FrameworkElement target, double value)
        //{
        //    target.SetValue(VerticalOffsetProperty, value);
        //}
        //public static double GetVerticalOffset(FrameworkElement target)
        //{
        //    return (double)target.GetValue(VerticalOffsetProperty);
        //}
        //private static void OnVerticalOffsetChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        //{
        //    var view = target as ScrollViewer;
        //    if (view != null)
        //    {
        //        view.ScrollToVerticalOffset((double)e.NewValue);
        //    }
        //}
    }



}
