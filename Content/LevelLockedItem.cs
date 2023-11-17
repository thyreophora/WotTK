using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using WotTK.Utilities;

namespace WotTK.Content
{
    public abstract class LevelLockedItem : ModItem
    {
        public virtual int MininmalLevel => -1;
        public override bool CanUseItem(Player player)
        {
            return base.CanUseItem(player) && (MininmalLevel == -1 ? true : (player.WotTKPlayer().playerLevel >= MininmalLevel));
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int index = tooltips.FindLastIndex(tt => tt.Mod.Equals("Terraria") && tt.Name.Equals("Tooltip0"));
            if (index != -1 && MininmalLevel != -1)
            {
                tooltips.Insert(index, new TooltipLine(Mod, "LevelLock", LangHelper.GetText("UI.LevelLock", MininmalLevel, Main.LocalPlayer.WotTKPlayer().playerLevel >= MininmalLevel ? "00FF00" : "FF0000")));
            }
        }
    }
}
