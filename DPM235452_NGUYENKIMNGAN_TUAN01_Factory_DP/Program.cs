using System;

public interface IProduct
{
    string Operation();
}

public class ConcreteProductA : IProduct
{
    public string Operation() => "Kết quả từ ConcreteProductA";
}

public class ConcreteProductB : IProduct
{
    public string Operation() => "Kết quả từ ConcreteProductB";
}

public abstract class Creator
{
    public abstract IProduct FactoryMethod();

    public string SomeOperation()
    {
        var product = FactoryMethod();
        return "Creator: " + product.Operation();
    }
}

public class ConcreteCreatorA : Creator
{
    public override IProduct FactoryMethod() => new ConcreteProductA();
}

public class ConcreteCreatorB : Creator
{
    public override IProduct FactoryMethod() => new ConcreteProductB();
}

class Program
{
    static void Main()
    {
        Creator creatorA = new ConcreteCreatorA();
        Console.WriteLine(creatorA.SomeOperation());

        Creator creatorB = new ConcreteCreatorB();
        Console.WriteLine(creatorB.SomeOperation());
    }
}