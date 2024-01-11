using BetterPrisoners.Localizations;
using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;

namespace BetterPrisoners.Settings {

    public class MCMSettings : AttributeGlobalSettings<MCMSettings> {

        [SettingPropertyGroup(RefValues.EscapeText)]
        [SettingPropertyFloatingInteger(RefValues.EscapeTravelText, 0f, 1f, "0.0%", Order = 0, RequireRestart = false, HintText = RefValues.EscapeTravelHint)]
        public float PrisonerEscapeWhileTravelingChance { get; set; } = 0;

        [SettingPropertyGroup(RefValues.EscapeText)]
        [SettingPropertyFloatingInteger(RefValues.EscapeSettleText, 0f, 1f, "0.0%", Order = 0, RequireRestart = false, HintText = RefValues.EscapeSettleHint)]
        public float PrisonerEscapeWhileInSettlementChance { get; set; } = 0;

        [SettingPropertyGroup(RefValues.EscapeText)]
        [SettingPropertyInteger(RefValues.MinDaysText, 0, 100, "0 " + RefValues.DaysText, Order = 0, RequireRestart = false, HintText = RefValues.MinDaysHint)]
        public int PrisonerMinDaysToBeImprisoned { get; set; } = 0;

        [SettingPropertyGroup(RefValues.RelationText)]
        [SettingPropertyInteger(RefValues.RelationPenText, -10, 10, "0", Order = 0, RequireRestart = false, HintText = RefValues.RelationPenHint)]
        public int PrisonerRelationPenalty { get; set; } = 0;

        [SettingPropertyGroup(RefValues.RelationText)]
        [SettingPropertyBool(RefValues.ApplyFamText, Order = 0, RequireRestart = false, HintText = RefValues.ApplyFamHint)]
        public bool PrisonerRelationPenaltyAppliesToFamily { get; set; } = false;

        [SettingPropertyGroup(RefValues.EscapeText + "/" + RefValues.StrengthText)]
        [SettingPropertyBool(RefValues.FactorStrText, IsToggle = true, Order = 0, RequireRestart = false, HintText = RefValues.FactorStrHint)]
        public bool FactorInStrengthForEscapeChance { get; set; } = false;

        [SettingPropertyGroup(RefValues.EscapeText + "/" + RefValues.StrengthText)]
        [SettingPropertyInteger(RefValues.SettleStrText, 0, 2000, "0", Order = 0, RequireRestart = false, HintText = RefValues.SettleStrHint)]
        public int SettlementStrengthToPreventEscape { get; set; } = 500;

        [SettingPropertyGroup(RefValues.EscapeText + "/" + RefValues.StrengthText)]
        [SettingPropertyInteger(RefValues.PartyStrText, 0, 2000, "0", Order = 0, RequireRestart = false, HintText = RefValues.PartyStrHint)]
        public int PartyStrengthToPreventEscape { get; set; } = 500;

        [SettingPropertyGroup(RefValues.RansomsText)]
        [SettingPropertyBool(RefValues.AllowRanText, Order = 0, RequireRestart = false, HintText = RefValues.AllowRanHint)]
        public bool AllowRansoms { get; set; } = true;

        [SettingPropertyGroup(RefValues.RansomsText)]
        [SettingPropertyBool(RefValues.AutoRejText, Order = 0, RequireRestart = false, HintText = RefValues.AutoRejHint)]
        public bool AutoRejectRansomsForPlayer { get; set; } = false;


        public override string Id { get { return base.GetType().Assembly.GetName().Name; } }
        public override string DisplayName { get { return base.GetType().Assembly.GetName().Name; } }
        public override string FolderName { get { return base.GetType().Assembly.GetName().Name; } }
        public override string FormatType { get; } = "xml";
    }
}
