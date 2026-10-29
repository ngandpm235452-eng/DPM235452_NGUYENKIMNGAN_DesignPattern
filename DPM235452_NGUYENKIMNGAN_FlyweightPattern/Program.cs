using System;
using System.Collections.Generic;

namespace DPM235452_NGUYENKIMNGAN_FlyweightPattern
{
    // Flyweight: Chứa trạng thái dùng chung (Intrinsic State)
    public class Flyweight
    {
        private readonly string _sharedState;

        public Flyweight(string sharedState)
        {
            _sharedState = sharedState;
        }

        // Truyền trạng thái riêng biệt (Extrinsic State) qua tham số
        public void Operation(string uniqueState)
        {
            Console.WriteLine($"Flyweight [Dùng chung: {_sharedState}] <---> [Riêng biệt: {uniqueState}]");
        }
    }

    // FlyweightFactory: Quản lý và tái sử dụng các đối tượng Flyweight
    public class FlyweightFactory
    {
        private readonly Dictionary<string, Flyweight> _flyweights = new Dictionary<string, Flyweight>();

        public FlyweightFactory(params string[] states)
        {
            foreach (var state in states)
            {
                _flyweights[state] = new Flyweight(state);
            }
        }

        public Flyweight GetFlyweight(string key)
        {
            if (!_flyweights.ContainsKey(key))
            {
                Console.WriteLine($"Factory: Không tìm thấy '{key}', đang khởi tạo mới...");
                _flyweights[key] = new Flyweight(key);
            }
            else
            {
                Console.WriteLine($"Factory: Tái sử dụng đối tượng Flyweight '{key}' có sẵn.");
            }
            return _flyweights[key];
        }

        public void ListFlyweights()
        {
            Console.WriteLine($"\n-> Tổng số Flyweight trong Cache: {_flyweights.Count}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 6: FLYWEIGHT PATTERN (LÝ THUYẾT) ===");

            FlyweightFactory factory = new FlyweightFactory("Loại_A", "Loại_B");

            // Tái sử dụng loại đã có
            Flyweight fly1 = factory.GetFlyweight("Loại_A");
            fly1.Operation("Dữ liệu riêng 001");

            // Tạo loại mới
            Flyweight fly2 = factory.GetFlyweight("Loại_C");
            fly2.Operation("Dữ liệu riêng 002");

            factory.ListFlyweights();

            Console.ReadLine();
        }
    }
}