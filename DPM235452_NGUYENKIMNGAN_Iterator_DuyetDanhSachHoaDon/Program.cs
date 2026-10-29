using System;
using System.Collections.Generic;

namespace DPM235452_NGUYENKIMNGAN_Iterator_DuyetDanhSachHoaDon
{
    // Model Hóa đơn
    public class HoaDon
    {
        public string MaHoaDon { get; set; }
        public string TenKhachHang { get; set; }
        public double TongTien { get; set; }
        public bool DaThanhToan { get; set; }

        public HoaDon(string ma, string ten, double tien, bool daThanhToan)
        {
            MaHoaDon = ma;
            TenKhachHang = ten;
            TongTien = tien;
            DaThanhToan = daThanhToan;
        }

        public override string ToString()
        {
            string trangThai = DaThanhToan ? "ĐÃ THANH TOÁN" : "CHƯA THANH TOÁN";
            return $"[{MaHoaDon}] Khách: {TenKhachHang} | Tiền: {TongTien:N0} VNĐ | Trạng thái: {trangThai}";
        }
    }

    // Iterator Interface
    public interface IIteratorHoaDon
    {
        HoaDon First();
        HoaDon Next();
        bool IsDone { get; }
        HoaDon CurrentItem { get; }
    }

    // Aggregate Interface
    public interface ITapHopHoaDon
    {
        IIteratorHoaDon CreateIterator();
    }

    // Concrete Aggregate: Bộ sưu tập hóa đơn
    public class QuanLyHoaDonCollection : ITapHopHoaDon
    {
        private readonly List<HoaDon> _danhSachHoaDon = new List<HoaDon>();

        public void ThemHoaDon(HoaDon hd) => _danhSachHoaDon.Add(hd);
        public int SoLuong => _danhSachHoaDon.Count;
        public HoaDon GetAt(int index) => _danhSachHoaDon[index];

        public IIteratorHoaDon CreateIterator()
        {
            return new HoaDonIterator(this);
        }
    }

    // Concrete Iterator: Bộ duyệt hóa đơn
    public class HoaDonIterator : IIteratorHoaDon
    {
        private readonly QuanLyHoaDonCollection _collection;
        private int _current = 0;

        public HoaDonIterator(QuanLyHoaDonCollection collection)
        {
            _collection = collection;
        }

        public HoaDon First()
        {
            _current = 0;
            return _collection.SoLuong > 0 ? _collection.GetAt(0) : null;
        }

        public HoaDon Next()
        {
            _current++;
            if (!IsDone)
            {
                return _collection.GetAt(_current);
            }
            return null;
        }

        public bool IsDone => _current >= _collection.SoLuong;

        public HoaDon CurrentItem => !IsDone ? _collection.GetAt(_current) : null;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 3: ITERATOR PATTERN (DUYỆT DANH SÁCH HÓA ĐƠN BÁN HÀNG) ===");

            QuanLyHoaDonCollection danhSach = new QuanLyHoaDonCollection();
            danhSach.ThemHoaDon(new HoaDon("HD001", "Nguyễn Văn A", 1_200_000, true));
            danhSach.ThemHoaDon(new HoaDon("HD002", "Trần Thị B", 4_500_000, false));
            danhSach.ThemHoaDon(new HoaDon("HD003", "Lê Văn C", 850_000, true));
            danhSach.ThemHoaDon(new HoaDon("HD004", "Phạm Thị D", 12_000_000, false));

            // Khởi tạo Iterator duyệt qua danh sách
            IIteratorHoaDon iterator = danhSach.CreateIterator();

            Console.WriteLine("\n--- DÙNG ITERATOR DUYỆT TẤT CẢ HÓA ĐƠN ---");
            for (HoaDon hd = iterator.First(); !iterator.IsDone; hd = iterator.Next())
            {
                Console.WriteLine(hd);
            }

            Console.ReadLine();
        }
    }
}