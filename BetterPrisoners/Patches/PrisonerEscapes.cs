using BetterCore.Utils;
using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Localization;

namespace BetterPrisoners.Patches
{

    [HarmonyPatch(typeof(PrisonerReleaseCampaignBehavior), "DailyHeroTick")]
    class PrisonerEscapes {

        public static bool Prefix(Hero hero) {

            bool proceedToTWCode = true;
            bool playersPrisoner = false;
            TextObject escapeText = new TextObject("");
            double chance = 0.0d;
            int partyStrength = 0;
            int partyStrengthMin = 0;

            try {
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

                            if (BetterPrisoners.Settings.FactorInStrengthForEscapeChance) {
                                partyStrength = PartyHelper.CalculatePartyStrength(hero.PartyBelongedToAsPrisoner);
                                partyStrengthMin = BetterPrisoners.Settings.PartyStrengthToPreventEscape;
                            }

                            chance = BetterPrisoners.Settings.PrisonerEscapeWhileTravelingChance + MathHelper.GetPercentage(partyStrength, partyStrengthMin);

                            escapeText = new TextObject(Strings.Escaped);

                            // Is hero prisoner part of a settlement?
                        } else if (hero.PartyBelongedToAsPrisoner.IsSettlement) {

                            if (hero.PartyBelongedToAsPrisoner.Settlement.OwnerClan == Clan.PlayerClan) {
                                playersPrisoner = true;

                                //float militia = hero.PartyBelongedToAsPrisoner.Settlement.Militia;
                                //Helper.DisplayWarningMsg("Roster Count: " + militia);
                            }

                            if (BetterPrisoners.Settings.FactorInStrengthForEscapeChance) {
                                partyStrength = PartyHelper.CalculatePartyStrength(hero.PartyBelongedToAsPrisoner.Settlement.Town.GarrisonParty.Party);
                                partyStrengthMin = BetterPrisoners.Settings.SettlementStrengthToPreventEscape;
                            }

                            chance = BetterPrisoners.Settings.PrisonerEscapeWhileInSettlementChance + MathHelper.GetPercentage(partyStrength, partyStrengthMin);

                            if (hero.IsFemale) {
                                escapeText = new TextObject(Strings.HerMissing);
                            } else {
                                escapeText = new TextObject(Strings.HisMissing);
                            }

                        }

                        bool isReleased = false;

                        if (playersPrisoner) {
                            //Helper.DisplayWarningMsg("Escape attempt for " + hero.Name.ToString() + "! Validating... random: " + random + " chance: " + chance);
                        }

                        if (MathHelper.RandomChance(chance)) {
                            //Helper.DisplayWarningMsg("Escape attempt triggered for "+ hero.Name.ToString() + "! Validating... random: " + random + " chance: " + chance);
                            if (hero.CaptivityStartTime.ElapsedDaysUntilNow > BetterPrisoners.Settings.PrisonerMinDaysToBeImprisoned) {

                                //Send message to player for awareness.
                                if (playersPrisoner)
                                    NotifyHelper.WriteMessage(hero.Name.ToString() + " " + escapeText, MsgType.Notify);

                                EndCaptivityAction.ApplyByEscape(hero, null);
                                isReleased = true;
                            }
                        }

                        if (!isReleased) {
                            if (playersPrisoner) {
                                if (BetterPrisoners.Settings.PrisonerRelationPenalty != 0) {
                                    RelationHelper.ChangeRelation(Hero.MainHero, hero, BetterPrisoners.Settings.PrisonerRelationPenalty);

                                    if (BetterPrisoners.Settings.PrisonerRelationPenaltyAppliesToFamily) {
                                        RelationHelper.ChangeFamilyRelation(Hero.MainHero, hero, BetterPrisoners.Settings.PrisonerRelationPenalty);
                                    }
                                }
                            }
                        }

                        proceedToTWCode = false;
                    }
                }
            } catch (Exception e) {
                NotifyHelper.WriteError(BetterPrisoners.ModName, "PrisonerReleaseCampaignBehavior.DailyHeroTick threw exception: " + e);
            }
            return proceedToTWCode;
        }
    }
}
