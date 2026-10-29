using System;

namespace DPM235452_NGUYENKIMNGAN_Proxy_KiemSoatQuyenTaiKhoan
{
    // Subject
    public interface IBanHangService
    {
        void HuyDonHang(string maDonHang);
        void ApDungGiamGiaDacBiet(string maDonHang, double phanTram);
    }

    // RealSubject: Thực hiện nghiệp vụ bán hàng thực tế
    public class RealBanHangService : IBanHangService
    {
        public void HuyDonHang(string maDonHang)
        {
            Console.WriteLine($"[HỆ THỐNG] Đơn hàng '{maDonHang}' đã được hủy thành công trên CSDL.");
        }

        public void ApDungGiamGiaDacBiet(string maDonHang, double phanTram)
        {
            Console.WriteLine($"[HỆ THỐNG] Đã áp dụng chiết khấu {phanTram}% cho đơn hàng '{maDonHang}'.");
        }
    }

    // Proxy: Kiểm soát quyền hạn (Role-Based Authorization) trước khi gọi RealSubject
    public class BanHangProxy : IBanHangService
    {
        private readonly RealBanHangService _realService;
        public string TenNguoiDung { get; set; }
        public string ChucVu { get; set; } // "QuanLy", "NhanVien"

        public BanHangProxy(string tenNguoiDung, string chucVu)
        {
            TenNguoiDung = tenNguoiDung;
            ChucVu = chucVu;
            _realService = new RealBanHangService();
        }

        public void HuyDonHang(string maDonHang)
        {
            Console.WriteLine($"\n[Proxy Log] Tài khoản '{TenNguoiDung}' ({ChucVu}) yêu cầu HỦY đơn '{maDonHang}'");

            // Chỉ Quản lý mới được quyền hủy đơn
            if (ChucVu.Equals("QuanLy", StringComparison.OrdinalIgnoreCase) || ChucVu.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                _realService.HuyDonHang(maDonHang);
            }
            else
            {
                Console.WriteLine($"[TỪ CHỐI TRUY CẬP] Chức vụ '{ChucVu}' không có quyền hủy đơn! Vui lòng báo Quản lý.");
            }
        }

        public void ApDungGiamGiaDacBiet(string maDonHang, double phanTram)
        {
            Console.WriteLine($"\n[Proxy Log] Tài khoản '{TenNguoiDung}' ({ChucVu}) yêu cầu GIẢM GIÁ {phanTram}% cho đơn '{maDonHang}'");

            // Nhân viên chỉ được giảm tối đa 20%, trên 20% phải là Quản lý
            if (phanTram > 20 && !ChucVu.Equals("QuanLy", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"[TỪ CHỐI TRUY CẬP] Mức giảm {phanTram}% vượt quá thẩm quyền của Nhân viên (tối đa 20%)!");
            }
            else
            {
                _realService.ApDungGiamGiaDacBiet(maDonHang, phanTram);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 7: PROXY PATTERN (THỰC TẾ BÁN HÀNG) ===");

            // 1. Kiểm tra thao tác bởi Nhân viên bán hàng
            IBanHangService nhanVienProxy = new BanHangProxy("Trần Văn B", "NhanVien");
            nhanVienProxy.ApDungGiamGiaDacBiet("DH_101", 15); // Hợp lệ (<= 20%)
            nhanVienProxy.ApDungGiamGiaDacBiet("DH_102", 30); // Bị chặn (> 20%)
            nhanVienProxy.HuyDonHang("DH_101");              // Bị chặn (Không phải Quản lý)

            Console.WriteLine("\n--------------------------------------------------");

            // 2. Kiểm tra thao tác bởi Quản lý
            IBanHangService quanLyProxy = new BanHangProxy("Nguyễn Kim Ngân", "QuanLy");
            quanLyProxy.ApDungGiamGiaDacBiet("DH_102", 30);  // Hợp lệ
            quanLyProxy.HuyDonHang("DH_101");               // Hợp lệ

            Console.ReadLine();
        }
    }
}