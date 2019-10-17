using CommonUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
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
                CommonData.CurrentIndex -= 1;
                CommonData.CurrentPaper = CommonData.Papers[CommonData.CurrentIndex.Value];
                this.CurImg = RefreshImg();
            }
            catch (Exception ex) {
                
            }
        }
        //下一页
        void NextClick(object obj)
        {
            try
            {
                if (CommonData.Papers.Count > 0) { 
                CommonData.CurrentIndex += 1;
                CommonData.CurrentPaper = CommonData.Papers[CommonData.CurrentIndex.Value];
                this.CurImg = RefreshImg();
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
