using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace WotTK
{
    public class WotTKPlayer : ModPlayer
    {
        public bool hasPaladinTeamBuff;
        public int paladinsTeam;
        public bool maceHitOnGround = false;

        public int playerLevel = 1;
        public int playerLevelPoints = 0;
        public override void ResetEffects()
        {
            paladinsTeam = -1;
            hasPaladinTeamBuff = false;
        }
        public override void PostUpdateEquips()
        {
            if (playerLevelPoints >= 100 * MathF.Sqrt(playerLevel))
            {
                playerLevelPoints -= (int)(100 * MathF.Sqrt(playerLevel));
                playerLevel++;
                Main.NewText(LangHelper.GetText("Events.LevelUp", playerLevel));
                Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, -Vector2.UnitY, Main.rand.Next(167, 171), 0, 0, Player.whoAmI);
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
        }
        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.life >= hit.Damage)
            {
                playerLevelPoints += (int)target.value;
            }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {

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
