using HarmonyLib;
using BetterCore.Utils;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Party;

namespace BetterPrisoners.Patches {
    [HarmonyPatch(typeof(RansomOfferCampaignBehavior), "ConsiderRansomPrisoner")]
    class PrisonerFreedom {
        public static bool Prefix(Hero hero) {

            bool proceedToTWCode = true;

            if (SubModule._settings.AllowRansoms) {
                if (hero != null) {
                    if (hero.IsPrisoner && hero.PartyBelongedToAsPrisoner != null && hero != Hero.MainHero) {
                        if (hero.PartyBelongedToAsPrisoner == PartyBase.MainParty || (hero.PartyBelongedToAsPrisoner.IsSettlement && hero.PartyBelongedToAsPrisoner.Settlement.OwnerClan == Clan.PlayerClan)) {
                            if (SubModule._settings.AutoRejectRansomsForPlayer) {
                                Logger.SendMessage("Auto rejected ransom for " + hero.Name.ToString(), Severity.Notify);
                                proceedToTWCode = false;
                            } else {
                                if (hero.CaptivityStartTime.ElapsedDaysUntilNow < SubModule._settings.PrisonerMinDaysToBeImprisoned) {
                                    proceedToTWCode = false;
                                }
                            }
                        } else {
                            if (hero.CaptivityStartTime.ElapsedDaysUntilNow < SubModule._settings.PrisonerMinDaysToBeImprisoned) {
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
