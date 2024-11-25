using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class BLLChiTietDonHang
    {
        DALChiTietDonHang dalchitietdonhang = new DALChiTietDonHang();
        public BLLChiTietDonHang()
        {

        }

        public List<ChiTietDonHang> GetChiTietDonHangs(string maDonHang)
        {
            return dalchitietdonhang.LoadChiTietDonHang(maDonHang);
        }

    }
}
