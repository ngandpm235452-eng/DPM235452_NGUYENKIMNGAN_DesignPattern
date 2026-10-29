using System;

namespace DPM235452_NGUYENKIMNGAN_Mediator_DieuHueKhoVaBanHang
{
    // Mediator Interface
    public interface ITrungTamDieuHue
    {
        void ThongBao(object phongBanSend, string suKien, string maDon);
    }

    // Base Department
    public abstract class PhongBan
    {
        protected ITrungTamDieuHue _trungTam;

        public PhongBan(ITrungTamDieuHue trungTam)
        {
            _trungTam = trungTam;
        }
    }

    // Concrete Component 1: Bộ phận Bán hàng
    public class BoPhanBanHang : PhongBan
    {
        public BoPhanBanHang(ITrungTamDieuHue trungTam) : base(trungTam) { }

        public void TaoDonHang(string maDon)
        {
            Console.WriteLine($"[BÁN HÀNG] Khách đặt đơn mới '{maDon}'.");
            _trungTam.ThongBao(this, "TAO_DON", maDon);
        }

        public void ThongBaoKhachHang(string maDon, string noiDung)
        {
            Console.WriteLine($"[BÁN HÀNG] Gửi thông báo tới Khách hàng đơn '{maDon}': {noiDung}");
        }
    }

    // Concrete Component 2: Bộ phận Kho
    public class BoPhanKho : PhongBan
    {
        public BoPhanKho(ITrungTamDieuHue trungTam) : base(trungTam) { }

        public void XuatKho(string maDon)
        {
            Console.WriteLine($"[KHO HÀNG] Đã kiểm tra & đóng gói xuất kho cho đơn '{maDon}'.");
            _trungTam.ThongBao(this, "XUAT_KHO_THANH_CONG", maDon);
        }
    }

    // Concrete Component 3: Bộ phận Kế toán
    public class BoPhanKeToan : PhongBan
    {
        public BoPhanKeToan(ITrungTamDieuHue trungTam) : base(trungTam) { }

        public void XuatHoaDon(string maDon)
        {
            Console.WriteLine($"[KẾ TOÁN] Đã lập hóa đơn GTGT cho đơn '{maDon}'.");
            _trungTam.ThongBao(this, "XUAT_HOA_DON_THANH_CONG", maDon);
        }
    }

    // Concrete Mediator: Trung tâm điều phối giao tiếp liên phòng ban
    public class TrungTamDieuHueMediator : ITrungTamDieuHue
    {
        public BoPhanBanHang BanHang { get; set; }
        public BoPhanKho Kho { get; set; }
        public BoPhanKeToan KeToan { get; set; }

        public void ThongBao(object phongBanSend, string suKien, string maDon)
        {
            if (suKien == "TAO_DON")
            {
                Console.WriteLine("==> [MEDIATOR] Nhận đơn mới -> Lệnh Kho xuất hàng & Kế toán xuất hóa đơn...");
                Kho?.XuatKho(maDon);
                KeToan?.XuatHoaDon(maDon);
            }
            else if (suKien == "XUAT_KHO_THANH_CONG")
            {
                Console.WriteLine("==> [MEDIATOR] Nhận báo cáo Kho -> Bán hàng cập nhật giao vận.");
                BanHang?.ThongBaoKhachHang(maDon, "Đơn hàng đang trên đường giao.");
            }
            else if (suKien == "XUAT_HOA_DON_THANH_CONG")
            {
                Console.WriteLine("==> [MEDIATOR] Nhận báo cáo Kế toán -> Lưu nhật ký doanh thu.");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 4: MEDIATOR PATTERN (ĐIỀU HUẾ KHO & BÁN HÀNG) ===");

            TrungTamDieuHueMediator mediator = new TrungTamDieuHueMediator();

            BoPhanBanHang banHang = new BoPhanBanHang(mediator);
            BoPhanKho kho = new BoPhanKho(mediator);
            BoPhanKeToan keToan = new BoPhanKeToan(mediator);

            mediator.BanHang = banHang;
            mediator.Kho = kho;
            mediator.KeToan = keToan;

            // Bán hàng tạo đơn -> Mediator tự động điều phối Kho và Kế toán làm việc
            banHang.TaoDonHang("DH_2026_001");

            Console.ReadLine();
        }
    }
}