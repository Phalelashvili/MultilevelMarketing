namespace MultilevelMarketing.Domain.Configs
{
    /// <summary>
    /// values used to calculate bonus (as percentage)    
    /// </summary>
    public static class ReferralConfig
    {
        /// <summary>
        /// how many direct referee distributor can have
        /// </summary>
        public static int FirstLevelReferralLimit = 3;

        /// <summary>
        /// how far referral chain can go down
        /// </summary>
        public static int ReferralLayerLimit = 5;
    }
}