using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DALSanPham
    {
        QL_VLXDDataContext vlxd = new QL_VLXDDataContext();
        public DALSanPham()
        {

        }
        public List<SanPham> LoadSanPham()
        {
            return vlxd.SanPhams.ToList();
        }
        public bool ThemSanPham(SanPham pSanpham)
        {
            try
            {
                vlxd.SanPhams.InsertOnSubmit(pSanpham);
                vlxd.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        public bool CapNhatSanPham(string pMaSanPham, string pTenSanPham, int pDonGia, int pSoLuongTon,string pMoTa, string pHinhAnh, string pMaNhaCungCap)
        {
            SanPham CapNhat = vlxd.SanPhams.Where(sp => sp.MaSanPham == pMaSanPham).FirstOrDefault();
            if (CapNhat != null)
            {
                CapNhat.TenSanPham = pTenSanPham;
                CapNhat.DonGia = pDonGia;
                CapNhat.SoLuongTon = pSoLuongTon;
                CapNhat.MoTa = pMoTa;
                CapNhat.HinhAnh = pHinhAnh;
                CapNhat.MaNhaCungCap = pMaNhaCungCap;
                vlxd.SubmitChanges();
                return true;
            }
            return false;
        }
        public bool XoaSanPham(string pMaSanPham)
        {
            SanPham Xoa = vlxd.SanPhams.Where(sp => sp.MaSanPham == pMaSanPham).FirstOrDefault();
            if (Xoa != null)
            {
                vlxd.SanPhams.DeleteOnSubmit(Xoa);
                vlxd.SubmitChanges();
                return true;
            }
            return false;
        }

        public List<SanPham> TimKiemSanPham(string tenSanPham)
        {
            return vlxd.SanPhams.Where(x => x.TenSanPham.Contains(tenSanPham)).ToList();
        }


        public List<SanPham> LoadSanPhamTheoNhaCungCap(string maNhaCungCap)
        {
            return vlxd.SanPhams.Where(sp => sp.MaNhaCungCap == maNhaCungCap).ToList();
        }

    }
}
