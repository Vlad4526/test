public class Fox
{
    public bool Jivoi { get; private set; } = true;
    public int Golod { get; private set; } = 0;

    public void Охота(Klitunka[,] klitunku, int x, int y)
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
                    if (klitunku[nx, ny].Зайець != null)
                    {
                        Console.WriteLine($"Лисиця у ({x}, {y}) зловив зайця у ({nx}, {ny}).");
                        klitunku[nx, ny].Зайець = null;
                        Golod = 0;
                        return;
                    }
                }
            }
        }

       Golod++;
        if (Golod >= 2)
        {
            Jivoi = false;
            Console.WriteLine($"Лисиця у ({x}, {y}) загинув від голоду.");
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
                    if (klitunku[nx, ny].Лисиця != null && Golod == 0)
                    {
                        Console.WriteLine($"Лисиця у ({x}, {y}) відновився у ({nx}, {ny}).");
                        klitunku[nx, ny].Лисиця = new Fox();
                    }
                }
            }
        }
    }
}