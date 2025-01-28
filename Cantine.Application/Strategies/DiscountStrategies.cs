using Cantine.Application.Strategies.IStrategies;

namespace Cantine.Application.Strategies
{
    public class NoDiscountStrategy: IDiscountStrategy
    {
        public decimal ApplyDiscount(decimal total, decimal discount) => total;
    }

    public class PercentageDiscountStrategy : IDiscountStrategy
    {
        public decimal ApplyDiscount(decimal total, decimal discountValue)
            => total * (100 - discountValue) / 100;
    }

    public class FixedDiscountStrategy : IDiscountStrategy
    {
        public decimal ApplyDiscount(decimal total, decimal discountValue)
            => Math.Max(0, total - discountValue);
    }


}
