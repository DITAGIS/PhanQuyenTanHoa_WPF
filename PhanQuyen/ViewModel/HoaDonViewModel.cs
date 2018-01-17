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
            listYear = new List<String>(HoaDonDBViewModel.getInstance.getDistinctYear());
            Year = User.getInstance.Year;
            listMonth = new List<string>();
            for (int i = 1; i <= 12; i++)
                if (i < 10)
                    listMonth.Add("0" + i);
                else listMonth.Add(i.ToString());
            Month = User.getInstance.Month;
            listDate = new List<string>();
            for (int i = 1; i <= 20; i++)
                if (i < 10)
                    listDate.Add("0" + i);
                else listDate.Add(i.ToString());
            Date = User.getInstance.Date;
            listGroup = new List<string>(HoaDonDBViewModel.getInstance.getDistinctGroup());
            foreach (String toID in ToID.GetToID())
                if (toID.Equals(User.getInstance.ToID))
                    ListGroup.Add(toID);
            listMachine = new List<string>();
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

                List<String> danhBas = HoaDonDBViewModel.getInstance.getDanhBasByCondition(Int16.Parse(Year), Month, Date, Int16.Parse(Group), Machine);
                max = danhBas.Count;
                value = 0;

                List<HoaDon> hoaDons = new List<HoaDon>();
                foreach (String danhBa in danhBas)
                {

                    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        value++;
                        ListHoaDon.Add(GetDataDBViewModel.getInstance.getDocSoLocalByDanhBa(danhBa, Int16.Parse(Year), Month, Date, Int16.Parse(Group), Machine));
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
        public string Year
        {
            get
            {
                return year;
            }
            set
            {
                year = value;
                //Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                //{
                //    listMonth.Clear();
                //    listMonth.AddRange(HoaDonDBViewModel.getInstance.getDistinctMonth(year));
                //    if (ListMonth.Count > 0)
                //        month = listMonth[0];

                //}), DispatcherPriority.Loaded);

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
                //Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                //{
                //    listDate.Clear();
                //    listDate.AddRange(HoaDonDBViewModel.getInstance.getDistinctDate(year, month));
                //    if (listDate.Count > 0)
                //        date = listDate[0];
                //}), DispatcherPriority.Loaded);

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
        public DocSoLocal SelectedHoaDon
        {
            get
            {
                if (selectedHoaDon == null)
                    HasImage = false;
                else if (Image != null)
                    HasImage = true;
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
