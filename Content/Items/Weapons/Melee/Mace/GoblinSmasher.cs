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
    public class GoblinSmasher : BaseMace
    {
        public override int MaceUseTime => 45;
        public override int MinimalLevel => 16;
        public override void SafeSetDefaults()
        {
            Item.width = Item.height = 50;
            Item.value = 4000;
            Item.rare = ItemRarityID.Green;
            Item.scale = 1.1f;

            Item.damage = 30;
            Item.knockBack = 1f;

            Item.shoot = ModContent.ProjectileType<GoblinSmasherProj>();
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
            if (Main.invasionProgressNearInvasion)
            {
                damage *= 3;
                damage /= 2;
            }
        }
    }
    public class GoblinSmasherProj : BaseMaceProj<GoblinSmasher>
    {
        public override void OnHitGround(Player player, Vector2 hitCenter, ref int damage, ref float kb)
        {
        }
    }
}
