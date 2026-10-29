using System;

namespace DPM235452_NGUYENKIMNGAN_ChainOfResponsibilityPattern
{
    // Handler Interface
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);
        object Handle(object request);
    }

    // Abstract Handler
    public abstract class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;

        public IHandler SetNext(IHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public virtual object Handle(object request)
        {
            if (_nextHandler != null)
            {
                return _nextHandler.Handle(request);
            }
            return null;
        }
    }

    // Concrete Handler 1
    public class ConcreteHandlerA : AbstractHandler
    {
        public override object Handle(object request)
        {
            if (request.ToString() == "A")
            {
                return $"ConcreteHandlerA: Đã xử lý yêu cầu {request}.\n";
            }
            return base.Handle(request);
        }
    }

    // Concrete Handler 2
    public class ConcreteHandlerB : AbstractHandler
    {
        public override object Handle(object request)
        {
            if (request.ToString() == "B")
            {
                return $"ConcreteHandlerB: Đã xử lý yêu cầu {request}.\n";
            }
            return base.Handle(request);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 1: CHAIN OF RESPONSIBILITY PATTERN (LÝ THUYẾT) ===");

            var handlerA = new ConcreteHandlerA();
            var handlerB = new ConcreteHandlerB();

            // Thiết lập chuỗi xử lý: A -> B
            handlerA.SetNext(handlerB);

            // Gửi yêu cầu qua chuỗi
            foreach (var req in new[] { "A", "B", "C" })
            {
                Console.WriteLine($"Gửi yêu cầu '{req}':");
                var result = handlerA.Handle(req);
                if (result != null)
                {
                    Console.Write($"  {result}");
                }
                else
                {
                    Console.WriteLine($"  Yêu cầu '{req}' không có Handler nào xử lý.\n");
                }
            }

            Console.ReadLine();
        }
    }
}