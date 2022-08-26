namespace GarageRelation.API.Models
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

		public override bool Equals(object? obj)
		{
			return obj is Car car &&
				   Id == car.Id &&
				   Plate == car.Plate &&
				   Brand == car.Brand &&
				   Model == car.Model &&
				   Year == car.Year &&
				   PersonId == car.PersonId;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, Plate, Brand, Model, Year, PersonId);
		}

		public static bool operator ==(Car car, object? comparator)
		{
			return car.Equals(comparator);
		}

		public static bool operator !=(Car car, object? comparator)
		{
			return !car.Equals(comparator);
		}
	}
}
