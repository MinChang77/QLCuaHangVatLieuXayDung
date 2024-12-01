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
    public partial class frmThongKe : Form
    {
        BLLDonHang blldonhang = new BLLDonHang();
        public frmThongKe()
        {
            InitializeComponent();
            btnThongKe.Click += BtnThongKe_Click;
        }

        private void BtnThongKe_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date;

            if (tuNgay > denNgay)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var donHangs = blldonhang.LayDonHangTheoKhoangThoiGian(tuNgay, denNgay);
            dgvThongKeDonHang.DataSource = donHangs;
            txtTongTien.Text = blldonhang.TinhTongTien(donHangs).ToString("N0") + " VNĐ";

        }
    }
}
