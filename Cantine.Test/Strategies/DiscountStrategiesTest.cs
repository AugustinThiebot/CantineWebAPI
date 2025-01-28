using Cantine.Application.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cantine.Test.Strategies
{
    public class DiscountStrategiesTest
    {
        [Fact]
        public void ApplyDiscount_NoDiscount_ReturnsOriginalPrice()
        {
            var strategy = new NoDiscountStrategy();
            decimal price = 100m;
            decimal discount = 25m;

            var result = strategy.ApplyDiscount(price, discount);

            Assert.Equal(price, result);
        }

        [Fact]
        public void ApplyDiscount_PercentageDiscount_ReturnsDiscountedPrice()
        {
            var strategy = new PercentageDiscountStrategy();
            decimal price = 80m;
            decimal discount = 25m;

            var result = strategy.ApplyDiscount(price, discount);

            Assert.Equal(60m, result);
        }

        [Theory]
        [InlineData("80", "25", "55")]
        [InlineData("80", "100", "0")]
        public void ApplyDiscount_FixedDiscount_ReturnsDiscountedPrice(String priceStr, String discountStr, String expectedDiscountedPriceStr)
        {
            var strategy = new FixedDiscountStrategy();
            decimal price = Convert.ToDecimal(priceStr);
            decimal discount = Convert.ToDecimal(discountStr);
            decimal expectedDiscountedPrice = Convert.ToDecimal(expectedDiscountedPriceStr);

            var result = strategy.ApplyDiscount(price, discount);

            Assert.Equal(expectedDiscountedPrice, result);
        }

    }

}
