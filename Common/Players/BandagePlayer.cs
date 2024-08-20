using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using WotTK.Content.Items.Consumables.Abstract;
using static WotTK.Common.Players.BandagePlayer;

namespace WotTK.Common.Players
{
    public class BandagePlayer : ModPlayer
    {
        public static int heal_interval = 6;
        public struct BandageData
        {
            public int HealAmount;
            public int HealsLeft;
        }
        public BandageData bandageData;

        //is called when player loaded
        public override void Initialize()
        {
            bandageData.HealsLeft = 0;
            bandageData.HealAmount = 0;
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            if (0 < bandageData.HealsLeft)
            {
                Initialize();
                Player.channel = false;
            }
        }

        // is called after buffs update
        public override void PostUpdateBuffs()
        {
            if(0 != bandageData.HealsLeft && !Player.HasBuff<BandageBuff>())
            {
                int current_heal = bandageData.HealAmount / bandageData.HealsLeft;
                --bandageData.HealsLeft;
                bandageData.HealAmount -= current_heal;
                Player.Heal(current_heal);

                Player.AddBuff(ModContent.BuffType<BandageBuff>(), heal_interval);
                if (0 == bandageData.HealsLeft)
                {
                    Player.channel = false;
                }
            }
        }
    }
}
