using DPM235452_NGUYENKIMNGAN_Visitor_ThongKeDoanhThu;
using System;
using System.Collections.Generic;

namespace DPM235452_NGUYENKIMNGAN_Visitor_ThongKeDoanhThu
{
    // Element Interface: Định nghĩa điểm chấp nhận Visitor
    public interface ISanPhamElement
    {
        void Accept(IBaoCaoVisitor visitor);
    }

    // Concrete Element 1: Sản phẩm Điện tử
    public class SanPhamDienTu : ISanPhamElement
    {
        public string TenSP { get; set; }
        public double GiaGoc { get; set; }
        public double PhiBaoHanh { get; set; }

        public SanPhamDienTu(string tenSP, double giaGoc, double phiBaoHanh)
        {
            TenSP = tenSP;
            GiaGoc = giaGoc;
            PhiBaoHanh = phiBaoHanh;
        }

        public void Accept(IBaoCaoVisitor visitor)
        {
            visitor.VisitDienTu(this);
        }
    }

    // Concrete Element 2: Sản phẩm Thực phẩm
    public class SanPhamThucPham : ISanPhamElement
    {
        public string TenSP { get; set; }
        public double GiaGoc { get; set; }
        public double TyLeHaoHut { get; set; } // Tỷ lệ hư hỏng/quá hạn dự kiến (%)

        public SanPhamThucPham(string tenSP, double giaGoc, double tyLeHaoHut)
        {
            TenSP = tenSP;
            GiaGoc = giaGoc;
            TyLeHaoHut = tyLeHaoHut;
        }

        public void Accept(IBaoCaoVisitor visitor)
        {
            visitor.VisitThucPham(this);
        }
    }

    // Visitor Interface
    public interface IBaoCaoVisitor
    {
        void VisitDienTu(SanPhamDienTu sp);
        void VisitThucPham(SanPhamThucPham sp);
    }

    // Concrete Visitor 1: Tính tổng giá trị doanh thu thuần (sau VAT)
    public class TinhDoanhThuThu Visitor : IBaoCaoVisitor
    {
        public double TongDoanhThu { get; private set; } = 0;

    public void VisitDienTu(SanPhamDienTu sp)
    {
        // Đồ điện tử chịu thuế VAT 10% + tiền phí bảo hành
        double doanhThu = (sp.GiaGoc + sp.PhiBaoHanh) * 1.10;
        TongDoanhThu += doanhThu;
        Console.WriteLine($"  + [Điện tử] {sp.TenSP}: {doanhThu:N0} VNĐ (Đã gồm VAT 10% & Phí BH)");
    }

    public void VisitThucPham(SanPhamThucPham sp)
    {
        // Thực phẩm thuế VAT 5% trừ đi hao hụt
        double doanhThu = (sp.GiaGoc * (1 - sp.TyLeHaoHut)) * 1.05;
        TongDoanhThu += doanhThu;
        Console.WriteLine($"  + [Thực phẩm] {sp.TenSP}: {doanhThu:N0} VNĐ (Đã gồm VAT 5% & Khấu trừ hao hụt)");
    }
}

// Concrete Visitor 2: Tính tổng nghĩa vụ thuế phải nộp
public class TinhThueVATVisitor : IBaoCaoVisitor
{
    public double TongThue { get; private set; } = 0;

    public void VisitDienTu(SanPhamDienTu sp)
    {
        double thue = (sp.GiaGoc + sp.PhiBaoHanh) * 0.10;
        TongThue += thue;
        Console.WriteLine($"  + [Thuế Điện tử 10%] {sp.TenSP}: {thue:N0} VNĐ");
    }

    public void VisitThucPham(SanPhamThucPham sp)
    {
        double thue = (sp.GiaGoc * (1 - sp.TyLeHaoHut)) * 0.05;
        TongThue += thue;
        Console.WriteLine($"  + [Thuế Thực phẩm 5%] {sp.TenSP}: {thue:N0} VNĐ");
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("=== MẪU 10: VISITOR PATTERN (THỐNG KÊ DOANH THU & THUẾ KHO HÀNG) ===");

        List<ISanPhamElement> danhSachKho = new List<ISanPhamElement>
            {
                new SanPhamDienTu("Laptop Dell XPS 15", 35_000_000, 1_500_000),
                new SanPhamDienTu("iPhone 15 Pro", 28_000_000, 1_000_000),
                new SanPhamThucPham("Táo Envy Import", 500_000, 0.05), // Hao hụt 5%
                new SanPhamThucPham("Thịt Bò Mỹ Prime", 1_200_000, 0.02)  // Hao hụt 2%
            };

        // 1. Thống kê Doanh thu
        Console.WriteLine("--- 1. BÁO CÁO TỔNG DOANH THU ---");
        var visitorDoanhThu = new TinhDoanhThuThu Visitor();
        foreach (var sp in danhSachKho)
        {
            sp.Accept(visitorDoanhThu);
        }
        Console.WriteLine($"=> TỔNG DOANH THU DỰ KIẾN: {visitorDoanhThu.TongDoanhThu:N0} VNĐ\n");

        // 2. Thống kê Thuế VAT
        Console.WriteLine("--- 2. BÁO CÁO NGHĨA VỤ THUẾ VAT ---");
        var visitorThue = new TinhThueVATVisitor();
        foreach (var sp in danhSachKho)
        {
            sp.Accept(visitorThue);
        }
        Console.WriteLine($"=> TỔNG THUẾ VAT PHẢI NỘP: {visitorThue.TongThue:N0} VNĐ");

        Console.ReadLine();
    }
}
}