using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class GetDataDBViewModel
    {


        private static GetDataDBViewModel _instance;
        private DataClassesLocalDataContext localContext;
        private DataClassServerDataContext serverContext;
        public static GetDataDBViewModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GetDataDBViewModel();
                return _instance;
            }
        }
        private GetDataDBViewModel()
        {
            localContext = new DataClassesLocalDataContext();
            serverContext = new DataClassServerDataContext();
        }
        public List<String> getDocsosByConditionCount(int year, String month, String date, int xGroup, String machine)
        {
            var getData = (from x in serverContext.DocSos
                           where x.Nam == year && x.Ky == month && x.Dot == date && x.TODS == xGroup && x.May == machine
                           select x.DanhBa).ToList();
            return getData;
        }



        public bool getDocSosByDanhBa(String danhBa, int year, String month, String date, int xGroup, String machine)
        {

            bool result = false;

            DataClassServerDataContext serverContext = new DataClassServerDataContext();
            var getData = (from x in serverContext.DocSos
                           where x.Nam == year && x.Ky == month && x.Dot == date && x.TODS == xGroup && x.DanhBa == danhBa && x.May == machine
                           select x).ToList();

            DataClassesLocalDataContext localContext = new DataClassesLocalDataContext();
            foreach (var item in getData)
            {
                try
                {
                    localContext.DocSoLocals.InsertOnSubmit(new DocSoLocal()
                    {
                        DocSoID = item.DocSoID,
                        DanhBa = item.DanhBa,
                        MLT1 = item.MLT1,
                        MLT2 = item.MLT2,
                        SoNhaCu = item.SoNhaCu,
                        SoNhaMoi = item.SoNhaMoi,
                        Duong = item.Duong,
                        SDT = item.SDT,
                        GB = item.GB,
                        DM = item.DM,
                        Nam = item.Nam,
                        Ky = item.Ky,
                        Dot = item.Dot,
                        May = item.May,
                        TBTT = item.TBTT,
                        CSCu = item.CSCu,
                        CSMoi = item.CSMoi,
                        CodeCu = item.CodeCu,
                        CodeMoi = item.CodeMoi,
                        TTDHNCu = item.TTDHNCu,
                        TTDHNMoi = item.TTDHNMoi,
                        TieuThuCu = item.TieuThuCu,
                        TieuThuMoi = item.TieuThuMoi,
                        TuNgay = (DateTime)item.TuNgay,
                        DenNgay = (DateTime)item.DenNgay,
                        TienNuoc = item.TienNuoc,
                        BVMT = item.BVMT,
                        Thue = item.Thue,
                        TongTien = item.TongTien,
                        SoThanCu = item.SoThanCu,
                        SoThanMoi = item.SoThanMoi,
                        TODS = item.TODS,
                        HieuCu = item.HieuCu,
                        HieuMoi = item.HieuMoi,
                        ViTriCu = item.ViTriCu,
                        ViTriMoi = item.ViTriMoi,
                        GhiChuDS = item.GhiChuDS,
                        GIOGHI = (DateTime)item.GIOGHI
                    });

                    localContext.SubmitChanges();

                    var imageData = (from x in serverContext.HinhDHNs
                                     from y in serverContext.DocSos
                                     where x.DanhBo == y.DanhBa && x.CreateDate == y.GIOGHI && x.DanhBo == item.DanhBa
                                     select x).SingleOrDefault();
                    if (imageData != null)
                    {
                        localContext.HinhDHNLocals.InsertOnSubmit(new HinhDHNLocal()
                        {
                            ID = imageData.ID,
                            DanhBo = imageData.DanhBo,
                            Image = imageData.Image,
                            CreateDate = imageData.CreateDate
                        });
                        localContext.SubmitChanges();
                    }
                }
                catch (Exception e)
                {
                }
            }

            return result;
        }

        public List<ChuyenMayDS> getKH_ChuyenMayDS(string date, string machineLeft)
        {
            List<ChuyenMayDS> listData = new List<ChuyenMayDS>();
            var getData = (from x in localContext.DocSoLocals
                           where x.Dot == date && x.May == machineLeft
                           select new { x.DanhBa, x.MLT1, x.SoNhaCu, x.Duong }).ToList();
            foreach (var item in getData)
            {
                listData.Add(new ChuyenMayDS()
                {
                    DanhBa = item.DanhBa,
                    MLT = item.MLT1,
                    DiaChi = item.SoNhaCu + " " + item.Duong
                });
            }
            return listData;
        }

        public DocSoLocal getDocSoLocalByDanhBa(String danhBa, int year, String month, String date, int xGroup, String machine)
        {

            DataClassesLocalDataContext localContext = new DataClassesLocalDataContext();
            var data = (from x in localContext.DocSoLocals
                        where x.DanhBa == danhBa && x.Nam == year && x.Ky == month && x.TODS == xGroup && x.May == machine
                        select x).Single();
            return data;
        }
        public byte[] getImageLocalByDanhBa(String danhBa, DateTime gioGhi)
        {

            var data = (from x in localContext.HinhDHNLocals
                        where x.DanhBo == danhBa && x.CreateDate == gioGhi
                        select x.Image).FirstOrDefault();
            if (data == null)
                return null;
            return ((System.Data.Linq.Binary)data).ToArray();
        }
        public ObservableCollection<DocSoLocal> getDistinctHoaDon(SoDaNhan selectedSoDaNhan)
        {
            ObservableCollection<DocSoLocal> listHoaDon = new ObservableCollection<DocSoLocal>();
            var hoaDons = (from x in localContext.DocSoLocals
                           where x.Nam == selectedSoDaNhan.nam && x.Ky == selectedSoDaNhan.ky && x.Dot == selectedSoDaNhan.dot && x.May == selectedSoDaNhan.may
                           select x).ToList();
            foreach (var item in hoaDons)
                listHoaDon.Add(item);
            return listHoaDon;
        }

        public bool WriteSoDaNhan(int year, String month, String date, String machine, int count, int xGroup)
        {
            DataClassesLocalDataContext localDataContext = new DataClassesLocalDataContext();
            try
            {

                localDataContext.SoDaNhans.InsertOnSubmit(new SoDaNhan()
                {
                    So = year + "_" + month + "_" + date + "_" + machine,
                    SoLuong = count,
                    nam = year,
                    ky = month,
                    dot = date,
                    may = machine,
                    ToDS = xGroup
                });
                localDataContext.SubmitChanges();
                return true;
            }
            catch
            {

            }
            return false;
        }

        public bool getDocSosByCondition(int year, String month, String date, int xGroup)
        {

            bool result = false;

            DataClassServerDataContext serverContext = new DataClassServerDataContext();
            var getData = (from x in serverContext.DocSos
                           where x.Nam == year && x.Ky == month && x.Dot == date && x.TODS == xGroup
                           select x).ToList();

            DataClassesLocalDataContext localContext = new DataClassesLocalDataContext();
            foreach (var item in getData)
            {
                try
                {
                    localContext.DocSoLocals.InsertOnSubmit(new DocSoLocal()
                    {
                        DocSoID = item.DocSoID,
                        DanhBa = item.DanhBa,
                        MLT1 = item.MLT1,
                        MLT2 = item.MLT2,
                        SoNhaCu = item.SoNhaCu,
                        SoNhaMoi = item.SoNhaMoi,
                        Duong = item.Duong,
                        SDT = item.SDT,
                        GB = item.GB,
                        DM = item.DM,
                        Nam = item.Nam,
                        Ky = item.Ky,
                        Dot = item.Dot,
                        May = item.May,
                        TBTT = item.TBTT,
                        CSCu = item.CSCu,
                        CSMoi = item.CSMoi,
                        CodeCu = item.CodeCu,
                        CodeMoi = item.CodeMoi,
                        TTDHNCu = item.TTDHNCu,
                        TTDHNMoi = item.TTDHNMoi,
                        TieuThuCu = item.TieuThuCu,
                        TieuThuMoi = item.TieuThuMoi,
                        TuNgay = (DateTime)item.TuNgay,
                        DenNgay = (DateTime)item.DenNgay,
                        TienNuoc = item.TienNuoc,
                        BVMT = item.BVMT,
                        Thue = item.Thue,
                        TongTien = item.TongTien,
                        SoThanCu = item.SoThanCu,
                        SoThanMoi = item.SoThanMoi,
                        HieuCu = item.HieuCu,
                        HieuMoi = item.HieuMoi,
                        ViTriCu = item.ViTriCu,
                        ViTriMoi = item.ViTriMoi,
                        GhiChuDS = item.GhiChuDS,
                        GIOGHI = (DateTime)item.GIOGHI
                    });

                    localContext.SubmitChanges();

                    var imageData = (from x in serverContext.HinhDHNs
                                     from y in serverContext.DocSos
                                     where x.DanhBo == y.DanhBa && x.CreateDate == y.GIOGHI && x.DanhBo == item.DanhBa
                                     select x).SingleOrDefault();
                    if (imageData != null)
                    {
                        localContext.HinhDHNLocals.InsertOnSubmit(new HinhDHNLocal()
                        {
                            ID = imageData.ID,
                            DanhBo = imageData.DanhBo,
                            Image = imageData.Image,
                            CreateDate = imageData.CreateDate
                        });
                        localContext.SubmitChanges();
                    }
                }
                catch (Exception e)
                {
                }
            }

            return result;
        }

        public ObservableCollection<int> getDistinctYear()
        {
            ObservableCollection<int> lstYear = new ObservableCollection<int>();
            var items = (from x in localContext.DocSoLocals
                         select x.Nam).Distinct().ToList();
            foreach (int item in items)
                lstYear.Add(item);
            return lstYear;
        }

        public ObservableCollection<DocSo_1Ky> get12Months(int year, string Month, string danhBa)
        {
            ObservableCollection<DocSo_1Ky> listDocSo = new ObservableCollection<DocSo_1Ky>();
            try
            {
                string pattern = "dd/MM/yyyy";
                int count = 0;
                int month = Int16.Parse(Month);
                while (count <= 12)
                {
                    month--;
                    if (month == 0)
                    {
                        year--;
                        month = 12;
                    }
                    var data = (from x in localContext.DocSoLocals
                                where x.DanhBa == danhBa && x.Nam == year && x.Ky == (month + "")
                                select new { x.Ky, x.GIOGHI, x.CodeMoi, x.CSMoi, x.TieuThuMoi }).FirstOrDefault();
                    listDocSo.Add(new DocSo_1Ky(data.Ky + "/" + year, data.GIOGHI.GetValueOrDefault().ToString(pattern), data.CodeMoi, data.CSMoi + "", data.TieuThuMoi + ""));
                    //switch (count)
                    //{

                    //    case 1:
                    //        SelectedHoaDon12Month.Code1 = hoaDon.CodeMoi;
                    //        code1 = hoaDon.CodeMoi;
                    //        break;
                    //    case 2:
                    //        break;
                    //    case 3:
                    //        break;
                    //    case 4:
                    //        break;
                    //    case 5:
                    //        break;
                    //    case 6:
                    //        break;
                    //    case 7: break;
                    //    case 8: break;
                    //    case 9:
                    //        break;
                    //    case 10: break;
                    //    case 11: break;
                    //    case 12:
                    //        break;


                    //}
                    count++;
                }

            }
            catch { }

            return listDocSo;
        }

        public ObservableCollection<int> getDistinctYearServer()
        {
            ObservableCollection<int> lstYear = new ObservableCollection<int>();
            var items = (from x in serverContext.DocSos
                         select x.Nam).Distinct().OrderByDescending(x => x).ToList();
            foreach (int item in items)
                lstYear.Add(item);
            return lstYear;
        }
        public ObservableCollection<String> getDistinctMonth()
        {
            ObservableCollection<String> lstMonth = new ObservableCollection<String>();
            var items = (from x in localContext.DocSoLocals
                         select x.Ky).Distinct().ToList();
            foreach (String item in items)
                lstMonth.Add(item);
            return lstMonth;
        }
        public ObservableCollection<String> getDistinctMonthServer(int year)
        {
            ObservableCollection<String> lstMonth = new ObservableCollection<String>();
            var items = (from x in serverContext.DocSos
                         where x.Nam == year
                         select x.Ky).Distinct().ToList();
            foreach (String item in items)
                lstMonth.Add(item);
            return lstMonth;
        }
        public ObservableCollection<String> getDistinctDate()
        {
            ObservableCollection<String> lstDate = new ObservableCollection<String>();
            var items = (from x in localContext.DocSoLocals
                         select x.Dot).Distinct().ToList();
            foreach (String item in items)
                lstDate.Add(item);
            return lstDate;
        }
        public ObservableCollection<String> getDistinctDateServer(int year, String month)
        {
            ObservableCollection<String> lstDate = new ObservableCollection<String>();
            var items = (from x in serverContext.DocSos
                         where x.Nam == year && x.Ky == month
                         select x.Dot).Distinct().ToList();
            foreach (String item in items)
                lstDate.Add(item);
            return lstDate;
        }
        public ObservableCollection<int> getDistinctGroup()
        {
            ObservableCollection<int> lstGroup = new ObservableCollection<int>();

            var items = (from x in localContext.DocSoLocals
                         select x.TODS).Distinct().ToList();
            foreach (int item in items)
                lstGroup.Add(item);

            return lstGroup;
        }
        public ObservableCollection<String> getDistinctMachine()
        {
            ObservableCollection<String> lstGroup = new ObservableCollection<String>();

            var items = (from x in localContext.DocSoLocals
                         select x.May).Distinct().ToList();
            foreach (String item in items)
                lstGroup.Add(item);

            return lstGroup;
        }
        public ObservableCollection<int> getDistinctGroupServer(int year, String month, String date)
        {
            ObservableCollection<int> lstGroup = new ObservableCollection<int>();
            var items = (from x in serverContext.DocSos
                         where x.Nam == year && x.Ky == month && x.Dot == date
                         select x.TODS).Distinct().ToList();
            foreach (int item in items)
                lstGroup.Add(item);
            return lstGroup;
        }
        public ObservableCollection<String> getDistinctMachineServer(int year, String month, String date, int xGroup)
        {
            ObservableCollection<String> lstMachine = new ObservableCollection<String>();
            List<String> items;
            if (xGroup == 0)
                items = (from x in serverContext.DocSos
                         where x.Nam == year && x.Ky == month && x.Dot == date
                         select x.May).Distinct().ToList();
            else
                items = (from x in serverContext.DocSos
                         where x.Nam == year && x.Ky == month && x.Dot == date && x.TODS == xGroup
                         select x.May).Distinct().ToList();
            foreach (String item in items)
                lstMachine.Add(item);
            return lstMachine;
        }
        public ObservableCollection<SoDaNhan> getDistinctSoDaNhan(int year, String month, String date, int xGroup)
        {
            ObservableCollection<SoDaNhan> listSoDaNhan = new ObservableCollection<SoDaNhan>();
            var soDaNhans = (from x in localContext.SoDaNhans
                             where x.nam == year && x.ky == month && x.dot == date && x.ToDS == xGroup
                             select x).ToList();
            foreach (var item in soDaNhans)
                listSoDaNhan.Add(item);
            return listSoDaNhan;
        }

    }
}

