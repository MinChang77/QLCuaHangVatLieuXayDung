using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DALNhaCungCap
    {
        QL_VLXDDataContext vlxd = new QL_VLXDDataContext();
        public DALNhaCungCap()
        {

        }
        public List<NhaCungCap> LoadNhaCungCap()
        {
            return vlxd.NhaCungCaps.ToList();
        }
        public bool ThemNhaCungCap(NhaCungCap pNhaCungCap)
        {
            try
            {
                vlxd.NhaCungCaps.InsertOnSubmit(pNhaCungCap);
                vlxd.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CapNhatNhaCungCap(string pMaNhaCungCap, string pTenNhaCungCap, string pDiaChi, string pEmail, string pSoDienThoai, string pLoaiSanPhamCungCap, string pDonViTinh)
        {
            NhaCungCap ncc = vlxd.NhaCungCaps.Where(n => n.MaNhaCungCap == pMaNhaCungCap).FirstOrDefault();
            if (ncc != null)
            {
                ncc.TenNhaCungCap = pTenNhaCungCap;
                ncc.DiaChi = pDiaChi;
                ncc.Email = pEmail;
                ncc.SoDienThoai = pSoDienThoai;
                ncc.LoaiSanPhamCungCap = pLoaiSanPhamCungCap;
                ncc.DonViTinhSanPhamCungCap = pDonViTinh;
                vlxd.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool XoaNhaCungCap(string pMaNhaCungCap)
        {
            NhaCungCap ncc = vlxd.NhaCungCaps.Where(n => n.MaNhaCungCap == pMaNhaCungCap).FirstOrDefault();
            if (ncc != null)
            {
                vlxd.NhaCungCaps.DeleteOnSubmit(ncc);
                vlxd.SubmitChanges();
                return true;
            }
            return false;
        }

        public List<NhaCungCap> TimKiemNhaCungCap(string pTenNhaCungCap)
        {
            return vlxd.NhaCungCaps.Where(n => n.TenNhaCungCap.Contains(pTenNhaCungCap)).ToList();
        }
    }
}
