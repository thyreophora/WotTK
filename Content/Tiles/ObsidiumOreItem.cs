using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace WotTK.Content.Tiles
{
    public class ObsidiumOreItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<ObsidiumOre>());
            Item.width = Item.height = 52;
            Item.value = 0;
            Item.rare = ItemRarityID.Green;

        }
    }
}
