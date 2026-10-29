using System;
using System.Collections.Generic;

namespace DPM235452_NGUYENKIMNGAN_ObserverPattern
{
    // Observer Interface
    public interface IObserver
    {
        void Update(ISubject subject);
    }

    // Subject Interface
    public interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }

    // Concrete Subject
    public class Subject : ISubject
    {
        public int State { get; set; } = -1;

        private readonly List<IObserver> _observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            Console.WriteLine("Subject: Đã đăng ký một Observer mới.");
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
            Console.WriteLine("Subject: Đã hủy đăng ký một Observer.");
        }

        public void Notify()
        {
            Console.WriteLine("Subject: Đang phát thông báo tới tất cả Observers...");
            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }

        public void SomeBusinessLogic()
        {
            Console.WriteLine("\nSubject: Đang thực hiện thay đổi trạng thái nội bộ...");
            State = new Random().Next(0, 10);

            Console.WriteLine($"Subject: Trạng thái mới đổi thành: {State}");
            Notify();
        }
    }

    // Concrete Observer A
    public class ConcreteObserverA : IObserver
    {
        public void Update(ISubject subject)
        {
            if ((subject as Subject)?.State < 5)
            {
                Console.WriteLine("ConcreteObserverA: Phản ứng với sự kiện (State < 5).");
            }
        }
    }

    // Concrete Observer B
    public class ConcreteObserverB : IObserver
    {
        public void Update(ISubject subject)
        {
            if ((subject as Subject)?.State >= 5 || (subject as Subject)?.State == 0)
            {
                Console.WriteLine("ConcreteObserverB: Phản ứng với sự kiện (State >= 5).");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 6: OBSERVER PATTERN (LÝ THUYẾT) ===");

            var subject = new Subject();
            var observerA = new ConcreteObserverA();
            subject.Attach(observerA);

            var observerB = new ConcreteObserverB();
            subject.Attach(observerB);

            subject.SomeBusinessLogic();
            subject.SomeBusinessLogic();

            subject.Detach(observerA);

            subject.SomeBusinessLogic();

            Console.ReadLine();
        }
    }
}