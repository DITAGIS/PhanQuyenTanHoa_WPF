using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class GetDataDBViewModel
    {


        private static GetDataDBViewModel _instance;
        public static GetDataDBViewModel getInstance
        {
            get
            {
                if (_instance == null)
                    _instance = new GetDataDBViewModel();
                return _instance;
            }
        }
        private GetDataDBViewModel() { }

        public bool getHoaDonsByCondition(int year, String month, String date, int xGroup)
        {

            bool result = false;
            try
            {
                DataClassServerDataContext serverContext = new DataClassServerDataContext();
                var getData = (from x in serverContext.DocSos
                               where x.Nam == year && x.Ky == month && x.Dot == date && x.TODS == xGroup
                               select x).ToList();

                DataClassesLocalDataContext localContext = new DataClassesLocalDataContext();
                foreach (var item in getData)
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
                        Dot = item.Dot
                    });

                    localContext.SubmitChanges();
                }
            }
            catch (Exception e)
            {

            }
            return result;
        }



    }
}

