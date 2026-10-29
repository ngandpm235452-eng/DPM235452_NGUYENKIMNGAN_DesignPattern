using System;
using System.Collections.Generic;

namespace DPM235452_NGUYENKIMNGAN_Composite_DanhMucSanPham
{
    // Component
    public abstract class ComponentDanhMuc
    {
        public string Ten { get; set; }
        public ComponentDanhMuc(string ten) => Ten = ten;
        public abstract double TinhTongGiaTri();
        public abstract void HienThi(int cap = 0);
    }

    // Leaf: Sản phẩm đơn lẻ
    public class SanPham : ComponentDanhMuc
    {
        public double Gia { get; set; }

        public SanPham(string ten, double gia) : base(ten)
        {
            Gia = gia;
        }

        public override double TinhTongGiaTri() => Gia;

        public override void HienThi(int cap = 0)
        {
            Console.WriteLine($"{new string(' ', cap * 2)}- [Sản phẩm] {Ten}: {Gia:N0} VNĐ");
        }
    }

    // Composite: Nhóm danh mục chứa sản phẩm hoặc danh mục con
    public class DanhMucGroup : ComponentDanhMuc
    {
        private readonly List<ComponentDanhMuc> _danhSachCon = new List<ComponentDanhMuc>();

        public DanhMucGroup(string ten) : base(ten) { }

        public void Them(ComponentDanhMuc item) => _danhSachCon.Add(item);
        public void Xoa(ComponentDanhMuc item) => _danhSachCon.Remove(item);

        public override double TinhTongGiaTri()
        {
            double tong = 0;
            foreach (var item in _danhSachCon)
            {
                tong += item.TinhTongGiaTri();
            }
            return tong;
        }

        public override void HienThi(int cap = 0)
        {
            Console.WriteLine($"{new string(' ', cap * 2)}+ [Danh mục] {Ten}");
            foreach (var item in _danhSachCon)
            {
                item.HienThi(cap + 1);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 3: COMPOSITE PATTERN (THỰC TẾ BÁN HÀNG) ===");

            // Danh mục con 1: Điện thoại
            DanhMucGroup dmDienThoai = new DanhMucGroup("Điện Thoại");
            dmDienThoai.Them(new SanPham("iPhone 15 Pro", 28_000_000));
            dmDienThoai.Them(new SanPham("Samsung Galaxy S24", 22_000_000));

            // Danh mục con 2: Phụ kiện
            DanhMucGroup dmPhuKien = new DanhMucGroup("Phụ Kiện");
            dmPhuKien.Them(new SanPham("Sạc Anker 65W", 850_000));
            dmPhuKien.Them(new SanPham("Tai nghe AirPods Pro 2", 5_900_000));

            // Danh mục gốc: Cửa hàng Công Nghệ
            DanhMucGroup tongCuaHang = new DanhMucGroup("TỔNG KHO HÀNG CÔNG NGHỆ");
            tongCuaHang.Them(dmDienThoai);
            tongCuaHang.Them(dmPhuKien);

            // Hiển thị cây danh mục và tính tổng
            tongCuaHang.HienThi();
            Console.WriteLine($"\n==> TỔNG GIÁ TRỊ TỒN KHO: {tongCuaHang.TinhTongGiaTri():N0} VNĐ");

            Console.ReadLine();
        }
    }
}