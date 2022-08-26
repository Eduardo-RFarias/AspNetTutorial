using GarageRelation.Controllers.Services;
using GarageRelation.Models.Dtos.GarageRelation.Dtos;
using GarageRelation.Utils;
using Microsoft.AspNetCore.Mvc;

namespace GarageRelation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class CarController : ControllerBase
    {
        private readonly ICarService carService;
        private readonly IPersonService personService;

        public CarController(ICarService carService, IPersonService personService)
        {
            this.carService = carService;
            this.personService = personService;
        }

        [HttpGet]
        public async Task<IEnumerable<CarResponseDto>> GetAsync()
        {
            var cars = await carService.GetAllCarsAsync();
            var carsDto = cars.Select(car => car.AsDto());

            return carsDto;
        }

        [HttpGet("by-person/{personId}")]
        public async Task<ActionResult<IEnumerable<CarResponseDto>>> GetAllByPersonAsync(int personId)
        {
            var retrievedPerson = await personService.GetPersonByIdAsync(personId);

            if (retrievedPerson is null)
            {
                return NotFound();
            }

            var cars = await carService.GetAllCarsAsync();
            var carsDto = cars.Where(car => car.PersonId == personId).Select(car => car.AsDto());

            return Ok(carsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarResponseDto>> GetByIdAsync(int id)
        {
            var car = await carService.GetCarByIdAsync(id);

            if (car is null)
            {
                return NotFound();
            }

            return Ok(car.AsDto());
        }

        [HttpPost]
        public async Task<ActionResult<CarResponseDto>> CreateAsync(CarCreateDto dto)
        {
            var retrievedPerson = await personService.GetPersonByIdAsync(dto.PersonId);

            if (retrievedPerson is null)
            {
                return BadRequest();
            }

            var createdCar = await carService.SaveCarAsync(new()
            {
                Plate = dto.Plate,
                Brand = dto.Brand,
                Model = dto.Model,
                Year = dto.Year,
                Person = retrievedPerson,
                PersonId = retrievedPerson.Id,
            });

            return CreatedAtAction(nameof(GetByIdAsync), new { id = createdCar.Id }, createdCar.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, CarUpdateDto carDto)
        {
            var carToUpdate = await carService.GetCarByIdAsync(id);

            if (carToUpdate is null)
            {
                return NotFound();
            }

            carToUpdate.Plate = carDto.Plate;
            carToUpdate.Brand = carDto.Brand;
            carToUpdate.Model = carDto.Model;
            carToUpdate.Year = carDto.Year;
            carToUpdate.PersonId = carDto.PersonId;

            var retrievedPerson = await personService.GetPersonByIdAsync(carToUpdate.PersonId);

            if (retrievedPerson is null)
            {
                return BadRequest();
            }

            carToUpdate.Person = retrievedPerson;

            await carService.UpdateCarAsync(carToUpdate);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUpdateAsync(int id, CarPartialUpdateDto carDto)
        {
            var carToUpdate = await carService.GetCarByIdAsync(id);

            if (carToUpdate is null)
            {
                return NotFound();
            }

            carToUpdate.Plate = carDto.Plate ?? carToUpdate.Plate;
            carToUpdate.Brand = carDto.Brand ?? carToUpdate.Brand;
            carToUpdate.Model = carDto.Model ?? carToUpdate.Model;
            carToUpdate.Year = carDto.Year ?? carToUpdate.Year;

            if (carDto.PersonId is not null)
            {
                carToUpdate.PersonId = (int)carDto.PersonId;

                var retrievedPerson = await personService.GetPersonByIdAsync(carToUpdate.PersonId);

                if (retrievedPerson is null)
                {
                    return BadRequest();
                }

                carToUpdate.Person = retrievedPerson;
            }

            await carService.UpdateCarAsync(carToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var retrievedCar = await carService.GetCarByIdAsync(id);

            if (retrievedCar is null)
            {
                return NotFound();
            }

            await carService.DeleteCarAsync(retrievedCar);
            return NoContent();
        }
    }
}
