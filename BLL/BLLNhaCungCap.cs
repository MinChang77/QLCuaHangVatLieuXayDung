using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class BLLNhaCungCap
    {
        DALNhaCungCap dalnhacungcap = new DALNhaCungCap();
        public BLLNhaCungCap()
        {

        }
        public List<NhaCungCap> GetNhaCungCaps()
        {
            return dalnhacungcap.LoadNhaCungCap();
        }

        public bool KTKC(string maNhaCungCap)
        {
            List<NhaCungCap> nhaCungCaps = GetNhaCungCaps();
            return nhaCungCaps.Any(ncc => ncc.MaNhaCungCap == maNhaCungCap);
        }

        public bool Them(NhaCungCap pNhaCungCap)
        {
            return dalnhacungcap.ThemNhaCungCap(pNhaCungCap);
        }

        public bool CapNhat(string pMaNhaCungCap, string pTenNhaCungCap, string pDiaChi, string pEmail, string pSoDienThoai, string pLoaiSanPhamCungCap,  string pDonViTinh)
        {
            return dalnhacungcap.CapNhatNhaCungCap(pMaNhaCungCap, pTenNhaCungCap, pDiaChi, pEmail, pSoDienThoai, pLoaiSanPhamCungCap, pDonViTinh);
        }

        public bool Xoa(string pMaNhaCungCap)
        {
            return dalnhacungcap.XoaNhaCungCap(pMaNhaCungCap);
        }

        public List<NhaCungCap> TimKiemNhaCungCap(string pTenNhaCungCap)
        {
            return dalnhacungcap.TimKiemNhaCungCap(pTenNhaCungCap);
        }
    }
}
