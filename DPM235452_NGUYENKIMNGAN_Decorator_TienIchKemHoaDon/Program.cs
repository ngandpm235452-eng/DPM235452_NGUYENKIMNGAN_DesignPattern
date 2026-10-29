using System;

namespace DPM235452_NGUYENKIMNGAN_Decorator_TienIchKemHoaDon
{
    // Component
    public abstract class HoaDonBase
    {
        public abstract string GetMoTa();
        public abstract double TinhTien();
    }

    // ConcreteComponent: Hóa đơn cơ bản chỉ gồm tiền sản phẩm
    public class HoaDonGoc : HoaDonBase
    {
        private readonly string _tenSanPham;
        private readonly double _tienHang;

        public HoaDonGoc(string tenSanPham, double tienHang)
        {
            _tenSanPham = tenSanPham;
            _tienHang = tienHang;
        }

        public override string GetMoTa() => $"Hóa đơn sản phẩm: {_tenSanPham}";
        public override double TinhTien() => _tienHang;
    }

    // Decorator
    public abstract class HoaDonDecorator : HoaDonBase
    {
        protected HoaDonBase _hoaDon;

        protected HoaDonDecorator(HoaDonBase hoaDon)
        {
            _hoaDon = hoaDon;
        }

        public override string GetMoTa() => _hoaDon.GetMoTa();
        public override double TinhTien() => _hoaDon.TinhTien();
    }

    // ConcreteDecorator 1: Dịch vụ đóng gói quà
    public class GoiQuaDecorator : HoaDonDecorator
    {
        public GoiQuaDecorator(HoaDonBase hoaDon) : base(hoaDon) { }

        public override string GetMoTa() => _hoaDon.GetMoTa() + "\n  + [Dịch vụ] Hộp quà & Nơ trang trí cao cấp (+30,000 VNĐ)";
        public override double TinhTien() => _hoaDon.TinhTien() + 30_000;
    }

    // ConcreteDecorator 2: Bảo hiểm rơi vỡ
    public class BaoHiemRoiVoDecorator : HoaDonDecorator
    {
        public BaoHiemRoiVoDecorator(HoaDonBase hoaDon) : base(hoaDon) { }

        public override string GetMoTa() => _hoaDon.GetMoTa() + "\n  + [Dịch vụ] Gói bảo hiểm rơi vỡ 1 năm (+150,000 VNĐ)";
        public override double TinhTien() => _hoaDon.TinhTien() + 150_000;
    }

    // ConcreteDecorator 3: Phụ phí giao hàng hỏa tốc
    public class GiaoHangHoaTocDecorator : HoaDonDecorator
    {
        public GiaoHangHoaTocDecorator(HoaDonBase hoaDon) : base(hoaDon) { }

        public override string GetMoTa() => _hoaDon.GetMoTa() + "\n  + [Dịch vụ] Giao hàng siêu tốc 2H (+50,000 VNĐ)";
        public override double TinhTien() => _hoaDon.TinhTien() + 50_000;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 4: DECORATOR PATTERN (THỰC TẾ BÁN HÀNG) ===");

            // 1. Tạo hóa đơn gốc
            HoaDonBase hoaDon = new HoaDonGoc("Đồng hồ Apple Watch Series 9", 10_500_000);

            // 2. Khách hàng chọn thêm tiện ích đóng gói quà
            hoaDon = new GoiQuaDecorator(hoaDon);

            // 3. Khách hàng chọn thêm gói bảo hiểm rơi vỡ
            hoaDon = new BaoHiemRoiVoDecorator(hoaDon);

            // 4. Khách hàng chọn thêm giao hàng hỏa tốc
            hoaDon = new GiaoHangHoaTocDecorator(hoaDon);

            // In thông tin thanh toán cuối cùng
            Console.WriteLine("--- CHI TIẾT ĐƠN HÀNG ---");
            Console.WriteLine(hoaDon.GetMoTa());
            Console.WriteLine("-------------------------");
            Console.WriteLine($"==> TỔNG TIỀN THANH TOÁN: {hoaDon.TinhTien():N0} VNĐ");

            Console.ReadLine();
        }
    }
}