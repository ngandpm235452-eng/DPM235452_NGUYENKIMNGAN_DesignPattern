using System;
using System.Collections.Generic;

namespace DPM235452_NGUYENKIMNGAN_MementoPattern
{
    // Memento: Lưu giữ trạng thái nội bộ của Originator
    public class Memento
    {
        public string State { get; }

        public Memento(string state)
        {
            State = state;
        }
    }

    // Originator: Tạo Memento chứa ảnh chụp trạng thái hiện tại và khôi phục từ Memento
    public class Originator
    {
        public string State { get; set; }

        public Memento SaveStateToMemento()
        {
            Console.WriteLine($"Originator: Đã lưu trạng thái -> '{State}'");
            return new Memento(State);
        }

        public void RestoreStateFromMemento(Memento memento)
        {
            State = memento.State;
            Console.WriteLine($"Originator: Đã khôi phục trạng thái -> '{State}'");
        }
    }

    // Caretaker: Quản lý và lưu trữ danh sách các Memento
    public class Caretaker
    {
        private readonly List<Memento> _mementoList = new List<Memento>();

        public void Add(Memento state)
        {
            _mementoList.Add(state);
        }

        public Memento Get(int index)
        {
            return _mementoList[index];
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 5: MEMENTO PATTERN (LÝ THUYẾT) ===");

            Originator originator = new Originator();
            Caretaker caretaker = new Caretaker();

            originator.State = "Trạng thái #1";
            originator.State = "Trạng thái #2";
            caretaker.Add(originator.SaveStateToMemento());

            originator.State = "Trạng thái #3";
            caretaker.Add(originator.SaveStateToMemento());

            originator.State = "Trạng thái #4";
            Console.WriteLine($"Trạng thái hiện tại: {originator.State}");

            Console.WriteLine("\n--- Tiến hành Restore ---");
            originator.RestoreStateFromMemento(caretaker.Get(0));
            originator.RestoreStateFromMemento(caretaker.Get(1));

            Console.ReadLine();
        }
    }
}