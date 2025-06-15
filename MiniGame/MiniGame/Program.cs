class Program
{
    static void Main(string[] args)
    {
        Map карта = new Map(12, 12);
        карта.Ініціалізація();

        for (int день = 1; день <= 5; день++)
        {
            Console.WriteLine($"День {день}");
            карта.СимулюватиДень();
            карта.ВивестиКарту();
            Console.WriteLine();

            if (карта.КінецьГри())
            {
                break;
            }

            Console.WriteLine("Натисніть будь-яку клавішу для продовження до наступного дня...");
            Console.ReadKey();
        }

        Console.WriteLine("Кінець гри");
    }
}