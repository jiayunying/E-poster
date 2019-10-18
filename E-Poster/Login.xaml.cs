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
            try
            {
                //调接口进行权限校验(后期可以把配置文件中的相关信息在数据库中建表调接口处理。)
                string str_login = JsonConvert.SerializeObject(new
                {
                    cid = int.Parse(mettingId.Text.Trim()),
                    idnt = checksum.Text.Trim()
                });
                string res_login = ServiceRequest.HttpPost(CommonData.pre_url + "/login", str_login);
                JObject jo_check = (JObject)JsonConvert.DeserializeObject(res_login);

                if (jo_check.ContainsKey("code") && jo_check["code"].ToString().Equals("0"))
                {   //校验成功，
                    //将会议ID和校验码存到配置文件中
                    CommonData.cid = int.Parse(this.mettingId.Text);
                    CommonData.jsonFilters.cid = CommonData.cid;
                    CommonData.jsonFilters.limit = CommonData.PageSize;
                    CommonData.jsonFilters.poster_result_id = CommonData.poster_result_id;
                    string json_req = JsonConvert.SerializeObject(new
                    {
                        cid = CommonData.cid,
                        poster_result_id = CommonData.poster_result_id
                    });
                    //调接口查询中英文论文分类分别保存至静态变量;
                    string response = ServiceRequest.HttpPost(CommonData.pre_url + "/typelist", json_req);

                    JObject jo = (JObject)JsonConvert.DeserializeObject(response);
                    if (jo.ContainsKey("code") && jo["code"].ToString().Equals("0"))
                    {
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
                                    t_en_name = (string)token["typeEnName"] + "(" + (string)token["count"] + "篇)",
                                };
                                CommonData.PaperTypes.Add(pt);
                            }
                        }

                        //跳转列表页
                        this.NavigationService.Navigate(new Uri("/PaperList.xaml", UriKind.Relative));
                    }         
                    else
                    {
                        MessageBox.Show("初始化类型列表失败，请检查后台服务!");
                    }

                    //查询本次会议的投稿类型，根据投稿类型判断是否显示中英文切换按钮
                    string con_req = JsonConvert.SerializeObject(new
                    {
                        cid = CommonData.cid
                    }
                    );
                    string res_con = ServiceRequest.HttpPost(CommonData.pre_url + "/config", con_req);

                    JObject jo_con = (JObject)JsonConvert.DeserializeObject(res_con);

                    if (jo_con["code"].ToString().Equals("0"))
                    {
                        if (jo_con["cnfl"].ToString().Equals("1") && jo_con["enfl"].ToString().Equals("1"))
                        {
                            CommonData.langFl = true;
                        }
                        else {
                            CommonData.langFl = false;
                            if (jo_con["cnfl"].ToString().Equals("1"))
                            {
                                CommonData.jsonFilters.language = "cn";
                            }
                            else if (jo_con["enfl"].ToString().Equals("1")) {
                                CommonData.jsonFilters.language = "en";
                            }
                        }
                    }
                }
                else
                {
                    //TODO:校验失败提示失败
                    MessageBox.Show("会议ID不存在或校验码不匹配，请检查!");
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
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
