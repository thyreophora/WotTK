using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using WotTK.Common.Globals;
using WotTK.Common.QuestSystem;

namespace WotTK.Common.Players
{
    public class WotTKPlayer : ModPlayer
    {
        public bool hasPaladinTeamBuff;
        public int paladinsTeam;
        public bool maceHitOnGround = false;

        public int playerLevel = 1;
        public int playerLevelPoints = 0;
        public int playerLevelPointsNeed { get => (int)(1000 * MathF.Pow(playerLevel, 0.75f) * (Main.masterMode ? 3f : (Main.expertMode ? 2f : 1f))); }

        public int agility;
        public int intellect;
        public int strength;
        public int stamina;
        public int haste;
        public int armor;
        public int spirit;

        public bool _spawnTentacleSpikesClone;
        public bool _spawnTentacleSpikesClone2;
        public static Point[] _tentacleSpikesMax5 = new Point[5];

        private readonly Dictionary<string, Dictionary<string, object>> questProgressDict = new();
        private readonly HashSet<Quest> activeQuests = new();

        private static int DownedMechBossCount()
        {
            return NPC.downedMechBoss1.ToInt() + NPC.downedMechBoss2.ToInt() + NPC.downedMechBoss3.ToInt();
        }

        public static readonly List<Func<int>> stages = new()
        {
            () => { return NPC.downedSlimeKing ? 10 : 0; },
            () => { return NPC.downedBoss1 ? 15 : 0; },
            () => { return NPC.downedBoss2 ? 20 : 0; },
            () => { return NPC.downedBoss3 ? 25 : 0; },
            () => { return NPC.downedDeerclops ? 30 : 0; },
            () => { return Main.hardMode ? 35 : 0; },
            () => { return NPC.downedQueenSlime ? 40 : 0; },
            () => { return WotTKPlayer.DownedMechBossCount() == 1 ? 45 : 0; },
            () => { return WotTKPlayer.DownedMechBossCount() == 2 ? 50 : 0; },
            () => { return WotTKPlayer.DownedMechBossCount() == 3 ? 55 : 0; },
            () => { return NPC.downedPlantBoss ? 60 : 0; },
            () => { return NPC.downedGolemBoss ? 65 : 0; },
            () => { return NPC.downedEmpressOfLight ? 70 : 0; },
            () => { return NPC.downedFishron ? 70 : 0; },
            () => { return NPC.downedAncientCultist ? 75 : 0; },
            () => { return NPC.downedMoonlord ? 80 : 0; }
        };

        public static int CurrectMaxLevel()
        {
            int max = 5;
            foreach (Func<int> func in stages)
            {
                if (func.Invoke() > max)
                    max = func.Invoke();
            }
            return max;
        }

        public bool CanUseIfLevelIs(int minlevel) => playerLevel >= minlevel;

        public override void ResetEffects()
        {
            paladinsTeam = -1;
            hasPaladinTeamBuff = false;

            agility = 0;
            intellect = 0;
            strength = 0;
            stamina = 0;
            haste = 0;
            armor = 0;
            spirit = 0;
        }

        public static readonly SoundStyle LevelUpSound = new SoundStyle("WotTK/Sounds/Custom/LevelUp");

        public override void PostUpdateEquips()
        {
            Item item = Player.HeldItem;
            if (playerLevel <= 0)
                playerLevel = 1;
            if (playerLevelPoints >= playerLevelPointsNeed && playerLevel < CurrectMaxLevel())
            {
                playerLevelPoints -= playerLevelPointsNeed;
                playerLevel++;
                
                
                Main.NewText(LangHelper.GetText("Events.LevelUp", playerLevel));
                Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, -Vector2.UnitY * 7f, Main.rand.Next(167, 171), 0, 0, Player.whoAmI);
                SoundEngine.PlaySound(LevelUpSound, Player.Center);
            }

            if (hasPaladinTeamBuff)
            {
                paladinsTeam = Player.team;
            }
            if (paladinsTeam != -1)
            {
                for (int i = 0; i < Main.player.Length; i++)
                {
                    Player plr = Main.player[i];
                    if (plr == null) continue;
                    if (!plr.active || plr.whoAmI == paladinsTeam) continue;
                    if (plr.team == paladinsTeam && !Player.dead)
                    {
                        // Effect for your team
                    }
                }
            }
        }

        public override void UpdateEquips()
        {
            Player.GetDamage<MeleeDamageClass>() += strength * 0.005f;
            Player.GetDamage<RangedDamageClass>() += agility * 0.005f;
            Player.GetDamage<MagicDamageClass>() += intellect * 0.005f;

            float lifeIncreasePercentage = stamina * 0.005f;
            int lifeIncrease = (int)(Player.statLifeMax2 * lifeIncreasePercentage);

            Player.statLifeMax2 += lifeIncrease;
            Player.statLifeMax = Player.statLifeMax2;

            if (Player.statLife > Player.statLifeMax)
            {
                Player.statLife = Player.statLifeMax;
            }

            Player.pickSpeed -= haste * 0.005f;

            Player.endurance += armor * 0.001f;

            Player.lifeRegen += (int)(Player.lifeRegen * spirit * 0.005f);
            Player.manaRegen += (int)(Player.manaRegen * spirit * 0.005f);
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            TriggerPointUp(target, hit);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            TriggerPointUp(target, hit);
        }

        private void TriggerPointUp(NPC target, NPC.HitInfo hit)
        {
            if (!target.TryGetGlobalNPC<NPCLevels>(out var globalNPC))
            {
                return;
            }
            
            var difference = Math.Abs(playerLevel - globalNPC.Level);
            
            var chance = 0;
        
            switch (difference)
            {
                case 3:
                    chance = 0;
                    break;
                case 2:
                    chance = 4;
                    break;
                case 1:
                    chance = 3;
                    break;
                case 0:
                    chance = 1;
                    break;
            }

            if (chance == 0 || !Main.rand.NextBool(chance))
            {
                return;
            }
            
            if (target.life <= hit.Damage && target.GetGlobalNPC<WotTKGlobalNPC>().canGetExp && playerLevel < CurrectMaxLevel())
            {
                int vvalue = (int)target.value / 2;
                if (target.boss)
                    vvalue /= 2;
                playerLevelPoints += vvalue;
                target.GetGlobalNPC<WotTKGlobalNPC>().canGetExp = false;
            }
            
            if (playerLevel >= CurrectMaxLevel())
                playerLevelPoints = 0;
        }
        
        public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (item.DamageType == DamageClass.Melee || item.DamageType == DamageClass.MeleeNoSpeed)
            {
                velocity *= (1f + agility * 0.01f + strength * 0.01f);
            }
            if (item.DamageType == DamageClass.Ranged)
            {
                velocity *= (1f + agility * 0.01f);
            }
            if (item.DamageType == DamageClass.Magic || item.DamageType == DamageClass.MagicSummonHybrid)
            {
                velocity *= (1f + intellect * 0.01f);
            }
        }

        public override void ModifyManaCost(Item item, ref float reduce, ref float mult)
        {
            base.ModifyManaCost(item, ref reduce, ref mult);
            mult -= intellect * 0.01f;
        }

        public override void SaveData(TagCompound tag)
        {
            tag.Add("WoWLevel", playerLevel);
            tag.Add("WoWLevelPoints", playerLevelPoints);
        }

        public override void LoadData(TagCompound tag)
        {
            playerLevel = tag.GetInt("WoWLevel");
            playerLevelPoints = tag.GetInt("WoWLevelPoints");
        }

        public void StartQuest(Quest quest)
        {
            if (!activeQuests.Contains(quest))
            {
                quest.AddPlayer(this);
                
                activeQuests.Add(quest);
            }
        }

        public bool TryFinishQuest(Quest quest)
        {
            if (activeQuests.Contains(quest))
            {
                if (quest.Finish(this))
                {
                    quest.RemovePlayer(this);
                
                    activeQuests.Remove(quest);

                    return true;
                }
            }

            return false;
        }

        public TDataType GetTaskProgress<TDataType>(string questId, string taskId)
        {
            if (questProgressDict.TryGetValue(questId, out var taskProgressDict))
            {
                if (taskProgressDict.TryGetValue(taskId, out var progress))
                {
                    if (progress is TDataType valid)
                    {
                        return valid;
                    }
                }
            }

            return default;
        }
        
        public void SetTaskProgress<TDataType>(string questId, string taskId, TDataType data)
        {
            if (!questProgressDict.ContainsKey(questId))
            {
                questProgressDict.Add(questId, new Dictionary<string, object>());
            }

            var taskProgressDict = questProgressDict[questId];
            taskProgressDict.Add(taskId, data);
        }

        public void ClearQuestProgress(string questId)
        {
            questProgressDict.Remove(questId);
        }
    }
}