using log4net.Core;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using WotTK.Common.Players;
using WotTK.Utilities;

namespace WotTK.Content
{
    public abstract class LevelLockedItem : ModItem
    {
        public virtual int MinimalLevel => 0;
        public virtual bool IsWeapon => false;
        public override bool CanUseItem(Player player)
        {
            int level = player.WotTKPlayer().playerLevel;
            if (IsWeapon && MinimalLevel != 0)
            {
                if (MinimalLevel <= 10)
                    return base.CanUseItem(player);
                if (MinimalLevel > 10)
                    return base.CanUseItem(player) && (MinimalLevel - level) <= 10;
            }
            return base.CanUseItem(player) && (MinimalLevel == 0 ? true : (level >= MinimalLevel));
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int level = Main.LocalPlayer.WotTKPlayer().playerLevel;

            int index = tooltips.FindLastIndex(tt => tt.Mod.Equals("Terraria") && tt.Name.Equals("Tooltip0"));
            if (index != -1 && MinimalLevel != 0)
            {
                int stage = -1;
                if (MinimalLevel != 0)
                {
                    if (IsWeapon)
                    {
                        //stage = -1;
                        if (MinimalLevel <= 10)
                            stage = 1;
                        if (MinimalLevel > 10)
                        {
                            if (MinimalLevel - level > 0 && MinimalLevel - level <= 10)
                                stage = 0;
                            if (level >= MinimalLevel)
                                stage = 1;
                        }
                        //stage = ((MinimalLevel - level) <= 10 && (MinimalLevel - level) > 0) ? 0 : 1;

                    }
                    else
                    {
                        stage = level >= MinimalLevel ? 1 : -1;
                    }
                }
                else if (MinimalLevel == 0 || WotTKConfig.Instance.Debug)
                    stage = 1;
                //tooltips.Insert(index, new TooltipLine(Mod, "LevelLock", LangHelper.GetText("UI.LevelLock", MinimalLevel, Main.LocalPlayer.WotTKPlayer().playerLevel >= MinimalLevel ? "00FF00" : "FF0000")));
                tooltips.Insert(index, new TooltipLine(Mod, "LevelLock", LangHelper.GetText("UI.LevelLock", MinimalLevel, stage == -1 ? "FF0000" : (stage == 1 ? "00FF00" : "FFFF00"))));

            }
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (IsWeapon && MinimalLevel != 0)
            {
                float level = (float)player.GetModPlayer<WotTKPlayer>().playerLevel;
                float percent = 1f;
                int damage2 = damage;

                if (MinimalLevel <= 10 && MinimalLevel > 0)
                {
                    percent = level / MinimalLevel;
                    if (level > MinimalLevel)
                        percent = 1f;
                }
                else
                {
                    percent = 1f - (MinimalLevel - level) / 10f;

                    if (MinimalLevel - level >= 10) //Low when minimal
                    {
                        percent = 1;
                        damage = 1;
                    }
                    if (MinimalLevel - level <= 0) //High and Equal when minimal
                    {
                        percent = 1;
                    }
                }
                damage = (int)(damage * percent);
                if (WotTKConfig.Instance.Debug)
                    damage = damage2;
            }
        }
    }
}
