namespace BetterPrisoners.Localizations {
    public class RefValues {

        public const string EscapeText = "Excape";
        public const string RelationText = "Relation";
        public const string StrengthText = "Strength";
        public const string RansomsText = "Ransoms";

        public const string DaysText = "Days";

        public const string EscapeTravelText = "Prisoner Escape While Traveling Chance";
        public const string EscapeTravelHint = "Percent chance at escaping while traveling";

        public const string EscapeSettleText = "Prisoner Escape While In Settlement Chance";
        public const string EscapeSettleHint = "Percent chance at escaping while in a settlement";

        public const string MinDaysText = "Prisoner Min Days To Be Imprisoned";
        public const string MinDaysHint = "Minimum days a prisoner will be imprisoned (before escape or ransom is possible)";

        public const string RelationPenText = "Prisoner Relation Penalty";
        public const string RelationPenHint = "Amount of relation lost or gained while imprisoned";

        public const string ApplyFamText = "Prisoner Relation Penalty Applies To Family";
        public const string ApplyFamHint = "Should relationship loss extended to entire family of prisoner";

        public const string FactorStrText = "Factor In Strength For Escape Chance";
        public const string FactorStrHint = "Should strength be a factor in escape chance";

        public const string SettleStrText = "Settlement Strength To Prevent Escape";
        public const string SettleStrHint = "Minimum strength a settlement must have to prevent escape";

        public const string PartyStrText = "Party Strength To Prevent Escape";
        public const string PartyStrHint = "Minimum strength a party must have to prevent escape";

        public const string AllowRanText = "Allow Ransoms";
        public const string AllowRanHint = "Should ransoms be allowed? if not prisoners should only be released at peace time (or if they escape)";

        public const string AutoRejText = "Auto Reject Ransoms For Player";
        public const string AutoRejHint = "Prisoners will not be auto ransomed, if set to false auto ransom will only occur after minimum days to be imprisoned has past";

        public const string HisMissing = "is missing from his cell";
        public const string HerMissing = "is missing from her cell";
        public const string Escaped = "has escaped";
        public const string AutoRejected = "Auto rejected ransom for ";
    }
}
