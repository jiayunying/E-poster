using CommonUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
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

namespace E_Poster
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.MainFrame.Content = new Login();
            ImageBrush b = new ImageBrush();
            b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/bg_login.png"));
            b.Stretch = Stretch.Fill;
            this.Background = b;
            //初始化屏保数据
            mLastInputInfo = new LASTINPUTINFO();
            mLastInputInfo.cbSize = Marshal.SizeOf(mLastInputInfo);

            mIdleTimer = new System.Windows.Threading.DispatcherTimer();
            mIdleTimer.Tick += new EventHandler(IdleTime);//起个Timer一直获取当前时间 
            mIdleTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            mIdleTimer.Start();
        }
        #region 屏保
        [StructLayout(LayoutKind.Sequential)]
        struct LASTINPUTINFO
        {
            // 设置结构体块容量
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;
            // 捕获的时间
            [MarshalAs(UnmanagedType.U4)]
            public uint dwTime;
        }

        private DispatcherTimer mIdleTimer;
        private LASTINPUTINFO mLastInputInfo;

        [DllImport("user32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        private static string str_scrSaveTime = (new SystemConfig()).GetValue("screensaver.time");

        //屏保效果
        private void IdleTime(object sender, EventArgs e)
        {

            if (!GetLastInputInfo(ref mLastInputInfo))
                MessageBox.Show("GetLastInputInfo Failed!");
            else
            {
                //判断配置文件中的时间是正整数
                if (Regex.IsMatch(str_scrSaveTime, @"^[1-9]\d*|0$"))
                {
                    if ((Environment.TickCount - (long)mLastInputInfo.dwTime) / 1000 >10 )//int.Parse(str_scrSaveTime)*60
                    {
                        ScreenSaver screenSaver = new ScreenSaver();
                        screenSaver.ShowDialog();
                    }
                }

                
            }

        }
        #endregion

    }
}
