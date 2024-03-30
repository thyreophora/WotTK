﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WotTK.Content.Items.Materials;
using WotTK.Common;

namespace WotTK.Content.Items.Placeble
{
    public class DarkIronOre : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
        {
            Item.createTile = ModContent.TileType<Content.Tiles.DarkIronOreTile>();
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
}
