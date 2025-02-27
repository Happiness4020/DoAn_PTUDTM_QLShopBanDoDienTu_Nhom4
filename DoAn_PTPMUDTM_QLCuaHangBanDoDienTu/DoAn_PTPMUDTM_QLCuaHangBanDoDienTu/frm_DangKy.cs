﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn_PTPMUDTM_QLCuaHangBanDoDienTu
{
    public partial class frm_DangKy : Form
    {
        DataDataContext db = new DataDataContext();
        public frm_DangKy()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnQuaylai_Click(object sender, EventArgs e)
        {
            frm_DangNhap frmDN = new frm_DangNhap();
            frmDN.Show();
            this.Visible = false;
        }
        private void frm_DangKy_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnDangky_Click(object sender, EventArgs e)
        {
            TaiKhoan taikhoan = new TaiKhoan
            {
                TenDN = txtTendangnhap.Text,
                MatKhau = txtMatkhau.Text,
                HoTen = txtHoten.Text,
                Email = txtEmail.Text,
                SoDienThoai = txtSdt.Text,
                NgaySinh = dtpNgaysinh.Value,
                GioiTinh = cbxGioitinh.SelectedItem.ToString(),
                AnhBiaUser = "user.jpg",
                VaiTro = "User",
                isDelete = 0
            };
            DangKyTaiKhoan(taikhoan);
        }

        private void DangKyTaiKhoan(TaiKhoan taikhoan)
        {
            try
            {
                if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtHoten.Text) || string.IsNullOrEmpty(txtMatkhau.Text) || string.IsNullOrEmpty(txtTendangnhap.Text)
                                || string.IsNullOrEmpty(txtXacnhan.Text) || string.IsNullOrEmpty(txtSdt.Text) || cbxGioitinh.SelectedIndex == 0)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin đăng ký!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if(txtSdt.Text.Length > 10 || txtSdt.Text.Length < 10)
                {
                    MessageBox.Show("Số điện thoại phải có độ dài là 10!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtMatkhau.Text != txtXacnhan.Text)
                {
                    MessageBox.Show("Mật khẩu xác nhận không trùng khớp!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var taiKhoanDaTonTai = db.TaiKhoans.Where(t => t.TenDN == txtTendangnhap.Text || t.Email == txtEmail.Text).FirstOrDefault();

                if (taiKhoanDaTonTai != null)
                {
                    MessageBox.Show("Tên đăng nhập hoặc Email đã tồn tại!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                db.TaiKhoans.InsertOnSubmit(taikhoan);
                db.SubmitChanges();
                MessageBox.Show("Bạn đã đăng ký tài khoản thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
                frm_DangNhap frmDN = new frm_DangNhap();
                frmDN.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi khi đăng ký: " + ex, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            txtEmail.Clear();
            txtHoten.Clear();
            txtMatkhau.Clear();
            txtSdt.Clear();
            txtTendangnhap.Clear();
            txtXacnhan.Clear();
            dtpNgaysinh.Value = DateTime.Now;
            cbxGioitinh.SelectedIndex = 0;
        }

        private void frm_DangKy_Load(object sender, EventArgs e)
        {
            dtpNgaysinh.Value = DateTime.Now;
            cbxGioitinh.SelectedIndex = 0;
        }

        private void txtSdt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtHoten_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }
    }
}
