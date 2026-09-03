using System;

public interface ITransport
{
    void Deliver();
}

public class Truck : ITransport
{
    public void Deliver() => Console.WriteLine("Vận chuyển hàng hóa bằng Xe tải qua đường bộ.");
}

public class Ship : ITransport
{
    public void Deliver() => Console.WriteLine("Vận chuyển hàng hóa bằng Tàu thủy qua đường biển.");
}

public abstract class Logistics
{
    public abstract ITransport CreateTransport();

    public void PlanDelivery()
    {
        ITransport transport = CreateTransport();
        transport.Deliver();
    }
}

public class RoadLogistics : Logistics
{
    public override ITransport CreateTransport() => new Truck();
}

public class SeaLogistics : Logistics
{
    public override ITransport CreateTransport() => new Ship();
}

class Program
{
    static void Main()
    {
        Console.WriteLine("--- DỊCH VỤ LOGISTICS ĐƯỜNG BỘ ---");
        Logistics road = new RoadLogistics();
        road.PlanDelivery();

        Console.WriteLine("\n--- DỊCH VỤ LOGISTICS ĐƯỜNG BIỂN ---");
        Logistics sea = new SeaLogistics();
        sea.PlanDelivery();
    }
}