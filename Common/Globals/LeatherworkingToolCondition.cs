using System;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace WotTK.Common
{
    public static class LeatherworkingToolCondition
    {
        public static LocalizedText ConstructLeatherworkingToolCondition(int requiredTool, out Func<bool> condition)
        {
            condition = () => Main.LocalPlayer.HasItem(requiredTool);

            DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(48, 1);
            interpolatedStringHandler.AppendLiteral("Mods.WotTK.Misc.LeatherworkingToolCondition");

            return Language.GetOrRegister(interpolatedStringHandler.ToStringAndClear(), (Func<string>)null);
        }

        public static bool HasLeatherworkingTool()
        {
            int requiredTool = ModContent.ItemType<Content.Items.Tools.LeatherworkingTool>();
            return Main.LocalPlayer.HasItem(requiredTool);
        }
    }
}