using GarageRelation.Dtos;
using GarageRelation.Models;
using GarageRelation.Models.Dtos.GarageRelation.Dtos;

namespace GarageRelation.Utils
{
    public static class Extensions
    {
        public static PersonResponseDto AsDto(this Person person)
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
            return new()
            {
                Id = car.Id,
                Plate = car.Plate,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                PersonId = car.PersonId
            };
        }
    }
}
