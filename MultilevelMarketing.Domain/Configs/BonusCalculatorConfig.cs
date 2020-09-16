namespace MultilevelMarketing.Domain.Configs
{
    /// <summary>
    /// values used to calculate bonus (as percentage)    
    /// </summary>
    public static class BonusCalculatorConfig
    {
        public static decimal SelfMadeSalesBonus = (decimal) 0.1;

        // array indexes are same as referred "level"
        public static decimal[] ReferredSaleBonuses = new[]
        {
            (decimal) 0.05, // first level referral
            (decimal) 0.01 // second level referral
        };
    }
}