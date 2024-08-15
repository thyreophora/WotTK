using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.UI.States;
using Terraria.ID;
using Terraria.ModLoader;


namespace WotTK.Content.Items.Weapons.Bows
{
    public class ProjBowItem : LevelLockedItem
    {
        public override int MinimalLevel => 15;
        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.knockBack = 1f;
            Item.shootSpeed = 16f;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.reuseDelay = 20;
            Item.useAnimation = 15;
            Item.useTime = 13;
            Item.width = 54;
            Item.height = 14;
            Item.shoot = ModContent.ProjectileType<ProjBowProj>();
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 15);
            Item.rare = ItemRarityID.Orange;
            Item.DamageType = DamageClass.Ranged;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            return false;
        }
    }
}
