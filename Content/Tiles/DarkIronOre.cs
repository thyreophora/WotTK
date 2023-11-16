using Microsoft.Xna.Framework;
using WotTK.Content.Materials;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace WotTK.Content.Tiles
{
    public class DarkIronOre : ModTile
    {
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.Origin = new Terraria.DataStructures.Point16(1, 1);
            TileObjectData.newTile.StyleHorizontal = false;
			TileObjectData.newTile.RandomStyleRange = 4;
			TileObjectData.addTile(Type);

			DustType = DustID.Stone;

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(144, 144, 144), name);
        }

        public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY) => offsetY = 2;

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            int itemID = ModContent.ItemType<DarkIronNugget>();
            int frame = frameY / 36;

            Item.NewItem(new Terraria.DataStructures.EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 16, itemID, Main.rand.Next(22, 31));
        }
    }
}
