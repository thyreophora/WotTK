﻿using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WotTK.Content.Items.Materials;
using WotTK.Common;

namespace WotTK.Content.Items.Placeables
{
    public class DarkIronOre : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
        {
            Item.createTile = ModContent.TileType<DarkIronOreTile>();
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.width = 13;
            Item.height = 10;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(silver: 5);
            Item.rare = ItemRarityID.Pink;
        }
    }

    public class DarkIronOreTile : ModTile
    {
        public static readonly SoundStyle MineSound = new("WotTK/Sounds/Custom/DarkIronMine", 3);
        public byte[,] tileAdjacency;
        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileOreFinderPriority[Type] = 690;
            TileID.Sets.Ore[Type] = true;
            Main.tileShine[Type] = 2500;
            Main.tileShine2[Type] = true;

            AddMapEntry(new Color(48, 48, 48), CreateMapEntryName());
            MineResist = 4f;
            MinPick = 100;
            
            HitSound = MineSound;

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
            r = 224f / 2500f;
            g = 219f / 2500f;
            b = 124f / 2500f;
        }
    }
}
