using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;

namespace GiaoDien.MenuTab
{
    public partial class frmDonHang : Form
    {
        BLLDonHang blldonhang = new BLLDonHang();
        BLLChiTietDonHang bllchitietdonhang = new BLLChiTietDonHang();
        BLLKhachHang bllkhachhang = new BLLKhachHang();
        BLLNhanVien bllnhanvien = new BLLNhanVien();
        public frmDonHang()
        {
            InitializeComponent();
            this.Load += FrmDonHang_Load;
            btnCapNhat.Click += BtnCapNhat_Click;
            btnTimKiem.Click += BtnTimKiem_Click;
            btnLamMoi.Click += BtnLamMoi_Click;
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            LoadDonHang();
            ClearFields();
        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
           
            string trangThai = cboTrangThai.SelectedValue?.ToString();

            
            if (cboTrangThai.SelectedIndex == -1) trangThai = null;

            List<DonHang> donHangs = blldonhang.TimKiemDonHang(trangThai);

            dgvDonHang.DataSource = donHangs;
            dgvChiTietDonHang.DataSource = null;
        }

        private void BtnCapNhat_Click(object sender, EventArgs e)
        {
            string maDonHang = txtMaDonHang.Text;
            string trangThai = cboTrangThai.SelectedItem.ToString();

            if (string.IsNullOrEmpty(maDonHang) || string.IsNullOrEmpty(trangThai))
            {
                MessageBox.Show("Vui lòng chọn đơn hàng và trạng thái trước khi cập nhật!");
                return;
            }

            bool ketQua = blldonhang.CapNhatTrangThai(maDonHang, trangThai);

            if (ketQua)
            {
                if (trangThai == "Đang vận chuyển" || trangThai == "Giao hàng thành công")
                {
                    var chiTietDonHangs = bllchitietdonhang.GetChiTietDonHangs(maDonHang);
                    
                    foreach (var chiTiet in chiTietDonHangs)
                    {
                        string message;
                        bool KiemTra = bllchitietdonhang.CapNhatSoLuongSanPham(chiTiet.MaSanPham, chiTiet.SoLuong ?? 0, out message);

                        if (!KiemTra) 
                        {
                            MessageBox.Show($"Mã sản phẩm: {chiTiet.MaSanPham} - {message}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return; 
                        }
                    }
                }

                MessageBox.Show("Cập nhật trạng thái đơn hàng thành công!");
                LoadDonHang();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Cập nhật trạng thái thất bại! Đơn hàng không tồn tại.");
            }
        }

        private void FrmDonHang_Load(object sender, EventArgs e)
        {
            LoadDonHang();
            LoadComboboxNhanVien();
            LoadComboboxTrangThai();
        }
        public void LoadDonHang()
        {
            dgvDonHang.DataSource = blldonhang.GetDonHangs();
        }
        private void LoadComboboxKhachHang()
        {
            cboKhachHang.DataSource = bllkhachhang.GetKhachHangs();
            cboKhachHang.DisplayMember = "TenKhachHang";
            cboKhachHang.ValueMember = "MaKhachHang";
        }
        private void LoadComboboxNhanVien()
        {
            cboNhanVien.DataSource = bllnhanvien.GetNhanViens();
            cboNhanVien.DisplayMember = "TenNhanVien";
            cboNhanVien.ValueMember = "MaNhanVien";
        }

        private void LoadComboboxTrangThai()
        {
            var trangThaiList = new List<string>
            {
                "Đang vận chuyển",
                "Giao hàng thành công",
                "Hủy đơn",
                "Chưa xác nhận"
            };
            cboTrangThai.DataSource = trangThaiList;

            cboTrangThai.SelectedIndex = -1;
        }


        private void ClearFields()
        {
            txtMaDonHang.Clear();
            txtTongTien.Clear();
            txtNgayLap.Clear();
            cboKhachHang.SelectedIndex = -1;
            cboNhanVien.SelectedIndex = -1;
            cboTrangThai.SelectedIndex = -1;
            cboKhachHang.Enabled = true;
            cboNhanVien.Enabled = true;
        }

        private void dgvDonHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string maDonHang = dgvDonHang.Rows[e.RowIndex].Cells["MaDonHang"].Value.ToString();

                var chiTietDonHangList = bllchitietdonhang.GetChiTietDonHangs(maDonHang);

                dgvChiTietDonHang.DataSource = chiTietDonHangList;

                txtMaDonHang.Text = dgvDonHang.CurrentRow.Cells["MaDonHang"].Value.ToString();
                txtTongTien.Text = dgvDonHang.CurrentRow.Cells["TongTien"].Value.ToString();
                txtNgayLap.Text = dgvDonHang.CurrentRow.Cells["NgayLap"].Value.ToString();
                cboKhachHang.SelectedValue = dgvDonHang.CurrentRow.Cells["MaKhachHang"].Value.ToString();
                cboNhanVien.SelectedValue = dgvDonHang.CurrentRow.Cells["MaNhanVien"].Value.ToString();
                cboTrangThai.SelectedItem = dgvDonHang.CurrentRow.Cells["TrangThai"].Value.ToString();
                cboKhachHang.Enabled = false;
                cboNhanVien.Enabled = false;
                LoadComboboxKhachHang();
            }
        }

        
    }
}
