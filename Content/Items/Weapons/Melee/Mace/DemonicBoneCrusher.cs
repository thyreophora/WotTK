using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WotTK.Content.Items.Weapons.Melee.Mace
{
    public class DemonicBoneCrusher : BaseMace
    {
        public override int MaceUseTime => 80;
        public override int MinimalLevel => 28;
        public override void SafeSetDefaults()
        {
            Item.width = Item.height = 60;
            Item.value = 17500;
            Item.autoReuse = true;
            Item.rare = ItemRarityID.Green;

            Item.damage = 25;
            Item.knockBack = 1f;

            Item.shoot = ModContent.ProjectileType<DemonicBoneCrusherProj>();
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DemoniteBar, 8)
                .AddIngredient(ItemID.Bone, 12)
                .AddTile(TileID.Anvils)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.CrimtaneBar, 8)
                .AddIngredient(ItemID.Bone, 12)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
    public class DemonicBoneCrusherProj : BaseMaceProj<DemonicBoneCrusher>
    {
        //public override float HeadOffset => 8f;
        public override void OnHitGround(Player player, Vector2 hitCenter, ref int damage, ref float kb)
        {
            for (int i = 0; i < 3; i++)
            {
                Projectile projectile = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), hitCenter, -Vector2.UnitY * 5 + Main.rand.NextVector2Square(-1, 1), ProjectileID.Bone, damage / 4, kb);
                projectile.DamageType = PaladinDamageType.Instance;
            }
            //Dust dust = Dust.NewDustPerfect(hitCenter, DustID.Flare, Scale: 2f);
            //dust.noGravity = true;
        }
    }
}
