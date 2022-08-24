namespace GarageRelation.Models
{
    public class Car
    {
        public int Id { get; set; } = default!;
        public string Plate { get; set; } = default!;
        public string Brand { get; set; } = default!;
        public string Model { get; set; } = default!;
        public int Year { get; set; } = default!;
        public int PersonId { get; set; } = default!;
        public Person Person { get; set; } = default!;
    }
}
