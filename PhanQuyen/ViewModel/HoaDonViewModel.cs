using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace ViewModel
{
    public class HoaDonViewModel : INotifyPropertyChanged
    {
        #region Initialize
        private Byte[] image;
        public Byte[] Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }
        private bool hasImage;
        private String status;
        private DocSoLocal selectedHoaDon;
        private HoaDon12Month hoaDon12Month;
        private int value;
        private int year;
        private ObservableCollection<int> listYear;
        public ObservableCollection<int> ListYear
        {
            get { return listYear; }
            set
            {
                listYear = value;
                OnPropertyChanged("ListYear");
            }
        }
        private String month;
        private ObservableCollection<String> listMonth;
        public ObservableCollection<String> ListMonth
        {
            get { return listMonth; }
            set
            {
                listMonth = value;
                OnPropertyChanged("ListMonth");
            }
        }
        private String date;
        private ObservableCollection<String> listDate;
        public ObservableCollection<String> ListDate
        {
            get { return listDate; }
            set { listDate = value; OnPropertyChanged("ListDate"); }
        }
        private String group;
        private ObservableCollection<int> listGroup;
        public ObservableCollection<int> ListGroup
        {
            get { return listGroup; }
            set { listGroup = value; OnPropertyChanged("ListGroup"); }
        }
        private String machine;
        private ObservableCollection<String> listMachine;
        public ObservableCollection<String> ListMachine
        {
            get { return listMachine; }
            set { listMachine = value; OnPropertyChanged("ListMachine"); }
        }
        private String code;
        private List<String> listCode;
        public List<String> ListCode
        {
            get { return listCode; }
            set { listCode = value; OnPropertyChanged("ListCode"); }
        }
        public ObservableCollection<DocSo_1Ky> listDocSo_1Ky;
        public ObservableCollection<DocSo_1Ky> ListDocSo_1Ky
        {
            get { return listDocSo_1Ky; }
            set
            {
                listDocSo_1Ky = value;
                OnPropertyChanged("ListDocSo_1Ky");
            }
        }
        private void Innitialize()
        {

            listHoaDon = new ObservableCollection<DocSoLocal>();
            //listMonth = GetDataDBViewModel.getInstance.getDistinctMonthServer(Year);
            //Month = User.getInstance.Month;
            //listDate = GetDataDBViewModel.getInstance.getDistinctDateServer(Year, Month);
            //Date = User.getInstance.Date;
            //listGroup = GetDataDBViewModel.getInstance.getDistinctGroupServer(Year, Month, Date);
            //listMachine = GetDataDBViewModel.getInstance.getDistinctMachineServer(Year, Month, Date, Int16.Parse(User.getInstance.UserGroup));
            listCode = CodeModel.GetCodes();
        }
        private ObservableCollection<DocSoLocal> listHoaDon;



        public ObservableCollection<DocSoLocal> ListHoaDon
        {
            get { return listHoaDon; }
            set { listHoaDon = value; }
        }

        #endregion
        public HoaDonViewModel()
        {
            Innitialize();
            UpdateCommand = new RelayCommand<UIElementCollection>((p) => true, update);
            RotateCommand = new RelayCommand<UIElementCollection>((p) => true, rotate);
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
        #region command
        private void update(UIElementCollection p)
        {
            int max, value;
            if (checkInfo())
            {
                //String year = "", month = "", date = "", group = "", machine = "";
                listHoaDon.Clear();
                //foreach (var item in p)
                //{
                //    ComboBox cbb = item as ComboBox;
                //    if (cbb == null)
                //        continue;
                //    switch (cbb.Name)
                //    {
                //        case "cbbYear":
                //            year = cbb.SelectedItem.ToString();
                //            break;
                //        case "cbbMonth":
                //            month = cbb.SelectedItem.ToString();
                //            break;
                //        case "cbbDate":
                //            date = cbb.SelectedItem.ToString();
                //            break;
                //        case "cbbGroup":
                //            group = cbb.SelectedItem.ToString();
                //            break;
                //        case "cbbMachine":
                //            machine = cbb.SelectedItem.ToString();
                //            break;
                //    }
                //}
                //ConnectionViewModel.getInstance.disConnect();

                Status = "Đang tính toán dữ liệu...";

                List<String> danhBas = HoaDonDBViewModel.getInstance.getDanhBasByCondition(Year, Month, Date, Int16.Parse(Group), Machine);
                max = danhBas.Count;
                value = 0;

                List<HoaDon> hoaDons = new List<HoaDon>();
                foreach (String danhBa in danhBas)
                {

                    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        value++;
                        ListHoaDon.Add(GetDataDBViewModel.getInstance.getDocSoLocalByDanhBa(danhBa, Year, Month, Date, Int16.Parse(Group), Machine));
                        if (value < max)
                            Status = String.Format("Đang tải {0}/{1}", value, max);
                        else
                            Status = "Tải dữ liệu hoàn tất";
                    }), DispatcherPriority.Loaded);

                }

                MessageBox.Show("Cập nhật hoàn tất!");
            }
        }
        private void rotate(UIElementCollection p)
        {
            if (p != null)
                foreach (var item in p)
                {
                    Image img = item as Image;
                    if (img == null)
                        continue;
                    switch (img.Name)
                    {

                        case "imgView":
                            RotateTransform rotateTransform = new RotateTransform(45);
                            if (img != null)
                                img.RenderTransform = rotateTransform;
                            break;
                    }
                }
        }



        private Action EmptyDelegate = delegate () { };
        public ICommand UpdateCommand { get; set; }
        public ICommand RotateCommand { get; set; }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(String name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));

        }
        public int Year
        {
            get
            {
                return year;
            }
            set
            {
                year = value;
               
            }
        }
        public string Month
        {
            get
            {
                return month;
            }
            set
            {
                month = value;
              

            }
        }
        public string Date { get => date; set => date = value; }
        public string Group
        {
            get
            {
                return group;
            }
            set
            {
                group = value;
               
            }
        }
        public string Machine { get => machine; set => machine = value; }
        public string Code { get => code; set => code = value; }
        public DocSoLocal SelectedHoaDon
        {
            get
            {
                if (selectedHoaDon == null)
                    HasImage = false;
                else if (Image != null)
                    HasImage = true;
                else
                    HasImage = false;
                return selectedHoaDon;
            }
            set
            {

                hoaDon12Month = new HoaDon12Month();
                selectedHoaDon = value;
                Image = GetDataDBViewModel.getInstance.getImageLocalByDanhBa(selectedHoaDon.DanhBa, selectedHoaDon.GIOGHI.GetValueOrDefault());
                ListDocSo_1Ky = GetDataDBViewModel.getInstance.get12Months(Year, Month, selectedHoaDon.DanhBa);

                OnPropertyChanged("SelectedHoaDon");
            }
        }
        public HoaDon12Month SelectedHoaDon12Month
        {
            get
            {

                return hoaDon12Month;
            }
            set
            {
                hoaDon12Month = value;
                OnPropertyChanged("SelectedHoaDon12Month");
            }
        }

        public int Value { get => value; set => this.value = value; }
        public string Status { get { return status; } set { status = value; OnPropertyChanged("Status"); } }

        public bool HasImage
        {
            get
            {
                return hasImage;
            }
            set
            {
                hasImage = value;
                OnPropertyChanged("HasImage");
            }
        }
    }
}
