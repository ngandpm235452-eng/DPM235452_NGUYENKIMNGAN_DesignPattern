using System;

namespace DPM235452_NGUYENKIMNGAN_ProxyPattern
{
    // Subject: Giao diện chung cho RealSubject và Proxy
    public interface ISubject
    {
        void Request();
    }

    // RealSubject: Đối tượng thực sự xử lý nghiệp vụ chính
    public class RealSubject : ISubject
    {
        public void Request()
        {
            Console.WriteLine("RealSubject: Đang thực thi xử lý yêu cầu chính...");
        }
    }

    // Proxy: Ủy nhiệm kiểm soát truy cập đến RealSubject
    public class Proxy : ISubject
    {
        private RealSubject _realSubject;

        public void Request()
        {
            if (_realSubject == null)
            {
                _realSubject = new RealSubject();
            }

            if (CheckAccess())
            {
                _realSubject.Request();
                LogAccess();
            }
        }

        private bool CheckAccess()
        {
            Console.WriteLine("Proxy: Kiểm tra quyền truy cập trước khi chuyển tiếp yêu cầu...");
            return true;
        }

        private void LogAccess()
        {
            Console.WriteLine("Proxy: Ghi nhận nhật ký (Log) thời gian truy cập thành công.");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 7: PROXY PATTERN (LÝ THUYẾT) ===");

            ISubject proxy = new Proxy();
            proxy.Request();

            Console.ReadLine();
        }
    }
}