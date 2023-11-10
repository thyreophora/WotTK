using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WotTK.Content.Materials
{
    public class DarkIronNugget : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 5;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 26;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = -11;
        }
    }
}