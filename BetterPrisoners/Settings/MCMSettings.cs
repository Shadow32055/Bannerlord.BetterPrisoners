using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;

namespace BetterPrisoners.Settings {

    internal class MCMSettings : AttributeGlobalSettings<MCMSettings>, ISettings {

        public override string Id { get { return base.GetType().Assembly.GetName().Name; } }
        public override string DisplayName { get; } = "Better Prisoners";
        public override string FolderName { get { return base.GetType().Assembly.GetName().Name; } }
        public override string FormatType { get; } = "xml";


        [SettingPropertyGroup("Escape")]
        [SettingPropertyFloatingInteger("Prisoner Escape While Traveling Chance", 0f, 1f, "0.0%", Order = 0, RequireRestart = false, HintText = "Percent chance at escaping while traveling")]
        public float PrisonerEscapeWhileTravelingChance { get; set; } = 0;

        [SettingPropertyGroup("Escape")]
        [SettingPropertyFloatingInteger("Prisoner Escape While In Settlement Chance", 0f, 1f, "0.0%", Order = 0, RequireRestart = false, HintText = "Percent chance at escaping while in a settlement")]
        public float PrisonerEscapeWhileInSettlementChance { get; set; } = 0;

        [SettingPropertyGroup("Escape")]
        [SettingPropertyInteger("Prisoner Min Days To Be Imprisoned", 0, 100, "0 Days", Order = 0, RequireRestart = false, HintText = "Minimum days a prisoner will be imprisoned (before escape or ransom is possible)")]
        public int PrisonerMinDaysToBeImprisoned { get; set; } = 0;

        [SettingPropertyGroup("Relation")]
        [SettingPropertyInteger("Prisoner Relation Penalty", -10, 10, "0", Order = 0, RequireRestart = false, HintText = "Amount of relation lost or gained while imprisoned")]
        public int PrisonerRelationPenalty { get; set; } = 0;


        [SettingPropertyGroup("Relation")]
        [SettingPropertyBool("Prisoner Relation Penalty Applies To Family", Order = 0, RequireRestart = false, HintText = "Should relationship loss extended to entire family of prisoner")]
        public bool PrisonerRelationPenaltyAppliesToFamily { get; set; } = false;

        [SettingPropertyGroup("Escape/Strength")]
        [SettingPropertyBool("Factor In Strength For Escape Chance", IsToggle = true, Order = 0, RequireRestart = false, HintText = "Should strength be a factor in escape chance")]
        public bool FactorInStrengthForEscapeChance { get; set; } = false;

        [SettingPropertyGroup("Escape/Strength")]
        [SettingPropertyInteger("Settlement Strength To Prevent Escape", 0, 2000, "0", Order = 0, RequireRestart = false, HintText = "Minimum strength a settlement must have to prevent escape")]
        public int SettlementStrengthToPreventEscape { get; set; } = 500;

        [SettingPropertyGroup("Escape/Strength")]
        [SettingPropertyInteger("Party Strength To Prevent Escape", 0, 2000, "0", Order = 0, RequireRestart = false, HintText = "Minimum strength a party must have to prevent escape")]
        public int PartyStrengthToPreventEscape { get; set; } = 500;

        [SettingPropertyGroup("Ransoms")]
        [SettingPropertyBool("Allow Ransoms", Order = 0, RequireRestart = false, HintText = "Should ransoms be allowed? if not prisoners should only be released at peace time (or if they escape)")]
        public bool AllowRansoms { get; set; } = true;

        [SettingPropertyGroup("Ransoms")]
        [SettingPropertyBool("Auto Reject Ransoms For Player", Order = 0, RequireRestart = false, HintText = "Prisoners will not be auto ransomed, if set to false auto ransom will only occur after minimum days to be imprisoned has past")]
        public bool AutoRejectRansomsForPlayer { get; set; } = false;

   
    }
}
