using System;

namespace DPM235452_NGUYENKIMNGAN_Strategy_TinhChietKhauDonHang
{
    // Strategy Interface: Chiến lược tính chiết khấu
    public interface ITinhChietKhauStrategy
    {
        double TinhSoTienGiam(double tongTienDonHang);
        string GetTenChienLuoc();
    }

    // Concrete Strategy 1: Khách hàng thông thường (Không giảm giá)
    public class KhongChietKhauStrategy : ITinhChietKhauStrategy
    {
        public double TinhSoTienGiam(double tongTienDonHang) => 0;
        public string GetTenChienLuoc() => "Không áp dụng chiết khấu (0%)";
    }

    // Concrete Strategy 2: Khách hàng Thân thiết (Giảm 10%)
    public class ChietKhauThanThietStrategy : ITinhChietKhauStrategy
    {
        public double TinhSoTienGiam(double tongTienDonHang) => tongTienDonHang * 0.10;
        public string GetTenChienLuoc() => "Chiết khấu Khách hàng Thân thiết (10%)";
    }

    // Concrete Strategy 3: Khách hàng VIP (Giảm 20%)
    public class ChietKhauVIPStrategy : ITinhChietKhauStrategy
    {
        public double TinhSoTienGiam(double tongTienDonHang) => tongTienDonHang * 0.20;
        public string GetTenChienLuoc() => "Chiết khấu Khách VIP (20%)";
    }

    // Concrete Strategy 4: Khuyến mãi Tết (Giảm 15% cho đơn từ 2.000.000 VNĐ)
    public class ChietKhauKhuyenMaiTetStrategy : ITinhChietKhauStrategy
    {
        public double TinhSoTienGiam(double tongTienDonHang)
        {
            if (tongTienDonHang >= 2_000_000)
            {
                return tongTienDonHang * 0.15;
            }
            return 0;
        }

        public string GetTenChienLuoc() => "Khuyến mãi Khai xuân Tết (15% cho đơn >= 2M)";
    }

    // Context: Đơn hàng bán lẻ
    public class DonHangContext
    {
        public string MaDonHang { get; set; }
        public double TongTienBanDau { get; set; }

        // Dynamic Strategy
        private ITinhChietKhauStrategy _chietKhauStrategy;

        public DonHangContext(string maDonHang, double tongTien, ITinhChietKhauStrategy strategy = null)
        {
            MaDonHang = maDonHang;
            TongTienBanDau = tongTien;
            _chietKhauStrategy = strategy ?? new KhongChietKhauStrategy();
        }

        public void SetStrategy(ITinhChietKhauStrategy strategy)
        {
            _chietKhauStrategy = strategy;
        }

        public void InHoaDon()
        {
            double soTienGiam = _chietKhauStrategy.TinhSoTienGiam(TongTienBanDau);
            double tienPhaiThanhToan = TongTienBanDau - soTienGiam;

            Console.WriteLine($"\n==================================================");
            Console.WriteLine($"[ĐƠN HÀNG: {MaDonHang}]");
            Console.WriteLine($"  + Tổng tiền hàng: {TongTienBanDau:N0} VNĐ");
            Console.WriteLine($"  + Áp dụng chính sách: {_chietKhauStrategy.GetTenChienLuoc()}");
            Console.WriteLine($"  + Số tiền được giảm: -{soTienGiam:N0} VNĐ");
            Console.WriteLine($"  => THÀNH TIỀN THANH TOÁN: {tienPhaiThanhToan:N0} VNĐ");
            Console.WriteLine($"==================================================");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 8: STRATEGY PATTERN (TÍNH CHIẾT KHẤU ĐƠN HÀNG) ===");

            DonHangContext donHang = new DonHangContext("DH_STRATEGY_888", 5_000_000);

            // 1. Mặc định chưa áp dụng chiết khấu
            donHang.InHoaDon();

            // 2. Chuyển sang Khách Thân Thiết
            donHang.SetStrategy(new ChietKhauThanThietStrategy());
            donHang.InHoaDon();

            // 3. Chuyển sang Khách VIP
            donHang.SetStrategy(new ChietKhauVIPStrategy());
            donHang.InHoaDon();

            // 4. Áp dụng Sự kiện Tết
            donHang.SetStrategy(new ChietKhauKhuyenMaiTetStrategy());
            donHang.InHoaDon();

            Console.ReadLine();
        }
    }
}