using System.Text.Json;
class Program
{
    static void Main(string[] args)
    {
        // Load last game result if exists
        if (File.Exists("game_result.json"))
        {
            var json = File.ReadAllText("game_result.json");
            var result = JsonSerializer.Deserialize<GameResult>(json);

            Console.WriteLine($"--- Прошлі Показники Гри ---");
            Console.WriteLine($"Winner: {result.Переможці}");
            Console.WriteLine($"Days Survived: {result.ДнівПрожито}");
            Console.WriteLine("-----------------------------\n");
        }

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

        string winner = карта.GetWinner();
        int days = карта.GetDaysSurvived();

        карта.SaveGameResult(winner, days);

        Console.WriteLine("Кінець гри");
        Console.WriteLine("Бажаєте почати знову? (y/n)");

        while (true)
        {
            var key = Console.ReadKey(true).KeyChar;
            if (key == 'y' || key == 'Y')
            {
                Console.Clear();
                Main(args); // Restart the game
                break;
            }
            else if (key == 'n' || key == 'N')
            {
                Console.WriteLine("\nДо побачення!");
                break;
            }
            else
            {
                Console.WriteLine("\nБудь ласка, введіть 'y' або 'n'.");
            }
        }
    }
}