using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WotTK.Common;

namespace WotTK.Content.Items.Weapons.Melee.Mace
{
    public class TheUnstoppableForce : BaseMace
    {
        public override int MaceUseTime => 40;
        public override int MinimalLevel => 35;
        public override bool IsWeapon => true;
        public override void SafeSetDefaults()
        {
            Item.width = Item.height = 72;
            Item.value = 4000;
            Item.autoReuse = true;
            Item.rare = ItemRarityID.Green;
            Item.scale = 1.5f;

            Item.damage = 45;
            Item.knockBack = 1f;

            Item.shoot = ModContent.ProjectileType<TheUnstoppableForceProj>();
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.GoldBar, 12)
                .AddIngredient(ItemID.Ruby)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
    public class TheUnstoppableForceProj : BaseMaceProj<TheUnstoppableForce>
    {
        public override void OnHitGround(Player player, Vector2 hitCenter, ref int damage, ref float kb)
        {

        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            Owner.HealEffect(5);
        }
    }
}
