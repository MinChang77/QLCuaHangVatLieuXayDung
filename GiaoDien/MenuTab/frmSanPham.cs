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
    public partial class frmSanPham : Form
    {
        BLLSanPham bllSanPham = new BLLSanPham();
        BLLNhaCungCap bllnhacungcap = new BLLNhaCungCap();
        public frmSanPham()
        {
            InitializeComponent();
            this.Load += FrmSanPham_Load;
            btnThem.Click += BtnThem_Click;
            btnXoa.Click += BtnXoa_Click;
            btnCapNhat.Click += BtnCapNhat_Click;
            btnTimKiem.Click += BtnTimKiem_Click;
            btnLamMoi.Click += BtnLamMoi_Click;
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            string tenSanPham = txtTenSanPham.Text;
            var ketQua = bllSanPham.TimKiemSanPham(tenSanPham);

            if (ketQua.Count > 0)
            {
                dgvSanPham.DataSource = ketQua; 
            }
            else
            {
                MessageBox.Show("Không tìm thấy sản phẩm nào.");
                LoadSanPham(); 
            }
        }

        private void BtnCapNhat_Click(object sender, EventArgs e)
        {

            if (dgvSanPham.CurrentRow != null)
            {
                string maSanPham = dgvSanPham.CurrentRow.Cells[0].Value.ToString();
                string tenSanPham = txtTenSanPham.Text;
                int donGia = int.TryParse(txtDonGia.Text, out int tempDonGia) ? tempDonGia : 0; 
                int soLuongTon = int.TryParse(txtSoLuongTon.Text, out int tempSoLuongTon) ? tempSoLuongTon : 0; 
                string moTa = txtMoTa.Text;
                string hinhAnh = txtHinhAnh.Text;
                string maNhaCungCap = cboMaNhaCungCap.SelectedValue.ToString();

                // Gọi hàm sửa từ BLL
                if (bllSanPham.CapNhat(maSanPham, tenSanPham, donGia, soLuongTon, moTa, hinhAnh, maNhaCungCap)) 
                {
                    MessageBox.Show("Cập nhật thành công");
                    LoadSanPham();
                    ClearFields(); 
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại");
                }
            }
            else
            {
                MessageBox.Show("Chọn một đối tượng để sửa");
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.CurrentRow != null)
            {
                string maSanPham = dgvSanPham.CurrentRow.Cells[0].Value.ToString();
                bllSanPham.Xoa(maSanPham);
                MessageBox.Show("Xóa thành công.");
                LoadSanPham();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Chọn một sản phẩm để xóa.");
            }
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (txtMaSanPham.Text == string.Empty || txtTenSanPham.Text == string.Empty || txtDonGia.Text == string.Empty || txtSoLuongTon.Text == string.Empty || txtHinhAnh.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            // Tạo đối tượng gán dữ liệu
            SanPham spThem = new SanPham
            {
                MaSanPham = txtMaSanPham.Text,
                TenSanPham = txtTenSanPham.Text,
                DonGia = int.TryParse(txtDonGia.Text, out int donGia) ? donGia : 0,
                SoLuongTon = int.TryParse(txtSoLuongTon.Text, out int soLuongTon) ? soLuongTon : 0,
                MoTa = txtMoTa.Text,
                HinhAnh = txtHinhAnh.Text,
                MaNhaCungCap = cboMaNhaCungCap.SelectedValue.ToString()
            };

            // Kiểm tra khóa chính
            if (bllSanPham.KTKC(spThem.MaSanPham))
            {
                MessageBox.Show("Trùng khóa chính.");
                return;
            }

            // Thêm
            if (bllSanPham.Them(spThem))
            {
                MessageBox.Show("Thêm thành công.");
                LoadSanPham();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Thêm thất bại.");
            }
        }

        private void FrmSanPham_Load(object sender, EventArgs e)
        {
            LoadComboboxNhaCungCap();
            LoadSanPham();
        }

        private void LoadSanPham()
        {
            dgvSanPham.DataSource = bllSanPham.GetSanPhams();
        }

        private void LoadComboboxNhaCungCap()
        {
            cboMaNhaCungCap.DataSource = bllnhacungcap.GetNhaCungCaps();
            cboMaNhaCungCap.DisplayMember = "TenNhaCungCap";
            cboMaNhaCungCap.ValueMember = "MaNhaCungCap";
        }

        private void ClearFields()
        {
            txtMaSanPham.Clear();
            txtTenSanPham.Clear();
            txtDonGia.Clear();
            txtSoLuongTon.Clear();
            txtMoTa.Clear();
            txtHinhAnh.Clear();
            cboMaNhaCungCap.SelectedIndex = -1; 
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSanPham.CurrentRow != null)
            {
                txtMaSanPham.Text = dgvSanPham.CurrentRow.Cells["MaSanPham"].Value.ToString();
                txtTenSanPham.Text = dgvSanPham.CurrentRow.Cells["TenSanPham"].Value.ToString();
                txtDonGia.Text = dgvSanPham.CurrentRow.Cells["DonGia"].Value.ToString();
                txtSoLuongTon.Text = dgvSanPham.CurrentRow.Cells["SoLuongTon"].Value.ToString();
                txtMoTa.Text = dgvSanPham.CurrentRow.Cells["MoTa"].Value.ToString();
                txtHinhAnh.Text = dgvSanPham.CurrentRow.Cells["HinhAnh"].Value.ToString();
                cboMaNhaCungCap.SelectedValue = dgvSanPham.CurrentRow.Cells["MaNhaCungCap"].Value.ToString();
            }
        }

        private void dgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {

        }

        private void btnCapNhat_Click_1(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {

        }
    }
}
