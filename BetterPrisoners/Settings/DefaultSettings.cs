
namespace BetterPrisoners.Settings {
    public class DefaultSettings : ISettings {
        public float PrisonerEscapeWhileTravelingChance { get; set; } = 0;
        public float PrisonerEscapeWhileInSettlementChance { get; set; } = 0;
        public int PrisonerMinDaysToBeImprisoned { get; set; } = 0;
        public int PrisonerRelationPenalty { get; set; } = 0;
        public bool PrisonerRelationPenaltyAppliesToFamily { get; set; } = false;
        public bool FactorInStrengthForEscapeChance { get; set; } = false;
        public int SettlementStrengthToPreventEscape { get; set; } = 500;
        public int PartyStrengthToPreventEscape { get; set; } = 500;
        public bool AllowRansoms { get; set; } = true;
        public bool AutoRejectRansomsForPlayer { get; set; } = false;
    }
}
