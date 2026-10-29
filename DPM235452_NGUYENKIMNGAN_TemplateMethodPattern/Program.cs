using System;

namespace DPM235452_NGUYENKIMNGAN_TemplateMethodPattern
{
    // Abstract Class định nghĩa Template Method
    public abstract class AbstractClass
    {
        // Template method định nghĩa khung thuật toán (thứ tự thực hiện các bước)
        public void TemplateMethod()
        {
            BaseOperation1();
            RequiredOperations1();
            BaseOperation2();
            RequiredOperation2();
            Hook1();
        }

        protected void BaseOperation1()
        {
            Console.WriteLine("AbstractClass: Thực hiện bước cơ sở 1");
        }

        protected void BaseOperation2()
        {
            Console.WriteLine("AbstractClass: Thực hiện bước cơ sở 2");
        }

        // Các bước bắt buộc lớp con phải triển khai cụ thể
        protected abstract void RequiredOperations1();
        protected abstract void RequiredOperation2();

        // Hook: Phương thức tùy chọn (lớp con có thể ghi đè hoặc không)
        protected virtual void Hook1() { }
    }

    // Concrete Class 1
    public class ConcreteClass1 : AbstractClass
    {
        protected override void RequiredOperations1()
        {
            Console.WriteLine("ConcreteClass1: Triển khai Bước bắt buộc 1");
        }

        protected override void RequiredOperation2()
        {
            Console.WriteLine("ConcreteClass1: Triển khai Bước bắt buộc 2");
        }
    }

    // Concrete Class 2 (Có ghi đè Hook)
    public class ConcreteClass2 : AbstractClass
    {
        protected override void RequiredOperations1()
        {
            Console.WriteLine("ConcreteClass2: Triển khai Bước bắt buộc 1");
        }

        protected override void RequiredOperation2()
        {
            Console.WriteLine("ConcreteClass2: Triển khai Bước bắt buộc 2");
        }

        protected override void Hook1()
        {
            Console.WriteLine("ConcreteClass2: Thực hiện Hook1 tùy chỉnh");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 9: TEMPLATE METHOD PATTERN (LÝ THUYẾT) ===");

            Console.WriteLine("--- Thực thi với ConcreteClass1 ---");
            AbstractClass class1 = new ConcreteClass1();
            class1.TemplateMethod();

            Console.WriteLine("\n--- Thực thi với ConcreteClass2 ---");
            AbstractClass class2 = new ConcreteClass2();
            class2.TemplateMethod();

            Console.ReadLine();
        }
    }
}