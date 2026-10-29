using System;

namespace DPM235452_NGUYENKIMNGAN_InterpreterPattern
{
    // Abstract Expression
    public interface IExpression
    {
        bool Interpret(string context);
    }

    // Terminal Expression: Kiểm tra từ khóa cụ thể trong chuỗi Context
    public class TerminalExpression : IExpression
    {
        private readonly string _data;

        public TerminalExpression(string data)
        {
            _data = data;
        }

        public bool Interpret(string context)
        {
            return context.Contains(_data);
        }
    }

    // Nonterminal Expression: Toán tử OR
    public class OrExpression : IExpression
    {
        private readonly IExpression _expr1;
        private readonly IExpression _expr2;

        public OrExpression(IExpression expr1, IExpression expr2)
        {
            _expr1 = expr1;
            _expr2 = expr2;
        }

        public bool Interpret(string context)
        {
            return _expr1.Interpret(context) || _expr2.Interpret(context);
        }
    }

    // Nonterminal Expression: Toán tử AND
    public class AndExpression : IExpression
    {
        private readonly IExpression _expr1;
        private readonly IExpression _expr2;

        public AndExpression(IExpression expr1, IExpression expr2)
        {
            _expr1 = expr1;
            _expr2 = expr2;
        }

        public bool Interpret(string context)
        {
            return _expr1.Interpret(context) && _expr2.Interpret(context);
        }
    }

    internal class Program
    {
        // Tạo biểu thức quy tắc 1: C# OR .NET
        public static IExpression GetLapTrinhRule()
        {
            IExpression csharp = new TerminalExpression("C#");
            IExpression dotnet = new TerminalExpression(".NET");
            return new OrExpression(csharp, dotnet);
        }

        // Tạo biểu thức quy tắc 2: Design Pattern AND C#
        public static IExpression GetPatternRule()
        {
            IExpression pattern = new TerminalExpression("Design Pattern");
            IExpression csharp = new TerminalExpression("C#");
            return new AndExpression(pattern, csharp);
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 11: INTERPRETER PATTERN (LÝ THUYẾT) ===");

            IExpression isLapTrinh = GetLapTrinhRule();
            IExpression isPatternCSharp = GetPatternRule();

            Console.WriteLine("1. Kiểm tra 'C# 12 Web API': " + isLapTrinh.Interpret("C# 12 Web API"));
            Console.WriteLine("2. Kiểm tra 'Java Spring Boot': " + isLapTrinh.Interpret("Java Spring Boot"));

            Console.WriteLine("3. Kiểm tra 'Học Design Pattern với C#': " + isPatternCSharp.Interpret("Học Design Pattern với C#"));
            Console.WriteLine("4. Kiểm tra 'Học Design Pattern với Python': " + isPatternCSharp.Interpret("Học Design Pattern với Python"));

            Console.ReadLine();
        }
    }
}