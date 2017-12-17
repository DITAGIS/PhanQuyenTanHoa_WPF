using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class HoaDon
    {
        public String DanhBa { get; set; }
        public String TTHDNCu { get; set; }
        public String TTHDNMoi { get; set; }
        public String CodeCu { get; set; }
        public String CodeMoi { get; set; }
        public String CSC { get; set; }
        public String CSM { get; set; }
        public String TieuThuMoi { get; set; }
        public String TBTT { get; set; }
        public String DiaChi { get; set; }
        public String GhiChuDS { get; set; }
        public String TenKH { get; set; }
        public String HopDong { get; set; }
        public String Hieu { get; set; }
        public String Co { get; set; }
        public String GB { get; set; }
        public String DM { get; set; }
        public String SoThan { get; set; }
        public String MLT { get; set; }
        public Byte[] Image { get; set; }
        public HoaDon(string danhBa, string tTHDNCu, string tTHDNMoi, string codeCu, string codeMoi, string cSC, string cSM, string tieuThuMoi, string tBTT, string diaChi, string ghiChuDS)
        {
            this.DanhBa = danhBa;
            TTHDNCu = tTHDNCu;
            TTHDNMoi = tTHDNMoi;
            CodeCu = codeCu;
            CodeMoi = codeMoi;
            CSC = cSC;
            CSM = cSM;
            TieuThuMoi = tieuThuMoi;
            TBTT = tBTT;
            DiaChi = diaChi;
            GhiChuDS = ghiChuDS;
        }
        public HoaDon()
        {

        }
    }
}
