using BetterCore.Utils;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Party;

namespace BetterPrisoners.Patches {

    [HarmonyPatch(typeof(PrisonerReleaseCampaignBehavior), "DailyHeroTick")]
    class PrisonerEscapes {

        public static bool Prefix(Hero hero) {

            bool proceedToTWCode = true;
            bool playersPrisoner = false;
            string excapeText = "";
            double chance = 0.0d;
            int partyStrength = 0;
            int partyStrengthMin = 0;

            /*if (hero.PartyBelongedTo.MapFaction == Hero.MainHero.MapFaction) {

            }*/

            //Null check on hero
            if (hero != null) {
                //Make sure the hero is a prisoner, make sure they belong to party and is not the main hero
                if (hero.IsPrisoner && hero.PartyBelongedToAsPrisoner != null && hero != Hero.MainHero) {
                    
                    // Is hero prisoner part of a party
                    if (hero.PartyBelongedToAsPrisoner.IsMobile) {

                        if (hero.PartyBelongedToAsPrisoner == PartyBase.MainParty) {
                            playersPrisoner = true;

                            //TroopRoster roster = hero.PartyBelongedToAsPrisoner.MobileParty.MemberRoster;
                            //Helper.DisplayWarningMsg("Roster Count: " + roster.Count);
                        }

                        if (SubModule._settings.FactorInStrengthForEscapeChance) {
                            partyStrength = PartyHelper.CalculatePartyStrength(hero.PartyBelongedToAsPrisoner);
                            partyStrengthMin = SubModule._settings.PartyStrengthToPreventEscape;
                        }

                        chance = SubModule._settings.PrisonerEscapeWhileTravelingChance + MathHelper.GetPercentage(partyStrength, partyStrengthMin);
                        
                        excapeText = "has escaped";

                    // Is hero prisoner part of a settlement?
                    } else if (hero.PartyBelongedToAsPrisoner.IsSettlement) {

                        if (hero.PartyBelongedToAsPrisoner.Settlement.OwnerClan == Clan.PlayerClan) {
                            playersPrisoner = true;

                            //float militia = hero.PartyBelongedToAsPrisoner.Settlement.Militia;
                            //Helper.DisplayWarningMsg("Roster Count: " + militia);
                        }

                        if (SubModule._settings.FactorInStrengthForEscapeChance) {
                            partyStrength = PartyHelper.CalculatePartyStrength(hero.PartyBelongedToAsPrisoner.Settlement.Town.GarrisonParty.Party);
                            partyStrengthMin = SubModule._settings.SettlementStrengthToPreventEscape;
                        }

                        chance = SubModule._settings.PrisonerEscapeWhileInSettlementChance + MathHelper.GetPercentage(partyStrength, partyStrengthMin);

                        excapeText = "is missing from " + (hero.IsFemale ? "her" : "his") + " cell"; ;

                    }

                    bool isReleased = false;

                    if (playersPrisoner) {
                        //Helper.DisplayWarningMsg("Escape attempt for " + hero.Name.ToString() + "! Validating... random: " + random + " chance: " + chance);
                    }

                    if (MathHelper.RandomChance(chance)) {
                        //Helper.DisplayWarningMsg("Escape attempt triggered for "+ hero.Name.ToString() + "! Validating... random: " + random + " chance: " + chance);
                        if (hero.CaptivityStartTime.ElapsedDaysUntilNow > SubModule._settings.PrisonerMinDaysToBeImprisoned) {

                            //Send message to player for awareness.
                            if (playersPrisoner)
                                Logger.SendMessage(hero.Name.ToString() + " " + excapeText, Severity.Notify);

                            EndCaptivityAction.ApplyByEscape(hero, null);
                            isReleased = true;
                        }
                    }

                    if (!isReleased) {
                        if (playersPrisoner) {
                            if (SubModule._settings.PrisonerRelationPenalty != 0) {
                                RelationHelper.ChangeRelation(Hero.MainHero, hero, SubModule._settings.PrisonerRelationPenalty);

                                if (SubModule._settings.PrisonerRelationPenaltyAppliesToFamily) {
                                    RelationHelper.ChangeFamilyRelation(Hero.MainHero, hero, SubModule._settings.PrisonerRelationPenalty);
                                }
                            }
                        }
                    }

                    proceedToTWCode = false;
                }
            }
            return proceedToTWCode;
        }
    }
}
