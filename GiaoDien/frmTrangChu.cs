using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GiaoDien.MenuTab;

namespace GiaoDien
{
    public partial class frmTrangChu : Form
    {
        private Form TrangCon;
        private string chucVu;
        public frmTrangChu(string chucVu)
        {
            InitializeComponent();
            this.chucVu = chucVu;
            PhanQuyen();
            timer1.Start();
            timer1.Tick += Timer1_Tick;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            this.lblTime.Text = datetime.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void PhanQuyen()
        {
            if (chucVu == "Admin")
            {
                // Hiển thị tất cả các nút
                btnSanPham.Visible = true;
                btnNhaCungCap.Visible = true;
                btnKhachHang.Visible = true;
                btnNhanVien.Visible = true;
                btnDonHang.Visible = true;
                btnPhieuNhap.Visible = true;
                btnThongKe.Visible = true;
                btnDangXuat.Visible = true;
                // Thêm các nút khác nếu cần
            }
            else if (chucVu == "Nhân viên")
            {
                // Hiển thị chỉ các nút cần thiết cho nhân viên
                btnSanPham.Visible = true;
                btnDonHang.Visible = true;
                btnPhieuNhap.Visible = true;
                btnDangXuat.Visible = true;
                // Ẩn các nút khác
                btnNhaCungCap.Visible = false;
                btnKhachHang.Visible = false;
                btnNhanVien.Visible = false;
                btnThongKe.Visible = false;

            }
            
        }

        private void motrangcon(Form trangcon)
        {
            if (TrangCon != null)
            {
                TrangCon.Close();

            }
            TrangCon = trangcon;
            trangcon.TopLevel = false;
            trangcon.FormBorderStyle = FormBorderStyle.None;
            trangcon.Dock = DockStyle.Fill;
            pnlGiaoDienChucNang.Controls.Add(trangcon);
            pnlGiaoDienChucNang.Tag = trangcon;
            trangcon.BringToFront();
            trangcon.Show();
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            motrangcon(new frmSanPham());
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();

            frmDangNhap formDangNhap = new frmDangNhap();
            formDangNhap.Show();

        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            motrangcon(new frmNhanVien());
        }

        private void btnNhaSanXuat_Click(object sender, EventArgs e)
        {
            motrangcon(new frmLoaiSanPham_NhaCungCap());
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            motrangcon(new frmKhachHang());
        }

        private void btnDonHang_Click(object sender, EventArgs e)
        {
            motrangcon(new frmDonHang());
        }

        private void btnPhieuNhap_Click(object sender, EventArgs e)
        {
            motrangcon(new frmPhieuNhap());
        }

        private void frmTrangChu_Resize(object sender, EventArgs e)
        {
            
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            motrangcon(new frmThongKe());
        }
    }
}
