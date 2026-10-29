using System;
using System.Collections.Generic;

namespace DPM235452_NGUYENKIMNGAN_CompositePattern
{
    // Component: Định nghĩa giao diện chung cho đối tượng đơn lẻ và đối tượng phức hợp
    public abstract class Component
    {
        protected string name;
        public Component(string name) => this.name = name;
        public abstract void Display(int depth);
    }

    // Leaf: Đối tượng lá (không chứa con)
    public class Leaf : Component
    {
        public Leaf(string name) : base(name) { }
        public override void Display(int depth) => Console.WriteLine(new string('-', depth) + name);
    }

    // Composite: Đối tượng phức hợp (chứa các con là Leaf hoặc Composite khác)
    public class Composite : Component
    {
        private readonly List<Component> _children = new List<Component>();
        public Composite(string name) : base(name) { }

        public void Add(Component component) => _children.Add(component);
        public void Remove(Component component) => _children.Remove(component);

        public override void Display(int depth)
        {
            Console.WriteLine(new string('-', depth) + name);
            foreach (Component component in _children)
            {
                component.Display(depth + 2);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 3: COMPOSITE PATTERN (LÝ THUYẾT) ===");

            Composite root = new Composite("Root");
            root.Add(new Leaf("Leaf A"));
            root.Add(new Leaf("Leaf B"));

            Composite comp = new Composite("Composite X");
            comp.Add(new Leaf("Leaf XA"));
            comp.Add(new Leaf("Leaf XB"));
            root.Add(comp);

            root.Display(1);

            Console.ReadLine();
        }
    }
}