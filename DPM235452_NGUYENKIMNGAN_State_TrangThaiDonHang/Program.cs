using System;

namespace DPM235452_NGUYENKIMNGAN_State_TrangThaiDonHang
{
    // State Interface
    public interface ITrangThaiDonHang
    {
        void ThanhToan(DonHangContext context);
        void GiaoHang(DonHangContext context);
        void HoanThanh(DonHangContext context);
        void HuyDon(DonHangContext context);
    }

    // Context: Đơn hàng
    public class DonHangContext
    {
        public string MaDonHang { get; }
        public double TongTien { get; }
        public ITrangThaiDonHang TrangThaiHienTai { get; set; }

        public DonHangContext(string maDon, double tongTien)
        {
            MaDonHang = maDon;
            TongTien = tongTien;
            TrangThaiHienTai = new ChoThanhToanState(); // Mặc định khi vừa khởi tạo
        }

        public void ThanhToan() => TrangThaiHienTai.ThanhToan(this);
        public void GiaoHang() => TrangThaiHienTai.GiaoHang(this);
        public void HoanThanh() => TrangThaiHienTai.HoanThanh(this);
        public void HuyDon() => TrangThaiHienTai.HuyDon(this);
    }

    // Concrete State 1: Chờ thanh toán
    public class ChoThanhToanState : ITrangThaiDonHang
    {
        public void ThanhToan(DonHangContext context)
        {
            Console.WriteLine($"[Đơn {context.MaDonHang}] Thanh toán số tiền {context.TongTien:N0} VNĐ thành công.");
            context.TrangThaiHienTai = new DaThanhToanState();
        }

        public void GiaoHang(DonHangContext context)
        {
            Console.WriteLine($"[LỖI] Đơn {context.MaDonHang} chưa thanh toán, không thể giao hàng!");
        }

        public void HoanThanh(DonHangContext context)
        {
            Console.WriteLine($"[LỖI] Đơn {context.MaDonHang} chưa giao, không thể hoàn thành!");
        }

        public void HuyDon(DonHangContext context)
        {
            Console.WriteLine($"[Đơn {context.MaDonHang}] Đã hủy đơn hàng thành công.");
            context.TrangThaiHienTai = new DaHuyState();
        }
    }

    // Concrete State 2: Đã thanh toán
    public class DaThanhToanState : ITrangThaiDonHang
    {
        public void ThanhToan(DonHangContext context)
        {
            Console.WriteLine($"[CẢNH BÁO] Đơn {context.MaDonHang} đã được thanh toán trước đó!");
        }

        public void GiaoHang(DonHangContext context)
        {
            Console.WriteLine($"[Đơn {context.MaDonHang}] Đã đóng gói và bàn giao cho bên vận chuyển.");
            context.TrangThaiHienTai = new DangGiaoState();
        }

        public void HoanThanh(DonHangContext context)
        {
            Console.WriteLine($"[LỖI] Đơn {context.MaDonHang} chưa giao tới khách, không thể hoàn thành!");
        }

        public void HuyDon(DonHangContext context)
        {
            Console.WriteLine($"[Đơn {context.MaDonHang}] Hủy đơn đã thanh toán. Hệ thống tiến hành hoàn tiền: {context.TongTien:N0} VNĐ.");
            context.TrangThaiHienTai = new DaHuyState();
        }
    }

    // Concrete State 3: Đang giao hàng
    public class DangGiaoState : ITrangThaiDonHang
    {
        public void ThanhToan(DonHangContext context)
        {
            Console.WriteLine($"[CẢNH BÁO] Đơn {context.MaDonHang} đã thanh toán!");
        }

        public void GiaoHang(DonHangContext context)
        {
            Console.WriteLine($"[CẢNH BÁO] Đơn {context.MaDonHang} đang trong quá trình giao hàng!");
        }

        public void HoanThanh(DonHangContext context)
        {
            Console.WriteLine($"[Đơn {context.MaDonHang}] Khách hàng đã nhận hàng và xác nhận thành công.");
            context.TrangThaiHienTai = new HoanThanhState();
        }

        public void HuyDon(DonHangContext context)
        {
            Console.WriteLine($"[LỖI] Đơn {context.MaDonHang} đang trên đường giao, không thể hủy ngang!");
        }
    }

    // Concrete State 4: Hoàn thành
    public class HoanThanhState : ITrangThaiDonHang
    {
        public void ThanhToan(DonHangContext context) => Console.WriteLine($"[Đơn {context.MaDonHang}] Đơn hàng đã hoàn thành, không thể thao tác.");
        public void GiaoHang(DonHangContext context) => Console.WriteLine($"[Đơn {context.MaDonHang}] Đơn hàng đã hoàn thành, không thể thao tác.");
        public void HoanThanh(DonHangContext context) => Console.WriteLine($"[Đơn {context.MaDonHang}] Đơn hàng đã hoàn thành rồi.");
        public void HuyDon(DonHangContext context) => Console.WriteLine($"[LỖI] Đơn {context.MaDonHang} đã hoàn thành, không thể hủy!");
    }

    // Concrete State 5: Đã hủy
    public class DaHuyState : ITrangThaiDonHang
    {
        public void ThanhToan(DonHangContext context) => Console.WriteLine($"[LỖI] Đơn {context.MaDonHang} đã bị hủy, không thể thanh toán!");
        public void GiaoHang(DonHangContext context) => Console.WriteLine($"[LỖI] Đơn {context.MaDonHang} đã bị hủy, không thể giao hàng!");
        public void HoanThanh(DonHangContext context) => Console.WriteLine($"[LỖI] Đơn {context.MaDonHang} đã bị hủy!");
        public void HuyDon(DonHangContext context) => Console.WriteLine($"[Đơn {context.MaDonHang}] Đơn hàng đã ở trạng thái hủy.");
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 7: STATE PATTERN (QUẢN LÝ TRẠNG THÁI ĐƠN HÀNG) ===");

            DonHangContext donHang = new DonHangContext("DH_STATE_001", 18_500_000);

            // Thử nghiệm quy trình hợp lệ
            Console.WriteLine("--- 1. Thử giao hàng khi chưa thanh toán ---");
            donHang.GiaoHang();

            Console.WriteLine("\n--- 2. Tiến hành thanh toán ---");
            donHang.ThanhToan();

            Console.WriteLine("\n--- 3. Tiến hành giao hàng ---");
            donHang.GiaoHang();

            Console.WriteLine("\n--- 4. Thử hủy đơn khi đang giao ---");
            donHang.HuyDon();

            Console.WriteLine("\n--- 5. Khách nhận hàng -> Hoàn thành đơn ---");
            donHang.HoanThanh();

            Console.ReadLine();
        }
    }
}