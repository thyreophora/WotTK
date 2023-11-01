using Microsoft.Xna.Framework;
using Mono.Cecil;
using System.Collections.ObjectModel;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WotTK.Content.Items.Weapons.Melee.Mace;

namespace WotTK
{
    public class WotTKGlobalItem : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (WotTKConfig.Instance.ChangeVanillaWeaponsToMace)
            {
                switch (item.type)
                {
                    case ItemID.ZombieArm:
                        SetMaceDefaults(item, ModContent.ProjectileType<ZombieArm>());
                        break;
                    case ItemID.PurpleClubberfish:
                        SetMaceDefaults(item, ModContent.ProjectileType<PurpleClubberfish>());
                        break;
                    case ItemID.SlapHand:
                        SetMaceDefaults(item, ModContent.ProjectileType<SlapHand>());
                        break;
                    case ItemID.WaffleIron:
                        SetMaceDefaults(item, ModContent.ProjectileType<WaffleIron>());
                        break;
                    case ItemID.HamBat:
                        SetMaceDefaults(item, ModContent.ProjectileType<HamBat>());
                        break;
                    case ItemID.TentacleSpike:
                        SetMaceDefaults(item, ModContent.ProjectileType<TentacleSpike>());
                        break;
                }
            }
        }
        private void SetMaceDefaults(Item item, int proj, int mult = 2)
        {
            item.shoot = proj;
            item.shootSpeed = 0;
            item.DamageType = PaladinDamageType.Instance;
            item.useTime *= mult;
            item.useAnimation *= mult;
            item.damage *= mult;
            item.useStyle = ItemUseStyleID.Shoot;
            item.noUseGraphic = true;
            item.noMelee = true;
        }
        /*public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (item.type == ItemID.WaffleIron && WotTKConfig.Instance.ChangeVanillaWeaponsToMace)
            {
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            }
            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }*/
        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            /*if (item.type == ItemID.WaffleIron && WotTKConfig.Instance.ChangeVanillaWeaponsToMace)
            {
                Vector2 vel = (Main.MouseWorld - position).SafeNormalize(Vector2.Zero) * 11f;
                Projectile.NewProjectile(player.GetSource_ItemUse(item), position, vel, 1020, damage, knockback, player.whoAmI);
            }*/
        }
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            /*if (item.type == ItemID.WaffleIron && WotTKConfig.Instance.ChangeVanillaWeaponsToMace)
            {
                Vector2 vel = (Main.MouseWorld - position).SafeNormalize(Vector2.Zero) * 11f;
                Projectile.NewProjectile(player.GetSource_ItemUse(item), position, vel, type, damage, knockback, player.whoAmI);
                Projectile.NewProjectile(player.GetSource_ItemUse(item), position, vel, type, damage, knockback, player.whoAmI);
                return false;
            }*/
            return true;
        }
        /*public override bool PreDrawTooltip(Item item, ReadOnlyCollection<TooltipLine> lines, ref int x, ref int y)
        {
            foreach (TooltipLine line in lines) 
            { 
                if (line.Mod == "Terraria" && line.Name == "ItemName")
                {
                    //Main.Text
                    //Main.spriteBatch.DrawString(Main.)
                    return false;
                }
            }
            return true;
        }*/
    }
}
