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
        private ObservableCollection<DocSoLocal> listHoaDon;

        public ObservableCollection<DocSoLocal> ListHoaDon
        {
            get { return listHoaDon; }
            set { listHoaDon = value; OnPropertyChanged("ListHoaDon"); }
        }
        private ObservableCollection<SoDaNhan> listSoDaNhan;

        public ObservableCollection<SoDaNhan> ListSoDaNhan
        {
            get { return listSoDaNhan; }
            set { listSoDaNhan = value; OnPropertyChanged("ListSoDaNhan"); }
        }
        private SoDaNhan selectedSoDaNhan;
        public SoDaNhan SelectedSoDaNhan
        {
            get
            {
                return selectedSoDaNhan;
            }
            set
            {
                selectedSoDaNhan = value;
                OnPropertyChanged("SelectedSoDaNhan");
                if (selectedSoDaNhan != null)
                    ListHoaDon = HandlingDataDBViewModel.Instance.getDistinctHoaDon(selectedSoDaNhan);
            }
        }
        private int year;
        public int Year
        {
            get { return year; }
            set
            {
                year = value; OnPropertyChanged("Year");
                ListSoDaNhan = HandlingDataDBViewModel.Instance.getDistinctSoDaNhan(Year, Month, Date, Group);
            }
        }
        private String month;
        public String Month
        {
            get { return month; }
            set { month = value; OnPropertyChanged("Month"); ListSoDaNhan = HandlingDataDBViewModel.Instance.getDistinctSoDaNhan(Year, Month, Date, Group); }
        }
        private String date;
        public String Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged("Date"); ListSoDaNhan = HandlingDataDBViewModel.Instance.getDistinctSoDaNhan(Year, Month, Date, Group); }
        }
        private int group;
        public int Group
        {
            get { return group; }
            set
            {
                group = value; OnPropertyChanged("Group");
                ListSoDaNhan = HandlingDataDBViewModel.Instance.getDistinctSoDaNhan(Year, Month, Date, Group);
            }
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
        private ObservableCollection<int> listGroup;

        public ObservableCollection<int> ListGroup
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
            listHoaDon = new ObservableCollection<DocSoLocal>();
        }
        public GetDataViewModel()
        {
            Initialize();
            ListYear = HandlingDataDBViewModel.Instance.getDistinctYear();
            //ListMonth = DataDBViewModel.Instance.getDistinctMonth();
            ListDate = HandlingDataDBViewModel.Instance.getDistinctDate();
            ListGroup = HandlingDataDBViewModel.Instance.getDistinctGroup();
        }
        #endregion
    }
}
