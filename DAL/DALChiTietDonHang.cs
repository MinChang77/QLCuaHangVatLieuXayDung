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

        public bool CapNhatSoLuongSanPham(string maSanPham, int soLuong, out string message)
        {
            message = string.Empty; // Khởi tạo thông báo rỗng
            try
            {
                var sanPham = vlxd.SanPhams.SingleOrDefault(sp => sp.MaSanPham == maSanPham);

                if (sanPham != null)
                {
                    if (sanPham.SoLuongTon < soLuong)
                    {
                        message = "Không đủ số lượng cung cấp! Bạn cần nhập thêm hàng.";
                        return false; 
                    }

                    sanPham.SoLuongTon -= soLuong;

                    vlxd.SubmitChanges();
                    return true;
                }

                message = "Sản phẩm không tồn tại.";
                return false;
            }
            catch
            {
                message = "Đã xảy ra lỗi khi cập nhật số lượng sản phẩm.";
                return false;
            }
        }


    }
}
