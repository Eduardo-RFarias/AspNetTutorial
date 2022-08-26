using GarageRelation.API.Models;
using GarageRelation.API.Models.Dtos;

namespace GarageRelation.UnitTests.Factories
{
	public class PersonFactory
	{
		private readonly Random rand = new();

		public Person CreateRandomPerson()
		{
			return new()
			{
				Id = rand.Next(255),
				Name = rand.NextDouble().ToString(),
				MainAddress = rand.NextDouble().ToString(),
				Number = rand.NextDouble().ToString(),
				Age = rand.Next(255)
			};
		}

		public PersonCreateDto CreateRandomPersonCreateDto()
		{
			return new(
				rand.NextDouble().ToString(),
				rand.Next(255),
				rand.NextDouble().ToString(),
				rand.NextDouble().ToString(),
				rand.NextDouble().ToString()
			);
		}

		public PersonUpdateDto CreatePersonUpdateDto()
		{
			return new(
				rand.NextDouble().ToString(),
				rand.Next(255),
				rand.NextDouble().ToString(),
				rand.NextDouble().ToString(),
				rand.NextDouble().ToString()
			);
		}

		public PersonPartialUpdateDto CreatePersonPartialUpdateDto()
		{
			return new()
			{
				Name = rand.NextDouble().ToString(),
				Complement = rand.NextDouble().ToString(),
			};
		}
	}
}
