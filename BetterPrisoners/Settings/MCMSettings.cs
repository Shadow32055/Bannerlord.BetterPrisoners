using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;

namespace BetterPrisoners.Settings
{

    public class MCMSettings : AttributeGlobalSettings<MCMSettings> {

        [SettingPropertyGroup(Strings.EscapeText)]
        [SettingPropertyFloatingInteger(Strings.EscapeTravelText, 0f, 1f, "0.0%", Order = 0, RequireRestart = false, HintText = Strings.EscapeTravelHint)]
        public float PrisonerEscapeWhileTravelingChance { get; set; } = 0;

        [SettingPropertyGroup(Strings.EscapeText)]
        [SettingPropertyFloatingInteger(Strings.EscapeSettleText, 0f, 1f, "0.0%", Order = 0, RequireRestart = false, HintText = Strings.EscapeSettleHint)]
        public float PrisonerEscapeWhileInSettlementChance { get; set; } = 0;

        [SettingPropertyGroup(Strings.EscapeText)]
        [SettingPropertyInteger(Strings.MinDaysText, 0, 100, "0 " + Strings.DaysText, Order = 0, RequireRestart = false, HintText = Strings.MinDaysHint)]
        public int PrisonerMinDaysToBeImprisoned { get; set; } = 0;

        [SettingPropertyGroup(Strings.RelationText)]
        [SettingPropertyInteger(Strings.RelationPenText, -10, 10, "0", Order = 0, RequireRestart = false, HintText = Strings.RelationPenHint)]
        public int PrisonerRelationPenalty { get; set; } = 0;

        [SettingPropertyGroup(Strings.RelationText)]
        [SettingPropertyBool(Strings.ApplyFamText, Order = 0, RequireRestart = false, HintText = Strings.ApplyFamHint)]
        public bool PrisonerRelationPenaltyAppliesToFamily { get; set; } = false;

        [SettingPropertyGroup(Strings.EscapeText + "/" + Strings.StrengthText)]
        [SettingPropertyBool(Strings.FactorStrText, IsToggle = true, Order = 0, RequireRestart = false, HintText = Strings.FactorStrHint)]
        public bool FactorInStrengthForEscapeChance { get; set; } = false;

        [SettingPropertyGroup(Strings.EscapeText + "/" + Strings.StrengthText)]
        [SettingPropertyInteger(Strings.SettleStrText, 0, 2000, "0", Order = 0, RequireRestart = false, HintText = Strings.SettleStrHint)]
        public int SettlementStrengthToPreventEscape { get; set; } = 500;

        [SettingPropertyGroup(Strings.EscapeText + "/" + Strings.StrengthText)]
        [SettingPropertyInteger(Strings.PartyStrText, 0, 2000, "0", Order = 0, RequireRestart = false, HintText = Strings.PartyStrHint)]
        public int PartyStrengthToPreventEscape { get; set; } = 500;

        [SettingPropertyGroup(Strings.RansomsText)]
        [SettingPropertyBool(Strings.AllowRanText, Order = 0, RequireRestart = false, HintText = Strings.AllowRanHint)]
        public bool AllowRansoms { get; set; } = true;

        [SettingPropertyGroup(Strings.RansomsText)]
        [SettingPropertyBool(Strings.AutoRejText, Order = 0, RequireRestart = false, HintText = Strings.AutoRejHint)]
        public bool AutoRejectRansomsForPlayer { get; set; } = false;


        public override string Id { get { return base.GetType().Assembly.GetName().Name; } }
        public override string DisplayName { get { return base.GetType().Assembly.GetName().Name; } }
        public override string FolderName { get { return base.GetType().Assembly.GetName().Name; } }
        public override string FormatType { get; } = "xml";
    }
}
