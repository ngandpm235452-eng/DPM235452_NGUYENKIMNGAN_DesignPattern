using System;

namespace DPM235452_NGUYENKIMNGAN_MediatorPattern
{
    // Mediator Interface
    public interface IMediator
    {
        void Notify(object sender, string ev);
    }

    // Base Component
    public class BaseComponent
    {
        protected IMediator _mediator;

        public BaseComponent(IMediator mediator = null)
        {
            _mediator = mediator;
        }

        public void SetMediator(IMediator mediator)
        {
            _mediator = mediator;
        }
    }

    // Concrete Component 1
    public class Component1 : BaseComponent
    {
        public void DoA()
        {
            Console.WriteLine("Component 1: Thực hiện hành động A.");
            _mediator?.Notify(this, "A");
        }

        public void DoB()
        {
            Console.WriteLine("Component 1: Thực hiện hành động B.");
            _mediator?.Notify(this, "B");
        }
    }

    // Concrete Component 2
    public class Component2 : BaseComponent
    {
        public void DoC()
        {
            Console.WriteLine("Component 2: Thực hiện hành động C.");
            _mediator?.Notify(this, "C");
        }

        public void DoD()
        {
            Console.WriteLine("Component 2: Thực hiện hành động D.");
            _mediator?.Notify(this, "D");
        }
    }

    // Concrete Mediator: Điều phối tương tác giữa Component 1 và Component 2
    public class ConcreteMediator : IMediator
    {
        private readonly Component1 _c1;
        private readonly Component2 _c2;

        public ConcreteMediator(Component1 c1, Component2 c2)
        {
            _c1 = c1;
            _c1.SetMediator(this);
            _c2 = c2;
            _c2.SetMediator(this);
        }

        public void Notify(object sender, string ev)
        {
            if (ev == "A")
            {
                Console.WriteLine("Mediator phản ứng khi nhận sự kiện A -> Kích hoạt Component 2 làm C:");
                _c2.DoC();
            }
            else if (ev == "D")
            {
                Console.WriteLine("Mediator phản ứng khi nhận sự kiện D -> Kích hoạt Component 1 làm B:");
                _c1.DoB();
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 4: MEDIATOR PATTERN (LÝ THUYẾT) ===");

            Component1 c1 = new Component1();
            Component2 c2 = new Component2();
            _ = new ConcreteMediator(c1, c2);

            Console.WriteLine("--- Client kích hoạt DoA() ---");
            c1.DoA();

            Console.WriteLine("\n--- Client kích hoạt DoD() ---");
            c2.DoD();

            Console.ReadLine();
        }
    }
}