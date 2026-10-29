using System;

namespace DPM235452_NGUYENKIMNGAN_Bridge_PhuongThucGiaoHang
{
    // Implementor Interface: Đơn vị vận chuyển thứ 3
    public interface IDonViVanChuyen
    {
        void GiaoHang(string tenDonHang, double phiCoBan);
    }

    public class GiaoHangNhanh : IDonViVanChuyen
    {
        public void GiaoHang(string tenDonHang, double phiCoBan)
        {
            double tongPhi = phiCoBan + 15_000;
            Console.WriteLine($"[GHN Express] Xử lý đơn '{tenDonHang}' | Phí giao hàng: {tongPhi:N0} VNĐ");
        }
    }

    public class GiaoHangTietKiem : IDonViVanChuyen
    {
        public void GiaoHang(string tenDonHang, double phiCoBan)
        {
            double tongPhi = phiCoBan + 5_000;
            Console.WriteLine($"[GHTK Economy] Xử lý đơn '{tenDonHang}' | Phí giao hàng: {tongPhi:N0} VNĐ");
        }
    }

    // Abstraction: Loại đơn hàng của hệ thống Bán Hàng
    public abstract class DonHang
    {
        protected IDonViVanChuyen _donViVanChuyen;
        public string TenDonHang { get; set; }
        public double GiaTri { get; set; }

        protected DonHang(string tenDonHang, double giaTri, IDonViVanChuyen donViVanChuyen)
        {
            TenDonHang = tenDonHang;
            GiaTri = giaTri;
            _donViVanChuyen = donViVanChuyen;
        }

        public abstract void XuLyGiaoHang();
    }

    // Refined Abstractions
    public class DonHangHoaToc : DonHang
    {
        public DonHangHoaToc(string ten, double giaTri, IDonViVanChuyen donVi)
            : base(ten, giaTri, donVi) { }

        public override void XuLyGiaoHang()
        {
            Console.WriteLine($"-> Loại đơn: [HỎA TỐC] - Sản phẩm: {TenDonHang} ({GiaTri:N0} VNĐ)");
            _donViVanChuyen.GiaoHang(TenDonHang, 30_000); // Phí cơ bản đơn hỏa tốc là 30k
        }
    }

    public class DonHangTieuChuan : DonHang
    {
        public DonHangTieuChuan(string ten, double giaTri, IDonViVanChuyen donVi)
            : base(ten, giaTri, donVi) { }

        public override void XuLyGiaoHang()
        {
            Console.WriteLine($"-> Loại đơn: [TIÊU CHUẨN] - Sản phẩm: {TenDonHang} ({GiaTri:N0} VNĐ)");
            _donViVanChuyen.GiaoHang(TenDonHang, 10_000); // Phí cơ bản đơn tiêu chuẩn là 10k
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== MẪU 2: BRIDGE PATTERN (THỰC TẾ BÁN HÀNG) ===");

            IDonViVanChuyen ghn = new GiaoHangNhanh();
            IDonViVanChuyen ghtk = new GiaoHangTietKiem();

            // Kết hợp Đơn hỏa tốc với GiaoHangNhanh
            DonHang don1 = new DonHangHoaToc("Laptop Dell XPS 15", 35_000_000, ghn);
            don1.XuLyGiaoHang();

            Console.WriteLine();

            // Kết hợp Đơn tiêu chuẩn với GiaoHangTietKiem
            DonHang don2 = new DonHangTieuChuan("Chuột Logi MX Master 3S", 2_500_000, ghtk);
            don2.XuLyGiaoHang();

            Console.ReadLine();
        }
    }
}