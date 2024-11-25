using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class BLLKhachHang
    {
        DALKhachHang dalkhachhang = new DALKhachHang();
        public BLLKhachHang()
        {

        }

        public List<KhachHang> GetKhachHangs()
        {
            return dalkhachhang.LoadKhachHang();
        }

        public bool KTKC(string maKhachHang)
        {
            List<KhachHang> khachHangs = GetKhachHangs();
            return khachHangs.Any(kh => kh.MaKhachHang == maKhachHang);
        }

        public bool Them(KhachHang pKhachHang)
        {
            return dalkhachhang.ThemKhachHang(pKhachHang);
        }

        public bool CapNhat(string pMaKhachHang, string pTenKhachHang, string pDiaChi, string pEmail, string pSoDienThoai, string pMatKhau)
        {
            return dalkhachhang.CapNhatKhachHang(pMaKhachHang, pTenKhachHang, pDiaChi, pEmail, pSoDienThoai, pMatKhau);
        }

       
        public bool Xoa(string pMaKhachHang)
        {
            return dalkhachhang.XoaKhachHang(pMaKhachHang);
        }

        
        public List<KhachHang> TimKiemKhachHang(string pTenKhachHang)
        {
            return dalkhachhang.TimKiemKhachHang(pTenKhachHang);
        }
    }
}
