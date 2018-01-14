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
            set { listSoDaNhan = value; }
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
            set { year = value; }
        }
        private int month;
        public int Month
        {
            get { return month; }
            set { month = value; }
        }
        private int date;
        public int Date
        {
            get { return date; }
            set { date = value; }
        }
        private int group;
        public int Group
        {
            get { return group; }
            set { group = value; }
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

            DataClassesLocalDataContext localDataContext = new DataClassesLocalDataContext();
            var soDaNhans = (from x in localDataContext.SoDaNhans
                             where x.So.Equals(Year + "_" + Month + "_" + Date + "_" + Group)
                             select x).ToList();
            foreach (var item in soDaNhans)
                ListSoDaNhan.Add(item);
        }
        #endregion
    }
}
