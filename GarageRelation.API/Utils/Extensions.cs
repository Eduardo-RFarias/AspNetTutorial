using GarageRelation.API.Models;
using GarageRelation.API.Models.Dtos;

namespace GarageRelation.API.Utils
{
	public static class Extensions
	{
		public static PersonResponseDto AsDto(this Person person)
		{
			return new(
				person.Id,
				person.Name,
				person.Age,
				person.MainAddress,
				person.Number,
				person.Complement
			);
		}

		public static Person AsModel(this PersonResponseDto person)
		{
			return new()
			{
				Id = person.Id,
				Name = person.Name,
				Age = person.Age,
				MainAddress = person.MainAddress,
				Number = person.Number,
				Complement = person.Complement
			};
		}

		public static CarResponseDto AsDto(this Car car)
		{
			return new(
				car.Id,
				car.Plate,
				car.Brand,
				car.Model,
				car.Year,
				car.PersonId
			);
		}

		public static Car AsModel(this CarResponseDto car)
		{
			return new()
			{
				Id = car.Id,
				Plate = car.Plate,
				Brand = car.Brand,
				Model = car.Model,
				Year = car.Year,
				PersonId = car.PersonId,
			};
		}
	}
}
