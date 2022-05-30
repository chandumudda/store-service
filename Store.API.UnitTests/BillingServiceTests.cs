using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using WIPFLI.Infrastructure.Services;
using WIPFLI.Models;
using WIPFLI.Models.Resource;
using Xunit;

namespace Store.API.UnitTests
{
    public class BillingServiceTests
    {
        private readonly Mock<IEnumerable<Item>> _itemsMock;
        private readonly Mock<IItemDiscountService> _itemDiscountServiceMock;
        private readonly Mock<IWeekDaysDiscountService> _weekDaysDiscountServiceMock;
        private readonly Mock<IEnumerable<ItemDiscount>> _itemDiscountDataMock;
        private readonly Mock<IItemService> _itemServiceMock;
        private readonly Mock<IDiscountService> _iDiscountServiceMock;

        public BillingServiceTests()
        {
            _itemDiscountDataMock = new Mock<IEnumerable<ItemDiscount>>();
            _itemServiceMock = new Mock<IItemService>();
            _iDiscountServiceMock = new Mock<IDiscountService>();
            _weekDaysDiscountServiceMock = new Mock<IWeekDaysDiscountService>();
            _itemDiscountServiceMock = new Mock<IItemDiscountService>();
            _itemsMock = new Mock<IEnumerable<Item>>();
            _itemDiscountServiceMock = new Mock<IItemDiscountService>();
        }

        [Fact]
        public void CalculateDiscountAndGetFinalBill_ShouldRunSuccessfully_GivenValidInput()
        {
            //Arrange
            var resource = new CartResource()
            {
                BillDate = DateTime.UtcNow,
                Items = new List<InputItems>()
                {
                    new InputItems() {ItemId = "1", Quantity = 2, UnitPrice = 10}
                }
            };

            var cartItems = new List<CartItem>()
            {
                new CartItem()
                {
                    ItemId = "1", UnitPrice = 10, Quantity = 2, DiscountType = DiscountType.Flat, DiscountValue = 2,
                    PriceAfterDiscount = 18, MinQuantity = 0, TotalPrice = 20, Name = "Thumbs Up",
                    UnitType = UnitType.Bottle
                }
            };

            var items = new List<Item>
            {
                new Item() { Id = "1", UnitType = UnitType.Bottle, Name = "Thumbs Up"}
            };

            var dscountList = new List<ItemDiscount>()
            {
                new ItemDiscount() { ItemId = "1", MinQuantity = 1, DiscountType = DiscountType.Flat, DiscountValue = 0}
            };

            decimal totalWeekDaysDiscount = 1;

            _itemDiscountServiceMock
                .Setup(x => x.CalculateItemDiscount(It.IsAny<CartResource>()))
                .Returns(cartItems);
            _weekDaysDiscountServiceMock
                .Setup(x => x.CalculateWeekdayDiscount(It.IsAny<DateTime>(), It.IsAny<decimal>()))
                .Returns(totalWeekDaysDiscount);
            _itemsMock
                .Setup(x => x.GetEnumerator())
                .Returns(items.GetEnumerator());
            _itemDiscountDataMock
                .Setup(x => x.GetEnumerator())
                .Returns(dscountList.GetEnumerator());
            _iDiscountServiceMock
                .Setup(x => x.GetAllItemDiscounts())
                .Returns((IEnumerable<ItemDiscount>)dscountList.AsEnumerable());
            _itemServiceMock
                .Setup(x => x.GetAllItems())
                .Returns((IEnumerable<Item>)items.AsEnumerable());

            //Act
            var svc = new BillingService(_itemDiscountServiceMock.Object, _weekDaysDiscountServiceMock.Object,
                _itemServiceMock.Object, _iDiscountServiceMock.Object);
            var result = svc.CalculateDiscountAndGetFinalBill(resource);

            //Assert
            Assert.NotNull(result);
            Assert.True(resource.Items.Count.Equals(result.Items.Count));
            _itemDiscountServiceMock
                .Verify(x => x.CalculateItemDiscount(It.IsAny<CartResource>()), Times.Once);
            _weekDaysDiscountServiceMock
                .Verify(x => x.CalculateWeekdayDiscount(It.IsAny<DateTime>(), It.IsAny<decimal>()), Times.Once);
            _iDiscountServiceMock
                .Verify(x => x.GetAllItemDiscounts(), Times.Once);
            _itemServiceMock
                .Verify(x => x.GetAllItems(), Times.Once);
        }
    }
}
