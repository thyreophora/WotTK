using System.ComponentModel;
using System.Numerics;
using Terraria;
using Terraria.ModLoader.Config;

namespace WotTK
{
    public class WotTKConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        public static WotTKConfig Instance;

        [DefaultValue(1f)]
        public float LevelBarX;

        [DefaultValue(1f)]
        public float LevelBarY; 
        [Range(0f, 1f)]
        [Increment(.1f)]
        [DrawTicks]
        public Vector2 Pos
        {
            get => new Vector2(LevelBarX, LevelBarY);
        }

        [DefaultValue(true)]
        public bool ChangeVanillaWeaponsToMace;
    }
}
