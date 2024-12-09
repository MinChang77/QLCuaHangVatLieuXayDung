using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BLL;
using System.Text.RegularExpressions;

namespace GiaoDien.MenuTab
{
    public partial class frmNhanVien : Form
    {
        BLLNhanVien bllnhanvien = new BLLNhanVien();
        public frmNhanVien()
        {
            InitializeComponent();
            this.Load += FrmNhanVien_Load;
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
            string tenNhanVien = txtTenNhanVien.Text;
            List<NhanVien> nhanViens = bllnhanvien.TimKiemNhanVien(tenNhanVien);
            dgvNhanVien.DataSource = nhanViens;

            if (nhanViens.Count == 0)
            {
                MessageBox.Show("Không tìm thấy nhân viên nào với tên này.");
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            
            if (bllnhanvien.Xoa(txtMaNhanVien.Text))
            {
                MessageBox.Show("Xóa thành công!");
                LoadNhanVien();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Xóa không thành công.");
            }
        }

        private void BtnCapNhat_Click(object sender, EventArgs e)
        {
            NhanVien nv = new NhanVien
            {
                MaNhanVien = txtMaNhanVien.Text,
                TenNhanVien = txtTenNhanVien.Text,
                DiaChi = txtDiaChi.Text,
                Email = txtEmail.Text,
                SoDienThoai = txtSoDienThoai.Text,
                MatKhau = txtMatKhau.Text,
                ChucVu = txtChucVu.Text
            };

            if (bllnhanvien.CapNhat(nv.MaNhanVien, nv.TenNhanVien, nv.DiaChi, nv.Email, nv.ChucVu, nv.SoDienThoai, nv.MatKhau))
            {
                MessageBox.Show("Cập nhật thành công!");
                LoadNhanVien();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Cập nhật không thành công.");
            }
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNhanVien.Text) || string.IsNullOrWhiteSpace(txtTenNhanVien.Text) ||
                string.IsNullOrWhiteSpace(txtDiaChi.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtSoDienThoai.Text) || string.IsNullOrWhiteSpace(txtMatKhau.Text) ||
                string.IsNullOrWhiteSpace(txtChucVu.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            if (!KiemTraSoDienThoai(txtSoDienThoai.Text))
            {
                MessageBox.Show("Số điện thoại không đúng định dạng");
                return;
            }

            if (!KiemTraEmail(txtEmail.Text))
            {
                MessageBox.Show("Email phải có định dạng @gmail.com");
                return;
            }

            NhanVien nv = new NhanVien
            {
                MaNhanVien = txtMaNhanVien.Text,
                TenNhanVien = txtTenNhanVien.Text,
                DiaChi = txtDiaChi.Text,
                Email = txtEmail.Text,
                ChucVu = txtChucVu.Text,
                SoDienThoai = txtSoDienThoai.Text,
                MatKhau = txtMatKhau.Text
            };

            if (bllnhanvien.KTKC(nv.MaNhanVien))
            {
                MessageBox.Show("Mã nhân viên đã tồn tại.");
                return;
            }

            if (bllnhanvien.Them(nv))
            {
                MessageBox.Show("Thêm thành công!");
                LoadNhanVien();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Thêm không thành công.");
            }
        }

        private void FrmNhanVien_Load(object sender, EventArgs e)
        {
            LoadNhanVien();
        }

        public void LoadNhanVien()
        {
            dgvNhanVien.DataSource = bllnhanvien.GetNhanViens();
            if (dgvNhanVien.Columns["MatKhau"] != null)
            {
                dgvNhanVien.Columns["MatKhau"].Visible = false;
            }
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
            txtMaNhanVien.Clear();
            txtTenNhanVien.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
            txtChucVu.Clear();
            txtSoDienThoai.Clear();
            txtMatKhau.Clear();
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvNhanVien.Rows[e.RowIndex];
                txtMaNhanVien.Text = row.Cells["MaNhanVien"].Value.ToString(); 
                txtTenNhanVien.Text = row.Cells["TenNhanVien"].Value.ToString();
                txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtChucVu.Text = row.Cells["ChucVu"].Value.ToString();
                txtSoDienThoai.Text = row.Cells["SoDienThoai"].Value.ToString();
                txtMatKhau.Text = "********";
            }
        }

    }
    
}
