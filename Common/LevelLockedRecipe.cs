using System;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.Localization;
using WotTK.Utilities;

namespace WotTK.Common
{
    public static class LevelLockedRecipe
    {
        public static LocalizedText ConstructRecipeCondition(int minimalLevel, out Func<bool> condition)
        {
            condition = (() => Main.LocalPlayer.WotTKPlayer().playerLevel >= minimalLevel);
            DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(48, 1);
            interpolatedStringHandler.AppendLiteral("Mods.WotTK.Misc.LevelRecipeCondition");
            return Language.GetOrRegister(interpolatedStringHandler.ToStringAndClear(), (Func<string>)null).WithFormatArgs(minimalLevel);
        }
    }
}
