using System;

public class Computer
{
    public string CPU { get; set; }
    public string RAM { get; set; }
    public string Storage { get; set; }
    public string GPU { get; set; }

    public void DisplayInfo() =>
        Console.WriteLine($"[Cấu hình PC] CPU: {CPU}, RAM: {RAM}, Storage: {Storage}, GPU: {GPU}");
}

public class ComputerBuilder
{
    private Computer _computer = new Computer();

    public ComputerBuilder SetCPU(string cpu) { _computer.CPU = cpu; return this; }
    public ComputerBuilder SetRAM(string ram) { _computer.RAM = ram; return this; }
    public ComputerBuilder SetStorage(string storage) { _computer.Storage = storage; return this; }
    public ComputerBuilder SetGPU(string gpu) { _computer.GPU = gpu; return this; }

    public Computer Build() => _computer;
}

class Program
{
    static void Main()
    {
        Computer gamingPC = new ComputerBuilder()
            .SetCPU("Intel Core i9 13900K")
            .SetRAM("32GB DDR5")
            .SetStorage("2TB NVMe SSD")
            .SetGPU("NVIDIA RTX 4090")
            .Build();

        gamingPC.DisplayInfo();
    }
}