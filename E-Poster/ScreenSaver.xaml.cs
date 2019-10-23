using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Shapes;

namespace E_Poster
{
    /// <summary>
    /// ScreenSaver.xaml 的交互逻辑
    /// </summary>
    public partial class ScreenSaver : Window
    {
        public ScreenSaver()
        {
            InitializeComponent();
            InitList();
            CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerAsync();
        }

        ObservableCollection<BitmapImage> bmList;
        int index = 0;
        bool isRendering = false;

        public void InitList()
        {
            bmList = new ObservableCollection<BitmapImage>();
            //TODO:初始化播放列表；返回则重置播放列表
            DirectoryInfo dirInfo = new DirectoryInfo(System.Environment.CurrentDirectory + @"\ydt-mettings\ScreenSaver");
            FileInfo[] files = dirInfo.GetFiles();
            if (files.Length > 0) { 
                int length = files.Length;
                foreach (FileInfo file in files)
                {
                    if (file.Name.EndsWith(".jpg") || file.Name.EndsWith(".jpeg")|| file.Name.EndsWith(".png"))
                    {
                        BitmapImage bmImg = new BitmapImage(new Uri(System.Environment.CurrentDirectory + @"\ydt-mettings\ScreenSaver\" + file.Name));
                        bmList.Add(bmImg);
                    }
                }
            }
        }
        /// <summary>
        /// 自动播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                isRendering = true;
                System.Threading.Thread.Sleep(10000); //停1秒
            }
        }

        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (isRendering)
            {
                if (index < bmList.Count)
                {
                    this.ScrSaver.Source = bmList[index];
                    ImgAnimation();
                    index++;
                }
                else
                {
                    index = 0;
                }
                isRendering = false;
            }
        }

        /// <summary>
        /// 翻页按钮闪烁动画
        /// </summary>
        private void ImgAnimation()
        {
            Storyboard img_animation = new Storyboard();
            DoubleAnimation da_opacity = new DoubleAnimation();
            da_opacity.From = 0;
            da_opacity.To = 1;

            Storyboard.SetTarget(da_opacity, ScrSaver);

            Storyboard.SetTargetProperty(da_opacity, new PropertyPath("Opacity", new object[] { }));

            img_animation.Duration = new Duration(TimeSpan.FromSeconds(5));
            img_animation.Children.Add(da_opacity);
            img_animation.Begin();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
