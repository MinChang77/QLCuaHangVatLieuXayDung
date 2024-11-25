using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class BLLSanPham
    {
        DALSanPham dalsanpham = new DALSanPham();
        public BLLSanPham()
        {

        }
        public List<SanPham> GetSanPhams()
        {
            return dalsanpham.LoadSanPham();
        }

        public bool KTKC(string maSanPham)
        {
            List<SanPham> sanPhams = GetSanPhams();
            return sanPhams.Any(sp => sp.MaSanPham == maSanPham);
        }

        public bool Them(SanPham pSanPham)
        {
            return dalsanpham.ThemSanPham(pSanPham);
        }
        public bool CapNhat(string pMaSanPham, string pTenSanPham, int pDonGia, int pSoLuongTon, string pMoTa, string pMaNhaCungCap)
        {
            return dalsanpham.CapNhatSanPham(pMaSanPham, pTenSanPham, pDonGia, pSoLuongTon, pMoTa, pMaNhaCungCap);
        }

        public bool Xoa(string pMaSanPham)
        {
            return dalsanpham.XoaSanPham(pMaSanPham);
        }

        public List<SanPham> TimKiemSanPham(string pTenSanPham)
        {
            return dalsanpham.TimKiemSanPham(pTenSanPham);
        }

        public List<SanPham> GetSanPhamsByNhaCungCap(string maNhaCungCap)
        {
            return dalsanpham.LoadSanPhamTheoNhaCungCap(maNhaCungCap);
        }
    }
}
