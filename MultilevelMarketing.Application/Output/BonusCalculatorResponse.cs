using System.Collections.Generic;

namespace MultilevelMarketing.Application.Output
{
    public class BonusCalculatorResponse : GeneralResponse
    {
        public BonusCalculatorResponse(bool success) : base(success)
        {
        }
        
        public decimal? TotalBonus { get; set; }
        
        public List<decimal> ReferralBonuses { get; set; }
    }
}