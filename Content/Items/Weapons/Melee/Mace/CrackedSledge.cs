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
    public class CrackedSledge : BaseMace
    {
        public override int MaceUseTime => 65;
        public override void SafeSetDefaults()
        {
            Item.width = Item.height = 50;
            Item.value = 4000;
            Item.rare = 1;
            Item.scale = 1f;

            Item.damage = 20;
            Item.knockBack = 2f;

            Item.shoot = ModContent.ProjectileType<CrackedSledgeProj>();
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.StoneBlock, 25)
                .AddIngredient(ItemID.Wood, 15)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
    public class CrackedSledgeProj : BaseMaceProj<CrackedSledge>
    {
        public int TargetIndex = -1;
        public override void OnHitGround(Player player, Vector2 hitCenter, ref int damage, ref float kb)
        {
            //NPC npc = Projectile.Center.ClosestNPCAt(1600f, ignoreTiles: false);
            Vector2 vel = -Vector2.UnitY;
            /*if (npc != null)
            {
                TargetIndex = npc.whoAmI;
                vel = Projectile.SafeDirectionTo(Main.npc[TargetIndex].Center);
            }*/
            for (int i = 0; i < 2; i++)
            {
                int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), hitCenter, vel * 7 + Main.rand.NextVector2Square(-0.5f, 0.5f), ProjectileID.DeerclopsRangedProjectile, damage / 4, kb, player.whoAmI, ai1: Main.rand.Next(0, 6));
                Main.projectile[index].extraUpdates = 1;
                Main.projectile[index].penetrate = 4;
                Main.projectile[index].hostile = false;
                Main.projectile[index].friendly = true;
                Main.projectile[index].usesLocalNPCImmunity = true;
                Main.projectile[index].localNPCHitCooldown = 30;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            //Vector2 vel = (target.Center - HammerCenter).SafeNormalize(Vector2.Zero) * 3f;
            int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), HammerCenter, -Vector2.UnitY * 7 + Main.rand.NextVector2Square(-0.5f, 0.5f), ProjectileID.DeerclopsRangedProjectile, Projectile.damage / 2, Projectile.knockBack, Owner.whoAmI, ai1: Main.rand.Next(0, 6));
            Main.projectile[index].extraUpdates = 1;
            Main.projectile[index].penetrate = 4;
            Main.projectile[index].hostile = false;
            Main.projectile[index].friendly = true;
            Main.projectile[index].usesLocalNPCImmunity = true;
            Main.projectile[index].localNPCHitCooldown = 30;
        }
    }
}
