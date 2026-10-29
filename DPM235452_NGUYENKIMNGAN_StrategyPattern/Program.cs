using System;
using System.Collections.Generic;

namespace DPM235452_NGUYENKIMNGAN_StrategyPattern
{
    // Strategy Interface: Định nghĩa thuật toán chung
    public interface IStrategy
    {
        object DoAlgorithm(object data);
    }

    // Concrete Strategy A: Sắp xếp tăng dần
    public class ConcreteStrategyA : IStrategy
    {
        public object DoAlgorithm(object data)
        {
            var list = data as List<string>;
            list.Sort();
            return list;
        }
    }

    // Concrete Strategy B: Sắp xếp giảm dần
    public class ConcreteStrategyB : IStrategy
    {
        public object DoAlgorithm(object data)
        {
            var list = data as List<string>;
            list.Sort();
            list.Reverse();
            return list;
        }
    }

    // Context: Chứa tham chiếu tới đối tượng Strategy và cho phép đổi Strategy lúc runtime
    public class Context
    {
        private IStrategy _strategy;

        public Context() { }

        public Context(IStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(IStrategy strategy)
        {
            _strategy = strategy;
        }

        public void DoSomeBusinessLogic()
        {
            Console.WriteLine("Context: Đang thực thi thuật toán theo Strategy hiện tại...");
            var result = _strategy.DoAlgorithm(new List<string> { "a", "b", "c", "d", "e" });

            string resultStr = string.Join(",", (List<string>)result);
            Console.WriteLine($"Kết quả: {resultStr}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 8: STRATEGY PATTERN (LÝ THUYẾT) ===");

            var context = new Context();

            Console.WriteLine("--- Thực thi với Strategy A (Tăng dần) ---");
            context.SetStrategy(new ConcreteStrategyA());
            context.DoSomeBusinessLogic();

            Console.WriteLine("\n--- Thực thi với Strategy B (Giảm dần) ---");
            context.SetStrategy(new ConcreteStrategyB());
            context.DoSomeBusinessLogic();

            Console.ReadLine();
        }
    }
}