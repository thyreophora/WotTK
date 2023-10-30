using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace WotTK.Content.UI
{
    [Autoload(Side = ModSide.Client)]
    public class LevelBarSystem : ModSystem
    {
        private UserInterface levelBarUserInterface;
        internal LevelBar levelBar;
        public static LocalizedText levelBarText { get; private set; }
        public override void Load()
        {
            levelBar = new LevelBar();
            levelBarUserInterface = new UserInterface();
            levelBarUserInterface.SetState(levelBar);

            levelBarText ??= Language.GetOrRegister($"Mods.WotTK.UI.LevelBar");
        }
        public override void UpdateUI(GameTime gameTime)
        {
            levelBarUserInterface?.Update(gameTime);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int levelBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (levelBarIndex != -1)
            {
                layers.Insert(levelBarIndex, new LegacyGameInterfaceLayer(
                    "WotTK: Level Bar",
                    delegate {
                        levelBarUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
