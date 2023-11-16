using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using WotTK.Common.Globals;

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

        public bool _spawnTentacleSpikesClone;
        public bool _spawnTentacleSpikesClone2;
        public static Point[] _tentacleSpikesMax5 = new Point[5];

        /*public static readonly List<Func<int>> stages = new()
        {
            () => { return }
        };*/
        public override void ResetEffects()
        {
            paladinsTeam = -1;
            hasPaladinTeamBuff = false;

            agility = 0;
            intellect = 0;
            strength = 0;
        }
        /*public override void UpdateEquips()
        {
            if (Player.HeldItem.type == 5094 && Player.itemAnimation == 2 && !_spawnTentacleSpikesClone)
            {
                Main.NewText("Trigger");
                _spawnTentacleSpikesClone = true;
            }
        }*/
        public static readonly SoundStyle LevelUpSound = new SoundStyle("WotTK/Sounds/Custom/LevelUp");
        public override void PostUpdateEquips()
        {
            Item item = Player.HeldItem;
            if (playerLevel <= 0)
                playerLevel = 1;
            if (playerLevelPoints >= playerLevelPointsNeed)
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
                        //Effect for your team
                    }
                }
            }

            /*if (item.DamageType == DamageClass.Melee || item.DamageType == DamageClass.MeleeNoSpeed || item.DamageType == DamageClass.Ranged)
            {
                velocity *= 1f + agility * 0.01f;
            }*/

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
            if (target.life <= hit.Damage && target.GetGlobalNPC<WotTKGlobalNPC>().canGetExp)
            {
                int vvalue = (int)target.value / 2;
                if (target.boss)
                    vvalue /= 2;
                playerLevelPoints += vvalue;
                target.GetGlobalNPC<WotTKGlobalNPC>().canGetExp = false;
            }
        }
        public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            //Main.ViewSize
            if (item.DamageType == DamageClass.Melee || item.DamageType == DamageClass.MeleeNoSpeed) 
            {
                velocity *= (1f + agility * 0.01f + strength * 0.01f); 
                damage = (int)(damage * (1f + strength * 0.01f));
            }
            if (item.DamageType == DamageClass.Ranged)
            {
                velocity *= (1f + agility * 0.01f);
                damage = (int)(damage * (1f + agility * 0.01f));
            }
            if (item.DamageType == DamageClass.Magic || item.DamageType == DamageClass.MagicSummonHybrid)
            {
                velocity *= (1f + intellect * 0.01f);
                damage = (int)(damage * (1f + intellect * 0.01f));
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
    }
}
