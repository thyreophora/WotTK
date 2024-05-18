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
        public string BulkTexture => "WotTK/Content/Items/Accessories/RogueScarf_Bulk";

        public override int MinimalLevel => 3;
        public override void SetStaticDefaults()
        {

            if (Main.netMode == NetmodeID.Server)
                return;

            int equipSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Neck);

            ArmorIDs.Body.Sets.HidesTopSkin[equipSlot] = true;
            ArmorIDs.Body.Sets.HidesArms[equipSlot] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 26;
            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
            Item.vanity = true;
        }


        public override void EquipFrameEffects(Player player, EquipType type)
        {
            player.back = (sbyte)EquipLoader.GetEquipSlot(Mod, Name, EquipType.Back);
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

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.dashType = 0;
        }
    }
}
