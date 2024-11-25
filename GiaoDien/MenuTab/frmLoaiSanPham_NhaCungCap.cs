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
    public partial class frmLoaiSanPham_NhaCungCap : Form
    {
        BLLNhaCungCap bllNhaCungCap = new BLLNhaCungCap();
        public frmLoaiSanPham_NhaCungCap()
        {
            InitializeComponent();
            this.Load += FrmLoaiSanPham_NhaCungCap_Load;
            btnThem.Click += BtnThem_Click;
            btnCapNhat.Click += BtnCapNhat_Click;
            btnXoa.Click += BtnXoa_Click;
            btnTimKiem.Click += BtnTimKiem_Click;
        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            string tenNhaCungCap = txtTenNhaCungCap.Text;
            var ketQua = bllNhaCungCap.TimKiemNhaCungCap(tenNhaCungCap);

            if (ketQua.Count > 0)
            {
                dgvNhaCungCap.DataSource = ketQua;
            }
            else
            {
                MessageBox.Show("Không tìm thấy nhà cung cấp.");
                LoadNhaCungCap();
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvNhaCungCap.CurrentRow != null)
            {
                string maNhaCungCap = dgvNhaCungCap.CurrentRow.Cells["MaNhaCungCap"].Value.ToString();
                bllNhaCungCap.Xoa(maNhaCungCap);
                MessageBox.Show("Xóa thành công.");
                LoadNhaCungCap();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Chọn một nhà cung cấp để xóa.");
            }
        }

        private void BtnCapNhat_Click(object sender, EventArgs e)
        {
            if (dgvNhaCungCap.CurrentRow != null)
            {
                string maNhaCungCap = dgvNhaCungCap.CurrentRow.Cells["MaNhaCungCap"].Value.ToString();
                string tenNhaCungCap = txtTenNhaCungCap.Text;
                string diaChi = txtDiaChi.Text;
                string email = txtEmail.Text;
                string soDienThoai = txtSoDienThoai.Text;
                string loaiSanPhamCungCap = txtLoaiSanPhamCungCap.Text;
                string donViTinh = cboDonViTinh.SelectedItem.ToString();

                // Cập nhật dữ liệu
                if (bllNhaCungCap.CapNhat(maNhaCungCap, tenNhaCungCap, diaChi, email, soDienThoai, loaiSanPhamCungCap, donViTinh))
                {
                    MessageBox.Show("Cập nhật thành công.");
                    LoadNhaCungCap();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại.");
                }
            }
            else
            {
                MessageBox.Show("Chọn một nhà cung cấp để sửa.");
            }
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (txtMaNhaCungCap.Text == string.Empty || txtTenNhaCungCap.Text == string.Empty || txtDiaChi.Text == string.Empty || txtSoDienThoai.Text == string.Empty || txtEmail.Text == string.Empty || txtLoaiSanPhamCungCap.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            if (!KiemTraEmail(txtEmail.Text))
            {
                MessageBox.Show("Định dạng email không hợp lệ.");
                return;
            }

            
            if (!KiemTraSoDienThoai(txtSoDienThoai.Text))
            {
                MessageBox.Show("Định dạng số điện thoại không hợp lệ.");
                return;
            }

            // Tạo đối tượng gán dữ liệu
            NhaCungCap nccThem = new NhaCungCap
            {
                MaNhaCungCap = txtMaNhaCungCap.Text,
                TenNhaCungCap = txtTenNhaCungCap.Text,
                DiaChi = txtDiaChi.Text,
                Email = txtEmail.Text,
                SoDienThoai = txtSoDienThoai.Text,
                LoaiSanPhamCungCap = txtLoaiSanPhamCungCap.Text,
                DonViTinhSanPhamCungCap = cboDonViTinh.SelectedItem.ToString()
            };

            // Kiểm tra khóa chính
            if (bllNhaCungCap.KTKC(nccThem.MaNhaCungCap))
            {
                MessageBox.Show("Trùng khóa chính.");
                ClearFields();
                return;
            }

            // Thêm
            if (bllNhaCungCap.Them(nccThem))
            {
                MessageBox.Show("Thêm thành công.");
                LoadNhaCungCap();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Thêm thất bại.");
            }
        }

        private void FrmLoaiSanPham_NhaCungCap_Load(object sender, EventArgs e)
        {
            LoadNhaCungCap();
        }

        private void LoadNhaCungCap()
        {
            dgvNhaCungCap.DataSource = bllNhaCungCap.GetNhaCungCaps();
        }

        private void ClearFields()
        {
            txtMaNhaCungCap.Clear();
            txtTenNhaCungCap.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
            txtSoDienThoai.Clear();
            txtLoaiSanPhamCungCap.Clear();
            cboDonViTinh.SelectedIndex = -1;
        }

        private bool KiemTraEmail(string email)
        {
            return email.EndsWith("@gmail.com");
        }

        
        private bool KiemTraSoDienThoai(string soDienThoai)
        {
            return Regex.IsMatch(soDienThoai, @"^\d{10}$");
        }

        private void dgvNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvNhaCungCap.CurrentRow != null)
            {
                txtMaNhaCungCap.Text = dgvNhaCungCap.CurrentRow.Cells["MaNhaCungCap"].Value.ToString();
                txtTenNhaCungCap.Text = dgvNhaCungCap.CurrentRow.Cells["TenNhaCungCap"].Value.ToString();
                txtDiaChi.Text = dgvNhaCungCap.CurrentRow.Cells["DiaChi"].Value.ToString();
                txtEmail.Text = dgvNhaCungCap.CurrentRow.Cells["Email"].Value.ToString();
                txtSoDienThoai.Text = dgvNhaCungCap.CurrentRow.Cells["SoDienThoai"].Value.ToString();
                txtLoaiSanPhamCungCap.Text = dgvNhaCungCap.CurrentRow.Cells["LoaiSanPhamCungCap"].Value.ToString();
                cboDonViTinh.SelectedItem = dgvNhaCungCap.CurrentRow.Cells["DonViTinhSanPhamCungCap"].Value.ToString();
            }
        }
    }
}
