using Dispo.Movement.Core.Application.Services;
using Dispo.Product.Core.Application.Interfaces;
using Dispo.Shared.Core.Domain.DTOs;
using Dispo.Shared.Core.Domain.Enums;
using Dispo.Shared.Core.Domain.Exceptions;
using Dispo.Shared.Core.Domain.Interfaces;
using Dispo.Shared.Log;
using Moq;

namespace Dispo.Core.Application.Tests
{
    public class MovementServiceTests
    {
        private MovementService _sut;
        private Mock<IMovementRepository> _movementRepositoryMock;
        private Mock<IProductService> _productServiceMock;
        private Mock<ILoggingService> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _movementRepositoryMock = new Mock<IMovementRepository>();
            _productServiceMock = new Mock<IProductService>();
            _loggerMock = new Mock<ILoggingService>();

            _sut = new MovementService(_movementRepositoryMock.Object, _productServiceMock.Object, null, null, null, _loggerMock.Object);
        }

        [Test]
        public async Task MoveProductAsync_ValidProductMovimentation_UpdateWarehouseQuantity()
        {
            // Arrange
            var productMovimentationDto = new ProductMovimentationDto(1, 1, 1, eMovementType.Output);

            _productServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(true);

            _movementRepositoryMock.Setup(s => s.CreateAsync(It.IsAny<Shared.Core.Domain.Entities.Movement>()))
                .ReturnsAsync(true);

            //_productWarehouseQuantityServiceMock.Setup(s => s.UpdateProductWarehouseQuantityAsync(It.IsAny<ProductMovimentationDto>())).ReturnsAsync(true);

            // Act
            await _sut.MoveProductAsync(productMovimentationDto);

            // Assert
            //_productWarehouseQuantityServiceMock.Verify(v => v.UpdateProductWarehouseQuantityAsync(productMovimentationDto), Times.Once);
        }

        [Test]
        public async Task MoveProductAsync_ProductDoesntExists_DontUpdateWarehouseQuantityAndThrowException()
        {
            // Arrange
            var productMovimentationDto = new ProductMovimentationDto(1, 1, 1, eMovementType.Output);

            _productServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(false);

            // Act
            var result = Assert.ThrowsAsync<NotFoundException>(async () => await _sut.MoveProductAsync(productMovimentationDto));

            // Assert
            Assert.That(result.Message, Is.EqualTo(string.Format("Produto com o Id {0} n�o foi encontrado.", productMovimentationDto.ProductId)));
            //_productWarehouseQuantityServiceMock.Verify(v => v.UpdateProductWarehouseQuantityAsync(productMovimentationDto), Times.Never);
        }

        [Test]
        public async Task MoveProductAsync_MovementCouldntBeCreated_DontUpdateWarehouseQuantityAndThrowException()
        {
            // Arrange
            var productMovimentationDto = new ProductMovimentationDto(1, 1, 1, eMovementType.Output);

            _productServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(true);

            _movementRepositoryMock.Setup(s => s.CreateAsync(It.IsAny<Shared.Core.Domain.Entities.Movement>()))
                .ReturnsAsync(false);

            // Act
            var result = Assert.ThrowsAsync<UnhandledException>(async () => await _sut.MoveProductAsync(productMovimentationDto));

            // Assert
            Assert.That(result.Message, Is.EqualTo("Movimenta��o n�o pode ser criada."));
            //_productWarehouseQuantityServiceMock.Verify(v => v.UpdateProductWarehouseQuantityAsync(productMovimentationDto), Times.Never);
        }

        [Test]
        public async Task MoveProductAsync_ProductWarehouseQuantityCoulntBeUpdated_DontUpdateWarehouseQuantityAndThrowException()
        {
            // Arrange
            var productMovimentationDto = new ProductMovimentationDto(1, 1, 1, eMovementType.Output);

            _productServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(true);

            _movementRepositoryMock.Setup(s => s.CreateAsync(It.IsAny<Shared.Core.Domain.Entities.Movement>()))
                .ReturnsAsync(true);

            //_productWarehouseQuantityServiceMock.Setup(s => s.UpdateProductWarehouseQuantityAsync(It.IsAny<ProductMovimentationDto>())).ReturnsAsync(false);

            // Act
            var result = Assert.ThrowsAsync<UnhandledException>(async () => await _sut.MoveProductAsync(productMovimentationDto));

            // Assert
            Assert.That(result.Message, Is.EqualTo("Quantidade n�o pode ser atualizada."));
            //_productWarehouseQuantityServiceMock.Verify(v => v.UpdateProductWarehouseQuantityAsync(productMovimentationDto), Times.Once);
        }
    }
}