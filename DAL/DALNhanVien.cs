using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DALNhanVien
    {
        QL_VLXDDataContext vlxd = new QL_VLXDDataContext();
        public DALNhanVien()
        {

        }

        public List<NhanVien> LoadNhanVien()
        {
            return vlxd.NhanViens.ToList();
        }


        public bool KiemTraDangNhap(string soDienThoai, string matKhau, out string chucVu)
        {
           
            var nhanVien = vlxd.NhanViens.FirstOrDefault(nv => nv.SoDienThoai == soDienThoai && nv.MatKhau == matKhau);

            if (nhanVien != null)
            {
                chucVu = nhanVien.ChucVu;
                return true; 
            }

            chucVu = null;
            return false;
        }

        public string GetMaNhanVienBySoDienThoai(string soDienThoai)
        {
            var nhanVien = vlxd.NhanViens.FirstOrDefault(nv => nv.SoDienThoai == soDienThoai);
            return nhanVien.MaNhanVien;
        }


        public bool ThemNhanVien(NhanVien nv)
        {
            try
            {
                vlxd.NhanViens.InsertOnSubmit(nv);
                vlxd.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CapNhatNhanVien(string maNhanVien, string tenNhanVien, string diaChi, string email, string chucVu, string soDienThoai, string matKhau)
        {
            NhanVien nv = vlxd.NhanViens.FirstOrDefault(n => n.MaNhanVien == maNhanVien);
            if (nv != null)
            {
                nv.TenNhanVien = tenNhanVien;
                nv.DiaChi = diaChi;
                nv.Email = email;
                nv.ChucVu = chucVu;
                nv.SoDienThoai = soDienThoai;
                nv.MatKhau = matKhau;
                vlxd.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool XoaNhanVien(string maNhanVien)
        {
            NhanVien nv = vlxd.NhanViens.FirstOrDefault(n => n.MaNhanVien == maNhanVien);
            if (nv != null)
            {
                vlxd.NhanViens.DeleteOnSubmit(nv);
                vlxd.SubmitChanges();
                return true;
            }
            return false;
        }

        public List<NhanVien> TimKiemNhanVien(string tenNhanVien)
        {
            return vlxd.NhanViens.Where(n => n.TenNhanVien.Contains(tenNhanVien)).ToList();
        }
    }
}
