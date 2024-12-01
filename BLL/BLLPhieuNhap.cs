using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class BLLPhieuNhap
    {
        DALPhieuNhap dalphieunhap = new DALPhieuNhap();
        public BLLPhieuNhap() 
        { 

        }
        public bool KTKC(string maPhieuNhap)
        {
            List<PhieuNhap> phieuNhaps = GetPhieuNhaps();
            return phieuNhaps.Any(sp => sp.MaPhieuNhap == maPhieuNhap);
        }
        public List<PhieuNhap> GetPhieuNhaps()
        {
            return dalphieunhap.LoadPhieuNhap();
        }
        public bool Them(PhieuNhap phieuNhap)
        {
            return dalphieunhap.ThemPhieuNhap(phieuNhap);
        }
        public string GetLastMaPhieuNhap()
        {
            return dalphieunhap.GetLastMaPhieuNhap();
        }
    }
}
