using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace WotTK.Common.UI;

[Autoload(Side = ModSide.Client)]
public sealed class UILevelManager : ModSystem
{
    // Keeps track of the last game time update, which isn't provided for rendering.
    private static GameTime gameTime;
    
    public static UserInterface State { get; private set; }
    
    public override void Load() {
        State = new UserInterface();
        State.SetState(new UILevelDisplay());
    }

    public override void Unload() {
        State.CurrentState?.Deactivate();
        State.SetState(null);
        State = null;
    }
    
    public override void UpdateUI(GameTime gameTime) {
        State.Update(gameTime);

        UILevelManager.gameTime = gameTime;
    }

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
        var index = layers.FindIndex(l => l.Name == "Vanilla: Mouse Text");

        if (index == -1) {
            return;
        }

        layers.Insert(index + 1, new LegacyGameInterfaceLayer("WotTK:LevelDisplay", static () => {
            State.Draw(Main.spriteBatch, gameTime);

            return true;
        }, InterfaceScaleType.UI));
    }
}
