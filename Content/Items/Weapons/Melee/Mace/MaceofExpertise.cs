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
        public override int MinimalLevel => 3;
        public override void SafeSetDefaults()
        {
            Item.width = Item.height = 50;
            Item.value = 4000;
            Item.rare = ItemRarityID.Green;
            Item.scale = 1.5f;

            Item.damage = 25;
            Item.knockBack = 1f;

            Item.shoot = ModContent.ProjectileType<MaceofExpertiseProj>();
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
    public class MaceofExpertiseProj : BaseMaceProj<MaceofExpertise>
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
