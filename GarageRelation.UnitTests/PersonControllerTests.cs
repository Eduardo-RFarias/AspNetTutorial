using GarageRelation.API.Controllers;
using GarageRelation.API.Controllers.Services;
using GarageRelation.API.Models;
using GarageRelation.API.Models.Dtos;
using GarageRelation.UnitTests.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace GarageRelation.UnitTests
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

            var controller = new PersonController(serviceStub.Object, loggerStub.Object);

            // Act
            var response = await controller.GetByIdAsync(0);

            // Assert
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public async Task GetByIdAsync_WithExistingPerson_ReturnsPerson()
        {
            // Arrange
            var expectedPerson = personFactory.CreateRandomPerson();

            serviceStub.Setup(service => service.GetPersonByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedPerson);

            var controller = new PersonController(serviceStub.Object, loggerStub.Object);

            // Act
            var response = await controller.GetByIdAsync(0);

            // Assert
            Assert.IsType<OkObjectResult>(response.Result);

            var responseBody = (ObjectResult)response.Result!;

            Assert.IsType<PersonResponseDto>(responseBody.Value);

            var dto = (PersonResponseDto)responseBody.Value!;

            Assert.NotNull(dto);

            Assert.Equal(expectedPerson.Id, dto!.Id);
            Assert.Equal(expectedPerson.Name, dto!.Name);
            Assert.Equal(expectedPerson.MainAddress, dto!.MainAddress);
            Assert.Null(dto.Complement);
            Assert.Equal(expectedPerson.Number, dto!.Number);
            Assert.Equal(expectedPerson.Age, dto!.Age);
        }
    }
}