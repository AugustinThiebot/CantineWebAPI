using Cantine.Application.Strategies.IStrategies;

namespace Cantine.Application.Strategies
{
    public static class DiscountStrategyFactory
    {
        public static IDiscountStrategy GetStrategy(string discountType)
        {
            return discountType switch
            {
                "Percentage" => new PercentageDiscountStrategy(),
                "Fixed" => new FixedDiscountStrategy(),
                _ => new NoDiscountStrategy()
            };
        }
    }

}
