using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class GetDataViewModel
    {
        #region Initialize
        private ObservableCollection<HoaDon> listHoaDon;

        public ObservableCollection<HoaDon> ListHoaDon
        {
            get { return listHoaDon; }
            set { listHoaDon = value; }
        }
        private ObservableCollection<SoDaNhan> listSoDaNhan;

        public ObservableCollection<SoDaNhan> ListSoDaNhan
        {
            get { return listSoDaNhan; }
            set { listSoDaNhan = value; OnPropertyChanged("ListSoDaNhan"); }
        }
        private HoaDon selectedHoaDon;
        public HoaDon SelectedHoaDon
        {
            get
            {
                return selectedHoaDon;
            }
            set
            {
                selectedHoaDon = value;
                OnPropertyChanged("SelectedHoaDon");
            }
        }
        private int year;
        public int Year
        {
            get { return year; }
            set { year = value; OnPropertyChanged("Year");
                ListSoDaNhan = GetDataDBViewModel.getInstance.getDistinctSoDaNhan(Year, Month, Date, Group); }
        }
        private String month;
        public String Month
        {
            get { return month; }
            set { month = value; OnPropertyChanged("Month"); ListSoDaNhan = GetDataDBViewModel.getInstance.getDistinctSoDaNhan(Year, Month, Date, Group); }
        }
        private String date;
        public String Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged("Date"); ListSoDaNhan = GetDataDBViewModel.getInstance.getDistinctSoDaNhan(Year, Month, Date, Group); }
        }
        private String group;
        public String Group
        {
            get { return group; }
            set { group = value; OnPropertyChanged("Group");
                ListSoDaNhan = GetDataDBViewModel.getInstance.getDistinctSoDaNhan(Year, Month, Date, Group); }
        }
        private ObservableCollection<int> listYear;

        public ObservableCollection<int> ListYear
        {
            get { return listYear; }
            set { listYear = value; OnPropertyChanged("ListYear"); }
        }
        private ObservableCollection<String> listMonth;

        public ObservableCollection<String> ListMonth
        {
            get { return listMonth; }
            set { listMonth = value; OnPropertyChanged("ListMonth"); }
        }
        private ObservableCollection<String> listDate;

        public ObservableCollection<String> ListDate
        {
            get { return listDate; }
            set { listDate = value; OnPropertyChanged("ListDate"); }
        }
        private ObservableCollection<String> listGroup;

        public ObservableCollection<String> ListGroup
        {
            get { return listGroup; }
            set { listGroup = value; OnPropertyChanged("ListGroup"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(String name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));

        }
        private void Initialize()
        {
            listSoDaNhan = new ObservableCollection<SoDaNhan>();
        }
        public GetDataViewModel()
        {
            Initialize();

            ListYear = GetDataDBViewModel.getInstance.getDistinctYear();
            ListMonth = GetDataDBViewModel.getInstance.getDistinctMonth();
            ListDate = GetDataDBViewModel.getInstance.getDistinctDate();
            ListGroup = GetDataDBViewModel.getInstance.getDistinctGroup();


        }
        #endregion
    }
}
