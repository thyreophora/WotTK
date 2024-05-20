using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using WotTK.Common.Players;
using WotTK.Content.Items.Placeble;
using WotTK.Content.Items.Materials;
using System;
using WotTK.Common;

namespace WotTK.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.Neck)]
    public class RogueScarf : LevelLockedItem
    {
        public override int MinimalLevel => 3;
        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 26;
            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<WoolCloth>(), 3)
                .AddIngredient(ModContent.ItemType<MediumLeather>(), 8)
                .AddTile<LeatherworkingTile>()
                .AddCondition(LevelLockedRecipe.ConstructRecipeCondition(MinimalLevel, out Func<bool> condition), condition)
                .Register();
        }

    }
}