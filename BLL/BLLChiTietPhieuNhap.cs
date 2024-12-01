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
        public List<ChiTietPhieuNhap> GetChiTietPhieuNhaps(string maPhieuNhap)
        {
            return dalchitietphieunhap.LoadChiTietPhieuNhap(maPhieuNhap);
        }

        public bool Them(ChiTietPhieuNhap pChiTietPhieuNhap)
        {
            return dalchitietphieunhap.ThemChiTietPhieuNhap(pChiTietPhieuNhap);
        }
        public bool CapNhat(string pMaPhieuNhap, string pMaSanPham, int pSoLuong, int pDonGia )
        {
            return dalchitietphieunhap.CapNhatChiTietPhieuNhap(pMaPhieuNhap, pMaSanPham, pSoLuong, pDonGia);
        }

        public bool Xoa(string pMaPhieuNhap)
        {
            return dalchitietphieunhap.XoaChiTietPhieuNhap(pMaPhieuNhap);
        }
    }
}
