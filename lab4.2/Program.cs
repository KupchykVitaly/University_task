using System;

public static class Program
{
    public static double Fun1(double x) => 1 / Math.Pow(x, 1 / 3);
    public static double Fun2(double x) => Math.Sin(x) / Math.Sqrt(x * x);
    public static double Fun3(double x) => x * Math.Cos(x);

    public delegate double FunctionDelegate(double x);

    public static double Trapecia(FunctionDelegate function, double a, double b, int numberOfSegments)
    {
        if (numberOfSegments <= 0)
        {
            Console.WriteLine("Некоректне значення для кількості сегментів.");
            return 0;
        }

        if (a >= b)
        {
            Console.WriteLine("Некоректний діапазон [a, b]. a повинно бути менше за b.");
            return 0;
        }

        double dx = (b - a) / numberOfSegments;
        double sum = 0;

        for (int i = 0; i < numberOfSegments; i++)
        {
            double x = a + i * dx;
            sum += function(x) * dx;
        }

        return sum;
    }

    internal static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        FunctionDelegate function1 = Fun1;
        FunctionDelegate function2 = Fun2;
        FunctionDelegate function3 = Fun3;

        Console.Write("Введіть кількість сегментів: ");
        int numberOfSegments = int.Parse(Console.ReadLine());

        Console.Write("Введіть a1: ");
        double a1 = double.Parse(Console.ReadLine());
        Console.Write("Введіть b1: ");
        double b1 = double.Parse(Console.ReadLine());

        Console.Write("Введіть a2: ");
        double a2 = double.Parse(Console.ReadLine());
        Console.Write("Введіть b2: ");
        double b2 = double.Parse(Console.ReadLine());

        Console.Write("Введіть a3: ");
        double a3 = double.Parse(Console.ReadLine());
        Console.Write("Введіть b3: ");
        double b3 = double.Parse(Console.ReadLine());

        Console.WriteLine($"a1={a1:f}\tb1={b1:f}\ttrap={Trapecia(function1, a1, b1, numberOfSegments):f}");
        Console.WriteLine($"a2={a2:f}\tb2={b2:f}\ttrap={Trapecia(function2, a2, b2, numberOfSegments):f}");
        Console.WriteLine($"a3={a3:f}\tb3={b3:f}\ttrap={Trapecia(function3, a3, b3, numberOfSegments):f}");

        // Створення об'єкта події та підписка на неї
        NameEvent nameEvent = new NameEvent();
        nameEvent.NameKeyPressed += (name) =>
        {
            Console.WriteLine("\n\nПідпис: " + name);
        };

        // Запуск слухача події
        nameEvent.StartListening();

        Console.ReadKey(true);
    }
}

public class NameEvent
{
    public event Action<string> NameKeyPressed;

    public void StartListening()
    {

        Console.Write("\nХочете поставити підпис, натисніть 'V' \nЯкщо бажаєте вийти, натисніть 'Q': ");

        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (keyInfo.KeyChar == 'V' || keyInfo.KeyChar == 'v')
            {
                NameKeyPressed?.Invoke("Vitaliy Kupchik");
            }

            if (keyInfo.KeyChar == 'Q' || keyInfo.KeyChar == 'q')
            {
                break;
            }
        }
    }
}
