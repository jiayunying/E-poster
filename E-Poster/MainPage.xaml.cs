using System;
using System.Collections.Generic;
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
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            ImageBrush b = new ImageBrush();
            b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/bg_login.jpg"));
            b.Stretch = Stretch.Fill;
            this.Background = b;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
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
                mainwindow.Content = new PaperList();
            }

            //mainwindow.Content = new PaperList();

            //NavigationService.GetNavigationService(this).Navigate(new Uri("../PaperList.xaml", UriKind.RelativeOrAbsolute));
            //NavigationService.GetNavigationService(this).GoForward(); 向后转
            //NavigationService.GetNavigationService(this).GoBack(); 向前转
        }
    }
}
