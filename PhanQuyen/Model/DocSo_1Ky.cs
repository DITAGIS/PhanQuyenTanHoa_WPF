using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DocSo_1Ky
    {
       
        public String KyNam { get; set; }
        public String NgayDoc { get; set; }
        public String Code { get; set; }
        public String ChiSo { get; set; }
        public String TieuThu { get; set; }

        public DocSo_1Ky(string kyNam, string ngayDoc, string code, string chiSo, string tieuThu)
        {
            KyNam = kyNam;
            NgayDoc = ngayDoc;
            Code = code;
            ChiSo = chiSo;
            TieuThu = tieuThu;
        }
    }
}
