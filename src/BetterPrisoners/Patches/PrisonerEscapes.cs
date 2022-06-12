using HarmonyLib;
using System.Linq;
using TaleWorlds.Core;
using BetterPrisoners.Utils;
using TaleWorlds.CampaignSystem;
using System.Collections.Generic;
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

                        if (Helper.settings.FactorInStrengthForEscapeChance) {
                            chance = CalculateChance(Helper.settings.PrisonerEscapeWhileTravelingChance, hero.PartyBelongedToAsPrisoner, Helper.settings.PartyStrengthToPreventEscape);
                        } else {
                            chance = Helper.settings.PrisonerEscapeWhileTravelingChance;
                        }
                        
                        excapeText = "has escaped";

                    // Is hero prisoner part of a settlement?
                    } else if (hero.PartyBelongedToAsPrisoner.IsSettlement) {

                        if (hero.PartyBelongedToAsPrisoner.Settlement.OwnerClan == Clan.PlayerClan) {
                            playersPrisoner = true;

                            //float militia = hero.PartyBelongedToAsPrisoner.Settlement.Militia;
                            //Helper.DisplayWarningMsg("Roster Count: " + militia);
                        }

                        if (Helper.settings.FactorInStrengthForEscapeChance) {
                            if (hero.PartyBelongedToAsPrisoner.Settlement.Town.GarrisonParty != null) {
                                chance = CalculateChance(Helper.settings.PrisonerEscapeWhileInSettlementChance, hero.PartyBelongedToAsPrisoner.Settlement.Town.GarrisonParty.Party, Helper.settings.SettlementStrengthToPreventEscape);
                            } else {
                                chance = CalculateChance(Helper.settings.PrisonerEscapeWhileInSettlementChance, null, Helper.settings.SettlementStrengthToPreventEscape);
                            }
                        } else {
                            chance = Helper.settings.PrisonerEscapeWhileInSettlementChance;
                        }

                        excapeText = "is missing from " + (hero.IsFemale ? "her" : "his") + " cell"; ;

                    }

                    bool isReleased = false;
                    double random = MBRandom.RandomFloat;

                    if (playersPrisoner) {
                        //Helper.DisplayWarningMsg("Escape attempt for " + hero.Name.ToString() + "! Validating... random: " + random + " chance: " + chance);
                    }

                    if (random <= chance) {
                        //Helper.DisplayWarningMsg("Escape attempt triggered for "+ hero.Name.ToString() + "! Validating... random: " + random + " chance: " + chance);
                        if (hero.CaptivityStartTime.ElapsedDaysUntilNow > Helper.settings.PrisonerMinDaysToBeImprisoned) {

                            //Send message to player for awareness.
                            if (playersPrisoner)
                                Helper.DisplayWarningMsg(hero.Name.ToString() + " " + excapeText);

                            EndCaptivityAction.ApplyByEscape(hero, null);
                            isReleased = true;
                        }
                    }

                    if (!isReleased) {
                        if (playersPrisoner) {
                            if (Helper.settings.PrisonerRelationPenalty != 0) {
                                ChangeRelation(Hero.MainHero, hero, Helper.settings.PrisonerRelationPenalty);

                                if (Helper.settings.PrisonerRelationPenaltyAppliesToFamily) {
                                    ChangeFamilyRelation(Hero.MainHero, hero, Helper.settings.PrisonerRelationPenalty);
                                }
                            }
                        }
                    }

                    proceedToTWCode = false;
                }
            }

            return proceedToTWCode;
        }

        public static double CalculateChance(double baseChance, PartyBase party, int strSetting) {
            double variation;

            if (party != null) {
                variation = CalculateVariation(CalculatePartyStrength(party), strSetting);
            } else {
                variation = CalculateVariation(0, strSetting);
            }

            return baseChance + variation;
        }

        public static double CalculateVariation(double min, double max) {
            //Prevent divide by zero
            if (max == 0) {
                max = 1;
            }

            double var = min / max;

            if (var > 1) {
                var = 1;
            }

            return (1d - var);
        }

        public static int CalculatePartyStrength(PartyBase party) {
            int strength = 0;

            if (party != null) {
                int partyCount, addTroops = 0, number;

                partyCount = party.NumberOfHealthyMembers;

                int tier1 = party.GetNumberOfHealthyMenOfTier(1);
                int tier2 = party.GetNumberOfHealthyMenOfTier(2);
                int tier3 = party.GetNumberOfHealthyMenOfTier(3);
                int tier4 = party.GetNumberOfHealthyMenOfTier(4);
                int tier5 = party.GetNumberOfHealthyMenOfTier(5);
                int tier6 = party.GetNumberOfHealthyMenOfTier(6);

                number = tier1 + tier2 + tier3 + tier4 + tier5 + tier6;

                if (number < partyCount) {
                    //Using troops mods for higher tier units
                    addTroops = partyCount - number;
                }

                strength = tier1 + (2 * tier2) + (3 * tier3) + (4 * tier4) + (5 * tier5) + (6 * tier6) + (7 * addTroops);
            }

            return strength;
        }

        public static void ChangeRelation(Hero lord1, Hero lord2, int change) {
            if (lord1 != null && lord2 != null) {
                int relation = CharacterRelationManager.GetHeroRelation(lord1, lord2) + change;

                if (relation > 100)
                    relation = 100;
                else {
                    if (relation < -100)
                        relation = -100;
                }

                CharacterRelationManager.SetHeroRelation(lord1, lord2, relation);
            }
        }

        /*** Change Family Relation ***/
        public static void ChangeFamilyRelation(Hero lord1, Hero lord2, int change) {
            List<Hero> familyMembers = FindFamily(lord2);

            for (int i = 0; i < familyMembers.Count; i++) { ChangeRelation(lord1, familyMembers[i], change); }
        }

        public static List<Hero> FindFamily(Hero hero) {
            List<Hero> familyMembers = new List<Hero>();

            if (hero != null) {
                Hero familyMember;

                for (int i = 0; i < hero.Children.Count; i++) {
                    familyMember = hero.Children[i];

                    if (familyMember != null) {
                        if (familyMember.IsAlive)
                            familyMembers.Add(familyMember);
                    }
                }

                for (int i = 0; i < hero.Siblings.Count(); i++) {
                    familyMember = hero.Siblings.ElementAt<Hero>(i);

                    if (familyMember != null) {
                        if (familyMember.IsAlive)
                            familyMembers.Add(familyMember);
                    }
                }

                if (hero.Spouse != null) {
                    if (hero.Spouse.IsAlive)
                        familyMembers.Add(hero.Spouse);
                }

                if (hero.Father != null) {
                    if (hero.Father.IsAlive)
                        familyMembers.Add(hero.Father);
                }

                if (hero.Mother != null) {
                    if (hero.Mother.IsAlive)
                        familyMembers.Add(hero.Mother);
                }
            }

            return familyMembers;
        }

    }
}
