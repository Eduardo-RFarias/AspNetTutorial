using GarageRelation.Controllers.Repositories;
using GarageRelation.Models;
using Microsoft.EntityFrameworkCore;

namespace GarageRelation.Controllers.Services
{
    public interface IPersonService
    {
        Task DeletePersonAsync(Person personToDelete);
        Task<IList<Person>> GetAllPersonsAsync();
        Task<Person?> GetPersonByIdAsync(int id);
        Task<Person> SavePersonAsync(Person personToSave);
        Task UpdatePersonAsync(Person personToUpdate);
    }

    public class PersonService : IPersonService
    {
        private readonly MySqlRepository repository;

        public PersonService(MySqlRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IList<Person>> GetAllPersonsAsync()
        {
            var people = await repository.Person.ToListAsync();

            return people;
        }

        public async Task<Person?> GetPersonByIdAsync(int id)
        {
            return await repository.Person.SingleOrDefaultAsync(person => person.Id == id);
        }

        public async Task<Person> SavePersonAsync(Person personToSave)
        {
            var saveResult = await repository.Person.AddAsync(personToSave);
            await repository.SaveChangesAsync();

            return saveResult.Entity;
        }

        public async Task UpdatePersonAsync(Person personToUpdate)
        {
            repository.Person.Update(personToUpdate);
            await repository.SaveChangesAsync();
        }

        public async Task DeletePersonAsync(Person personToDelete)
        {
            repository.Person.Remove(personToDelete);
            await repository.SaveChangesAsync();
        }
    }
}
