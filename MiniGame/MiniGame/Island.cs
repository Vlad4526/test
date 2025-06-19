public class Island
{
    private Cell[,] cells;
    private Random random;

    public Island(int width, int height)
    {
        cells = new Cell[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                cells[x, y] = new Cell();
            }
        }
        random = new Random();
    }

    public void Initialize()
    {
        // Встановлюємо траву на всі клітинки
        for (int x = 0; x < cells.GetLength(0); x++)
        {
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                cells[x, y].GetGrass().Grow();
            }
        }

        // Розміщуємо кролів і лисиць
        for (int i = 0; i < 5; i++)
        {
            PlaceAnimal(new Rabbit(GetName("Кріл")));
        }
        for (int i = 0; i < 3; i++)
        {
            PlaceAnimal(new Fox(GetName("Лис")));
        }
    }

    private string GetName(string type)
    {
        Console.Write($"Введіть ім'я {type}: ");
        return Console.ReadLine();
    }

    private void PlaceAnimal(Animal animal)
    {
        int x, y;
        do
        {
            x = random.Next(cells.GetLength(0));
            y = random.Next(cells.GetLength(1));
        } while (cells[x, y].GetAnimal() != null);
        cells[x, y].SetAnimal(animal);
    }

    public void SimulateDay()
    {
        Console.WriteLine("День починається...");

        // Рост трави
        for (int x = 0; x < cells.GetLength(0); x++)
        {
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                if (!cells[x, y].GetGrass().IsFresh())
                {
                    cells[x, y].GetGrass().Grow();
                }
            }
        }

        // Кролі їдять траву
        for (int x = 0; x < cells.GetLength(0); x++)
        {
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                if (cells[x, y].GetAnimal() is Rabbit rabbit)
                {
                    if (rabbit.IsHungry())
                    {
                        if (x > 0 && cells[x - 1, y].GetGrass().IsFresh())
                        {
                            cells[x - 1, y].GetGrass().Withew();
                            rabbit.TryEatGrass(cells[x - 1, y]);
                        }
                        else if (y > 0 && cells[x, y - 1].GetGrass().IsFresh())
                        {
                            cells[x, y - 1].GetGrass().Withew();
                            rabbit.TryEatGrass(cells[x, y - 1]);
                        }
                        else if (x < cells.GetLength(0) - 1 && cells[x + 1, y].GetGrass().IsFresh())
                        {
                            cells[x + 1, y].GetGrass().Withew();
                            rabbit.TryEatGrass(cells[x + 1, y]);
                        }
                        else if (y < cells.GetLength(1) - 1 && cells[x, y + 1].GetGrass().IsFresh())
                        {
                            cells[x, y + 1].GetGrass().Withew();
                            rabbit.TryEatGrass(cells[x, y + 1]);
                        }
                        else
                        {
                            rabbit.Die();
                            Console.WriteLine($"{rabbit.GetName()} помер через голод.");
                        }
                    }
                }
            }
        }

        // Лисиці їдять кролів
        for (int x = 0; x < cells.GetLength(0); x++)
        {
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                if (cells[x, y].GetAnimal() is Fox fox)
                {
                    if (fox.IsHungry())
                    {
                        if (x > 0 && cells[x - 1, y].GetAnimal() is Rabbit rabbit)
                        {
                            cells[x - 1, y].SetAnimal(null);
                            fox.TryEatRabbit(cells[x - 1, y]);
                        }
                        else if (y > 0 && cells[x, y - 1].GetAnimal() is Rabbit)
                        {
                            cells[x, y - 1].SetAnimal(null);
                            fox.TryEatRabbit(cells[x, y - 1]);
                        }
                        else if (x < cells.GetLength(0) - 1 && cells[x + 1, y].GetAnimal() is Rabbit)
                        {
                            cells[x + 1, y].SetAnimal(null);
                            fox.TryEatRabbit(cells[x + 1, y]);
                        }
                        else if (y < cells.GetLength(1) - 1 && cells[x, y + 1].GetAnimal() is Rabbit)
                        {
                            cells[x, y + 1].SetAnimal(null);
                            fox.TryEatRabbit(cells[x, y + 1]);
                        }
                        else
                        {
                            fox.Die();
                            Console.WriteLine($"{fox.GetName()} помер через голод.");
                        }
                    }
                }
            }
        }

        // Розмноження кролів
        for (int x = 0; x < cells.GetLength(0); x++)
        {
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                if (cells[x, y].GetAnimal() is Rabbit rabbit && !rabbit.IsHungry())
                {
                    if (x > 0 && cells[x - 1, y].GetAnimal() is Rabbit)
                    {
                        rabbit.Reproduce(cells[x - 1, y]);
                    }
                    else if (y > 0 && cells[x, y - 1].GetAnimal() is Rabbit)
                    {
                        rabbit.Reproduce(cells[x, y - 1]);
                    }
                    else if (x < cells.GetLength(0) - 1 && cells[x + 1, y].GetAnimal() is Rabbit)
                    {
                        rabbit.Reproduce(cells[x + 1, y]);
                    }
                    else if (y < cells.GetLength(1) - 1 && cells[x, y + 1].GetAnimal() is Rabbit)
                    {
                        rabbit.Reproduce(cells[x, y + 1]);
                    }
                }
            }
        }

        // Розмноження лисиць
        for (int x = 0; x < cells.GetLength(0); x++)
        {
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                if (cells[x, y].GetAnimal() is Fox fox && !fox.IsHungry())
                {
                    if (x > 0 && cells[x - 1, y].GetAnimal() is Fox)
                    {
                        fox.Reproduce(cells[x - 1, y]);
                    }
                    else if (y > 0 && cells[x, y - 1].GetAnimal() is Fox)
                    {
                        fox.Reproduce(cells[x, y - 1]);
                    }
                    else if (x < cells.GetLength(0) - 1 && cells[x + 1, y].GetAnimal() is Fox)
                    {
                        fox.Reproduce(cells[x + 1, y]);
                    }
                    else if (y < cells.GetLength(1) - 1 && cells[x, y + 1].GetAnimal() is Fox)
                    {
                        fox.Reproduce(cells[x, y + 1]);
                    }
                }
            }
        }

        Console.WriteLine("День завершився.");
    }

    public void PrintIsland()
    {
        for (int y = 0; y < cells.GetLength(1); y++)
        {
            for (int x = 0; x < cells.GetLength(0); x++)
            {
                if (x >= 1 && x <= 10 && y >= 1 && y <= 10)
                {
                    if (cells[x, y].GetAnimal() != null)
                    {
                        Console.Write(cells[x, y].GetAnimal().GetName());
                    }
                    else if (cells[x, y].GetGrass().IsFresh())
                    {
                        Console.Write("G");
                    }
                    else
                    {
                        Console.Write("_");
                    }
                    Console.Write(" ");
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
    }
}