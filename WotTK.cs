using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.UI;

namespace WotTK
{
    public class WotTK : Mod
    {
        public static WotTK Instance;

        public override void Load()
        {
            Instance = this;
        }

        public override void Unload()
        {
            Instance = null;
        }

        internal static void SaveConfig(WotTKConfig cfg)
        {
            try
            {
                MethodInfo method = typeof(ConfigManager).GetMethod("Save", (BindingFlags)40);
                if (method != null)
                    ((MethodBase)method).Invoke(null, new object[1]
                    {
                        cfg
                    });
            }
            catch
            {

            }
        }
    }
}