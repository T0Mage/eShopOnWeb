using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.BasketServiceTests
{
    public class AddItemToBasket
    {
        private readonly string _buyerId = "Test buyerId";
        private readonly Mock<IAsyncRepository<Basket>> _mockBasketRepository;

        public AddItemToBasket()
        {
            _mockBasketRepository = new Mock<IAsyncRepository<Basket>>();
        }

        [Fact]
        public async Task InvokesBasketRepositoryGetByIdAsyncOnce()
        {
            var basket = new Basket(_buyerId);
            basket.AddItem(1, It.IsAny<decimal>(), It.IsAny<int>());
            _mockBasketRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<BasketWithItemsSpecification>(), default)).ReturnsAsync(basket);

            var basketService = new BasketService(_mockBasketRepository.Object, null);

            await basketService.AddItemToBasket(basket.Id, 1, 1.50m);

            _mockBasketRepository.Verify(x => x.FirstOrDefaultAsync(It.IsAny<BasketWithItemsSpecification>(),default), Times.Once);
        }

        [Fact]
        public async Task InvokesBasketRepositoryUpdateAsyncOnce()
        {
            var basket = new Basket(_buyerId);
            basket.AddItem(1, It.IsAny<decimal>(), It.IsAny<int>());
            _mockBasketRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<BasketWithItemsSpecification>(),default)).ReturnsAsync(basket);

            var basketService = new BasketService(_mockBasketRepository.Object, null);

            await basketService.AddItemToBasket(basket.Id, 1, 1.50m);

            _mockBasketRepository.Verify(x => x.UpdateAsync(basket,default), Times.Once);
        }
    }
}
