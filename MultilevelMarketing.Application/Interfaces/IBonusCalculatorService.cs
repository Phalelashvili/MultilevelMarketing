using MultilevelMarketing.Application.Input;
using MultilevelMarketing.Application.Output;

namespace MultilevelMarketing.Application.Interfaces
{
    public interface IBonusCalculatorService
    {
        public BonusCalculatorResponse CalculateBonus(BonusCalculatorRequest request);
    }
}