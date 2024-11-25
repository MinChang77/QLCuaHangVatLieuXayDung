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
        public bool LuuChiTietPhieuNhap(List<ChiTietPhieuNhap> chiTietPhieuNhaps)
        {
            try
            {
                vlxd.ChiTietPhieuNhaps.InsertAllOnSubmit(chiTietPhieuNhaps);
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
