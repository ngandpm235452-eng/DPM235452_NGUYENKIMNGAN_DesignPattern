using System;
using System.Collections.Generic;

namespace DPM235452_NGUYENKIMNGAN_Command_XuLyDonHang
{
    // Command Interface
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    // Receiver: Thực hiện thao tác trực tiếp trên CSDL / Hệ thống
    public class DonHangReceiver
    {
        public void TaoDonHang(string maDon, double tongTien)
        {
            Console.WriteLine($"[CSDL] TẠO MỚI đơn hàng '{maDon}' | Tổng tiền: {tongTien:N0} VNĐ thành công.");
        }

        public void XoaDonHang(string maDon)
        {
            Console.WriteLine($"[CSDL] XÓA/HỦY đơn hàng '{maDon}' khỏi hệ thống.");
        }

        public void CapNhatGiamGia(string maDon, double phanTram)
        {
            Console.WriteLine($"[CSDL] ÁP DỤNG giảm giá {phanTram}% cho đơn hàng '{maDon}'.");
        }
    }

    // Concrete Command 1: Lệnh tạo đơn hàng
    public class TaoDonHangCommand : ICommand
    {
        private readonly DonHangReceiver _receiver;
        private readonly string _maDon;
        private readonly double _tongTien;

        public TaoDonHangCommand(DonHangReceiver receiver, string maDon, double tongTien)
        {
            _receiver = receiver;
            _maDon = maDon;
            _tongTien = tongTien;
        }

        public void Execute()
        {
            _receiver.TaoDonHang(_maDon, _tongTien);
        }

        public void Undo()
        {
            Console.WriteLine($"[UNDO] Hoàn tác Tạo đơn -> Tiến hành xóa đơn '{_maDon}'");
            _receiver.XoaDonHang(_maDon);
        }
    }

    // Concrete Command 2: Lệnh áp dụng giảm giá
    public class GiamGiaCommand : ICommand
    {
        private readonly DonHangReceiver _receiver;
        private readonly string _maDon;
        private readonly double _phanTram;

        public GiamGiaCommand(DonHangReceiver receiver, string maDon, double phanTram)
        {
            _receiver = receiver;
            _maDon = maDon;
            _phanTram = phanTram;
        }

        public void Execute()
        {
            _receiver.CapNhatGiamGia(_maDon, _phanTram);
        }

        public void Undo()
        {
            Console.WriteLine($"[UNDO] Hoàn tác Giảm giá -> Hủy mức giảm {0}% cho đơn '{_maDon}'");
            _receiver.CapNhatGiamGia(_maDon, 0);
        }
    }

    // Invoker: Điều khiển thực thi và hỗ trợ Lịch sử Undo
    public class QuanLyThaoTacInvoker
    {
        private readonly Stack<ICommand> _lichSuThaoTac = new Stack<ICommand>();

        public void ThucHienThaoTac(ICommand command)
        {
            command.Execute();
            _lichSuThaoTac.Push(command);
        }

        public void HoanTac()
        {
            if (_lichSuThaoTac.Count > 0)
            {
                ICommand commandGanhNhat = _lichSuThaoTac.Pop();
                commandGanhNhat.Undo();
            }
            else
            {
                Console.WriteLine("Không có thao tác nào để hoàn tác (Undo)!");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 2: COMMAND PATTERN (THỰC TẾ XỬ LÝ ĐƠN HÀNG) ===");

            DonHangReceiver receiver = new DonHangReceiver();
            QuanLyThaoTacInvoker invoker = new QuanLyThaoTacInvoker();

            Console.WriteLine("--- 1. Thực hiện tạo đơn hàng DH_101 ---");
            ICommand cmd1 = new TaoDonHangCommand(receiver, "DH_101", 15_000_000);
            invoker.ThucHienThaoTac(cmd1);

            Console.WriteLine("\n--- 2. Thực hiện áp dụng giảm giá 10% ---");
            ICommand cmd2 = new GiamGiaCommand(receiver, "DH_101", 10);
            invoker.ThucHienThaoTac(cmd2);

            Console.WriteLine("\n--- 3. Thực hiện Undo (Hoàn tác lần 1: Hủy giảm giá) ---");
            invoker.HoanTac();

            Console.WriteLine("\n--- 4. Thực hiện Undo (Hoàn tác lần 2: Hủy tạo đơn hàng) ---");
            invoker.HoanTac();

            Console.ReadLine();
        }
    }
}