public class Rabbit
{
    public bool Jivoi { get; private set; } = true;
    public int Golod { get; private set; } = 0;

    public void Food(Klitunka[,] klitunku, int x, int y)
    {
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue;
                int nx = x + dx;
                int ny = y + dy;
                if (nx >= 1 && nx < klitunku.GetLength(0) - 1 && ny >= 1 && ny < klitunku.GetLength(1) - 1)
                {
                    if (klitunku[nx, ny].Трава != null)
                    {
                        Console.WriteLine($"Зайець у ({x}, {y}) з'їв траву у ({nx}, {ny}).");
                        klitunku[nx, ny].Трава = null;
                        Golod = 0;
                        return;
                    }
                }
            }
        }

        Golod ++;
        if (Golod >= 1)
        {
            Jivoi = false;
            Console.WriteLine($"Зайець у ({x}, {y}) загинув від голоду.");
        }
    }

    public void Відтворення(Klitunka[,] klitunku, int x, int y)
    {
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue;
                int nx = x + dx;
                int ny = y + dy;
                if (nx >= 1 && nx < klitunku.GetLength(0) - 1 && ny >= 1 && ny < klitunku.GetLength(1) - 1)
                {
                    if (klitunku[nx, ny].Зайець != null && klitunku[nx, ny].Трава != null)
                    {
                        Console.WriteLine($"Зайець у ({x}, {y}) відновився у ({nx}, {ny}).");
                        klitunku[nx, ny].Зайець = new Rabbit();
                    }
                }
            }
        }
    }
}