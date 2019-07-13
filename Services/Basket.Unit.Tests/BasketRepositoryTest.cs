using System;
using System.Threading.Tasks;
using Basket.Models;
using Basket.Repositories;
using Xunit;

namespace Basket.Unit.Tests
{
    public class BasketRepositoryTest : IDisposable
    {
        private readonly BasketRepository _basketRepository;

        public BasketRepositoryTest()
        {
            _basketRepository = new BasketRepository();
        }

        public void Dispose()
        {
            _basketRepository.Clear();
        }

        [Fact]
        public async Task<BasketModel> Get_Add_basket()
        {
            //Arrange
            var basketId = "test";

            //Act
            var actualBasket = await _basketRepository.GetBasketAsync(basketId);

            //Assert
            Assert.Equal(actualBasket.Id, basketId);
            Assert.True(actualBasket.Items.Count == 0, "Count of basket items should be equal zero");
            return actualBasket;
        }

        [Fact]
        public async Task<BasketModel> Update_existing_basket()
        {
            //Arrange
            var existingBasket = await Get_Add_basket();
            existingBasket.Items.Add(new Item {Id = 7, RentalDays = 7});

            //Act
            var actualBasket = await _basketRepository.UpdateBasketAsync(existingBasket);

            //Assert
            Assert.NotNull(actualBasket);
            Assert.Equal(existingBasket.Id, actualBasket.Id);
            Assert.Single(actualBasket.Items);
            Assert.Equal(7, actualBasket.Items[0].Id);
            return actualBasket;
        }

        [Fact]
        public async Task Merge_existing_items_in_basket()
        {
            //Arrange
            var existingBasket = await Update_existing_basket();
            existingBasket.Items.Add(new Item { Id = 7, RentalDays = 3 });

            //Act
            var actualBasket = await _basketRepository.UpdateBasketAsync(existingBasket);

            //Assert
            Assert.NotNull(actualBasket);
            Assert.Equal(existingBasket.Id, actualBasket.Id);
            Assert.Single(actualBasket.Items);
            Assert.Equal(7, actualBasket.Items[0].Id);
            Assert.Equal(10, actualBasket.Items[0].RentalDays);
        }

        [Fact]
        public async Task Delete_existing_basket()
        {
            //Arrange
            var existingBasket = await Update_existing_basket();

            //Act
            var deletedBasketResult = await _basketRepository.DeleteBasketAsync(existingBasket.Id);

            //Assert
            Assert.True(deletedBasketResult);
            var actualBasket = await _basketRepository.GetBasketAsync(existingBasket.Id);
            Assert.NotNull(actualBasket);
            Assert.NotEqual(actualBasket, existingBasket);
        }
    }
}
