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

        int sellPrice = item.value / 5;

        if (sellPrice > 0)
        {

            string priceText = "Sell Price: " + GetPriceString(sellPrice);


            TooltipLine priceLine = new TooltipLine(Mod, "SellPrice", priceText)
            {
                OverrideColor = Color.Yellow
            };
            tooltips.Add(priceLine);
        }
    }

    private string GetPriceString(int value)
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
            priceString += $"[i:{ItemID.PlatinumCoin}]{coins[0]} ";
        if (coins[1] > 0)
            priceString += $"[i:{ItemID.GoldCoin}]{coins[1]} ";
        if (coins[2] > 0)
            priceString += $"[i:{ItemID.SilverCoin}]{coins[2]} ";
        if (coins[3] > 0)
            priceString += $"[i:{ItemID.CopperCoin}]{coins[3]} ";

        return priceString.Trim();
    }
}
