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
using CommonUtil;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            //TODO:调接口进行权限校验

            //TODO:校验失败提示失败

            //TODO：校验成功，调接口查询中英文论文分类分别保存至静态变量;

            //string response = ServiceRequest.HttpPost("调查询论文类型的接口", null);
            string response = "{\"code\": 0,\"msg\": \"\",\"paper_type\": [{\"t_id\": 1,\"t_name\": \"康复医学基础研究\",\"p_count\": 39,\"t_en_name\": \"Rehabilitation medicine researc\",\"p_en_count\": 39}, {\"t_id\": 2,\"t_name\": \"康复医学临床研究\",\"p_count\": 78,\"t_en_name\": \"Rehabilitation medicine researc\",\"p_en_count\": 39}, {\"t_id\": 3,\"t_name\": \"骨关节疼痛研究\",\"p_count\": 113,\"t_en_name\": \"Rehabilitation medicine researc\",\"p_en_count\": 39}]}";

            JObject jo = (JObject)JsonConvert.DeserializeObject(response);
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
                        p_count = (int)token["p_count"],
                        t_en_name= (string)token["t_en_name"] + "(" + (string)token["p_count"] + "篇)",
                        p_en_count = (int)token["p_en_count"]
                    };
                    CommonData.PaperTypes.Add(pt);
                }
            }

            //跳转列表页
            this.NavigationService.Navigate(new Uri("/PaperList.xaml", UriKind.Relative));
            //TODO:将会议ID和校验码存到配置文件中
        }


        /// <summary>
        /// 登录页输入框获得焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Text_GotFocus(object sender, RoutedEventArgs e) {
            (sender as TextBox).Text = null;
            //System.Diagnostics.Process.Start("osk.exe");
            InputPanel.ShowInputPanel();
        }
        /// <summary>
        /// 登录页输入框失去焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Text_LostFocus(object sender, RoutedEventArgs e)
        {
            InputPanel.HideInputPanel();
        }

        private void CLose_Click(object sender, RoutedEventArgs e)
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
            mainwindow.Close();
        }
    }
}
