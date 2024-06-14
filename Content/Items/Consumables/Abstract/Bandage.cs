//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using WotTK.Common.Players;

namespace WotTK.Content.Items.Consumables.Abstract
{
    public abstract class Bandage : ModItem
    {
        public int healing_time;
        public int heal_amount;
        public override void SetDefaults()
        {
            DefaultBandage();
        }
        public void DefaultBandage(int time = 60, int amount = 100)
        {
            Item.consumable = true;
            Item.maxStack = 69;
            Item.width = 24;
            Item.height = 24;
            Item.rare = ItemRarityID.Purple;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;

            healing_time = time;
            heal_amount = amount;
        }

        public override bool CanUseItem(Player player)
        {
            // TODO: replace with buff in the UseItem function
            return true;//player.HasBuff(BuffID.PotionSickness);
        }
        public override bool? UseItem(Player player)
        {
            player.AddBuff(BuffID.PotionSickness, 60 * 60);
            player.AddBuff(ModContent.BuffType<BandageBuff>(), 60);

            player.GetModPlayer<BandagePlayer>().bandageData.HealAmount = heal_amount;
            player.GetModPlayer<BandagePlayer>().bandageData.HealsLeft = healing_time;
            return base.UseItem(player);
        }
    }

    public class BandageBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
        }
    }
}
