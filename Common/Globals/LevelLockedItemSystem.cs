using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
//using WotTK.Common.Players;
using WotTK.Utilities;
using Terraria.ID;
//using System;

namespace WotTK.Common.Globals
{
	// apply to all items AT ONCE, code level locking by calling a "GetMinimalLevel" function in the "CanUse" function
	// don't use variable because it depends on itemID.
	public class LevelLockedItemSystem : GlobalItem
	{
		public int MinimalLevel = 0;
		public bool IsWeapon = false;
		public bool LevelSet = false;

        private const int DEFAULT_DAMAGE_VAL = -1;

        public override bool InstancePerEntity => true;

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
			if (LevelSet)
				return;
			// base initialization - safeguard against stupid people not initializing all cases in the switch case
			MinimalLevel = 0;
			if((item.maxStack != 1) && (item.damage == DEFAULT_DAMAGE_VAL))
			{
				// no level requirement for food and other common items
				return;
			}
			// default levels for rarity - USE THE FUCKING ENUM!!!!
			switch (item.rare)
			{
				case (int)ItemRarityID.Gray: // -1
                    MinimalLevel = 0;
                    break;
                case (int)ItemRarityID.White:
					MinimalLevel = 5; // we do a little trolling
					break;
				case (int)ItemRarityID.Blue:
					MinimalLevel = 15;
					break;
				case (int)ItemRarityID.Green:
					MinimalLevel = 20;
					break;
				case (int)ItemRarityID.Orange:
					MinimalLevel = 30;
					break;
				case (int)ItemRarityID.LightRed:
					MinimalLevel = 40;
					break;
				case (int)ItemRarityID.Pink:
					MinimalLevel = 50;
					break;
				case (int)ItemRarityID.LightPurple:
					MinimalLevel = 55;
					break;
				case (int)ItemRarityID.Lime:
					MinimalLevel = 60;
					break;
				case (int)ItemRarityID.Yellow:
					MinimalLevel = 65;
					break;
				case (int)ItemRarityID.Cyan:
					MinimalLevel = 75;
					break;
				case (int)ItemRarityID.Red:
					MinimalLevel = 80;
                    break;
				case (int)ItemRarityID.Purple:
					MinimalLevel = 80;
					break;
				default:
					if(item.rare > ItemRarityID.Purple)
					{
						// custom rarities - would rather have a "GetRarityDefaultLevel"
						// because game may just randomly assign 103 to a rarity due to allocation space
						MinimalLevel = 90 + ((item.rare - 11) * 5);
					}
					else
                    {
						// negative rarities exist as well - would just quit if I were you
                        // ItemRarityID.Quest = -11
                        // ItemRarityID.Expert = -12
                        // ItemRarityID.Master = -13
                        MinimalLevel = 0;
                    }
					// who THE FUCK put a return here?
					break;
			}

			// fuck not being able to grapple level 1 - I'm adding this and fuck you guys.
            if (item.type == ItemID.GrapplingHook)
                MinimalLevel = 0;
            if (item.type == ItemID.BabyBirdStaff)
                MinimalLevel = 0;

            // is it nessecery? probably not, but this place is for specific, PER ITEM level requirements
            if (item.type == ItemID.WoodenSword)
				MinimalLevel = 0;
			if (item.type == ItemID.WoodenHammer)
				MinimalLevel = 0;
		}
		public override bool CanUseItem(Item item, Player player)
		{ 
			int level = player.WotTKPlayer().playerLevel;
			return base.CanUseItem(item,player) && (level >= MinimalLevel);
		}

        public override bool CanShoot(Item item, Player player)
        {
            int level = player.WotTKPlayer().playerLevel;
            return base.CanUseItem(item, player) && (level >= MinimalLevel);
        }
        // return when it makes sense and actually works
        /*
			// wtf is this?
		if (IsWeapon && MinimalLevel != 0)
		{
			if (MinimalLevel <= 10)
				return base.CanUseItem(item, player);
			if (MinimalLevel > 10)
				return base.CanUseItem(item, player) && (MinimalLevel - level) <= 10;
		}
		*/

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			int level = Main.LocalPlayer.WotTKPlayer().playerLevel;

			int index = tooltips.FindLastIndex(tt => tt.Mod.Equals("Terraria") && tt.Name.Equals("Tooltip0"));
			if (index == -1) 
			{
				TooltipLine item2 = new TooltipLine(Mod, "PlaceHolder", "Fuck You, Thyreo.")
				{
					OverrideColor = Color.White
				};
				tooltips.Add(item2);

				index = tooltips.FindLastIndex(tt => tt.Mod.Equals("WotTK") && tt.Name.Equals("PlaceHolder"));
			}

            if (index != -1 && MinimalLevel != 0)
            {
                int stage = -1;
                //if (MinimalLevel != 0)
                //{
                if (false)//IsWeapon)
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

		/*
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
	   */

		public override void UpdateEquip(Item item, Player player)
		{  
			if (GetLevel(player) < MinimalLevel)
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
