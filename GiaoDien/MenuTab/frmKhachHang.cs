using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;

namespace GiaoDien.MenuTab
{
    public partial class frmKhachHang : Form
    {
        BLLKhachHang bllkhachhang = new BLLKhachHang();
        public frmKhachHang()
        {
            InitializeComponent();
            this.Load += FrmKhachHang_Load;
            btnThem.Click += BtnThem_Click;
            btnCapNhat.Click += BtnCapNhat_Click;
            btnXoa.Click += BtnXoa_Click;
            btnTimKiem.Click += BtnTimKiem_Click;
            btnLamMoi.Click += BtnLamMoi_Click;
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            string tenKhachHang = txtTenKhachHang.Text;
            List<KhachHang> khachHangs = bllkhachhang.TimKiemKhachHang(tenKhachHang);
            dgvKhachHang.DataSource = khachHangs;

            if (khachHangs.Count == 0)
            {
                MessageBox.Show("Không tìm thấy khách hàng nào với tên này.");
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKhachHang.Text))
            {
                MessageBox.Show("Vui lòng nhập mã khách hàng để xóa.");
                return;
            }

            if (bllkhachhang.Xoa(txtMaKhachHang.Text))
            {
                MessageBox.Show("Xóa khách hàng thành công.");
                LoadKhachHang();
                ClearFields(); 
            }
            else
            {
                MessageBox.Show("Xóa khách hàng không thành công. Vui lòng kiểm tra mã khách hàng.");
            }
        }

        private void BtnCapNhat_Click(object sender, EventArgs e)
        {
            
            KhachHang kh = new KhachHang
            {
                MaKhachHang = txtMaKhachHang.Text,
                TenKhachHang = txtTenKhachHang.Text,
                DiaChi = txtDiaChi.Text,
                Email = txtEmail.Text,
                SoDienThoai = txtSoDienThoai.Text,
                MatKhau = txtMatKhau.Text
            };

            if (bllkhachhang.CapNhat(kh.MaKhachHang, kh.TenKhachHang, kh.DiaChi, kh.Email, kh.SoDienThoai, kh.MatKhau))
            {
                MessageBox.Show("Cập nhật khách hàng thành công.");
                LoadKhachHang();
                ClearFields(); 
            }
            else
            {
                MessageBox.Show("Cập nhật khách hàng không thành công.");
            }
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKhachHang.Text) ||
                string.IsNullOrWhiteSpace(txtTenKhachHang.Text) ||
                string.IsNullOrWhiteSpace(txtDiaChi.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtSoDienThoai.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            if (!KiemTraSoDienThoai(txtSoDienThoai.Text))
            {
                MessageBox.Show("Số điện thoại không hợp lệ!");
                return;
            }

            if (!KiemTraEmail(txtEmail.Text))
            {
                MessageBox.Show("Email phải có định dạng @gmail.com.");
                return;
            }

            KhachHang kh = new KhachHang
            {
                MaKhachHang = txtMaKhachHang.Text,
                TenKhachHang = txtTenKhachHang.Text,
                DiaChi = txtDiaChi.Text,
                Email = txtEmail.Text,
                SoDienThoai = txtSoDienThoai.Text,
                MatKhau = txtMatKhau.Text
            };

            if (bllkhachhang.KTKC(kh.MaKhachHang))
            {
                MessageBox.Show("Mã khách hàng đã tồn tại!");
                return;
            }

            if (bllkhachhang.Them(kh))
            {
                MessageBox.Show("Thêm khách hàng thành công.");
                LoadKhachHang();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Thêm khách hàng không thành công.");
            }
        }

        private void FrmKhachHang_Load(object sender, EventArgs e)
        {
            LoadKhachHang();
        }

        public void LoadKhachHang()
        {
            dgvKhachHang.DataSource = bllkhachhang.GetKhachHangs();
        }
        private bool KiemTraSoDienThoai(string soDienThoai)
        {
            return Regex.IsMatch(soDienThoai, @"^\d{10}$");
        }

        private bool KiemTraEmail(string email)
        {
            return email.EndsWith("@gmail.com");
        }

        private void ClearFields()
        {
            txtMaKhachHang.Clear();
            txtTenKhachHang.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
            txtSoDienThoai.Clear();
            txtMatKhau.Clear();
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];
                txtMaKhachHang.Text = row.Cells["MaKhachHang"].Value.ToString();
                txtTenKhachHang.Text = row.Cells["TenKhachHang"].Value.ToString();
                txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtSoDienThoai.Text = row.Cells["SoDienThoai"].Value.ToString();
                txtMatKhau.Text = "********";
            }
        }
    }
}
