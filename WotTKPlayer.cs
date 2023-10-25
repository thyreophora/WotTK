using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace WotTK
{
    public class WotTKPlayer : ModPlayer
    {
        public bool hasPaladinTeamBuff;
        public int paladinsTeam;
        public bool maceHitOnGround = false;
        public override void ResetEffects()
        {
            paladinsTeam = -1;
            hasPaladinTeamBuff = false;
        }
        public override void PostUpdateEquips()
        {
            if (hasPaladinTeamBuff)
            {
                paladinsTeam = Player.team;
            }
            for (int i = 0; i < Main.player.Length; i++)
            {
                Player plr = Main.player[i];
                if (plr == null) continue;
                if (!plr.active) continue;
                if (plr.team == paladinsTeam && !Player.dead)
                {
                    //Effect for your team
                }
            }
        }
    }
}
