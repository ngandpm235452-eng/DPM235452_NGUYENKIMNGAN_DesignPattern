using System;
using System.Collections.Generic;

public class Product
{
    private List<string> _parts = new List<string>();
    public void Add(string part) => _parts.Add(part);
    public void Show() => Console.WriteLine("Product Parts: " + string.Join(", ", _parts));
}

public interface IBuilder
{
    void BuildPartA();
    void BuildPartB();
    Product GetResult();
}

public class ConcreteBuilder : IBuilder
{
    private Product _product = new Product();
    public void BuildPartA() => _product.Add("PartA");
    public void BuildPartB() => _product.Add("PartB");
    public Product GetResult() => _product;
}

public class Director
{
    public void Construct(IBuilder builder)
    {
        builder.BuildPartA();
        builder.BuildPartB();
    }
}

class Program
{
    static void Main()
    {
        Director director = new Director();
        IBuilder builder = new ConcreteBuilder();

        director.Construct(builder);
        Product p = builder.GetResult();

        p.Show();
    }
}