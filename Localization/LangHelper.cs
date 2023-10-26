﻿using Terraria.Localization;
using Terraria.ModLoader;

namespace WotTK
{
	public static class LangHelper
	{
		/// <summary>
		/// prefixes the modname for the key
		/// </summary>
		/// <returns>Text associated with this key</returns>
		public static string GetTextByMod(Mod mod, string key, params object[] args)
		{
			return Language.GetTextValue($"Mods.{mod.Name}.{key}", args);
		}

        /// <summary>
        /// Defaults to Mods.WotTK. as the prefix for the key
        /// </summary>
        /// <returns>Text associated with this key</returns>
        internal static string GetText(string key, params object[] args)
		{
			return GetTextByMod(WotTK.Instance, key, args);
		}
    }
}
