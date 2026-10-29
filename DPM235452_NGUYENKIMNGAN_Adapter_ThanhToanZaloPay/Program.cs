using System;

namespace DPM235452_NGUYENKIMNGAN_Adapter_ThanhToanZaloPay
{
    // Target interface thanh toán chuẩn của hệ thống Bán hàng
    public interface IThanhToanHeThong
    {
        void ThanhToan(double soTien);
    }

    // Adaptee: Cổng thanh toán ZaloPay cũ (API khác biệt)
    public class ZaloPayService
    {
        public void SendZaloPayPayment(decimal amount)
        {
            Console.WriteLine($"[ZaloPay API] Đã xử lý giao dịch thanh toán: {amount:N0} VNĐ thành công.");
        }
    }

    // Adapter: Chuyển đổi ZaloPayService sang chuẩn IThanhToanHeThong
    public class ZaloPayAdapter : IThanhToanHeThong
    {
        private readonly ZaloPayService _zaloPayService;

        public ZaloPayAdapter(ZaloPayService zaloPayService)
        {
            _zaloPayService = zaloPayService;
        }

        public void ThanhToan(double soTien)
        {
            decimal amountInDecimal = Convert.ToDecimal(soTien);
            _zaloPayService.SendZaloPayPayment(amountInDecimal);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== MẪU 1: ADAPTER PATTERN (THỰC TẾ BÁN HÀNG) ===");

            ZaloPayService oldService = new ZaloPayService();
            IThanhToanHeThong congThanhToan = new ZaloPayAdapter(oldService);

            congThanhToan.ThanhToan(1500000);

            Console.ReadLine();
        }
    }
}