using HarmonyLib;
using BetterPrisoners.Utils;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Party;

namespace BetterPrisoners.Patches {
    [HarmonyPatch(typeof(RansomOfferCampaignBehavior), "ConsiderRansomPrisoner")]
    class PrisonerFreedom {
        public static bool Prefix(Hero hero) {

            bool proceedToTWCode = true;

            if (Helper.settings.AllowRansoms) {
                if (hero != null) {
                    if (hero.IsPrisoner && hero.PartyBelongedToAsPrisoner != null && hero != Hero.MainHero) {
                        if (hero.PartyBelongedToAsPrisoner == PartyBase.MainParty || (hero.PartyBelongedToAsPrisoner.IsSettlement && hero.PartyBelongedToAsPrisoner.Settlement.OwnerClan == Clan.PlayerClan)) {
                            if (Helper.settings.AutoRejectRansomsForPlayer) {
                                Helper.DisplayMsg("Auto rejected ransom for " + hero.Name.ToString());
                                proceedToTWCode = false;
                            } else {
                                if (hero.CaptivityStartTime.ElapsedDaysUntilNow < Helper.settings.PrisonerMinDaysToBeImprisoned) {
                                    proceedToTWCode = false;
                                }
                            }
                        } else {
                            if (hero.CaptivityStartTime.ElapsedDaysUntilNow < Helper.settings.PrisonerMinDaysToBeImprisoned) {
                                proceedToTWCode = false;
                            }
                        }
                    }
                }

            } else {
                proceedToTWCode = false;
            }

            return proceedToTWCode;
        }
    }
}
