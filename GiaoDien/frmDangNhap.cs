using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using BLL;

namespace GiaoDien
{
    public partial class frmDangNhap : Form
    {
        BLLNhanVien bllnhanvien = new BLLNhanVien();
        public frmDangNhap()
        {
            InitializeComponent();
            txtTenDangNhap.Click += TxtTenDangNhap_Click;
            txtMatKhau.Click += TxtMatKhau_Click;
            btnDangNhap.Click += BtnDangNhap_Click;
           
        }

        public static string LoggedInMaNhanVien { get;  set; }

        private void BtnDangNhap_Click(object sender, EventArgs e)
        {
            
            string soDienThoai = txtTenDangNhap.Text;
            string matKhau = txtMatKhau.Text;
            string chucVu;

            if (bllnhanvien.DangNhap(soDienThoai, matKhau, out chucVu))
            {
                LoggedInMaNhanVien = bllnhanvien.LayMaNhanVien(soDienThoai);
                frmTrangChu frm = new frmTrangChu(chucVu); 
                frm.Show(); 
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!");
            }
        }

        private void TxtMatKhau_Click(object sender, EventArgs e)
        {
            txtMatKhau.Clear();
        }

        private void TxtTenDangNhap_Click(object sender, EventArgs e)
        {
            txtTenDangNhap.Clear();
        }
    }
}
