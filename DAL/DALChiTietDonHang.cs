using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DALChiTietDonHang
    {
        QL_VLXDDataContext vlxd = new QL_VLXDDataContext();
        public DALChiTietDonHang()
        {

        }

        public List<ChiTietDonHang> LoadChiTietDonHang(string maDonHang)
        {
            return vlxd.ChiTietDonHangs.Where(ct => ct.MaDonHang == maDonHang).ToList();
        }

    }
}
