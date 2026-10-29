using System;

namespace DPM235452_NGUYENKIMNGAN_Interpreter_PhanTichDieuKienGiamGia
{
    // Context Class: Thông tin đơn hàng
    public class DonHangContext
    {
        public string MaDonHang { get; set; }
        public string LoaiKhachHang { get; set; } // "VIP", "MEMBER", "GUEST"
        public double TongTien { get; set; }
        public bool IsSinhNhatKhachHang { get; set; }

        public DonHangContext(string maDonHang, string loaiKhachHang, double tongTien, bool isSinhNhat)
        {
            MaDonHang = maDonHang;
            LoaiKhachHang = loaiKhachHang;
            TongTien = tongTien;
            IsSinhNhatKhachHang = isSinhNhat;
        }
    }

    // Abstract Expression
    public interface IBieuThucKhuyenMai
    {
        bool Interpret(DonHangContext context);
    }

    // Terminal Expression 1: Điều kiện Loại khách hàng
    public class DieuKienLoaiKhachExpression : IBieuThucKhuyenMai
    {
        private readonly string _loaiKhachYeuCau;

        public DieuKienLoaiKhachExpression(string loaiKhachYeuCau)
        {
            _loaiKhachYeuCau = loaiKhachYeuCau;
        }

        public bool Interpret(DonHangContext context)
        {
            return context.LoaiKhachHang.Equals(_loaiKhachYeuCau, StringComparison.OrdinalIgnoreCase);
        }
    }

    // Terminal Expression 2: Điều kiện Tổng tiền đơn hàng tối thiểu
    public class DieuKienTongTienExpression : IBieuThucKhuyenMai
    {
        private readonly double _tongTienToiThieu;

        public DieuKienTongTienExpression(double tongTienToiThieu)
        {
            _tongTienToiThieu = tongTienToiThieu;
        }

        public bool Interpret(DonHangContext context)
        {
            return context.TongTien >= _tongTienToiThieu;
        }
    }

    // Terminal Expression 3: Điều kiện Sinh nhật
    public class DieuKienSinhNhatExpression : IBieuThucKhuyenMai
    {
        public bool Interpret(DonHangContext context)
        {
            return context.IsSinhNhatKhachHang;
        }
    }

    // Nonterminal Expression 1: Logic AND
    public class AndExpression : IBieuThucKhuyenMai
    {
        private readonly IBieuThucKhuyenMai _expr1;
        private readonly IBieuThucKhuyenMai _expr2;

        public AndExpression(IBieuThucKhuyenMai expr1, IBieuThucKhuyenMai expr2)
        {
            _expr1 = expr1;
            _expr2 = expr2;
        }

        public bool Interpret(DonHangContext context)
        {
            return _expr1.Interpret(context) && _expr2.Interpret(context);
        }
    }

    // Nonterminal Expression 2: Logic OR
    public class OrExpression : IBieuThucKhuyenMai
    {
        private readonly IBieuThucKhuyenMai _expr1;
        private readonly IBieuThucKhuyenMai _expr2;

        public OrExpression(IBieuThucKhuyenMai expr1, IBieuThucKhuyenMai expr2)
        {
            _expr1 = expr1;
            _expr2 = expr2;
        }

        public bool Interpret(DonHangContext context)
        {
            return _expr1.Interpret(context) || _expr2.Interpret(context);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 11: INTERPRETER PATTERN (ĐÁNH GIÁ ĐIỀU KIỆN KHUYẾN MÃI) ===");

            // Thiết lập Quy tắc 1: Khách VIP VÀ Tổng tiền >= 5.000.000 VNĐ -> Được giảm 20%
            IBieuThucKhuyenMai laKhachVip = new DieuKienLoaiKhachExpression("VIP");
            IBieuThucKhuyenMai donTu5Trieu = new DieuKienTongTienExpression(5_000_000);
            IBieuThucKhuyenMai quyTacKhuyenMaiKhachVIP = new AndExpression(laKhachVip, donTu5Trieu);

            // Thiết lập Quy tắc 2: Quy tắc 1 HOẶC Sinh nhật Khách hàng -> Được tặng quà đặc biệt
            IBieuThucKhuyenMai laSinhNhat = new DieuKienSinhNhatExpression();
            IBieuThucKhuyenMai quyTacNhanQuaMienPhi = new OrExpression(quyTacKhuyenMaiKhachVIP, laSinhNhat);

            // Giả lập các đơn hàng
            DonHangContext dh1 = new DonHangContext("DH001", "VIP", 6_000_000, false);
            DonHangContext dh2 = new DonHangContext("DH002", "MEMBER", 10_000_000, true);
            DonHangContext dh3 = new DonHangContext("DH003", "VIP", 2_000_000, false);

            Console.WriteLine("--- 1. ĐƠN HÀNG DH001 (VIP, 6tr, Không sinh nhật) ---");
            Console.WriteLine($"  - Được giảm 20%: {quyTacKhuyenMaiKhachVIP.Interpret(dh1)}");
            Console.WriteLine($"  - Được tặng quà: {quyTacNhanQuaMienPhi.Interpret(dh1)}");

            Console.WriteLine("\n--- 2. ĐƠN HÀNG DH002 (MEMBER, 10tr, Có sinh nhật) ---");
            Console.WriteLine($"  - Được giảm 20%: {quyTacKhuyenMaiKhachVIP.Interpret(dh2)}");
            Console.WriteLine($"  - Được tặng quà: {quyTacNhanQuaMienPhi.Interpret(dh2)}");

            Console.WriteLine("\n--- 3. ĐƠN HÀNG DH003 (VIP, 2tr, Không sinh nhật) ---");
            Console.WriteLine($"  - Được giảm 20%: {quyTacKhuyenMaiKhachVIP.Interpret(dh3)}");
            Console.WriteLine($"  - Được tặng quà: {quyTacNhanQuaMienPhi.Interpret(dh3)}");

            Console.ReadLine();
        }
    }
}