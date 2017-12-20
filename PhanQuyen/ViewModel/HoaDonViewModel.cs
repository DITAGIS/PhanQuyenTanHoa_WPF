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
        private bool hasImage;
        private String status;
        private HoaDon selectedHoaDon;
        private int value;
        private String year;
        private List<String> listYear;
        public List<String> ListYear
        {
            get { return listYear; }
            set
            {
                listYear = value;
                OnPropertyChanged("ListYear");
            }
        }
        private String month;
        private List<String> listMonth;
        public List<String> ListMonth
        {
            get { return listMonth; }
            set
            {
                listMonth = value;
                OnPropertyChanged("ListMonth");
            }
        }
        private String date;
        private List<String> listDate;
        public List<String> ListDate
        {
            get { return listDate; }
            set { listDate = value; OnPropertyChanged("ListDate"); }
        }
        private String group;
        private List<String> listGroup;
        public List<String> ListGroup
        {
            get { return listGroup; }
            set { listGroup = value; OnPropertyChanged("ListGroup"); }
        }
        private String machine;
        private List<String> listMachine;
        public List<String> ListMachine
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
        private void Innitialize()
        {
            listHoaDon = new ObservableCollection<HoaDon>();
            listYear = new List<String>(HoaDonDBViewModel.getInstance.getDistinctYear());
            listMonth = new List<string>();
            listDate = new List<string>();
            listGroup = new List<string>(HoaDonDBViewModel.getInstance.getDistinctGroup());
            listMachine = new List<string>();
            listCode = new List<string>() { "Tất cả", "Chưa ghi",
                "40",
                "41",
                "42",
                "54",
                "55",
                "56",
                "58",
                "5F",
                "5M",
                "5Q",
                "5K",
                "5N",
                "60",
                "61",
                "62",
                "63",
                "64",
                "66",
                "80",
                "81",
                "82",
                "83",
                "F1",
                "F2",
                "F3",
                "F4",
                "K",
                "M0",
                "M1",
                "M2",
                "M3",
                "N1",
                "N2",
                "N3",
                "X ",
                "68",
                "Q "};
        }
        private ObservableCollection<HoaDon> listHoaDon;



        public ObservableCollection<HoaDon> ListHoaDon
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

                List<String> danhBas = HoaDonDBViewModel.getInstance.getDanhBasByCondition(Year, Month, Date, Group, Machine);
                max = danhBas.Count;
                value = 0;

                List<HoaDon> hoaDons = new List<HoaDon>();
                foreach (String danhBa in danhBas)
                {

                    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        value++;
                        ListHoaDon.Add(HoaDonDBViewModel.getInstance.getHoaDonsIncludeImageByCondition(Year, Month, Date, Group, Machine, danhBa));
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
        public ICommand UpdateCommand { get; set; }
        public ICommand RotateCommand { get; set; }
        public string Year
        {
            get
            {
                return year;
            }
            set
            {
                year = value;
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    listMonth.Clear();
                    listMonth.AddRange(HoaDonDBViewModel.getInstance.getDistinctMonth(year));
                    if (ListMonth.Count > 0)
                        month = listMonth[0];

                }), DispatcherPriority.Loaded);
               
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
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    listDate.Clear();
                    listDate.AddRange(HoaDonDBViewModel.getInstance.getDistinctDate(year, month));
                    if (listDate.Count > 0)
                        date = listDate[0];
                }), DispatcherPriority.Loaded);

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
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    listMachine.Clear();
                    listMachine.AddRange(HoaDonDBViewModel.getInstance.getDistinctMachine(group));
                    if (listMachine.Count > 0)
                        machine = listMachine[0];

                }), DispatcherPriority.Loaded);

            }
        }
        public string Machine { get => machine; set => machine = value; }
        public string Code { get => code; set => code = value; }
        public HoaDon SelectedHoaDon
        {
            get
            {
                if (selectedHoaDon == null)
                    HasImage = false;
                else if (selectedHoaDon.Image != null)
                    HasImage = true;
                return selectedHoaDon;
            }
            set
            {
                selectedHoaDon = value;
                OnPropertyChanged("SelectedHoaDon");
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
