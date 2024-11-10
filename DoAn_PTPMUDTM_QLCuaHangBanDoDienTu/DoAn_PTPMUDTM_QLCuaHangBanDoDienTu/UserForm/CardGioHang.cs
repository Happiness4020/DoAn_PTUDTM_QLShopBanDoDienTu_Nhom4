﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn_PTPMUDTM_QLCuaHangBanDoDienTu.UserForm
{
    public partial class CardGioHang : UserControl
    {
        GioHang gh;
        public event EventHandler ButtonSuaClicked;
        public event EventHandler ButtonXoaClicked;
        dbQLCuaHangDienTuDataContext db = new dbQLCuaHangDienTuDataContext();
        public CardGioHang()
        {
            InitializeComponent();
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXua_Click;
        }
        private void BtnSua_Click(object sender, EventArgs e)
        {
            ButtonSuaClicked?.Invoke(this, e); // Kích hoạt sự kiện tùy chỉnh khi Button được nhấn
        }
        private void BtnXua_Click(object sender, EventArgs e)
        {
            ButtonXoaClicked?.Invoke(this, e); // Kích hoạt sự kiện tùy chỉnh khi Button được nhấn
        }
        public void setData(ViewGioHang view)
        {
            lblTenSanPham.Text = view.TenSanPham;
            lblDonGia.Text = view.Gia.ToString();
            lblThanhTien.Text = (view.Gia * view.SoLuong).ToString();
            nbrSoLuong.Value = (decimal)view.SoLuong;
            lblTuyChon.Text = "Tùy chọn: " + view.MauSac;
            gh = db.GioHangs.Where(g => g.TenDN == view.TenDN && g.MaCTSanPham == view.MaCTSanPham).FirstOrDefault();
        }
        public void ReLoadData()
        {
            ViewGioHang view = db.ViewGioHangs.Where(g => g.TenDN == gh.TenDN && g.MaCTSanPham == gh.MaCTSanPham).FirstOrDefault();
            lblTenSanPham.Text = view.TenSanPham;
            lblDonGia.Text = view.Gia.ToString();
            lblThanhTien.Text = (view.Gia * view.SoLuong).ToString();
            nbrSoLuong.Value = (decimal)view.SoLuong;
            lblTuyChon.Text = "Tùy chọn: " + view.MauSac;
        }
        private void UpdateGioHang()
        {
            try
            {
                if ((int)nbrSoLuong.Value > 0)
                {
                    gh.SoLuong = (int)nbrSoLuong.Value;
                    db.SubmitChanges();
                    MessageBox.Show("Cập nhật giỏ hàng thành công");
                    ReLoadData();
                }
                else
                {
                    db.GioHangs.DeleteOnSubmit(gh);
                    db.SubmitChanges();
                    MessageBox.Show("Xóa sản phẩm khỏi giỏ hàng thành công");
                    this.Dispose();
                }
            }
            catch
            {
                MessageBox.Show("Cập nhật giỏ hàng thất bại");
            }
           
        }
        private void DeleteGioHang()
        {
            try
            {
                db.GioHangs.DeleteOnSubmit(gh);
                db.SubmitChanges();
                MessageBox.Show("Xóa sản phẩm khỏi giỏ hàng thành công");
                this.Dispose();

            }
            catch
            {
                MessageBox.Show("Xóa sản phẩm khỏi giỏ hàng thất bại");
            }

        }

        public void btnSua_Click(object sender, EventArgs e)
        {
            UpdateGioHang();
        }

        public void btnXoa_Click(object sender, EventArgs e)
        {
            DeleteGioHang();
        }
    }
}