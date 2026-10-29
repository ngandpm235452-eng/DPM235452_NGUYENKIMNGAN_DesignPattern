using System;
using System.Collections.Generic;

namespace DPM235452_NGUYENKIMNGAN_Flyweight_QuanLyKhoHang
{
    // Flyweight: Lưu trữ dữ liệu cố định, trùng lặp của sản phẩm để tiết kiệm RAM (Intrinsic State)
    public class LoaiSanPhamFlyweight
    {
        public string TenLoai { get; }
        public string NhanHieu { get; }
        public string XuatXu { get; }

        public LoaiSanPhamFlyweight(string tenLoai, string nhanHieu, string xuatXu)
        {
            TenLoai = tenLoai;
            NhanHieu = nhanHieu;
            XuatXu = xuatXu;
        }

        public void HienThiThongTinInKho(string maBarCode, double giaBan, int vịTriKe)
        {
            Console.WriteLine($"[BarCode: {maBarCode}] | {TenLoai} - {NhanHieu} ({XuatXu}) | Giá: {giaBan:N0} VNĐ | Kệ số: {vịTriKe}");
        }
    }

    // Flyweight Factory: Cache các loại sản phẩm
    public class SanPhamFlyweightFactory
    {
        private readonly Dictionary<string, LoaiSanPhamFlyweight> _cacheLoaiSP = new Dictionary<string, LoaiSanPhamFlyweight>();

        public LoaiSanPhamFlyweight GetLoaiSanPham(string tenLoai, string nhanHieu, string xuatXu)
        {
            string key = $"{tenLoai}_{nhanHieu}_{xuatXu}".ToLower();

            if (!_cacheLoaiSP.ContainsKey(key))
            {
                _cacheLoaiSP[key] = new LoaiSanPhamFlyweight(tenLoai, nhanHieu, xuatXu);
                Console.WriteLine($"[CACHE MISS] Tạo mới phân loại bộ nhớ: {tenLoai} - {nhanHieu}");
            }
            return _cacheLoaiSP[key];
        }

        public int GetSoLuongLoaiTrongCache() => _cacheLoaiSP.Count;
    }

    // Context: Sản phẩm cụ thể trong kho chứa Flyweight và các thông tin riêng (Extrinsic State)
    public class SanPhamInKho
    {
        public string MaBarCode { get; set; }
        public double GiaBan { get; set; }
        public int ViTriKe { get; set; }

        // Tham chiếu đến Flyweight dùng chung
        public LoaiSanPhamFlyweight LoaiSP { get; set; }

        public SanPhamInKho(string maBarCode, double giaBan, int viTriKe, LoaiSanPhamFlyweight loaiSP)
        {
            MaBarCode = maBarCode;
            GiaBan = giaBan;
            ViTriKe = viTriKe;
            LoaiSP = loaiSP;
        }

        public void InThongTin()
        {
            LoaiSP.HienThiThongTinInKho(MaBarCode, GiaBan, ViTriKe);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 6: FLYWEIGHT PATTERN (THỰC TẾ QUẢN LÝ KHO HÀNG) ===");

            SanPhamFlyweightFactory factory = new SanPhamFlyweightFactory();
            List<SanPhamInKho> khoHang = new List<SanPhamInKho>();

            // Nhập 1,000 sản phẩm Laptop Dell vào kho nhưng chỉ tốn 1 vùng nhớ cho thuộc tính chủng loại
            var loaiLaptopDell = factory.GetLoaiSanPham("Laptop Gaming", "Dell", "Mỹ");
            khoHang.Add(new SanPhamInKho("89310001", 25_000_000, 101, loaiLaptopDell));
            khoHang.Add(new SanPhamInKho("89310002", 26_500_000, 102, loaiLaptopDell));

            // Nhập Điện thoại Samsung
            var loaiSamsung = factory.GetLoaiSanPham("Điện Thoại SmartPhone", "Samsung", "Hàn Quốc");
            khoHang.Add(new SanPhamInKho("89320001", 18_000_000, 201, loaiSamsung));

            // Nhập thêm Laptop Dell -> Tái sử dụng Flyweight cũ từ Cache
            var loaiLaptopDell2 = factory.GetLoaiSanPham("Laptop Gaming", "Dell", "Mỹ");
            khoHang.Add(new SanPhamInKho("89310003", 28_000_000, 103, loaiLaptopDell2));

            Console.WriteLine("\n--- DANH SÁCH MẶT HÀNG TRONG KHO ---");
            foreach (var sp in khoHang)
            {
                sp.InThongTin();
            }

            Console.WriteLine($"\n==================================================");
            Console.WriteLine($"-> Tổng số lượng sản phẩm trong kho: {khoHang.Count}");
            Console.WriteLine($"-> Số lượng đối tượng Loại SP lưu trong RAM: {factory.GetSoLuongLoaiTrongCache()}");
            Console.WriteLine($"-> Kiểm tra tham chiếu bộ nhớ chung: {object.ReferenceEquals(loaiLaptopDell, loaiLaptopDell2)}");
            Console.WriteLine($"==================================================");

            Console.ReadLine();
        }
    }
}