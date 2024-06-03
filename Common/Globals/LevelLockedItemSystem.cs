using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using WotTK.Common.Players;
using WotTK.Utilities;
using Terraria.ID;

namespace WotTK.Common.Globals
{
    // apply to all items AT ONCE, code level locking by calling a "GetMinimalLevel" function in the "CanUse" function
    // don't use variable because it depends on itemID.
    public class LevelLockedItemSystem : GlobalItem
    {
        public int MinimalLevel = 0;
        public bool IsWeapon = false;

        public int GetLevel(Player player) => player.WotTKPlayer().playerLevel;


        public override void SetDefaults(Item item)
        {
            if (item.type != ItemID.PlatinumCoin &&
                item.type != ItemID.GoldCoin &&
                item.type != ItemID.SilverCoin &&
                item.type != ItemID.CopperCoin)
            {
                IsWeapon = ((item.maxStack == 1) && (item.damage > 0 && item.active));
            }

            MinimalLevel = 0;

            if (item.type == ItemID.WoodenSword)
                MinimalLevel = 0;
            if (item.type == ItemID.WoodenHammer)
                MinimalLevel = 0;

            switch (item.rare)
            {
                case 0:
                    MinimalLevel = 0;
                    break;
                case 1:
                    MinimalLevel = 15;
                    break;
                case 2:
                    MinimalLevel = 20;
                    break;
                case 3:
                    MinimalLevel = 30;
                    break;
                case 4:
                    MinimalLevel = 40;
                    break;
                case 5:
                    MinimalLevel = 50;
                    break;
                case 6:
                    MinimalLevel = 55;
                    break;
                case 7:
                    MinimalLevel = 60;
                    break;
                case 8:
                    MinimalLevel = 65;
                    break;
                case 9:
                    MinimalLevel = 75;
                    break;
                case 10:
                    MinimalLevel = 80;
                    break;
                case 11:
                    MinimalLevel = 80;
                    break;
                default:
                    if(item.rare > ItemRarityID.Purple)
                    {
                        MinimalLevel = 90 + ((item.rare - 11) * 5);
                    }
                    return;

            }
            base.SetDefaults(item);
        }

        public override bool InstancePerEntity => true; 
        public override bool CanUseItem(Item item, Player player)
        {  
            int level = player.WotTKPlayer().playerLevel;
            if (IsWeapon && MinimalLevel != 0)
            {
                if (MinimalLevel <= 10)
                    return base.CanUseItem(item, player);
                if (MinimalLevel > 10)
                    return base.CanUseItem(item, player) && (MinimalLevel - level) <= 10;
            }
            return base.CanUseItem(item,player) && (MinimalLevel == 0 ? true : (level >= MinimalLevel));
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            int level = Main.LocalPlayer.WotTKPlayer().playerLevel;

            int index = tooltips.FindLastIndex(tt => tt.Mod.Equals("Terraria") && tt.Name.Equals("Tooltip0"));
            if (index == -1) 
            {
                TooltipLine item2 = new TooltipLine(Mod, "PlaceHolder", "")
                {
                    OverrideColor = Color.White


                };
                tooltips.Add(item2);

                index = tooltips.FindLastIndex(tt => tt.Mod.Equals("WotTK") && tt.Name.Equals("PlaceHolder"));
            }
            if (IsWeapon)
            {
                if (index != -1 && MinimalLevel != 0)
                {
                    int stage = -1;
                    //if (MinimalLevel != 0)
                    //{
                    if (IsWeapon)
                    {
                        //stage = -1;
                        if (MinimalLevel <= 10)
                        {
                            stage = 0;
                            if (level >= MinimalLevel)
                                stage = 1;
                        }
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
                    //}
                    //else if (MinimalLevel == 0 || WotTKConfig.Instance.Debug)
                    //    stage = 1;
                    //tooltips.Insert(index, new TooltipLine(Mod, "LevelLock", LangHelper.GetText("UI.LevelLock", MinimalLevel, Main.LocalPlayer.WotTKPlayer().playerLevel >= MinimalLevel ? "00FF00" : "FF0000")));
                    tooltips.Insert(index, new TooltipLine(Mod, "LevelLock", LangHelper.GetText("UI.LevelLock", MinimalLevel, stage == -1 ? "FF0000" : (stage == 1 ? "00FF00" : "FFFF00"))));
                }
            }
        }
        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
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
       
        public override void UpdateEquip(Item item, Player player)
        {  
            if (GetLevel(player) < MinimalLevel && item.accessory)
            {
                for (int i = 0; i < player.armor.Length; i++)
                {
                    ref Item itm = ref player.armor[i];
                    //Item itm2 = player.armor[i];
                    if (itm.type == item.type)
                    {
                        player.QuickSpawnItemDirect(Player.GetSource_None(), itm);
                        itm = new Item();
                    }
                }
            }
        }
    }
}
