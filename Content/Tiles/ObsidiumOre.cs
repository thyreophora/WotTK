using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace WotTK.Content.Tiles
{
    public class ObsidiumOre : ModTile
    {
        public override void SetStaticDefaults()
        {

            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.Origin = new Terraria.DataStructures.Point16(1, 1);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16 };
            TileObjectData.newTile.StyleHorizontal = false;
            TileObjectData.newTile.RandomStyleRange = 4;
            TileObjectData.addTile(Type);

            DustType = DustID.PurpleMoss;

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(93, 63, 211), name);
        }

        public override void NumDust(int x, int y, bool fail, ref int num)
        {
            num = fail ? 1 : 4;
        }
    }
}
