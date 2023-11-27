using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WotTK.Content.Items.Materials
{
    public class ThickLeather : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25;
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 36;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 3, 0);
            Item.rare = 2;
        }
    }
}