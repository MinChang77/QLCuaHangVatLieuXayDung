using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DALDonHang
    {
        QL_VLXDDataContext vlxd = new QL_VLXDDataContext();
        public DALDonHang()
        {

        }

        public List<DonHang> LoadDonHang()
        {
            return vlxd.DonHangs.ToList();
        }


        public List<DonHang> TimKiemDonHang(string trangThai)
        {
            return vlxd.DonHangs
            .Where(dh => (
                         trangThai == null || dh.TrangThai == trangThai))
            .ToList();
        }

        public List<DonHang> LayDonHangTheoKhoangThoiGian(DateTime tuNgay, DateTime denNgay)
        {
            return vlxd.DonHangs
               .Where(dh => dh.NgayLap >= tuNgay &&
                            dh.NgayLap <= denNgay &&
                            dh.TrangThai == "Giao hàng thành công")
               .ToList();
        }

        public bool CapNhatTrangThai(string maDonHang, string trangThai)
        {
            try
            {
                var donHang = vlxd.DonHangs.SingleOrDefault(dh => dh.MaDonHang == maDonHang);

                donHang.TrangThai = trangThai;

                vlxd.SubmitChanges();

                return true; 
            }
            catch
            {
                return false;
            }
        }

    }
}
