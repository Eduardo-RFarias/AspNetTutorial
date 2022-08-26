namespace GarageRelation.API.Models
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

		public override bool Equals(object? obj)
		{
			return obj is Person person &&
				   Id == person.Id &&
				   Name == person.Name &&
				   Age == person.Age &&
				   MainAddress == person.MainAddress &&
				   Number == person.Number &&
				   Complement == person.Complement;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, Name, Age, MainAddress, Number, Complement);
		}

		public static bool operator ==(Person person, object? comparator)
		{
			return person.Equals(comparator);
		}

		public static bool operator !=(Person person, object? comparator)
		{
			return !person.Equals(comparator);
		}
	}
}