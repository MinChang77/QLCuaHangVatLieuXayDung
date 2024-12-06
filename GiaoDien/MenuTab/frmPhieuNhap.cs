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



namespace GiaoDien.MenuTab
{
    public partial class frmPhieuNhap : Form
    {
        BLLSanPham bllSanPham = new BLLSanPham();
        BLLPhieuNhap bllPhieuNhap = new BLLPhieuNhap();
        BLLChiTietPhieuNhap bllChiTietPhieuNhap = new BLLChiTietPhieuNhap();
        BLLNhanVien bllnhanvien = new BLLNhanVien();
        BLLNhaCungCap bllnhacungcap = new BLLNhaCungCap();
        public frmPhieuNhap()
        {
            InitializeComponent();
            btnLamMoi.Click += BtnLamMoi_Click;
            btnThem.Click += BtnThem_Click;
            btnXoa.Click += BtnXoa_Click;
            btnCapNhat.Click += BtnCapNhat_Click;
            btnLuuPhieuNhap.Click += BtnLuuPhieuNhap_Click;
        }

        private void BtnLuuPhieuNhap_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txtMaPhieuNhap.Text) ||
                    string.IsNullOrEmpty(txtNgayNhap.Text) ||
                    string.IsNullOrEmpty(txtTongTien.Text) ||
                    cboNhanVien.SelectedIndex == -1 ||
                    cboNhaCungCap.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var phieuNhap = new PhieuNhap
                {
                    MaPhieuNhap = txtMaPhieuNhap.Text,
                    MaNhanVien = frmDangNhap.LoggedInMaNhanVien,
                    MaNhaCungCap = cboNhaCungCap.SelectedValue?.ToString(),
                    NgayNhap = DateTime.ParseExact(txtNgayNhap.Text, "dd/MM/yyyy", null),
                    TongTien = int.Parse(txtTongTien.Text)
                };

                var resultPhieuNhap = bllPhieuNhap.Them(phieuNhap);

                if (!resultPhieuNhap)
                {
                    MessageBox.Show("Không thể lưu Phiếu Nhập. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (DataGridViewRow row in dgvChiTietPhieuNhap.Rows)
                {
                    if (row.Cells["MaSanPham"].Value != null &&
                        row.Cells["SoLuong"].Value != null &&
                        row.Cells["DonGia"].Value != null)
                    {
                        var chiTietPhieuNhap = new ChiTietPhieuNhap
                        {
                            MaPhieuNhap = txtMaPhieuNhap.Text,
                            MaSanPham = row.Cells["MaSanPham"].Value.ToString(),
                            SoLuong = int.Parse(row.Cells["SoLuong"].Value.ToString()),
                            DonGia = int.Parse(row.Cells["DonGia"].Value.ToString())
                        };

                        var resultChiTiet = bllChiTietPhieuNhap.Them(chiTietPhieuNhap);

                        if (!resultChiTiet)
                        {
                            MessageBox.Show($"Không thể lưu Chi Tiết Phiếu Nhập cho sản phẩm {chiTietPhieuNhap.MaSanPham}.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                MessageBox.Show("Lưu Phiếu Nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearFields();
                LoadPhieuNhap();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCapNhat_Click(object sender, EventArgs e)
        {
            if (dgvChiTietPhieuNhap.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvChiTietPhieuNhap.SelectedRows[0];

                string maSanPham = selectedRow.Cells["MaSanPham"].Value.ToString();
                string maPhieuNhap = selectedRow.Cells["MaPhieuNhap"].Value.ToString();
                int soLuongHienTai = int.Parse(selectedRow.Cells["SoLuong"].Value.ToString());
                int donGiaHienTai = int.Parse(selectedRow.Cells["DonGia"].Value.ToString());

                int soLuongMoi = (int)nudSoLuong.Value;

                if (soLuongMoi != soLuongHienTai)
                {
                    int donGiaMoi = donGiaHienTai / soLuongHienTai * soLuongMoi;

                    selectedRow.Cells["SoLuong"].Value = soLuongMoi;
                    selectedRow.Cells["DonGia"].Value = donGiaMoi;

                    MessageBox.Show($"Cập nhật số lượng sản phẩm {maSanPham} thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Số lượng không thay đổi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            UpdateTongTien();
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvChiTietPhieuNhap.SelectedRows.Count > 0)
            {
                int selectedIndex = dgvChiTietPhieuNhap.SelectedRows[0].Index;
                dgvChiTietPhieuNhap.Rows.RemoveAt(selectedIndex);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            UpdateTongTien();
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            string maPhieuNhap = txtMaPhieuNhap.Text;  
            string maSanPham = dgvSanPham.CurrentRow.Cells["MaSanPham"].Value.ToString(); 
            int soLuong = (int)nudSoLuong.Value; 
            int donGia = int.Parse(dgvSanPham.CurrentRow.Cells["DonGia"].Value.ToString());
            int donGiaTheoSoLuong = donGia * soLuong;

            dgvChiTietPhieuNhap.Rows.Add(maPhieuNhap, maSanPham, soLuong, donGiaTheoSoLuong);

            
            var chiTietPhieuNhap = new ChiTietPhieuNhap
            {
                MaPhieuNhap = maPhieuNhap,
                MaSanPham = maSanPham,
                SoLuong = soLuong,
                DonGia = donGia
            };

            UpdateTongTien();
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void frmPhieuNhap_Load(object sender, EventArgs e)
        {
            
            LoadSanPham();
            LoadPhieuNhap();
            LoadComboboxNhaCungCap();
            LoadComboboxNhanVien();
            XulyControl();
            AddColumnChiTietPhieuNhap();
            if (!string.IsNullOrEmpty(frmDangNhap.LoggedInMaNhanVien))
            {
                string tenNhanvien = bllnhanvien.LayHoTenNhanVien(frmDangNhap.LoggedInMaNhanVien);

                cboNhanVien.DataSource = null;
                cboNhanVien.Items.Clear();
                cboNhanVien.Items.Add(tenNhanvien);
                cboNhanVien.SelectedIndex = 0;
            }

        }
        private void LoadSanPham()
        {
            dgvSanPham.DataSource = bllSanPham.GetSanPhams();
        }
        private void LoadPhieuNhap()
        {
            dgvPhieuNhap.DataSource = bllPhieuNhap.GetPhieuNhaps();
        }

        private void LoadComboboxNhaCungCap()
        {
            cboNhaCungCap.DataSource = bllnhacungcap.GetNhaCungCaps();
            cboNhaCungCap.DisplayMember = "TenNhaCungCap";
            cboNhaCungCap.ValueMember = "MaNhaCungCap";
        }
        private void LoadComboboxNhanVien()
        {
            cboNhanVien.DataSource = bllnhanvien.GetNhanViens();
            cboNhanVien.DisplayMember = "TenNhanVien";
            cboNhanVien.ValueMember = "MaNhanVien";
        }


        private void ClearFields()
        {

            txtMaPhieuNhap.Text = GenerateNewMaPhieuNhap();
            txtTongTien.Clear();
            txtNgayNhap.Text = DateTime.Now.ToString("dd/MM/yyyy");
            nudSoLuong.Value = 0;
            
            cboNhaCungCap.SelectedIndex = -1;
            dgvChiTietPhieuNhap.Rows.Clear();
            btnThem.Enabled = false;
        }

        private void XulyControl()
        {
            txtNgayNhap.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtMaPhieuNhap.Text = GenerateNewMaPhieuNhap();
            btnThem.Enabled = false;
            
            cboNhaCungCap.SelectedIndex = -1;
        }

        private void UpdateTongTien()
        {
            decimal tongTien = 0;

            
            foreach (DataGridViewRow row in dgvChiTietPhieuNhap.Rows)
            {
               
                if (row.Cells["DonGia"].Value != null)
                {
                    tongTien += Convert.ToDecimal(row.Cells["DonGia"].Value);
                }
            }

           
            txtTongTien.Text = tongTien.ToString("0"); 
        }

        private void AddColumnChiTietPhieuNhap()
        {
            dgvChiTietPhieuNhap.Columns.Add("MaPhieuNhap", "Mã Phiếu Nhập");
            dgvChiTietPhieuNhap.Columns.Add("MaSanPham", "Mã Sản Phẩm");
            dgvChiTietPhieuNhap.Columns.Add("SoLuong", "Số Lượng");
            dgvChiTietPhieuNhap.Columns.Add("DonGia", "Đơn Giá");
        }

        private string GenerateNewMaPhieuNhap()
        {
            
            string lastMaPhieuNhap = bllPhieuNhap.GetLastMaPhieuNhap();

            if (string.IsNullOrEmpty(lastMaPhieuNhap))
            {
                return "PN001";
            }

            int lastNumber = int.Parse(lastMaPhieuNhap.Substring(2)); 
            int newNumber = lastNumber + 1;

            return "PN" + newNumber.ToString("D3"); 
        }

       

        private void dgvPhieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string maPhieuNhap = dgvPhieuNhap.Rows[e.RowIndex].Cells["MaPhieuNhap"].Value.ToString();

                var chiTietPhieuNhapList = bllChiTietPhieuNhap.GetChiTietPhieuNhaps(maPhieuNhap);

                dgvChiTietPhieuNhap.Rows.Clear();
                foreach (var chiTiet in chiTietPhieuNhapList)
                {
                    dgvChiTietPhieuNhap.Rows.Add(chiTiet.MaPhieuNhap, chiTiet.MaSanPham, chiTiet.SoLuong, chiTiet.DonGia);
                }

                txtMaPhieuNhap.Text = dgvPhieuNhap.CurrentRow.Cells["MaPhieuNhap"].Value.ToString();
                txtTongTien.Text = dgvPhieuNhap.CurrentRow.Cells["TongTien"].Value.ToString();
                txtNgayNhap.Text = dgvPhieuNhap.CurrentRow.Cells["NgayNhap"].Value.ToString();
                cboNhaCungCap.SelectedValue = dgvPhieuNhap.CurrentRow.Cells["MaNhaCungCap"].Value.ToString();
                cboNhanVien.SelectedValue = dgvPhieuNhap.CurrentRow.Cells["MaNhanVien"].Value.ToString();

                

            }
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (nudSoLuong.Value == 0 || dgvSanPham.SelectedRows.Count == 0)
            {
                btnThem.Enabled = false; 
            }
            else
            {
                btnThem.Enabled = true;
            }
        }

        private void cboNhaCungCap_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maNhaCungCap = cboNhaCungCap.SelectedValue?.ToString();

            if (!string.IsNullOrEmpty(maNhaCungCap))
            {
                dgvSanPham.DataSource = bllSanPham.LoadSanPhamTheoNhaCungCap(maNhaCungCap);
            }
            else
            {
                LoadSanPham();
            }
        }
    }
}
