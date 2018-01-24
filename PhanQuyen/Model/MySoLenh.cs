using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MySoLenh
    {
        public String SoLenh { get; set; }
        public String LoaiPhieu { get; set; }
        public String CSDongMo { get; set; }
        public String NgayThucHien { get; set; }
        public String NoiDung { get; set; }
        public String Hieu { get; set; }
        public String Co { get; set; }
        public String NgayNhap { get; set; }

        public MySoLenh(string soLenh, string loaiPhieu, string cSDongMo, string ngayThucHien, string noiDung, string hieu, string co, string ngayNhap)
        {
            SoLenh = soLenh;
            LoaiPhieu = loaiPhieu;
            CSDongMo = cSDongMo;
            NgayThucHien = ngayThucHien;
            NoiDung = noiDung;
            Hieu = hieu;
            Co = co;
            NgayNhap = ngayNhap;
        }
    }
}
