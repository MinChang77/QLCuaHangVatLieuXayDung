using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DALPhieuNhap
    {
        QL_VLXDDataContext vlxd = new QL_VLXDDataContext();
        public DALPhieuNhap() { }

        public List<PhieuNhap> LoadPhieuNhap()
        {
            return vlxd.PhieuNhaps.ToList();
        }
        public bool ThemPhieuNhap(PhieuNhap phieuNhap)
        {
            try
            {
                vlxd.PhieuNhaps.InsertOnSubmit(phieuNhap);
                vlxd.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public string GetLastMaPhieuNhap()
        {
            using (var context = new QL_VLXDDataContext())
            {
                var lastPhieuNhap = context.PhieuNhaps.OrderByDescending(pn => pn.MaPhieuNhap).FirstOrDefault();
                return lastPhieuNhap?.MaPhieuNhap;
            }
        }
    }
}
