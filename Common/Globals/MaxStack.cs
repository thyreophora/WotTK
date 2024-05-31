using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;

public class MaxStack : GlobalItem
{
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        base.ModifyTooltips(item, tooltips);

        if (item.maxStack > 1)
        {
            string maxStackText = $"Max Stack: {item.maxStack}";
            TooltipLine maxStackLine = new TooltipLine(Mod, "MaxStack", maxStackText)
            {
                OverrideColor = Microsoft.Xna.Framework.Color.Yellow
            };

            tooltips.Add(maxStackLine);
        }
    }
}
