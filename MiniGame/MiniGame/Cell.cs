public class Cell
{
    private Animal animal;
    private Grass grass;

    public Cell()
    {
        animal = null;
        grass = new Grass();
    }

    public Animal GetAnimal() => animal;
    public void SetAnimal(Animal animal) => this.animal = animal;
    public Grass GetGrass() => grass;
    public void SetGrass(Grass grass) => this.grass = grass;
}