using CommonUtil;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace E_Poster
{
    /// <summary>
    /// PaperDetail.xaml 的交互逻辑
    /// </summary>
    public partial class PaperDetail : Page
    {

        ObservableCollection<BitmapImage> bmList;
        int index = 0;
        bool isRendering = false;

        public PaperDetail()
        {
            InitializeComponent();
            InitList();

            CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerAsync();
        }

        public void InitList()
        {
            bmList = new ObservableCollection<BitmapImage>();
            //TODO:初始化播放列表；返回则重置播放列表
            DirectoryInfo dirInfo = new DirectoryInfo(System.Environment.CurrentDirectory + @"\ydt-mettings\1103\posters");
            FileInfo[] files = dirInfo.GetFiles();
            int length = files.Length;
            foreach (FileInfo file in files)
            {
                if (file.Name.EndsWith(".jpg") || file.Name.EndsWith(".jpeg")) {
                    BitmapImage bmImg = new BitmapImage(new Uri(System.Environment.CurrentDirectory + @"\ydt-mettings\1103\posters\" + file.Name));
                    bmList.Add(bmImg);
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
                    this.imgViewer.Source = bmList[index];
                    //this.imgViewer.Width = this.imgViewer.Source.Width;
                    //this.imgViewer.Height = this.imgViewer.Source.Height;

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
        /// 向下翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            List<Paper> list = MainWindow.paperlist;
        }
        /// <summary>
        /// 向上翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Last_Click(object sender, RoutedEventArgs e)
        {
            
        }
        /// <summary>
        /// 返回列表页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            ////TODO:调接口进行权限校验
            NavigationService.GetNavigationService(this).GoBack(); //向前转
            this.NavigationService.Navigate(new Uri("/PaperList.xaml", UriKind.Relative));
        }
    }
}
