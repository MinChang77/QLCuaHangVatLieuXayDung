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
            btnTimKiem.Click += BtnTimKiem_Click;
            
        }

       
        private void FrmDonHang_Load(object sender, EventArgs e)
        {
            LoadDonHang();
            LoadComboboxKhachHang();
            LoadComboboxNhanVien();

        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            string maKhachHang = cboKhachHang.SelectedValue?.ToString();
            string maNhanVien = cboNhanVien.SelectedValue?.ToString();
            dgvDonHang.DataSource = blldonhang.TimKiemDonHang(maKhachHang, maNhanVien);
            cboKhachHang.SelectedIndex = -1;
            cboNhanVien.SelectedIndex = -1;
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

        private void ClearFields()
        {
            txtMaDonHang.Clear();
            txtTongTien.Clear();
            txtNgayLap.Clear();
            cboKhachHang.SelectedIndex = -1;
            cboNhanVien.SelectedIndex = -1;
        }

        private void dgvDonHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy giá trị của cột MaDonHang trong hàng được chọn
                string maDonHang = dgvDonHang.Rows[e.RowIndex].Cells["MaDonHang"].Value.ToString();

                // Lấy dữ liệu chi tiết đơn hàng từ BLL
                var chiTietDonHangList = bllchitietdonhang.GetChiTietDonHangs(maDonHang);

                // Gán dữ liệu cho dgvChiTietDonHang
                dgvChiTietDonHang.DataSource = chiTietDonHangList;

                txtMaDonHang.Text = dgvDonHang.CurrentRow.Cells["MaDonHang"].Value.ToString();
                txtTongTien.Text = dgvDonHang.CurrentRow.Cells["TongTien"].Value.ToString();
                txtNgayLap.Text = dgvDonHang.CurrentRow.Cells["NgayLap"].Value.ToString();
                cboKhachHang.SelectedValue = dgvDonHang.CurrentRow.Cells["MaKhachHang"].Value.ToString();
                cboNhanVien.SelectedValue = dgvDonHang.CurrentRow.Cells["MaNhanVien"].Value.ToString();

            }
        }

       
    }
}
