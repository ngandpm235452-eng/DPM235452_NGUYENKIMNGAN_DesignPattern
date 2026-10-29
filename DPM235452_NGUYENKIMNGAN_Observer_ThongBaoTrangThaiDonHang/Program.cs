using System;
using System.Collections.Generic;

namespace DPM235452_NGUYENKIMNGAN_Observer_ThongBaoTrangThaiDonHang
{
    // Observer Interface
    public interface INguoiNhanThongBaoObserver
    {
        void CapNhatTrangThai(string maDonHang, string trangThaiMoi);
    }

    // Subject Interface
    public interface IDonHangSubject
    {
        void DangKy(INguoiNhanThongBaoObserver observer);
        void HuyDangKy(INguoiNhanThongBaoObserver observer);
        void ThongBaoObservers();
    }

    // Concrete Subject: Đơn hàng bán lẻ
    public class DonHangSubject : IDonHangSubject
    {
        private readonly List<INguoiNhanThongBaoObserver> _danhSachObservers = new List<INguoiNhanThongBaoObserver>();

        public string MaDonHang { get; }
        private string _trangThai;

        public string TrangThai
        {
            get => _trangThai;
            set
            {
                if (_trangThai != value)
                {
                    _trangThai = value;
                    Console.WriteLine($"\n==================================================");
                    Console.WriteLine($"[ĐƠN HÀNG {MaDonHang}] Cập nhật trạng thái -> '{_trangThai}'");
                    Console.WriteLine($"==================================================");
                    ThongBaoObservers();
                }
            }
        }

        public DonHangSubject(string maDonHang, string trangThaiBanDau)
        {
            MaDonHang = maDonHang;
            _trangThai = trangThaiBanDau;
        }

        public void DangKy(INguoiNhanThongBaoObserver observer)
        {
            _danhSachObservers.Add(observer);
        }

        public void HuyDangKy(INguoiNhanThongBaoObserver observer)
        {
            _danhSachObservers.Remove(observer);
        }

        public void ThongBaoObservers()
        {
            foreach (var observer in _danhSachObservers)
            {
                observer.CapNhatTrangThai(MaDonHang, _trangThai);
            }
        }
    }

    // Concrete Observer 1: Khách hàng (Nhận SMS / App Notification)
    public class KhachHangObserver : INguoiNhanThongBaoObserver
    {
        public string TenKhachHang { get; }

        public KhachHangObserver(string tenKhachHang)
        {
            TenKhachHang = tenKhachHang;
        }

        public void CapNhatTrangThai(string maDonHang, string trangThaiMoi)
        {
            Console.WriteLine($"  -> [Khách hàng: {TenKhachHang}] Nhận thông báo: Đơn hàng '{maDonHang}' hiện tại là [{trangThaiMoi}].");
        }
    }

    // Concrete Observer 2: Bộ phận Kho
    public class KhoHangObserver : INguoiNhanThongBaoObserver
    {
        public void CapNhatTrangThai(string maDonHang, string trangThaiMoi)
        {
            if (trangThaiMoi == "Đã thanh toán")
            {
                Console.WriteLine($"  -> [Bộ phận Kho] Nhận lệnh: Tiến hành soạn & xuất kho đơn '{maDonHang}'.");
            }
            else if (trangThaiMoi == "Đã hủy")
            {
                Console.WriteLine($"  -> [Bộ phận Kho] Nhận lệnh: Nhập lại hàng trả về kho cho đơn '{maDonHang}'.");
            }
        }
    }

    // Concrete Observer 3: Đơn vị Vận chuyển
    public class VanChuyenObserver : INguoiNhanThongBaoObserver
    {
        public void CapNhatTrangThai(string maDonHang, string trangThaiMoi)
        {
            if (trangThaiMoi == "Đã đóng gói")
            {
                Console.WriteLine($"  -> [Đơn vị Vận Chuyển] Nhận lệnh: Điều shipper đến lấy đơn '{maDonHang}' để giao.");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 6: OBSERVER PATTERN (THÔNG BÁO TRẠNG THÁI ĐƠN HÀNG) ===");

            // Khởi tạo đơn hàng
            DonHangSubject donHang = new DonHangSubject("DH_2026_88", "Mới khởi tạo");

            // Tạo các Observers
            INguoiNhanThongBaoObserver khachHang = new KhachHangObserver("Nguyễn Kim Ngân");
            INguoiNhanThongBaoObserver khoHang = new KhoHangObserver();
            INguoiNhanThongBaoObserver vanChuyen = new VanChuyenObserver();

            // Đăng ký nhận thông báo
            donHang.DangKy(khachHang);
            donHang.DangKy(khoHang);
            donHang.DangKy(vanChuyen);

            // Giả lập vòng đời đơn hàng
            donHang.TrangThai = "Đã thanh toán";
            donHang.TrangThai = "Đã đóng gói";
            donHang.TrangThai = "Đang giao hàng";
            donHang.TrangThai = "Giao thành công";

            Console.ReadLine();
        }
    }
}