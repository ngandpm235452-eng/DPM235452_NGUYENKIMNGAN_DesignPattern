using System;

public interface IAbstractProductA { string UsefulFunctionA(); }
public interface IAbstractProductB { string UsefulFunctionB(); }

public class ConcreteProductA1 : IAbstractProductA { public string UsefulFunctionA() => "Sản phẩm A1"; }
public class ConcreteProductB1 : IAbstractProductB { public string UsefulFunctionB() => "Sản phẩm B1"; }

public interface IAbstractFactory
{
    IAbstractProductA CreateProductA();
    IAbstractProductB CreateProductB();
}

public class ConcreteFactory1 : IAbstractFactory
{
    public IAbstractProductA CreateProductA() => new ConcreteProductA1();
    public IAbstractProductB CreateProductB() => new ConcreteProductB1();
}

class Program
{
    static void Main()
    {
        IAbstractFactory factory = new ConcreteFactory1();
        Console.WriteLine(factory.CreateProductA().UsefulFunctionA());
        Console.WriteLine(factory.CreateProductB().UsefulFunctionB());
    }
}