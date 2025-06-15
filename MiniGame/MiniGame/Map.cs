using System.Text.Json;
public class Map
{
    private int shirinaMap;
    private int visotaMap;
    private Klitunka[,] klitunku;

    public Map(int shirinaMap, int visotaMap)
    {
        this.shirinaMap = shirinaMap;
        this.visotaMap = visotaMap;
        klitunku = new Klitunka[shirinaMap, visotaMap];
    }

    public void Ініціалізація()
    {
        for (int x = 0; x < shirinaMap; x++)
        {
            for (int y = 0; y < visotaMap; y++)
            {
                klitunku[x, y] = new Klitunka();
            }
        }

        // Ініціалізація трави, зайців і лисиць
        for (int i = 0; i < 20; i++)
        {
            int x = ВипадковеЧисло(1, 10);
            int y = ВипадковеЧисло(1, 10);
            klitunku[x, y].Трава = new Grass();
        }

        for (int i = 0; i < 10; i++)
        {
            int x = ВипадковеЧисло(1, 10);
            int y = ВипадковеЧисло(1, 10);
            klitunku[x, y].Зайець = new Rabbit();
        }

        for (int i = 0; i < 5; i++)
        {
            int x = ВипадковеЧисло(1, 10);
            int y = ВипадковеЧисло(1, 10);
            klitunku[x, y].Лисиця = new Fox();
        }
    }

    public void СимулюватиДень()
    {
        // Рост трави
        for (int x = 1; x < shirinaMap - 1; x++)
        {
            for (int y = 1; y < visotaMap - 1; y++)
            {
                if (klitunku[x, y].Трава != null)
                {
                    klitunku[x, y].Трава.Rost();
                }
                else if (ВипадковеЧисло(1, 10) == 1) // 10% шанс на рост нової трави
                {
                    klitunku[x, y].Трава = new Grass();
                }
            }
        }

        // Симуляція зайців
        for (int x = 1; x < shirinaMap - 1; x++)
        {
            for (int y = 1; y < visotaMap - 1; y++)
            {
                if (klitunku[x, y].Зайець != null)
                {
                    klitunku[x, y].Зайець.Food(klitunku, x, y);
                    klitunku[x, y].Зайець.Відтворення(klitunku, x, y);
                }
            }
        }

        // Симуляція волків
        for (int x = 1; x < shirinaMap - 1; x++)
        {
            for (int y = 1; y < visotaMap - 1; y++)
            {
                if (klitunku[x, y].Лисиця != null)
                {
                    klitunku[x, y].Лисиця.Охота(klitunku, x, y);
                    klitunku[x, y].Лисиця.Відтворення(klitunku, x, y);
                }
            }
        }

        // Видалення мертвих тварин та старої трави
        for (int x = 1; x < shirinaMap - 1; x++)
        {
            for (int y = 1; y < visotaMap - 1; y++)
            {
                if (klitunku[x, y].Зайець != null && !klitunku[x, y].Зайець.Jivoi)
                {
                    Console.WriteLine($"Зайець у ({x}, {y}) загинув.");
                    klitunku[x, y].Зайець = null;
                }

                if (klitunku[x, y].Лисиця != null && !klitunku[x, y].Лисиця.Jivoi)
                {
                    Console.WriteLine($"Лисиця у ({x}, {y}) загинув.");
                    klitunku[x, y].Лисиця = null;
                }

                if (klitunku[x, y].Трава != null && klitunku[x, y].Трава.Вік > 2)
                {
                    Console.WriteLine($"Трава у ({x}, {y}) з віком {klitunku[x, y].Трава.Вік} була видалена.");
                    klitunku[x, y].Трава = null;
                }
            }
        }
    }

    public void ВивестиКарту()
    {
        Console.WriteLine(new string('-', shirinaMap * 2));
        for (int y = 1; y < visotaMap - 1; y++)
        {
            Console.Write('|');
            for (int x = 1; x < shirinaMap - 1; x++)
            {
                if (klitunku[x, y].Лисиця != null)
                {
                    Console.Write('Л');
                }
                else if (klitunku[x, y].Зайець != null)
                {
                    Console.Write('З');
                }
                else if (klitunku[x, y].Трава != null)
                {
                    Console.Write('Т');
                }
                else
                {
                    Console.Write('_');
                }
                Console.Write(' ');
            }
            Console.WriteLine("|");
        }
        Console.WriteLine(new string('-', shirinaMap * 2));
    }

    public bool КінецьГри()
    {
        bool єЗайці = false;
        bool єЛисиця = false;

        for (int x = 1; x < shirinaMap - 1; x++)
        {
            for (int y = 1; y < visotaMap - 1; y++)
            {
                if (klitunku[x, y].Зайець != null)
                {
                    єЗайці = true;
                }

                if (klitunku[x, y].Лисиця != null)
                {
                    єЛисиця = true;
                }
            }
        }

        return !(єЗайці && єЛисиця);
    }

    public void SaveGameResult(string winner, int days)
    {
        var result = new GameResult
        {
            Переможці = winner,
            ДнівПрожито = days
        };

        string json = JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("game_result.json", json);
    }

    public int GetDaysSurvived()
    {
        int days = 0;
        for (int x = 1; x < shirinaMap - 1; x++)
        {
            for (int y = 1; y < visotaMap - 1; y++)
            {
                if (klitunku[x, y].Зайець != null || klitunku[x, y].Лисиця != null)
                {
                    days++;
                }
            }
        }
        return days;
    }

    public string GetWinner()
    {
        bool єЗайці = false;
        bool єЛисиця = false;

        for (int x = 1; x < shirinaMap - 1; x++)
        {
            for (int y = 1; y < visotaMap - 1; y++)
            {
                if (klitunku[x, y].Зайець != null)
                {
                    єЗайці = true;
                }

                if (klitunku[x, y].Лисиця != null)
                {
                    єЛисиця = true;
                }
            }
        }

        if (єЗайці && !єЛисиця)
        {
            return "Зайці";
        }
        else if (!єЗайці && єЛисиця)
        {
            return "Лисиці";
        }
        else
        {
            return "Ніхто";
        }
    }

    private int ВипадковеЧисло(int мін, int макс)
    {
        Random випадкова = new Random();
        return випадкова.Next(мін, макс + 1);
    }
}