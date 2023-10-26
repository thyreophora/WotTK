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
    public class TestMace : BaseMace
    {
        public override int MaceUseTime => 40;
        public override void SafeSetDefaults()
        {
            Item.width = Item.height = 40;
            Item.value = 4000;
            Item.rare = 0;

            /*Item.useTime = Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.noUseGraphic = true;
            Item.noMelee = true;*/

            Item.damage = 10;
            Item.knockBack = 1f;

            Item.shoot = ModContent.ProjectileType<TestMaceProj>();
        }
    }
    public class TestMaceProj : BaseMaceProj<TestMace>
    {
        public override void HitOnGround(Player player, Vector2 hitCenter)
        {
            Main.NewText("Bonk!");
        }
    }
    public class TestMace2 : BaseMace
    {
        public override int MaceUseTime => 100;
        public override void SafeSetDefaults()
        {
            Item.width = Item.height = 40;
            Item.value = 4000;
            Item.rare = 0;

            /*Item.useTime = Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.noUseGraphic = true;
            Item.noMelee = true;*/

            Item.damage = 10;
            Item.knockBack = 1f;

            Item.scale = 2f;

            Item.shoot = ModContent.ProjectileType<TestMaceProj2>();
        }
    }
    public class TestMaceProj2 : BaseMaceProj<TestMace2> {
        public override void HitOnGround(Player player, Vector2 hitCenter)
        {
            Main.NewText("BONK!!!");
            for (int i = 0; i < 12; i++)
                Gore.NewGore(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.UnitX.RotatedBy(MathHelper.TwoPi / 12 * i) * 2f, Main.rand.Next(61, 64));
        }
    }
    /*public class TestMace2 : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = Item.height = 40;
            Item.value = 4000;
            Item.rare = 0;

            Item.useTime = Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Swing;

            Item.damage = 10;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 1f;
        }
    }*/
}
