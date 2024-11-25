using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class BLLNhanVien
    {
        DALNhanVien dalnhanvien = new DALNhanVien();
        public BLLNhanVien()
        {

        }
        public List<NhanVien> GetNhanViens()
        {
            return dalnhanvien.LoadNhanVien();
        }

        public bool DangNhap(string soDienThoai, string matKhau, out string chucVu)
        {
            return dalnhanvien.KiemTraDangNhap(soDienThoai, matKhau, out chucVu);
        }

        public bool KTKC(string maNhanVien)
        {
            List<NhanVien> nhanViens = GetNhanViens();
            return nhanViens.Any(nv => nv.MaNhanVien == maNhanVien);
        }

        public bool Them(NhanVien nv)
        {
            return dalnhanvien.ThemNhanVien(nv);
        }

        public bool CapNhat(string maNhanVien, string tenNhanVien, string diaChi, string email, string chucVu, string soDienThoai, string matKhau)
        {
            return dalnhanvien.CapNhatNhanVien(maNhanVien, tenNhanVien, diaChi, email, chucVu, soDienThoai, matKhau);
        }

        public bool Xoa(string maNhanVien)
        {
            return dalnhanvien.XoaNhanVien(maNhanVien);
        }

        public List<NhanVien> TimKiemNhanVien(string tenNhanVien)
        {
            return dalnhanvien.TimKiemNhanVien(tenNhanVien);
        }
    }
}
