using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class BLLDonHang
    {
        DALDonHang daldonhang = new DALDonHang();   
        public BLLDonHang()
        {

        }

        public List<DonHang> GetDonHangs()
        {
            return daldonhang.LoadDonHang();
        }
        public List<DonHang> TimKiemDonHang(string maKhachHang, string maNhanVien)
        {
            return daldonhang.TimKiemDonHangTheoKhachHangHoacNhanVien(maKhachHang, maNhanVien);
        }

    }
}
