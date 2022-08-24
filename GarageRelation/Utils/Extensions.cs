using GarageRelation.Dtos;
using GarageRelation.Models;

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
    }
}
