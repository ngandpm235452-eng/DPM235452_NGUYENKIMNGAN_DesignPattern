using System;

public abstract class Prototype
{
    public string Id { get; private set; }
    public Prototype(string id) => Id = id;
    public abstract Prototype Clone();
}

public class ConcretePrototype : Prototype
{
    public ConcretePrototype(string id) : base(id) { }
    public override Prototype Clone() => (Prototype)this.MemberwiseClone();
}

class Program
{
    static void Main()
    {
        ConcretePrototype p1 = new ConcretePrototype("P-101");
        ConcretePrototype c1 = (ConcretePrototype)p1.Clone();

        Console.WriteLine($"Đối tượng gốc ID: {p1.Id}");
        Console.WriteLine($"Đối tượng sao chép ID: {c1.Id}");
    }
}