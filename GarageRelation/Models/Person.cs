namespace GarageRelation.Models
{
    public class Person
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public int Age { get; set; } = default!;
        public string MainAddress { get; set; } = default!;
        public string Number { get; set; } = default!;
        public string? Complement { get; set; } = default!;
        public List<Car> Cars { get; set; } = new();
    }
}