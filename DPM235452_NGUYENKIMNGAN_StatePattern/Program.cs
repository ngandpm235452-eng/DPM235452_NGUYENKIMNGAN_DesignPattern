using System;

namespace DPM235452_NGUYENKIMNGAN_StatePattern
{
    // Context: Quản lý đối tượng có trạng thái biến đổi
    public class Context
    {
        private State _state = null;

        public Context(State state)
        {
            TransitionTo(state);
        }

        public void TransitionTo(State state)
        {
            Console.WriteLine($"Context: Chuyển sang trạng thái {state.GetType().Name}.");
            _state = state;
            _state.SetContext(this);
        }

        public void Request1()
        {
            _state.Handle1();
        }

        public void Request2()
        {
            _state.Handle2();
        }
    }

    // Abstract State
    public abstract class State
    {
        protected Context _context;

        public void SetContext(Context context)
        {
            _context = context;
        }

        public abstract void Handle1();
        public abstract void Handle2();
    }

    // Concrete State A
    public class ConcreteStateA : State
    {
        public override void Handle1()
        {
            Console.WriteLine("ConcreteStateA xử lý Request1.");
            Console.WriteLine("ConcreteStateA muốn thay đổi trạng thái sang StateB.");
            _context.TransitionTo(new ConcreteStateB());
        }

        public override void Handle2()
        {
            Console.WriteLine("ConcreteStateA xử lý Request2.");
        }
    }

    // Concrete State B
    public class ConcreteStateB : State
    {
        public override void Handle1()
        {
            Console.WriteLine("ConcreteStateB xử lý Request1.");
        }

        public override void Handle2()
        {
            Console.WriteLine("ConcreteStateB xử lý Request2.");
            Console.WriteLine("ConcreteStateB muốn thay đổi trạng thái sang StateA.");
            _context.TransitionTo(new ConcreteStateA());
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 7: STATE PATTERN (LÝ THUYẾT) ===");

            var context = new Context(new ConcreteStateA());
            context.Request1();
            context.Request2();

            Console.ReadLine();
        }
    }
}