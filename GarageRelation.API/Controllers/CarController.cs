using GarageRelation.API.Controllers.Services;
using GarageRelation.API.Models;
using GarageRelation.API.Models.Dtos;
using GarageRelation.API.Utils;
using Microsoft.AspNetCore.Mvc;

namespace GarageRelation.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public sealed class CarController : ControllerBase
	{
		private readonly ICarService carService;
		private readonly IPersonService personService;
		private readonly ILogger<CarController> logger;

		public CarController(ICarService carService, IPersonService personService, ILogger<CarController> logger)
		{
			this.carService = carService;
			this.personService = personService;
			this.logger = logger;
		}

		[HttpGet]
		public async Task<IEnumerable<CarResponseDto>> GetAsync()
		{
			IQueryable<Car> cars = await carService.GetAllCarsAsync();
			IQueryable<CarResponseDto> carsDto = cars.Select(car => car.AsDto());

			return carsDto.AsEnumerable();
		}

		[HttpGet("by-person/{personId}")]
		public async Task<ActionResult<IEnumerable<CarResponseDto>>> GetAllByPersonAsync(int personId)
		{
			Person? retrievedPerson = await personService.GetPersonByIdAsync(personId);

			if (retrievedPerson is null)
			{
				return NotFound();
			}

			IQueryable<Car> cars = await carService.GetAllCarsAsync();
			IQueryable<CarResponseDto> carsDto = cars.Where(car => car.PersonId == personId).Select(car => car.AsDto());

			return Ok(carsDto);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<CarResponseDto>> GetByIdAsync(int id)
		{
			Car? car = await carService.GetCarByIdAsync(id);

			return car is null ? (ActionResult<CarResponseDto>)NotFound() : (ActionResult<CarResponseDto>)Ok(car.AsDto());
		}

		[HttpPost]
		public async Task<ActionResult<CarResponseDto>> CreateAsync(CarCreateDto dto)
		{
			Person? retrievedPerson = await personService.GetPersonByIdAsync(dto.PersonId);

			if (retrievedPerson is null)
			{
				return BadRequest();
			}

			Car createdCar = await carService.SaveCarAsync(new()
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
			Car? carToUpdate = await carService.GetCarByIdAsync(id);

			if (carToUpdate is null)
			{
				return NotFound();
			}

			carToUpdate.Plate = carDto.Plate;
			carToUpdate.Brand = carDto.Brand;
			carToUpdate.Model = carDto.Model;
			carToUpdate.Year = carDto.Year;
			carToUpdate.PersonId = carDto.PersonId;

			Person? retrievedPerson = await personService.GetPersonByIdAsync(carToUpdate.PersonId);

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
			Car? carToUpdate = await carService.GetCarByIdAsync(id);

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

				Person? retrievedPerson = await personService.GetPersonByIdAsync(carToUpdate.PersonId);

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
			Car? retrievedCar = await carService.GetCarByIdAsync(id);

			if (retrievedCar is null)
			{
				return NotFound();
			}

			await carService.DeleteCarAsync(retrievedCar);
			return NoContent();
		}
	}
}
