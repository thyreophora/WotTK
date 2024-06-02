using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using WotTK.Common.Players;

namespace WotTK.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class NightstalkerHoodie : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 22;
            Item.rare = ItemRarityID.Green;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<WotTKPlayer>().armor += 5; // Custom armor attribute
        }
    }

    [AutoloadEquip(EquipType.Body)]
    public class NightstalkerCloak : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            //Item.value = 15000;
            Item.rare = ItemRarityID.Green;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<WotTKPlayer>().armor += 10; // Custom armor attribute
        }
    }

    [AutoloadEquip(EquipType.Legs)]
    public class NightstalkerPants : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 14;
            //Item.value = 12000;
            Item.rare = ItemRarityID.Green;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<WotTKPlayer>().armor += 8; // Custom armor attribute
        }
    }
}
