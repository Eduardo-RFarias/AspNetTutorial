using GarageRelation.API.Models;

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
    }
}
