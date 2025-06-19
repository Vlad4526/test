public abstract class Animal
{
    private string name;
    private bool isHungry;
    private bool isAlive;

    protected Animal(string name)
    {
        this.name = name;
        isHungry = true;
        isAlive = true;
    }

    public void Eat()
    {
        isHungry = false;
    }

    public void Die()
    {
        isAlive = false;
    }

    public string GetName() => name;
    public bool IsHungry() => isHungry;
    public bool IsAlive() => isAlive;
}

public class Rabbit : Animal
{
    private int daysWithoutFood;

    public Rabbit(string name) : base(name)
    {
        daysWithoutFood = 0;
    }

    public void TryEatGrass(Cell cell)
    {
        if (cell.GetGrass().IsFresh())
        {
            Eat();
            cell.SetGrass(new Grass()); // Видаляємо траву з клітинки
            Console.WriteLine($"{GetName()} їсть траву.");
        }
        else
        {
            daysWithoutFood++;
            if (daysWithoutFood >= 1)
            {
                Die();
                Console.WriteLine($"{GetName()} помер через голод.");
            }
        }
    }

    public void Reproduce(Cell cell)
    {
        if (cell.GetAnimal() is Rabbit && !IsHungry())
        {
            Console.WriteLine($"{GetName()} і {cell.GetAnimal().GetName()} розмножилися.");
            cell.SetAnimal(new Rabbit("Розмножений кріл"));
        }
    }
}

public class Fox : Animal
{
    private int daysWithoutFood;

    public Fox(string name) : base(name)
    {
        daysWithoutFood = 0;
    }

    public void TryEatRabbit(Cell cell)
    {
        if (cell.GetAnimal() is Rabbit rabbit)
        {
            Eat();
            cell.SetAnimal(null);
            Console.WriteLine($"{GetName()} їсть кроля.");
        }
        else
        {
            daysWithoutFood++;
            if (daysWithoutFood >= 2)
            {
                Die();
                Console.WriteLine($"{GetName()} помер через голод.");
            }
        }
    }

    public void Reproduce(Cell cell)
    {
        if (cell.GetAnimal() is Fox && !IsHungry())
        {
            Console.WriteLine($"{GetName()} і {cell.GetAnimal().GetName()} розмножилися.");
            cell.SetAnimal(new Fox("Розмножений лис"));
        }
    }
}

public class Grass
{
    private bool isFresh;

    public Grass()
    {
        isFresh = true;
    }

    public void Grow()
    {
        isFresh = true;
    }

    public void Withew()
    {
        isFresh = false;
    }

    public bool IsFresh() => isFresh;
}

