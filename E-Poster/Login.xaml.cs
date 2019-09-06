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
using InputPanel;

namespace E_Poster
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
            ImageBrush b = new ImageBrush();
            b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/bg_login.jpg"));
            b.Stretch = Stretch.Fill;
            this.Background = b;
            MyInputPanel.HideInputPanel();
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            //TODO:


        }
        //会议ID输入框获得焦点时
        //TODO：输入时禁止输入非数字项
        private void MettingId_GotFocus(object sender, RoutedEventArgs e)
        {
            mettingId.Text = "";
            //System.Diagnostics.Process.Start("osk.exe");
            MyInputPanel.ShowInputPanel();
        }
        //会议ID输入框失去焦点时
        private void MettingId_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(mettingId.Text)) {
                mettingId.Text = "会议ID";
            }
            MyInputPanel.HideInputPanel();
        }
        //校验码输入框获得焦点时
        private void Checksum_GotFocus(object sender, RoutedEventArgs e)
        {
            mettingId.Text = "";
            //System.Diagnostics.Process.Start("osk.exe");
            MyInputPanel.ShowInputPanel();
        }
        //校验码输入框失去焦点时
        private void Checksum_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(mettingId.Text))
            {
                mettingId.Text = "校验码";
            }
            MyInputPanel.HideInputPanel();
        }
    }
}
