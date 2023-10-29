using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace WotTK
{
    public class WotTKConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        public static WotTKConfig Instance;

        [DefaultValue(400)]
        public float LevelBarX;

        [DefaultValue(100)]
        public float LevelBarY;
    }
}
