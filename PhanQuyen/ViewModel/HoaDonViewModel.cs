﻿using Model;
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
        private void innitialize()
        {
            listHoaDon = new ObservableCollection<HoaDon>();
            listYear = new List<String>(HoaDonDBViewModel.getInstance.getDistinctYear());
            listMonth = new List<string>() { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
            listDate = new List<string>() { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10",
            "11", "12", "13", "14", "15", "16", "17", "18", "19", "20"};
            listMachine = new List<string>();
            for (int i = 1; i <= 61; i++)
            {
                if (i < 10)
                    listMachine.Add("0" + i);
                else
                    listMachine.Add(i.ToString());
            }
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

            innitialize();
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
                    value++;
                    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        ListHoaDon.Add(HoaDonDBViewModel.getInstance.getHoaDonsIncludeImageByCondition(Year, Month, Date, Group, Machine, danhBa));
                        Status = String.Format("Đang tải {0}/{1}", value, max);
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
        public string Year { get => year; set => year = value; }
        public string Month { get => month; set => month = value; }
        public string Date { get => date; set => date = value; }
        public string Group { get => group; set => group = value; }
        public string Machine { get => machine; set => machine = value; }
        public string Code { get => code; set => code = value; }
        public HoaDon SelectedHoaDon { get { return selectedHoaDon; } set { selectedHoaDon = value; OnPropertyChanged("SelectedHoaDon"); } }

        public int Value { get => value; set => this.value = value; }
        public string Status { get { return status; } set { status = value; } }
    }
}
