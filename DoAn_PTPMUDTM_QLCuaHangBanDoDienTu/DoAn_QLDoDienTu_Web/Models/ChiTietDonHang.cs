//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoAn_QLDoDienTu_Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChiTietDonHang
    {
        public int MaDonHang { get; set; }
        public int MaCTSanPham { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<double> ThanhTien { get; set; }
        public string KichThuoc { get; set; }
        public Nullable<int> TrangThaiCTDH { get; set; }
    
        public virtual DonHang DonHang { get; set; }
        public virtual ChiTietSanPham ChiTietSanPham { get; set; }
    }
}
