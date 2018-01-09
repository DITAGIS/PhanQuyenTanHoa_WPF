using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace ViewModel
{
    public class UpdateHoaDonViewModel : INotifyPropertyChanged
    {
        #region Initialize
        private String pathHD;
        private String pathDS;
        private OpenFileDialog openFileDialog;
        private void Innitialize()
        {
            openFileDialog = new OpenFileDialog();
        }

        #endregion
        public UpdateHoaDonViewModel()
        {
            Innitialize();
            GetDataHDCommand = new RelayCommand<UIElementCollection>((p) => true, getDataHD);
            GetDataDSCommand = new RelayCommand<UIElementCollection>((p) => true, getDataDS);
        }
        private void getDataHD(UIElementCollection p)
        {
            openFileDialog.Filter = "DAT|*.dat";
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                PathHD = openFileDialog.FileName;
            }          
        }
        private void getDataDS(UIElementCollection p)
        {
            openFileDialog.Filter = "DAT|*.dat";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                PathDS = openFileDialog.FileName;
            }
        }
        private bool checkInfo()
        {

            return true;
        }

        public UIElement Refresh(UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
            return uiElement;
        }
        private Action EmptyDelegate = delegate () { };
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(String name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));

        }
        public ICommand GetDataHDCommand { get; set; }
        public ICommand GetDataDSCommand { get; set; }


        public String PathHD
        {
            get
            {
                return pathHD;
            }
            set
            {
                pathHD = value;
                OnPropertyChanged("PathHD");
            }
        }
        public String PathDS
        {
            get
            {
                return pathDS;
            }
            set
            {
                pathDS = value;
                OnPropertyChanged("PathDS");
            }
        }
    }
}
