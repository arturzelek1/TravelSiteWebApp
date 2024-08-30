using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using RepositoryUsingEFinMVC.Controllers;
using TravelSiteWeb.Models;
using TravelSiteWeb.Data;
using TravelSiteWeb.Services;
using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelSiteWeb.ViewModel;
using RepositoryUsingEFinMVC.Repository;

namespace RepositoryUsingEFinMVC.Tests.Controllers
{
    public class ClientControllerTests
    {
        private readonly Mock<IClientRepository> _mockClientRepository;
        private readonly Mock<IPaginatedListService> _mockPaginatedListService;
        private readonly Mock<MappingService> _mockMappingService;
        private readonly Mock<IValidator<Client>> _mockValidator;
        private readonly ClientController _controller;

        public ClientControllerTests()
        {
            _mockClientRepository = new Mock<IClientRepository>();
            _mockPaginatedListService = new Mock<IPaginatedListService>();
            _mockMappingService = new Mock<MappingService>();
            _mockValidator = new Mock<IValidator<Client>>();
            _controller = new ClientController(
                _mockClientRepository.Object,
                _mockPaginatedListService.Object,
                _mockMappingService.Object,
                _mockValidator.Object);
        }
        [Fact]
        public void AddClient_Get_ReturnsViewResult()
        {
            // Act
            var result = _controller.AddClient();

            // Assert
            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public async Task AddClient_Post_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var client = new Client { FirstName = "John", LastName = "Doe" };
            _mockValidator.Setup(v => v.ValidateAsync(client, default))
                .ReturnsAsync(new ValidationResult());

            // Act
            var result = await _controller.AddClient(client);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal("Client", redirectResult.ControllerName);
            _mockClientRepository.Verify(r => r.Insert(client), Times.Once);
            _mockClientRepository.Verify(r => r.Save(), Times.Once);
        }

        [Fact]
        public async Task AddClient_Post_InvalidModel_ReturnsViewResult()
        {
            // Arrange
            var client = new Client { FirstName = "John", LastName = "Doe" };
            var validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("FirstName", "First name is required")
            });

            _mockValidator.Setup(v => v.ValidateAsync(client, default))
                .ReturnsAsync(validationResult);

            // Act
            var result = await _controller.AddClient(client);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(client, viewResult.Model);
        }
        [Fact]
        public void EditClient_Get_ReturnsViewResult_WithClient()
        {
            // Arrange
            var client = new Client { ClientID = 1, FirstName = "John", LastName = "Doe" };
            _mockClientRepository.Setup(repo => repo.GetById(1)).Returns(client);

            // Act
            var result = _controller.EditClient(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Client>(viewResult.Model);
            Assert.Equal(1, model.ClientID);
        }
        [Fact]
        public async Task EditClient_Post_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var client = new Client { ClientID = 1, FirstName = "John", LastName = "Doe" };
            _mockValidator.Setup(v => v.ValidateAsync(client, default))
                .ReturnsAsync(new ValidationResult());

            // Act
            var result = await _controller.EditClient(client);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal("Client", redirectResult.ControllerName);
            _mockClientRepository.Verify(r => r.Update(client), Times.Once);
            _mockClientRepository.Verify(r => r.Save(), Times.Once);
        }

        [Fact]
        public async Task EditClient_Post_InvalidModel_ReturnsViewResult()
        {
            // Arrange
            var client = new Client { ClientID = 1, FirstName = "John", LastName = "Doe" };
            var validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("FirstName", "First name is required")
            });

            _mockValidator.Setup(v => v.ValidateAsync(client, default))
                .ReturnsAsync(validationResult);

            // Act
            var result = await _controller.EditClient(client);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(client, viewResult.Model);
        }
        [Fact]
        public void DeleteClient_Get_ReturnsViewResult_WithClient()
        {
            // Arrange
            var client = new Client { ClientID = 1, FirstName = "John", LastName = "Doe" };
            _mockClientRepository.Setup(repo => repo.GetById(1)).Returns(client);

            // Act
            var result = _controller.DeleteClient(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Client>(viewResult.Model);
            Assert.Equal(1, model.ClientID);
        }
        [Fact]
        public void Delete_Post_RedirectsToIndex()
        {
            // Act
            var result = _controller.Delete(1);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal("Client", redirectResult.ControllerName);
            _mockClientRepository.Verify(r => r.Delete(1), Times.Once);
            _mockClientRepository.Verify(r => r.Save(), Times.Once);
        }
    }
}

