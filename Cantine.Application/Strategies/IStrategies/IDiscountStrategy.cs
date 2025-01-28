namespace Cantine.Application.Strategies.IStrategies
{
    public interface IDiscountStrategy
    {
        decimal ApplyDiscount(decimal total, decimal discountValue);
    }
}
