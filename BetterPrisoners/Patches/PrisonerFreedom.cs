using BetterCore.Utils;
using BetterPrisoners.Localizations;
using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Localization;

namespace BetterPrisoners.Patches {
    [HarmonyPatch(typeof(RansomOfferCampaignBehavior), "ConsiderRansomPrisoner")]
    class PrisonerFreedom {
        public static bool Prefix(Hero hero) {
            bool proceedToTWCode = true;
            try {

                if (BetterPrisoners.Settings.AllowRansoms) {
                    if (hero != null) {
                        if (hero.IsPrisoner && hero.PartyBelongedToAsPrisoner != null && hero != Hero.MainHero) {
                            if (hero.PartyBelongedToAsPrisoner == PartyBase.MainParty || (hero.PartyBelongedToAsPrisoner.IsSettlement && hero.PartyBelongedToAsPrisoner.Settlement.OwnerClan == Clan.PlayerClan)) {
                                if (BetterPrisoners.Settings.AutoRejectRansomsForPlayer) {
                                    NotifyHelper.ChatMessage(new TextObject(RefValues.AutoRejected) + hero.Name.ToString(), MsgType.Notify);
                                    proceedToTWCode = false;
                                } else {
                                    if (hero.CaptivityStartTime.ElapsedDaysUntilNow < BetterPrisoners.Settings.PrisonerMinDaysToBeImprisoned) {
                                        proceedToTWCode = false;
                                    }
                                }
                            } else {
                                if (hero.CaptivityStartTime.ElapsedDaysUntilNow < BetterPrisoners.Settings.PrisonerMinDaysToBeImprisoned) {
                                    proceedToTWCode = false;
                                }
                            }
                        }
                    }

                } else {
                    proceedToTWCode = false;
                }
            } catch (Exception e) {
                NotifyHelper.ReportError(BetterPrisoners.ModName, "RansomOfferCampaignBehavior.ConsiderRansomPrisoner threw exception: " + e);
            }

            return proceedToTWCode;

        }
    }
}
