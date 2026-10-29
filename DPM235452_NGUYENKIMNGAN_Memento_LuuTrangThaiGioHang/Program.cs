using System;
using System.Collections.Generic;
using System.Linq;

namespace DPM235452_NGUYENKIMNGAN_Memento_LuuTrangThaiGioHang
{
    // Model Sản phẩm
    public class SanPhamItem
    {
        public string TenSP { get; set; }
        public double Gia { get; set; }

        public SanPhamItem(string ten, double gia)
        {
            TenSP = ten;
            Gia = gia;
        }

        public override string ToString() => $"{TenSP} ({Gia:N0} VNĐ)";
    }

    // Memento: Lưu snapshot danh sách sản phẩm trong giỏ hàng
    public class GioHangMemento
    {
        public List<SanPhamItem> DanhSachSanPhamSnapshot { get; }
        public DateTime ThoiGianLuu { get; }

        public GioHangMemento(List<SanPhamItem> items)
        {
            // Tạo bản sao deep copy danh sách
            DanhSachSanPhamSnapshot = new List<SanPhamItem>(items);
            ThoiGianLuu = DateTime.Now;
        }
    }

    // Originator: Giỏ hàng trực tuyến của khách hàng
    public class GioHangOriginator
    {
        private List<SanPhamItem> _danhSachSP = new List<SanPhamItem>();

        public void ThemSanPham(SanPhamItem sp)
        {
            _danhSachSP.Add(sp);
            Console.WriteLine($"[+] Đã thêm '{sp.TenSP}' vào giỏ hàng.");
        }

        public void XoaTatCa()
        {
            _danhSachSP.Clear();
            Console.WriteLine("[!] Đã xóa toàn bộ giỏ hàng.");
        }

        public void HienThiGioHang()
        {
            Console.WriteLine("\n--- CÁC SẢN PHẨM TRONG GIỎ HÀNG HIỆN TẠI ---");
            if (!_danhSachSP.Any())
            {
                Console.WriteLine("(Giỏ hàng trống)");
                return;
            }

            double tong = 0;
            foreach (var sp in _danhSachSP)
            {
                Console.WriteLine($"  - {sp}");
                tong += sp.Gia;
            }
            Console.WriteLine($"=> TỔNG TIỀN: {tong:N0} VNĐ");
        }

        // Tạo Memento
        public GioHangMemento LuuTrangThai()
        {
            return new GioHangMemento(_danhSachSP);
        }

        // Khôi phục từ Memento
        public void KhoiPhucTrangThai(GioHangMemento memento)
        {
            if (memento != null)
            {
                _danhSachSP = new List<SanPhamItem>(memento.DanhSachSanPhamSnapshot);
                Console.WriteLine($"\n[UNDO] Đã khôi phục lại giỏ hàng về mốc thời gian: {memento.ThoiGianLuu:HH:mm:ss}");
            }
        }
    }

    // Caretaker: Quản lý lịch sử thay đổi giỏ hàng
    public class LichSuGioHangCaretaker
    {
        private readonly Stack<GioHangMemento> _lichSu = new Stack<GioHangMemento>();

        public void LuuSnapshot(GioHangMemento memento)
        {
            _lichSu.Push(memento);
        }

        public GioHangMemento Undo()
        {
            if (_lichSu.Count > 0)
            {
                return _lichSu.Pop();
            }
            return null;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 5: MEMENTO PATTERN (LƯU TRẠNG THÁI GIỎ HÀNG) ===");

            GioHangOriginator gioHang = new GioHangOriginator();
            LichSuGioHangCaretaker caretaker = new LichSuGioHangCaretaker();

            // Thao tác 1: Thêm iPhone
            gioHang.ThemSanPham(new SanPhamItem("iPhone 15 Pro Max", 34_000_000));
            caretaker.LuuSnapshot(gioHang.LuuTrangThai()); // Lưu mốc 1

            // Thao tác 2: Thêm AirPods
            gioHang.ThemSanPham(new SanPhamItem("AirPods Pro 2", 5_900_000));
            caretaker.LuuSnapshot(gioHang.LuuTrangThai()); // Lưu mốc 2

            // Thao tác 3: Thêm Sạc Magsafe
            gioHang.ThemSanPham(new SanPhamItem("Sạc MagSafe", 1_200_000));
            gioHang.HienThiGioHang();

            // Khách hàng bấm Undo lần 1
            gioHang.KhoiPhucTrangThai(caretaker.Undo());
            gioHang.HienThiGioHang();

            // Khách hàng bấm Undo lần 2
            gioHang.KhoiPhucTrangThai(caretaker.Undo());
            gioHang.HienThiGioHang();

            Console.ReadLine();
        }
    }
}