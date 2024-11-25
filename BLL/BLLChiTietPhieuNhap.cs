using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class BLLChiTietPhieuNhap
    {
        DALChiTietPhieuNhap dalchitietphieunhap = new DALChiTietPhieuNhap();
        public BLLChiTietPhieuNhap()
        {

        }
        public List<ChiTietPhieuNhap> GetChiTietPhieuNhaps()
        {
            return dalchitietphieunhap.LoadChiTietPhieuNhap();
        }
    }
}
