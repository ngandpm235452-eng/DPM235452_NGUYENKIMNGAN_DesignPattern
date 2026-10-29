using System;
using System.Collections.Generic;

namespace DPM235452_NGUYENKIMNGAN_CommandPattern
{
    // Command Interface
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    // Receiver: Chứa logic nghiệp vụ thực sự
    public class Receiver
    {
        public void ActionA(string param) => Console.WriteLine($"Receiver: Đang thực hiện tác vụ A ({param}).");
        public void ActionB(string param) => Console.WriteLine($"Receiver: Đang thực hiện tác vụ B ({param}).");
    }

    // Concrete Command 1
    public class SimpleCommand : ICommand
    {
        private readonly string _payload;

        public SimpleCommand(string payload)
        {
            _payload = payload;
        }

        public void Execute()
        {
            Console.WriteLine($"SimpleCommand: Xử lý trực tiếp dữ liệu '{_payload}'.");
        }

        public void Undo()
        {
            Console.WriteLine($"SimpleCommand: Hoàn tác dữ liệu '{_payload}'.");
        }
    }

    // Concrete Command 2: Ủy nhiệm xử lý cho Receiver
    public class ComplexCommand : ICommand
    {
        private readonly Receiver _receiver;
        private readonly string _a;
        private readonly string _b;

        public ComplexCommand(Receiver receiver, string a, string b)
        {
            _receiver = receiver;
            _a = a;
            _b = b;
        }

        public void Execute()
        {
            Console.WriteLine("ComplexCommand: Gọi Receiver thực hiện các hành động...");
            _receiver.ActionA(_a);
            _receiver.ActionB(_b);
        }

        public void Undo()
        {
            Console.WriteLine("ComplexCommand: Hoàn tác các hành động của Receiver.");
        }
    }

    // Invoker: Quản lý và thực thi các lệnh
    public class Invoker
    {
        private ICommand _onStart;
        private ICommand _onFinish;

        public void SetOnStart(ICommand command) => _onStart = command;
        public void SetOnFinish(ICommand command) => _onFinish = command;

        public void DoSomething()
        {
            Console.WriteLine("Invoker: Bắt đầu thực thi lệnh khởi động...");
            _onStart?.Execute();

            Console.WriteLine("Invoker: Thực thi lệnh kết thúc...");
            _onFinish?.Execute();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 2: COMMAND PATTERN (LÝ THUYẾT) ===");

            Invoker invoker = new Invoker();
            Receiver receiver = new Receiver();

            invoker.SetOnStart(new SimpleCommand("Chào bạn!"));
            invoker.SetOnFinish(new ComplexCommand(receiver, "Gửi Email", "Lưu Log"));

            invoker.DoSomething();

            Console.ReadLine();
        }
    }
}