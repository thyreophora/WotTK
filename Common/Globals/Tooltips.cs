using System.Linq;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI.Chat;

public class Tooltips : GlobalItem
{
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {

        TooltipLine materialLine = tooltips.FirstOrDefault(tip => tip.Name == "Material" && tip.Mod == "Terraria");

        if (materialLine != null)
        {

            materialLine.Text = "Crafting Reagent";
            materialLine.OverrideColor = Color.Cyan;
        }
    }
}
