using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ViewModel
{
    public class HoaDonViewModel
    {
        private ObservableCollection<HoaDon> listHoaDon;
        public ObservableCollection<HoaDon> ListHoaDon
        {
            get { return listHoaDon; }
            set { listHoaDon = value; }
        }
        public HoaDonViewModel()
        {
            listHoaDon = new ObservableCollection<HoaDon>();
            //HoaDonDBViewModel hoaDonDBViewModel = new HoaDonDBViewModel();
            //foreach (HoaDon hoaDon in hoaDonDBViewModel.getAllHoaDon())
            //    listHoaDon.Add(hoaDon);

            UpdateCommand = new RelayCommand<UIElementCollection>((p) => true, (p) =>
            {
                if (checkInfo())
                {
                    listHoaDon.Clear();
                    //MessageBox.Show(Connection.getInstance.getConnection.ToString());
                    ConnectionViewModel.getInstance.disConnect();
                    HoaDonDBViewModel hoaDonDB = new HoaDonDBViewModel();
                    List<HoaDon> hoaDons = hoaDonDB.getAllHoaDon();
                    foreach (HoaDon hoaDon in hoaDons)
                    {
                        listHoaDon.Add(hoaDon);
                    }

                }
            });


        }
        private bool checkInfo()
        {

            return true;
        }
        public ICommand UpdateCommand { get; set; }
    }
}
