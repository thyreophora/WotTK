using System.Linq;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;

public class Prices : GlobalItem
{
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        int stackSize = item.stack;
        int sellPricePerUnit = item.value / 5;
        int totalSellPrice = sellPricePerUnit * stackSize;

        if (totalSellPrice > 0)
        {
            string priceText = "Sell Price: " + GetPriceString(totalSellPrice, out Color color);

            TooltipLine priceLine = new TooltipLine(Mod, "SellPrice", priceText)
            {
                OverrideColor = color
            };
            tooltips.Add(priceLine);
        }
    }

    private string GetPriceString(int value, out Color color)
    {
        int[] coins = new int[4]; // Platinum, Gold, Silver, Copper
        coins[3] = value % 100;
        value /= 100;
        coins[2] = value % 100;
        value /= 100;
        coins[1] = value % 100;
        value /= 100;
        coins[0] = value;

        string priceString = "";

        if (coins[0] > 0)
        {
            priceString += $"[i:{ItemID.PlatinumCoin}]{coins[0]} ";
            color = new Color(220, 220, 198); // platinum color in text
        }
        else if (coins[1] > 0)
        {
            priceString += $"[i:{ItemID.GoldCoin}]{coins[1]} ";
            color = new Color(255, 215, 0); // gold color in text
        }
        else if (coins[2] > 0)
        {
            priceString += $"[i:{ItemID.SilverCoin}]{coins[2]} ";
            color = new Color(192, 192, 192); // silver color in text
        }
        else
        {
            priceString += $"[i:{ItemID.CopperCoin}]{coins[3]} ";
            color = new Color(184, 115, 51); // copper color in text
        }

        return priceString.Trim();
    }
}
