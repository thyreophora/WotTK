using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

namespace WotTK.Content.Items.Spells
{
    public class BattleShout : LevelLockedItem
    {
        public override int MinimalLevel => 5;

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Pink;
            Item.width = 64;
            Item.height = 64;
            Item.useTime = Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Shoot;

            Item.UseSound = new SoundStyle("WotTK/Sounds/Custom/BattleShoutActivated");

            Item.expert = true;
            Item.autoReuse = false;
            Item.shootSpeed = 0f;
            Item.noUseGraphic = true;
        }

        public override bool CanUseItem(Player player) => !player.HasBuff(ModContent.BuffType<BattleShoutCooldown>()) && player.velocity.Y == 0;

        public override bool? UseItem(Player player)
        {
            player.AddBuff(ModContent.BuffType<BattleBuff>(), 7200); // 2 minutes in ticks
            player.AddBuff(ModContent.BuffType<BattleShoutCooldown>(), 900); // 2 minutes in ticks


            CombatText.NewText(player.getRect(), Color.Red, "Battle Shout Activated!", true, false);

            return true;
        }
    }

    public class BattleBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {

            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; // Defines if the buff is a debuff
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 0.05f; // Increase all damage types by 5%
        }
    }

    public class BattleShoutCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {

            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; // Defines if the buff is a debuff
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // No additional effect needed, just the presence of this buff is enough
        }
    }
}