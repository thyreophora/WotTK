using System;
using System.Collections.Generic;
using WotTK.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WotTK.Content.Items.Test
{
    public class DisplayAttributes : ModItem
    {
        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults()
        {
            Item.width = Item.height = 32;
            Item.value = 0;
            Item.rare = -1;
            Item.consumable = false;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var player = Main.LocalPlayer.GetModPlayer<WotTKPlayer>();

            tooltips.Add(new TooltipLine(Mod, "Armor", $"Armor: {player.armor}"));
            tooltips.Add(new TooltipLine(Mod, "Stamina", $"Stamina: {player.stamina}"));
            tooltips.Add(new TooltipLine(Mod, "Strength", $"Strength: {player.strength}"));
            tooltips.Add(new TooltipLine(Mod, "Haste", $"Haste: {player.haste}"));

            tooltips.Add(new TooltipLine(Mod, "Separator", "~~~~~~~~~~~~")); // dividing ugly shit

            tooltips.Add(new TooltipLine(Mod, "Agility", $"Agility: {player.agility}"));
            tooltips.Add(new TooltipLine(Mod, "Intellect", $"Intellect: {player.intellect}"));
            tooltips.Add(new TooltipLine(Mod, "Spirit", $"Spirit: {player.spirit}"));

        }
    }
}
