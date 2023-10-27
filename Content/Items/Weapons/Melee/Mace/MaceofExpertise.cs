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
    public class MaceofExpertise : BaseMace
    {
        public override int MaceUseTime => 50;
        public override void SafeSetDefaults()
        {
            Item.width = Item.height = 50;
            Item.value = 4000;
            Item.rare = 0;

            /*Item.useTime = Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.noUseGraphic = true;
            Item.noMelee = true;*/

            Item.damage = 10;
            Item.knockBack = 1f;

            Item.shoot = ModContent.ProjectileType<MaceofExpertiseProj>();
        }
    }
    public class MaceofExpertiseProj : BaseMaceProj<MaceofExpertise>
    {
        public override void HitOnGround(Player player, Vector2 hitCenter, ref int damage, ref float kb)
        {
            //Main.NewText("Bonk!");
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
