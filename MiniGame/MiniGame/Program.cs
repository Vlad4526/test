class Program
{
    static void Main(string[] args)
    {
        Island island = new Island(12, 12);
        island.Initialize();

        for (int day = 1; day <= 5; day++)
        {
            Console.WriteLine($"\nДень {day}:");
            island.PrintIsland();
            island.SimulateDay();
            Console.WriteLine("Натисніть будь-яку клавішу для продовження...");
            Console.ReadKey();
        }

        Console.WriteLine("\nГра завершилась.");
    }
}