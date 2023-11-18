using System.ComponentModel;
using System.Numerics;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader.Config;

namespace WotTK
{
    public class WotTKConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        public static WotTKConfig Instance;
        public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref NetworkText message)
        {
            return true;
        }
        [DefaultValue(0.5f)]
        public float LevelBarX { get; set; }

        [DefaultValue(0.4f)]
        public float LevelBarY { get; set; }

        /*[Range(0f, 1f)]
        [Increment(.1f)]
        [DrawTicks]
        public Vector2 Pos
        {
            get => new Vector2(LevelBarX, LevelBarY);
        }*/

        [DefaultValue(true)]
        public bool ChangeVanillaWeaponsToMace;

        [DefaultValue(true)]
        public bool Debug;

        //override Nee
    }
}
