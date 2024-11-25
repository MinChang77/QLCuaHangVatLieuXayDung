using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DALKhachHang
    {
        QL_VLXDDataContext vlxd = new QL_VLXDDataContext();
        public DALKhachHang()
        {

        }

        public List<KhachHang> LoadKhachHang()
        {
            return vlxd.KhachHangs.ToList();
        }

        public bool ThemKhachHang(KhachHang pKhachHang)
        {
            try
            {
                vlxd.KhachHangs.InsertOnSubmit(pKhachHang);
                vlxd.SubmitChanges();
                return true;
            }
            catch
            {
                return false; 
            }
        }

        public bool CapNhatKhachHang(string pMaKhachHang, string pTenKhachHang, string pDiaChi, string pEmail, string pSoDienThoai, string pMatKhau)
        {
            KhachHang kh = vlxd.KhachHangs.Where(k => k.MaKhachHang == pMaKhachHang).FirstOrDefault();
            if (kh != null)
            {
                kh.TenKhachHang = pTenKhachHang;
                kh.DiaChi = pDiaChi;
                kh.Email = pEmail;
                kh.SoDienThoai = pSoDienThoai;
                kh.MatKhau = pMatKhau;
                vlxd.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool XoaKhachHang(string pMaKhachHang)
        {
            KhachHang kh = vlxd.KhachHangs.Where(k => k.MaKhachHang == pMaKhachHang).FirstOrDefault();
            if (kh != null)
            {
                vlxd.KhachHangs.DeleteOnSubmit(kh);
                vlxd.SubmitChanges();
                return true;
            }
            return false;
        }

        
        public List<KhachHang> TimKiemKhachHang(string pTenKhachHang)
        {
            return vlxd.KhachHangs.Where(k => k.TenKhachHang.Contains(pTenKhachHang)).ToList();
        }
    }
}
