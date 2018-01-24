using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MyBaoThay
    {
        public String DanhBa { get; set; }
        public String LoaiBaoThay { get; set; }
        public String ChiSoGo { get; set; }
        public String ChiSoGan { get; set; }
        public String SoThanMoi { get; set; }
        public String NgayThay { get; set; }
        public String NgayCapNhat { get; set; }

        public MyBaoThay(string danhBa, string loaiBaoThay, string chiSoGo, string chiSoGan, string soThanMoi, string ngayThay, string ngayCapNhat)
        {
            DanhBa = danhBa;
            LoaiBaoThay = loaiBaoThay;
            ChiSoGo = chiSoGo;
            ChiSoGan = chiSoGan;
            SoThanMoi = soThanMoi;
            NgayThay = ngayThay;
            NgayCapNhat = ngayCapNhat;
        }
    }
}
