using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

namespace WotTK.Content.Items.Spells
{
    public class Stealth : LevelLockedItem
    {
        public override int MinimalLevel => 5;

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Pink;
            Item.width = 64;
            Item.height = 64;
            Item.useTime = Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Shoot;

            Item.UseSound = new SoundStyle("WotTK/Sounds/Custom/Stealth");

            Item.expert = true;
            Item.autoReuse = false;
            Item.shootSpeed = 0f;
            Item.noUseGraphic = true;
        }

        public override bool CanUseItem(Player player) => !player.HasBuff(ModContent.BuffType<StealthBuffCooldown>()) && player.velocity.Y == 0;

        public override bool? UseItem(Player player)
        
        {
            player.AddBuff(ModContent.BuffType<StealthBuff>(), 600);
            player.AddBuff(ModContent.BuffType<StealthBuffCooldown>(), 120);

            CombatText.NewText(player.getRect(), Color.Gray, "Stealth", true, false);

            return true;
        }
    }

    public class StealthBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            {
            player.immune = true; // Make player immune to enemy attacks
            player.immuneTime = 2; // Immunity frames
            player.immuneAlpha = 100; // Make the player semi-transparent

            player.moveSpeed -= 0.25f;

            }
        }
    }

    public class StealthBuffCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // No additional effect needed, just the presence of this buff is enough
        }
    }
}