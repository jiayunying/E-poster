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

            //校验成功，
            
            //将会议ID和校验码存到配置文件中
            CommonData.cid = int.Parse(this.mettingId.Text);
            string json_req = JsonConvert.SerializeObject(new
            {
                cid = CommonData.cid,
                poster_result_id= CommonData.poster_result_id
            });

            ////查询条件的object更新
            //CommonData.JsonFilters.

            //调接口查询中英文论文分类分别保存至静态变量;
            string response = ServiceRequest.HttpPost(CommonData.pre_url + "/typelist", json_req);

            JObject jo = (JObject)JsonConvert.DeserializeObject(response);
            String record = jo["typelist"].ToString();
            JArray array = (JArray)JsonConvert.DeserializeObject(record);

            if (array.Count < 1) return;
            else
            {
                foreach (JToken token in array)
                {
                    PaperType pt = new PaperType()
                    {
                        t_id = (int)token["typeId"],
                        t_name = (string)token["typeName"] + "(" + (string)token["count"] + "篇)",
                        p_count = (int)token["count"],
                        t_en_name= (string)token["typeEnName"] + "(" + (string)token["count"] + "篇)",
                    };
                    CommonData.PaperTypes.Add(pt);
                }
            }

            //跳转列表页
            this.NavigationService.Navigate(new Uri("/PaperList.xaml", UriKind.Relative));
           
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
