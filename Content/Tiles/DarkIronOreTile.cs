using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using WotTK.Content.Items.Placeble;
using Terraria.Audio;

namespace WotTK.Content.Tiles
{
    public class DarkIronOreTile : ModTile
    {
        public byte[,] tileAdjacency;
        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileOreFinderPriority[Type] = 690;
            TileID.Sets.Ore[Type] = true;
            Main.tileShine[Type] = 600;
            Main.tileShine2[Type] = true;

            AddMapEntry(new Color(48, 48, 48), CreateMapEntryName());
            MineResist = 4f;
            MinPick = 100;
            
            HitSound = new SoundStyle("WotTK/Sounds/Custom/WandBaseSound");

            Main.tileSpelunker[Type] = true;

        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 224f / 900f;
            g = 219f / 900f;
            b = 124f / 900f;
        }
    }
}
