using System;

namespace DPM235452_NGUYENKIMNGAN_Facade_QuyTrinhDatHang
{
    // Subsystem 1: Quản lý tồn kho
    public class SubsystemKhoHang
    {
        public bool KiemTraTonKho(string maSP, int soLuong)
        {
            Console.WriteLine($"[Kho Hàng] Kiểm tra sản phẩm '{maSP}' (Số lượng: {soLuong}) -> CÒN HÀNG.");
            return true;
        }

        public void TruTonKho(string maSP, int soLuong)
        {
            Console.WriteLine($"[Kho Hàng] Đã trừ {soLuong} sản phẩm '{maSP}' khỏi kho.");
        }
    }

    // Subsystem 2: Cổng thanh toán
    public class SubsystemThanhToan
    {
        public bool XuLyThanhToan(string maDonHang, double soTien)
        {
            Console.WriteLine($"[Thanh Toán] Xử lý giao dịch cho đơn '{maDonHang}' số tiền: {soTien:N0} VNĐ -> THÀNH CÔNG.");
            return true;
        }
    }

    // Subsystem 3: Đơn vị vận chuyển
    public class SubsystemVanChuyen
    {
        public string TaoVanDon(string maDonHang, string diaChiGiao)
        {
            string maVD = "VD_" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            Console.WriteLine($"[Vận Chuyển] Tạo mã vận đơn '{maVD}' cho đơn '{maDonHang}', giao tới: {diaChiGiao}");
            return maVD;
        }
    }

    // Subsystem 4: Gửi thông báo
    public class SubsystemThongBao
    {
        public void GuiEmailXacNhan(string email, string maDonHang)
        {
            Console.WriteLine($"[Thông Báo] Đã gửi email xác nhận đơn hàng '{maDonHang}' tới: {email}");
        }
    }

    // Facade: Gom toàn bộ quy trình đặt hàng phức tạp vào 1 phương thức duy nhất
    public class QuyTrinhDatHangFacade
    {
        private readonly SubsystemKhoHang _khoHang;
        private readonly SubsystemThanhToan _thanhToan;
        private readonly SubsystemVanChuyen _vanChuyen;
        private readonly SubsystemThongBao _thongBao;

        public QuyTrinhDatHangFacade()
        {
            _khoHang = new SubsystemKhoHang();
            _thanhToan = new SubsystemThanhToan();
            _vanChuyen = new SubsystemVanChuyen();
            _thongBao = new SubsystemThongBao();
        }

        public void DatHangTrucTuyen(string maSP, int soLuong, double giaTien, string diaChi, string emailKhach)
        {
            Console.WriteLine($"--- BẮT ĐẦU XỬ LÝ ĐẶT HÀNG ---");
            string maDonHang = "DH_" + DateTime.Now.ToString("yyyyMMddHHmmss");

            // 1. Kiểm tra kho
            if (!_khoHang.KiemTraTonKho(maSP, soLuong))
            {
                Console.WriteLine("=> ĐẶT HÀNG THẤT BẠI: Sản phẩm tạm hết hàng!");
                return;
            }

            // 2. Thanh toán
            double tongTien = giaTien * soLuong;
            if (!_thanhToan.XuLyThanhToan(maDonHang, tongTien))
            {
                Console.WriteLine("=> ĐẶT HÀNG THẤT BẠI: Lỗi thanh toán!");
                return;
            }

            // 3. Cập nhật kho
            _khoHang.TruTonKho(maSP, soLuong);

            // 4. Tạo vận đơn giao hàng
            _vanChuyen.TaoVanDon(maDonHang, diaChi);

            // 5. Gửi email xác nhận
            _thongBao.GuiEmailXacNhan(emailKhach, maDonHang);

            Console.WriteLine($"==================================================");
            Console.WriteLine($"==> ĐẶT HÀNG THÀNH CÔNG! MÃ ĐƠN: {maDonHang}");
            Console.WriteLine($"==================================================\n");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 5: FACADE PATTERN (THỰC TẾ BÁN HÀNG) ===");

            QuyTrinhDatHangFacade facade = new QuyTrinhDatHangFacade();

            // Client chỉ cần gọi 1 hàm duy nhất thay vì thao tác với 4 subsystem phức tạp
            facade.DatHangTrucTuyen(
                maSP: "IPHONE15_PRO_256",
                soLuong: 1,
                giaTien: 28_000_000,
                diaChi: "456 Lê Văn Sỹ, Quận 3, TP.HCM",
                emailKhach: "nguyenkimngan@gmail.com"
            );

            Console.ReadLine();
        }
    }
}