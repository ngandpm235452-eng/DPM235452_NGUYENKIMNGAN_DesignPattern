using System;

namespace DPM235452_NGUYENKIMNGAN_ChainOfResponsibility_XetDuyetGiamGia
{
    // Request Class
    public class YeuCauGiamGia
    {
        public string MaDonHang { get; set; }
        public double PhanTramGiam { get; set; }
        public double TongTienDonHang { get; set; }

        public YeuCauGiamGia(string maDonHang, double phanTramGiam, double tongTienDonHang)
        {
            MaDonHang = maDonHang;
            PhanTramGiam = phanTramGiam;
            TongTienDonHang = tongTienDonHang;
        }
    }

    // Abstract Handler
    public abstract class NguoiDuyetHandler
    {
        protected NguoiDuyetHandler _capTren;

        public void SetCapTren(NguoiDuyetHandler capTren)
        {
            _capTren = capTren;
        }

        public abstract void XuLyYeuCau(YeuCauGiamGia yeuCau);
    }

    // Concrete Handler 1: Nhân viên bán hàng (Duyệt giảm <= 5%)
    public class NhanVienBanHangHandler : NguoiDuyetHandler
    {
        public override void XuLyYeuCau(YeuCauGiamGia yeuCau)
        {
            if (yeuCau.PhanTramGiam <= 5)
            {
                Console.WriteLine($"[ĐÃ DUYỆT - NHÂN VIÊN] Chấp nhận giảm {yeuCau.PhanTramGiam}% cho đơn '{yeuCau.MaDonHang}'.");
            }
            else if (_capTren != null)
            {
                Console.WriteLine($"[CHUYỂN CẤP] Nhân viên không đủ thẩm quyền duyệt {yeuCau.PhanTramGiam}% -> Chuyển lên Trưởng phòng.");
                _capTren.XuLyYeuCau(yeuCau);
            }
        }
    }

    // Concrete Handler 2: Trưởng phòng kinh doanh (Duyệt giảm <= 15%)
    public class TruongPhongHandler : NguoiDuyetHandler
    {
        public override void XuLyYeuCau(YeuCauGiamGia yeuCau)
        {
            if (yeuCau.PhanTramGiam <= 15)
            {
                Console.WriteLine($"[ĐÃ DUYỆT - TRƯỞNG PHÒNG] Chấp nhận giảm {yeuCau.PhanTramGiam}% cho đơn '{yeuCau.MaDonHang}'.");
            }
            else if (_capTren != null)
            {
                Console.WriteLine($"[CHUYỂN CẤP] Trưởng phòng không đủ thẩm quyền duyệt {yeuCau.PhanTramGiam}% -> Chuyển lên Giám đốc.");
                _capTren.XuLyYeuCau(yeuCau);
            }
        }
    }

    // Concrete Handler 3: Giám đốc (Duyệt giảm <= 30%)
    public class GiamDocHandler : NguoiDuyetHandler
    {
        public override void XuLyYeuCau(YeuCauGiamGia yeuCau)
        {
            if (yeuCau.PhanTramGiam <= 30)
            {
                Console.WriteLine($"[ĐÃ DUYỆT - GIÁM ĐỐC] Chấp nhận duyệt đặc biệt giảm {yeuCau.PhanTramGiam}% cho đơn '{yeuCau.MaDonHang}'.");
            }
            else
            {
                Console.WriteLine($"[TỪ CHỐI - GIÁM ĐỐC] Yêu cầu giảm {yeuCau.PhanTramGiam}% vượt quá mức cho phép tối đa (30%)!");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 1: CHAIN OF RESPONSIBILITY PATTERN (XÉT DUYỆT GIẢM GIÁ) ===");

            // Thiết lập chuỗi cấp bậc: Nhân viên -> Trưởng phòng -> Giám đốc
            NguoiDuyetHandler nhanVien = new NhanVienBanHangHandler();
            NguoiDuyetHandler truongPhong = new TruongPhongHandler();
            NguoiDuyetHandler giamDoc = new GiamDocHandler();

            nhanVien.SetCapTren(truongPhong);
            truongPhong.SetCapTren(giamDoc);

            // Thử nghiệm gửi các mức giảm giá khác nhau
            Console.WriteLine("\n--- Đơn hàng 1: Yêu cầu giảm 4% ---");
            nhanVien.XuLyYeuCau(new YeuCauGiamGia("DH_001", 4, 2_000_000));

            Console.WriteLine("\n--- Đơn hàng 2: Yêu cầu giảm 12% ---");
            nhanVien.XuLyYeuCau(new YeuCauGiamGia("DH_002", 12, 15_000_000));

            Console.WriteLine("\n--- Đơn hàng 3: Yêu cầu giảm 25% ---");
            nhanVien.XuLyYeuCau(new YeuCauGiamGia("DH_003", 25, 50_000_000));

            Console.WriteLine("\n--- Đơn hàng 4: Yêu cầu giảm 40% ---");
            nhanVien.XuLyYeuCau(new YeuCauGiamGia("DH_004", 40, 100_000_000));

            Console.ReadLine();
        }
    }
}