
namespace BetterPrisoners.Settings {
    public interface ISettings {
        float PrisonerEscapeWhileTravelingChance { get; set; }
        float PrisonerEscapeWhileInSettlementChance { get; set; }
        int PrisonerMinDaysToBeImprisoned { get; set; }
        int PrisonerRelationPenalty { get; set; }
        bool PrisonerRelationPenaltyAppliesToFamily { get; set; }
        bool FactorInStrengthForEscapeChance { get; set; }
        int SettlementStrengthToPreventEscape { get; set; }
        int PartyStrengthToPreventEscape { get; set; }
        bool AllowRansoms { get; set; }
        bool AutoRejectRansomsForPlayer { get; set; }
    }
}