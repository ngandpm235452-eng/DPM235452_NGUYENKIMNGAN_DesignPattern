using System;

namespace DPM235452_NGUYENKIMNGAN_TemplateMethod_QuyTrinhXuatHoaDon
{
    // Abstract Template Class
    public abstract class QuyTrinhXuatHoaDonTemplate
    {
        public string MaDonHang { get; set; }
        public double TongTienHang { get; set; }

        protected QuyTrinhXuatHoaDonTemplate(string maDonHang, double tongTienHang)
        {
            MaDonHang = maDonHang;
            TongTienHang = tongTienHang;
        }

        // Template Method: Cố định quy trình xử lý hóa đơn
        public void XuatHoaDon()
        {
            Console.WriteLine($"\n==================================================");
            Console.WriteLine($"--- BẮT ĐẦU XUẤT HÓA ĐƠN CHO ĐƠN HÀNG [{MaDonHang}] ---");

            LayThongTinDonHang();
            double thue = TinhThueVAT();
            double tongThanhToan = TongTienHang + thue;

            TaoNoiDungHoaDon(tongThanhToan);
            InHoaDon();

            if (CanGuiEmail())
            {
                GuiEmailDienTu();
            }

            Console.WriteLine($"--- HOÀN TẤT XUẤT HÓA ĐƠN [{MaDonHang}] ---");
            Console.WriteLine($"==================================================");
        }

        // Bước cố định 1
        protected void LayThongTinDonHang()
        {
            Console.WriteLine($"1. Truy vấn dữ liệu đơn hàng '{MaDonHang}' | Tiền hàng: {TongTienHang:N0} VNĐ");
        }

        // Bước trừu tượng 1: Tính thuế VAT (Mỗi loại hóa đơn có mức thuế khác nhau)
        protected abstract double TinhThueVAT();

        // Bước trừu tượng 2: Tạo nội dung thể hiện hóa đơn
        protected abstract void TaoNoiDungHoaDon(double tongThanhToan);

        // Bước cố định 2
        protected void InHoaDon()
        {
            Console.WriteLine("4. Tiến hành in bản sao hóa đơn ra hệ thống.");
        }

        // Hook (Phương thức tùy chọn)
        protected virtual bool CanGuiEmail() => false;

        protected virtual void GuiEmailDienTu()
        {
            Console.WriteLine("5. Gửi file hóa đơn điện tử qua Email cho khách hàng.");
        }
    }

    // Concrete Class 1: Hóa đơn bán lẻ cho khách cá nhân
    public class HoaDonBanLe : QuyTrinhXuatHoaDonTemplate
    {
        public HoaDonBanLe(string maDonHang, double tongTienHang) : base(maDonHang, tongTienHang) { }

        protected override double TinhThueVAT()
        {
            Console.WriteLine("2. [Hóa đơn bán lẻ] Thuế VAT: 0 VNĐ (Không kê khai thuế)");
            return 0;
        }

        protected override void TaoNoiDungHoaDon(double tongThanhToan)
        {
            Console.WriteLine($"3. [Hóa đơn bán lẻ] Lập Bill tính tiền đơn giản. Tổng thanh toán: {tongThanhToan:N0} VNĐ");
        }
    }

    // Concrete Class 2: Hóa đơn GTGT cho Doanh nghiệp
    public class HoaDonDoanhNghiepGTGT : QuyTrinhXuatHoaDonTemplate
    {
        public string MaSoThue { get; set; }
        public string TenCongTy { get; set; }

        public HoaDonDoanhNghiepGTGT(string maDonHang, double tongTienHang, string mst, string tenCongTy)
            : base(maDonHang, tongTienHang)
        {
            MaSoThue = mst;
            TenCongTy = tenCongTy;
        }

        protected override double TinhThueVAT()
        {
            double thue = TongTienHang * 0.10; // VAT 10%
            Console.WriteLine($"2. [Hóa đơn GTGT] Tính Thuế VAT (10%): {thue:N0} VNĐ");
            return thue;
        }

        protected override void TaoNoiDungHoaDon(double tongThanhToan)
        {
            Console.WriteLine($"3. [Hóa đơn GTGT] Nhập MST: {MaSoThue} | Công ty: {TenCongTy}");
            Console.WriteLine($"   => Tổng tiền phải thanh toán (gồm VAT): {tongThanhToan:N0} VNĐ");
        }

        protected override bool CanGuiEmail() => true; // Kích hoạt Hook gửi email

        protected override void GuiEmailDienTu()
        {
            Console.WriteLine($"5. [Hóa đơn GTGT] Đã gửi file XML/PDF hóa đơn điện tử tới email Doanh nghiệp.");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 9: TEMPLATE METHOD PATTERN (QUY TRÌNH XUẤT HÓA ĐƠN) ===");

            // 1. Xuất hóa đơn bán lẻ
            QuyTrinhXuatHoaDonTemplate hdBanLe = new HoaDonBanLe("HD_BL_001", 750_000);
            hdBanLe.XuatHoaDon();

            // 2. Xuất hóa đơn GTGT Doanh nghiệp
            QuyTrinhXuatHoaDonTemplate hdDoanhNghiep = new HoaDonDoanhNghiepGTGT(
                "HD_DN_002",
                25_000_000,
                "0312345678",
                "CÔNG TY TNHH PHẦN MỀM ABC"
            );
            hdDoanhNghiep.XuatHoaDon();

            Console.ReadLine();
        }
    }
}