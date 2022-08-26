using GarageRelation.API.Controllers.Services;
using GarageRelation.API.Models.Dtos;
using GarageRelation.API.Utils;
using Microsoft.AspNetCore.Mvc;

namespace GarageRelation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class PersonController : ControllerBase
    {
        private readonly IPersonService personService;
        private readonly ILogger logger;

        public PersonController(IPersonService personService, ILogger logger)
        {
            this.personService = personService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<PersonResponseDto>> GetAsync()
        {
            var peopleList = await personService.GetAllPersonsAsync();
            var peopleDto = peopleList.Select(person => person.AsDto());

            return peopleDto;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonResponseDto>> GetByIdAsync(int id)
        {
            var person = await personService.GetPersonByIdAsync(id);

            if (person is null)
            {
                return NotFound();
            }

            return Ok(person.AsDto());
        }

        [HttpPost]
        public async Task<ActionResult<PersonResponseDto>> CreateAsync(PersonCreateDto personDto)
        {
            var savedPerson = await personService.SavePersonAsync(new()
            {
                Name = personDto.Name,
                Age = personDto.Age,
                MainAddress = personDto.MainAddress,
                Number = personDto.Number,
                Complement = personDto.Complement,
            });

            return CreatedAtAction(nameof(GetByIdAsync), new { id = savedPerson.Id }, savedPerson.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, PersonUpdateDto personDto)
        {
            var personToUpdate = await personService.GetPersonByIdAsync(id);

            if (personToUpdate is null)
            {
                return NotFound();
            }

            personToUpdate.Name = personDto.Name;
            personToUpdate.Age = personDto.Age;
            personToUpdate.MainAddress = personDto.MainAddress;
            personToUpdate.Number = personDto.Number;
            personToUpdate.Complement = personDto.Complement;

            await personService.UpdatePersonAsync(personToUpdate);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUpdateAsync(int id, PersonPartialUpdateDto personDto)
        {
            var personToUpdate = await personService.GetPersonByIdAsync(id);

            if (personToUpdate is null)
            {
                return NotFound();
            }

            if (personDto.Name != null)
            {
                personToUpdate.Name = personDto.Name;
            }

            if (personDto.Age != null)
            {
                personToUpdate.Age = (byte)personDto.Age;
            }

            if (personDto.MainAddress != null)
            {
                personToUpdate.MainAddress = personDto.MainAddress;
            }

            if (personDto.Number != null)
            {
                personToUpdate.Number = personDto.Number;
            }

            if (personDto.Complement != null)
            {
                personToUpdate.Complement = personDto.Complement;
            }

            await personService.UpdatePersonAsync(personToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var personToDelete = await personService.GetPersonByIdAsync(id);

            if (personToDelete is null)
            {
                return NotFound();
            }

            await personService.DeletePersonAsync(personToDelete);

            return NoContent();
        }
    }
}
