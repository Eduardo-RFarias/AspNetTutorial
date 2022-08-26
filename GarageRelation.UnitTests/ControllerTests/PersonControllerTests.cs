using GarageRelation.API.Controllers;
using GarageRelation.API.Controllers.Services;
using GarageRelation.API.Models;
using GarageRelation.API.Models.Dtos;
using GarageRelation.UnitTests.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace GarageRelation.UnitTests.ControllerTests
{
	public class PersonControllerTests
	{
		private readonly Mock<IPersonService> serviceStub = new();
		private readonly Mock<ILogger<PersonController>> loggerStub = new();
		private readonly PersonFactory personFactory = new();

		[Fact]
		public async Task GetByIdAsync_WithUnexistingPerson_ReturnsNotFound()
		{
			// Arrange
			serviceStub.Setup(service => service.GetPersonByIdAsync(It.IsAny<int>()))
				.ReturnsAsync((Person?)null);

			PersonController controller = new(serviceStub.Object, loggerStub.Object);

			// Act
			ActionResult<PersonResponseDto> response = await controller.GetByIdAsync(0);

			// Assert
			response.Result.Should().BeOfType<NotFoundResult>();
		}

		[Fact]
		public async Task GetByIdAsync_WithExistingPerson_ReturnsPerson()
		{
			// Arrange
			Person expectedPerson = personFactory.CreateRandomPerson();

			serviceStub.Setup(service => service.GetPersonByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(expectedPerson);

			PersonController controller = new(serviceStub.Object, loggerStub.Object);

			// Act
			ActionResult<PersonResponseDto> response = await controller.GetByIdAsync(0);

			// Assert
			response.Result.Should().BeOfType<OkObjectResult>();

			OkObjectResult responseBody = (OkObjectResult)response.Result!;

			responseBody.Value.Should().BeOfType<PersonResponseDto>();

			PersonResponseDto dto = (PersonResponseDto)responseBody.Value!;

			dto.Should().NotBeNull();

			dto.Should().BeEquivalentTo(expectedPerson, options => options
			.ComparingByMembers<Person>()
			.ExcludingMissingMembers()
			);
		}

		[Fact]
		public async Task GetAsync_WithEmptyList_ReturnsEmptyList()
		{
			// Arrange
			serviceStub.Setup(service => service.GetAllPersonsAsync())
				.ReturnsAsync(Array.Empty<Person>().AsQueryable());

			PersonController controller = new(serviceStub.Object, loggerStub.Object);

			// Act
			IEnumerable<PersonResponseDto> response = await controller.GetAsync();

			// Assert
			response.Should().BeEmpty();
		}

		[Fact]
		public async Task GetAsync_WithTwoPeople_ReturnsListWithTwoPeople()
		{
			// Arrange
			Person[] people = new[] {
				personFactory.CreateRandomPerson(),
				personFactory.CreateRandomPerson()
			};

			serviceStub.Setup(service => service.GetAllPersonsAsync())
				.ReturnsAsync(people.AsQueryable());

			PersonController controller = new(serviceStub.Object, loggerStub.Object);

			// Act
			IEnumerable<PersonResponseDto> response = await controller.GetAsync();

			// Assert
			response.Count().Should().Be(2);

			response.Should().BeEquivalentTo(people, options => options
			.ComparingByMembers<Person>()
			.ExcludingMissingMembers()
			);
		}

		[Fact]
		public async Task CreateAsync_WithValidPerson_ReturnsCreatedPerson()
		{
			// Arrange
			PersonCreateDto personToCreate = personFactory.CreateRandomPersonCreateDto();
			Person createdPerson = personFactory.CreateRandomPerson();

			serviceStub.Setup(service => service.SavePersonAsync(It.IsAny<Person>()))
				.ReturnsAsync(createdPerson);

			PersonController controller = new(serviceStub.Object, loggerStub.Object);

			// Act
			ActionResult<PersonResponseDto> response = await controller.CreateAsync(personToCreate);

			// Assert
			serviceStub.Verify(mock => mock.SavePersonAsync(
				It.Is<Person>(passedPerson =>
					passedPerson.Id == 0 &&
					passedPerson.Name == personToCreate.Name &&
					passedPerson.Age == personToCreate.Age &&
					passedPerson.MainAddress == personToCreate.MainAddress &&
					passedPerson.Number == personToCreate.Number &&
					passedPerson.Complement == personToCreate.Complement
				)
			), Times.Once());

			response.Result.Should().BeOfType<CreatedAtActionResult>();

			CreatedAtActionResult responseBody = (CreatedAtActionResult)response.Result!;

			responseBody.Value.Should().BeOfType<PersonResponseDto>();

			PersonResponseDto dto = (PersonResponseDto)responseBody.Value!;

			dto.Should().BeEquivalentTo(createdPerson, options => options
			.ComparingByMembers<Person>()
			.ExcludingMissingMembers()
			);
		}

		[Fact]
		public async Task UpdateAsync_WithExistingPerson_ReturnsNoContent()
		{
			// Arrange
			Person retrievedPerson = personFactory.CreateRandomPerson();
			PersonUpdateDto personDto = personFactory.CreatePersonUpdateDto();

			serviceStub.Setup(service => service.GetPersonByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(retrievedPerson);

			serviceStub.Setup(service => service.UpdatePersonAsync(It.IsAny<Person>()))
				.Returns(Task.CompletedTask);

			PersonController controller = new(serviceStub.Object, loggerStub.Object);

			// Act
			ActionResult response = await controller.UpdateAsync(0, personDto);

			// Assert
			serviceStub.Verify(mock => mock.UpdatePersonAsync(
				It.Is<Person>(passedPerson =>
					passedPerson.Id == retrievedPerson.Id &&
					passedPerson.Name == personDto.Name &&
					passedPerson.Age == personDto.Age &&
					passedPerson.MainAddress == personDto.MainAddress &&
					passedPerson.Number == personDto.Number &&
					passedPerson.Complement == personDto.Complement
				)
			), Times.Once());

			response.Should().BeOfType<NoContentResult>();
		}

		[Fact]
		public async Task UpdateAsync_WithUnexistingPerson_ReturnsNotFound()
		{
			// Arrange
			serviceStub.Setup(service => service.GetPersonByIdAsync(It.IsAny<int>()))
				.ReturnsAsync((Person?)null);

			serviceStub.Setup(service => service.UpdatePersonAsync(It.IsAny<Person>()))
				.Returns(Task.CompletedTask);

			PersonController controller = new(serviceStub.Object, loggerStub.Object);

			// Act
			ActionResult response = await controller.UpdateAsync(0, personFactory.CreatePersonUpdateDto());

			// Assert
			serviceStub.Verify(mock => mock.UpdatePersonAsync(It.IsAny<Person>()), Times.Never());

			response.Should().BeOfType<NotFoundResult>();
		}

		[Fact]
		public async Task PartialUpdateAsync_WithExistingPerson_ReturnsNoContent()
		{
			// Arrange
			Person retrievedPerson = personFactory.CreateRandomPerson();
			PersonPartialUpdateDto personDto = personFactory.CreatePersonPartialUpdateDto();

			serviceStub.Setup(service => service.GetPersonByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(retrievedPerson);

			serviceStub.Setup(service => service.UpdatePersonAsync(It.IsAny<Person>()))
				.Returns(Task.CompletedTask);

			PersonController controller = new(serviceStub.Object, loggerStub.Object);

			// Act
			ActionResult response = await controller.PartialUpdateAsync(0, personDto);

			// Assert
			serviceStub.Verify(mock => mock.UpdatePersonAsync(
				It.Is<Person>(passedPerson =>
					passedPerson.Id == retrievedPerson.Id &&
					passedPerson.Name == personDto.Name &&
					passedPerson.Age == retrievedPerson.Age &&
					passedPerson.MainAddress == retrievedPerson.MainAddress &&
					passedPerson.Number == retrievedPerson.Number &&
					passedPerson.Complement == personDto.Complement
				)
			), Times.Once());

			response.Should().BeOfType<NoContentResult>();
		}

		[Fact]
		public async Task PartialUpdateAsync_WithUnexistingPerson_ReturnsNotFound()
		{
			// Arrange
			serviceStub.Setup(service => service.GetPersonByIdAsync(It.IsAny<int>()))
				.ReturnsAsync((Person?)null);

			serviceStub.Setup(service => service.UpdatePersonAsync(It.IsAny<Person>()))
				.Returns(Task.CompletedTask);

			PersonController controller = new(serviceStub.Object, loggerStub.Object);

			// Act
			ActionResult response = await controller.PartialUpdateAsync(0, personFactory.CreatePersonPartialUpdateDto());

			// Assert
			serviceStub.Verify(mock => mock.UpdatePersonAsync(It.IsAny<Person>()), Times.Never());

			response.Should().BeOfType<NotFoundResult>();
		}

		[Fact]
		public async Task DeleteAsync_WithExistingPerson_ReturnsNoContent()
		{
			// Arrange
			Person retrievedPerson = personFactory.CreateRandomPerson();

			serviceStub.Setup(service => service.GetPersonByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(retrievedPerson);

			serviceStub.Setup(service => service.DeletePersonAsync(It.IsAny<Person>()))
				.Returns(Task.CompletedTask);

			PersonController controller = new(serviceStub.Object, loggerStub.Object);

			// Act
			ActionResult response = await controller.DeleteAsync(0);

			// Assert
			serviceStub.Verify(mock => mock.DeletePersonAsync(
				It.Is<Person>(passedPerson =>
					passedPerson == retrievedPerson
				)
			), Times.Once());

			response.Should().BeOfType<NoContentResult>();
		}

		[Fact]
		public async Task DeleteAsync_WithUnexistingPerson_ReturnsNotFound()
		{
			// Arrange
			serviceStub.Setup(service => service.GetPersonByIdAsync(It.IsAny<int>()))
				.ReturnsAsync((Person?)null);

			serviceStub.Setup(service => service.DeletePersonAsync(It.IsAny<Person>()))
				.Returns(Task.CompletedTask);

			PersonController controller = new(serviceStub.Object, loggerStub.Object);

			// Act
			ActionResult response = await controller.DeleteAsync(0);

			// Assert
			serviceStub.Verify(mock => mock.DeletePersonAsync(It.IsAny<Person>()), Times.Never());

			response.Should().BeOfType<NotFoundResult>();
		}
	}
}