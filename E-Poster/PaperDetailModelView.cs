using CommonUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace E_Poster
{
    public class PaperDetailModelView : INotifyPropertyChanged
    {
        public PaperDetailModelView()
        {
            this.CurImg = RefreshImg();
        }
        public PaperDetail View { get; set; }

        private BitmapImage curImg;
        public BitmapImage CurImg
        {
            get
            {
                return this.curImg;
            }
            set
            {
                if (this.curImg != value)
                {
                    this.curImg = value;
                    OnPropertyChanged("CurImg");
                }
            }
        }
        BitmapImage RefreshImg()
        {
            try {
                DirectoryInfo dirInfo = new DirectoryInfo(System.Environment.CurrentDirectory + @"\ydt-mettings\" + CommonData.cid + @"\posters");
                FileInfo[] files = dirInfo.GetFiles();
                int length = files.Length;
                foreach (FileInfo file in files)
                {
                    if (file.Extension.Equals(".jpg") || file.Extension.Equals(".jpeg"))
                    {
                        if (Path.GetFileNameWithoutExtension(CommonData.CurrentPaper.filename).Equals(Path.GetFileNameWithoutExtension(file.Name)))
                        {
                            return new BitmapImage(new Uri(System.Environment.CurrentDirectory + @"\ydt-mettings\" + CommonData.cid + @"\posters\" + file.Name));
                        }
                    }
                }
                return null;
            }
            catch (Exception ex) {
                return null;
            }
        }

        public DelegateCommand LastClickCommand
        {
            get {
                return new DelegateCommand(LastClick);
            }
        }
        public DelegateCommand NextClickCommand
        {
            get
            {
                return new DelegateCommand(NextClick);
            }
        }
        //上一页
        void LastClick(object obj)
        {
            try {
                if (CommonData.jsonFilters.offset==1&&CommonData.CurrentIndex == 0)
                {
                    //提示第一页
                    switch (CommonData.jsonFilters.language) {
                        case "cn":
                            MessageBox.Show("当前是第一篇！");
                            break;
                        case "en":
                            MessageBox.Show("No More Data！");
                            break;
                    }
                }
                else { 
                    CommonData.CurrentIndex -= 1;
                    CommonData.CurrentPaper = CommonData.Papers[CommonData.CurrentIndex.Value];
                    this.CurImg = RefreshImg();
                }
            }
            catch (Exception ex) {
                
            }
        }
        //下一页
        void NextClick(object obj)
        {
            try
            {
                //if (CommonData.Papers.Count < CommonData.PageSize && CommonData.CurrentIndex == CommonData.Papers.Count-1)
                //{
                //    //提示最后一页
                //    switch (CommonData.jsonFilters.language)
                //    {
                //        case "cn":
                //            MessageBox.Show("当前是最后一篇！");
                //            break;
                //        case "en":
                //            MessageBox.Show("No More Data！");
                //            break;
                //    }
                //}
                //else { 
                //    CommonData.CurrentIndex += 1;
                //    CommonData.CurrentPaper = CommonData.Papers[CommonData.CurrentIndex.Value];
                //    this.CurImg = RefreshImg();
                //}

                //未翻到本页最后一条，则正常翻页
                if (CommonData.CurrentIndex < CommonData.Papers.Count-1)
                {
                    CommonData.CurrentIndex += 1;
                    CommonData.CurrentPaper = CommonData.Papers[CommonData.CurrentIndex.Value];
                    this.CurImg = RefreshImg();
                }
                else {
                    //翻最后一条需判断是否是最后一页
                    if (CommonData.Papers.Count < CommonData.PageSize)
                    {
                        //提示最后一篇
                        switch (CommonData.jsonFilters.language)
                        {
                            case "cn":
                                MessageBox.Show("当前是最后一篇！");
                                break;
                            case "en":
                                MessageBox.Show("No More Data！");
                                break;
                        }
                    }
                    else {
                        //本页条数和pagesize相等需要翻一页判断是否是最后一页
                        CommonData.jsonFilters.offset += 1;
                        List<Paper> temp = CommonData.Papers;
                        ServiceRequest.RefreshList();
                        if (CommonData.Papers.Count == 0)
                        {
                            CommonData.Papers = temp;
                            CommonData.jsonFilters.offset -= 1;
                            //提示最后一篇
                            switch (CommonData.jsonFilters.language)
                            {
                                case "cn":
                                    MessageBox.Show("当前是最后一篇！");
                                    break;
                                case "en":
                                    MessageBox.Show("No More Data！");
                                    break;
                            }
                        }
                        else {
                            CommonData.CurrentIndex = 0;
                            CommonData.CurrentPaper = CommonData.Papers[CommonData.CurrentIndex.Value];
                            this.CurImg = RefreshImg();
                        }

                    }

                }


            }
            catch (Exception ex) {

            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }

    public class DelegateCommand : ICommand
    {
        private Action action;
        private Action<Object> actionT;

        public DelegateCommand(Action action)
        {
            this.action = action;
        }

        public DelegateCommand(Action<Object> action)
        {
            this.actionT = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (action != null)
            {
                action();
            }
            if (actionT != null)
            {
                actionT.Invoke(parameter);
            }
        }
    }
}
