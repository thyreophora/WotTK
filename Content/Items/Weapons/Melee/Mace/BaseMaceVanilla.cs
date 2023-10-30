using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace WotTK.Content.Items.Weapons.Melee.Mace
{
    /*public class NoUsedItem : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }
    }*/
    public abstract class BaseMaceVanilla : BaseMaceProj<ModItem>
    {
        public virtual int BaseID => 1;
        public override string Texture => "Terraria/Images/Item_" + BaseID;
    }

    public class ZombieArm : BaseMaceVanilla
    {
        public override int BaseID => ItemID.ZombieArm;
    }

    public class PurpleClubberfish : BaseMaceVanilla
    {
        public override int BaseID => ItemID.PurpleClubberfish;
    }

    public class SlapHand : BaseMaceVanilla
    {
        public override int BaseID => ItemID.SlapHand;
    }

    public class WaffleIron : BaseMaceVanilla
    {
        public override int BaseID => ItemID.WaffleIron;
        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);
            Player player = Main.player[Projectile.owner];
            Vector2 vel = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.Zero) * 11f;
            int proj = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center, vel, 1012, Projectile.damage, Projectile.knockBack, player.whoAmI);
            Main.projectile[proj].timeLeft = 600;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone); 
            ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.WaffleIron, new ParticleOrchestraSettings()
            {
                PositionInWorld = target.Center
            }, new int?(Main.player[Projectile.owner].whoAmI));
        }
    }

    public class HamBat : BaseMaceVanilla
    {
        public override int BaseID => ItemID.HamBat;
    }

    public class TentacleSpike : BaseMaceVanilla
    {
        public override int BaseID => ItemID.TentacleSpike;
        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);
            Main.player[Projectile.owner].GetModPlayer<WotTKPlayer>()._spawnTentacleSpikesClone = true;

        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            Player player = Main.player[Projectile.owner];
            if (!player.GetModPlayer<WotTKPlayer>()._spawnTentacleSpikesClone || Main.myPlayer != player.whoAmI || target != null && !target.CanBeChasedBy((object)player))
                return;
            Vector2 vector2_1 = (target.Center - player.MountedCenter).SafeNormalize(Vector2.Zero);
            Vector2 vector2_2 = target.Hitbox.ClosestPointInRect(player.MountedCenter) + vector2_1;
            Vector2 vector2_3 = (target.Center - vector2_2) * 0.8f;
            int protectedProjectileIndex = Projectile.NewProjectile(player.GetSource_OnHit(target), vector2_2.X, vector2_2.Y, vector2_3.X, vector2_3.Y, 971, (int)Projectile.damage, Projectile.knockBack, player.whoAmI, 1f, (float)target.whoAmI);
            Main.projectile[protectedProjectileIndex].StatusNPC(target.whoAmI);
            Projectile.KillOldestJavelin(protectedProjectileIndex, 971, target.whoAmI, WotTKPlayer._tentacleSpikesMax5);
            player.GetModPlayer<WotTKPlayer>()._spawnTentacleSpikesClone = false;
        }
    }
}
