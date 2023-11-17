using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using WotTK.Utilities;

namespace WotTK.Content.Items.Weapons.Magic.Staffs
{
	public class CharredRazormaneWand : LevelLockedItem
    {
        public override int MinimalLevel => 0;
        public override void SetStaticDefaults()
        {
            Item.staff[Type] = true;
        }
        public override void SetDefaults()
		{
            Item.width = 26;
            Item.height = 28;
            Item.rare = ItemRarityID.Green;

            Item.useTime = Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;

            Item.mana = 6;
            Item.damage = 7;
            Item.knockBack = 1f;
            Item.DamageType = DamageClass.Magic;

            Item.shoot = ModContent.ProjectileType<CharredProj>();
			Item.shootSpeed = 10f;

        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
            SoundEngine.PlaySound(new SoundStyle("WotTK/Sounds/Custom/WandFireCast2") with { PitchVariance = 0.2f, Volume = 0.4f }, player.Center);

            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * Item.Size.Length();
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
				position += muzzleOffset;
		}

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Wood, 15)
                .AddIngredient(ItemID.Acorn, 3)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}