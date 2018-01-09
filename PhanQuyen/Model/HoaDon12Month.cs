using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class HoaDon12Month
    {
        public String DanhBa { get; set; }
        public String Month1 { get; set; }
        public String Year1 { get; set; }
        public String NgayDoc1 { get; set; }
        public String Code1 { get; set; }
        public String ChiSo1 { get; set; }
        public String TieuThu1 { get; set; }
        public String Month2 { get; set; }
        public String Year2 { get; set; }
        public String NgayDoc2 { get; set; }
        public String Code2 { get; set; }
        public String ChiSo2 { get; set; }
        public String TieuThu2 { get; set; }

        public HoaDon12Month()
        {

        }

        public HoaDon12Month(string danhBa, string code1)
        {
            DanhBa = danhBa;
            Code1 = code1;
        }
    }
}
