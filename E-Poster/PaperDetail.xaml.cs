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



        public PaperDetail()
        {
            InitializeComponent();
            this.ViewModel = new PaperDetailModelView();
            this.ViewModel.View = this;
            //暂时不用
            //InitList();
            //CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
            //BackgroundWorker bw = new BackgroundWorker();
            //bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            //bw.RunWorkerAsync();
        }
        public PaperDetailModelView ViewModel
        {
            get
            {
                return this.DataContext as PaperDetailModelView;
            }
            set
            {
                this.DataContext = value;
            }
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
            CommonData.CurrentPaper = null;
            CommonData.CurrentIndex = null;
        }
     
    }
}
