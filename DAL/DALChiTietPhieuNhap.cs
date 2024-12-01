using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
    

namespace DAL
{
    public class DALChiTietPhieuNhap
    {
        QL_VLXDDataContext vlxd = new QL_VLXDDataContext();
        public DALChiTietPhieuNhap() { }

        public List<ChiTietPhieuNhap> LoadChiTietPhieuNhap(string maPhieuNhap)
        {
            return vlxd.ChiTietPhieuNhaps.Where(ct => ct.MaPhieuNhap == maPhieuNhap).ToList();
        }
        public bool ThemChiTietPhieuNhap(ChiTietPhieuNhap pChiTietPhieuNhap)
        {
            try
            {
                vlxd.ChiTietPhieuNhaps.InsertOnSubmit(pChiTietPhieuNhap);
                vlxd.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool CapNhatChiTietPhieuNhap(string pMaPhieuNhap, string pMaSanPham, int pSoLuong, int pDonGia )
        {
            ChiTietPhieuNhap CapNhat = vlxd.ChiTietPhieuNhaps.Where(ctph => ctph.MaPhieuNhap == pMaPhieuNhap).FirstOrDefault();
            if (CapNhat != null)
            {
                CapNhat.MaSanPham = pMaSanPham;
                CapNhat.SoLuong = pSoLuong;
                CapNhat.DonGia = pDonGia;
                vlxd.SubmitChanges();
                return true;
            }
            return false;
        }
        public bool XoaChiTietPhieuNhap(string pMaPhieuNhap)
        {
            ChiTietPhieuNhap Xoa = vlxd.ChiTietPhieuNhaps.Where(ctph => ctph.MaPhieuNhap == pMaPhieuNhap).FirstOrDefault();
            if (Xoa != null)
            {
                vlxd.ChiTietPhieuNhaps.DeleteOnSubmit(Xoa);
                vlxd.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}
