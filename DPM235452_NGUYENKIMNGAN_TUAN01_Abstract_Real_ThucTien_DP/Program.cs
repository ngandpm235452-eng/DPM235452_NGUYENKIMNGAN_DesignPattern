using System;

public interface IButton { void Render(); }
public interface ICheckbox { void Render(); }

public class WinButton : IButton { public void Render() => Console.WriteLine("Vẽ nút bấm Windows."); }
public class WinCheckbox : ICheckbox { public void Render() => Console.WriteLine("Vẽ ô chọn Windows."); }

public class MacButton : IButton { public void Render() => Console.WriteLine("Vẽ nút bấm macOS."); }
public class MacCheckbox : ICheckbox { public void Render() => Console.WriteLine("Vẽ ô chọn macOS."); }

public interface GUIFactory
{
    IButton CreateButton();
    ICheckbox CreateCheckbox();
}

public class WinFactory : GUIFactory
{
    public IButton CreateButton() => new WinButton();
    public ICheckbox CreateCheckbox() => new WinCheckbox();
}

public class MacFactory : GUIFactory
{
    public IButton CreateButton() => new MacButton();
    public ICheckbox CreateCheckbox() => new MacCheckbox();
}

class Program
{
    static void Main()
    {
        Console.WriteLine("--- Giao diện Windows ---");
        GUIFactory winFactory = new WinFactory();
        winFactory.CreateButton().Render();
        winFactory.CreateCheckbox().Render();

        Console.WriteLine("\n--- Giao diện macOS ---");
        GUIFactory macFactory = new MacFactory();
        macFactory.CreateButton().Render();
        macFactory.CreateCheckbox().Render();
    }
}