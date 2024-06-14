using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Achievements;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace WotTK.Common.Players
{
    public class PlayerSoundEngine : ModPlayer
    {
        public override bool OnPickup(Item item)
        {
            bool customPickup = false;
            SoundByItem(item);

            if(customPickup)
            {
                PickupItem(item);
            }

            return !customPickup;
            // can be replaced with false to not use the default "GrabItem" logic to re-implement it without sound hooks
        }
        SoundStyle SoundByItem(Item item)
        {
            switch (item.type)
            {
                case (int)ItemID.AdamantiteOre:
                case (int)ItemID.ChlorophyteOre:
                case (int)ItemID.CobaltOre:
                case (int)ItemID.CopperOre:
                case (int)ItemID.CrimtaneOre:
                case (int)ItemID.DemoniteOre:
                case (int)ItemID.FossilOre:
                case (int)ItemID.GoldOre:
                case (int)ItemID.IronOre:
                case (int)ItemID.LeadOre:
                case (int)ItemID.LunarOre:
                case (int)ItemID.MythrilOre:
                case (int)ItemID.OrichalcumOre:
                case (int)ItemID.PalladiumOre:
                case (int)ItemID.PlatinumOre:
                case (int)ItemID.SilverOre:
                case (int)ItemID.TinOre:
                case (int)ItemID.TitaniumOre:
                case (int)ItemID.TungstenOre:
                    return SoundID.ZombieMoan;
            }
            return SoundID.Grab;
        }
        Item PickupItem(Item itemToPickUp)
        {
            if (ItemID.Sets.NebulaPickup[itemToPickUp.type])
            {
                SoundEngine.PlaySound(SoundID.Grab, Player.position);
                int num = itemToPickUp.buffType;
                itemToPickUp = new Item();
                if (Main.netMode == 1)
                {
                    NetMessage.SendData(102, -1, -1, null, Player.whoAmI, num, Player.Center.X, Player.Center.Y);
                }
                else
                {
                    Player.NebulaLevelup(num);
                }
            }
            if (itemToPickUp.type == 58 || itemToPickUp.type == 1734 || itemToPickUp.type == 1867)
            {
                SoundEngine.PlaySound(SoundID.Grab, Player.position);;
                Player.Heal(20);
                itemToPickUp = new Item();
            }
            else if (itemToPickUp.type == 184 || itemToPickUp.type == 1735 || itemToPickUp.type == 1868)
            {
                SoundEngine.PlaySound(SoundID.Grab, Player.position);;
                Player.statMana += 100;
                if (Main.myPlayer == Player.whoAmI)
                {
                    Player.ManaEffect(100);
                }
                if (Player.statMana > Player.statManaMax2)
                {
                    Player.statMana = Player.statManaMax2;
                }
                itemToPickUp = new Item();
            }
            else if (itemToPickUp.type == 4143)
            {
                SoundEngine.PlaySound(SoundID.Grab, Player.position);;
                Player.statMana += 50;
                if (Main.myPlayer == Player.whoAmI)
                {
                    Player.ManaEffect(50);
                }
                if (Player.statMana > Player.statManaMax2)
                {
                    Player.statMana = Player.statManaMax2;
                }
                itemToPickUp = new Item();
            }
            else
            {
                itemToPickUp = GetItem(Player.whoAmI, itemToPickUp, GetItemSettings.PickupItemFromWorld);
            }
            return itemToPickUp;
        }
        
        public Item GetItem(int plr, Item newItem, GetItemSettings settings)
        {
            bool isACoin = newItem.IsACoin;
            Item item = newItem;
            int num = 50;
            if (newItem.noGrabDelay > 0)
            {
                return item;
            }
            int num2 = 0;
            if (newItem.uniqueStack && HasItem(newItem.type))
            {
                return item;
            }
            if (isACoin)
            {
                num2 = -4;
                num = 54;
            }
            if (item.FitsAmmoSlot())
            {
                item = FillAmmo(plr, item, settings);
                if (item.type == 0 || item.stack == 0)
                {
                    return new Item();
                }
            }
            for (int i = num2; i < 50; i++)
            {
                int num3 = i;
                if (num3 < 0)
                {
                    num3 = 54 + i;
                }
                if (GetItem_FillIntoOccupiedSlot(plr, newItem, settings, item, num3))
                {
                    return new Item();
                }
            }
            if (!isACoin && newItem.useStyle != 0)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (GetItem_FillEmptyInventorySlot(plr, newItem, settings, item, j))
                    {
                        return new Item();
                    }
                }
            }
            if (newItem.favorited)
            {
                for (int k = 0; k < num; k++)
                {
                    if (GetItem_FillEmptyInventorySlot(plr, newItem, settings, item, k))
                    {
                        return new Item();
                    }
                }
            }
            else
            {
                for (int num4 = num - 1; num4 >= 0; num4--)
                {
                    if (GetItem_FillEmptyInventorySlot(plr, newItem, settings, item, num4))
                    {
                        return new Item();
                    }
                }
            }
            if (settings.CanGoIntoVoidVault && Player.IsVoidVaultEnabled && CanVoidVaultAccept(newItem) && GetItem_VoidVault(plr, Player.bank4.item, newItem, settings, item))
            {
                return new Item();
            }
            return item;
        }

        private bool GetItem_VoidVault(int plr, Item[] inventory, Item newItem, GetItemSettings settings, Item returnItem)
        {
            if (!CanVoidVaultAccept(newItem))
            {
                return false;
            }
            for (int i = 0; i < inventory.Length; i++)
            {
                if (GetItem_FillIntoOccupiedSlot_VoidBag(plr, inventory, newItem, settings, returnItem, i))
                {
                    return true;
                }
            }
            for (int j = 0; j < inventory.Length; j++)
            {
                if (GetItem_FillEmptyInventorySlot_VoidBag(plr, inventory, newItem, settings, returnItem, j))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CanVoidVaultAccept(Item item)
        {
            if (item.questItem)
            {
                return false;
            }
            int type = item.type;
            if (type == 3822)
            {
                return false;
            }
            return true;
        }

        private bool GetItem_FillIntoOccupiedSlot_VoidBag(int plr, Item[] inv, Item newItem, GetItemSettings settings, Item returnItem, int i)
        {
            if (inv[i].type > 0 && inv[i].stack < inv[i].maxStack && returnItem.netID == inv[i].netID)
            {
                if (newItem.IsACoin)
                {
                    SoundEngine.PlaySound(SoundID.CoinPickup//38
                        , Player.position);
                }
                else
                {
                    SoundEngine.PlaySound(SoundByItem(newItem)//7
                        , Player.position);
                }
                if (returnItem.stack + inv[i].stack <= inv[i].maxStack)
                {
                    inv[i].stack += returnItem.stack;
                    if (!settings.NoText)
                    {
                        PopupText.NewText(PopupTextContext.ItemPickupToVoidContainer, newItem, returnItem.stack, noStack: false, settings.LongText);
                    }
                    AchievementsHelper.NotifyItemPickup(Player, returnItem);
                    settings.HandlePostAction(inv[i]);
                    return true;
                }
                AchievementsHelper.NotifyItemPickup(Player, returnItem, inv[i].maxStack - inv[i].stack);
                returnItem.stack -= inv[i].maxStack - inv[i].stack;
                if (!settings.NoText)
                {
                    PopupText.NewText(PopupTextContext.ItemPickupToVoidContainer, newItem, inv[i].maxStack - inv[i].stack, noStack: false, settings.LongText);
                }
                inv[i].stack = inv[i].maxStack;
                settings.HandlePostAction(inv[i]);
            }
            return false;
        }

        private bool GetItem_FillIntoOccupiedSlot(int plr, Item newItem, GetItemSettings settings, Item returnItem, int i)
        {
            if (Player.inventory[i].type > 0 && Player.inventory[i].stack < Player.inventory[i].maxStack && returnItem.netID == Player.inventory[i].netID)
            {
                if (newItem.IsACoin)
                {
                    SoundEngine.PlaySound(SoundID.CoinPickup, Player.position);
                }
                else
                {
                    SoundEngine.PlaySound(SoundByItem(newItem), Player.position);
                }
                if (returnItem.stack + Player.inventory[i].stack <= Player.inventory[i].maxStack)
                {
                    Player.inventory[i].stack += returnItem.stack;
                    if (!settings.NoText)
                    {
                        PopupText.NewText(PopupTextContext.RegularItemPickup, newItem, returnItem.stack, noStack: false, settings.LongText);
                    }
                    DoCoins(i);
                    if (plr == Main.myPlayer)
                    {
                        Recipe.FindRecipes();
                    }
                    AchievementsHelper.NotifyItemPickup(Player, returnItem);
                    settings.HandlePostAction(Player.inventory[i]);
                    return true;
                }
                AchievementsHelper.NotifyItemPickup(Player, returnItem, Player.inventory[i].maxStack - Player.inventory[i].stack);
                returnItem.stack -= Player.inventory[i].maxStack - Player.inventory[i].stack;
                if (!settings.NoText)
                {
                    PopupText.NewText(PopupTextContext.RegularItemPickup, newItem, Player.inventory[i].maxStack - Player.inventory[i].stack, noStack: false, settings.LongText);
                }
                Player.inventory[i].stack = Player.inventory[i].maxStack;
                DoCoins(i);
                if (plr == Main.myPlayer)
                {
                    Recipe.FindRecipes();
                }
                settings.HandlePostAction(Player.inventory[i]);
            }
            return false;
        }

        private bool GetItem_FillEmptyInventorySlot_VoidBag(int plr, Item[] inv, Item newItem, GetItemSettings settings, Item returnItem, int i)
        {
            if (inv[i].type != 0)
            {
                return false;
            }
            if (newItem.IsACoin)
            {
                SoundEngine.PlaySound(SoundID.CoinPickup, Player.position);;
            }
            else
            {
                SoundEngine.PlaySound(SoundByItem(newItem), Player.position);;
            }
            returnItem.shimmered = false;
            inv[i] = returnItem;
            if (!settings.NoText)
            {
                PopupText.NewText(PopupTextContext.ItemPickupToVoidContainer, newItem, newItem.stack, noStack: false, settings.LongText);
            }
            DoCoins(i);
            if (plr == Main.myPlayer)
            {
                Recipe.FindRecipes();
            }
            AchievementsHelper.NotifyItemPickup(Player, returnItem);
            settings.HandlePostAction(inv[i]);
            return true;
        }

        private bool GetItem_FillEmptyInventorySlot(int plr, Item newItem, GetItemSettings settings, Item returnItem, int i)
        {
            if (Player.inventory[i].type != 0)
            {
                return false;
            }
            if (newItem.IsACoin)
            {
                SoundEngine.PlaySound(SoundID.CoinPickup, Player.position);;
            }
            else
            {
                SoundEngine.PlaySound(SoundByItem(newItem), Player.position);;
            }
            returnItem.shimmered = false;
            Player.inventory[i] = returnItem;
            if (!settings.NoText)
            {
                PopupText.NewText(PopupTextContext.RegularItemPickup, newItem, newItem.stack, noStack: false, settings.LongText);
            }
            DoCoins(i);
            if (plr == Main.myPlayer)
            {
                Recipe.FindRecipes();
            }
            AchievementsHelper.NotifyItemPickup(Player, returnItem);
            if (plr == Main.myPlayer && newItem.type == 5095)
            {
                LucyAxeMessage.Create(LucyAxeMessage.MessageSource.PickedUp, Player.Top, new Vector2(0f, -7f));
            }
            settings.HandlePostAction(Player.inventory[i]);
            return true;
        }

        public bool HasItem(int type)
        {
            for (int i = 0; i < 58; i++)
            {
                if (type == Player.inventory[i].type && Player.inventory[i].stack > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasItem(int type, Item[] collection)
        {
            for (int i = 0; i < collection.Length; i++)
            {
                if (type == collection[i].type && collection[i].stack > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasItemInInventoryOrOpenVoidBag(int type)
        {
            if (!HasItem(type))
            {
                if (useVoidBag())
                {
                    return HasItem(type, Player.bank4.item);
                }
                return false;
            }
            return true;
        }

        public bool HasItemInAnyInventory(int type)
        {
            if (HasItem(type, Player.inventory))
            {
                return true;
            }
            if (HasItem(type, Player.armor))
            {
                return true;
            }
            if (HasItem(type, Player.dye))
            {
                return true;
            }
            if (HasItem(type, Player.miscEquips))
            {
                return true;
            }
            if (HasItem(type, Player.miscDyes))
            {
                return true;
            }
            if (HasItem(type, Player.bank.item))
            {
                return true;
            }
            if (HasItem(type, Player.bank2.item))
            {
                return true;
            }
            if (HasItem(type, Player.bank3.item))
            {
                return true;
            }
            if (HasItem(type, Player.bank4.item))
            {
                return true;
            }
            return false;
        }

        public Item FillAmmo(int plr, Item newItem, GetItemSettings settings)
        {
            for (int i = 54; i < 58; i++)
            {
                if (Player.inventory[i].type <= 0 || Player.inventory[i].stack >= Player.inventory[i].maxStack || !(newItem.netID == Player.inventory[i].netID))
                {
                    continue;
                }
                SoundEngine.PlaySound(SoundByItem(newItem), Player.position);;
                if (newItem.stack + Player.inventory[i].stack <= Player.inventory[i].maxStack)
                {
                    Player.inventory[i].stack += newItem.stack;
                    if (!settings.NoText)
                    {
                        PopupText.NewText(PopupTextContext.RegularItemPickup, newItem, newItem.stack);
                    }
                    DoCoins(i);
                    if (plr == Main.myPlayer)
                    {
                        Recipe.FindRecipes();
                    }
                    settings.HandlePostAction(Player.inventory[i]);
                    return new Item();
                }
                newItem.stack -= Player.inventory[i].maxStack - Player.inventory[i].stack;
                if (!settings.NoText)
                {
                    PopupText.NewText(PopupTextContext.RegularItemPickup, newItem, Player.inventory[i].maxStack - Player.inventory[i].stack);
                }
                Player.inventory[i].stack = Player.inventory[i].maxStack;
                DoCoins(i);
                if (plr == Main.myPlayer)
                {
                    Recipe.FindRecipes();
                }
                settings.HandlePostAction(Player.inventory[i]);
            }
            if (newItem.CanFillEmptyAmmoSlot())
            {
                for (int j = 54; j < 58; j++)
                {
                    if (Player.inventory[j].type == 0)
                    {
                        newItem.shimmered = false;
                        Player.inventory[j] = newItem;
                        if (!settings.NoText)
                        {
                            PopupText.NewText(PopupTextContext.RegularItemPickup, newItem, newItem.stack);
                        }
                        DoCoins(j);
                        SoundEngine.PlaySound(SoundByItem(newItem), Player.position);
                        if (plr == Main.myPlayer)
                        {
                            Recipe.FindRecipes();
                        }
                        settings.HandlePostAction(Player.inventory[j]);
                        return new Item();
                    }
                }
            }
            return newItem;
        }

        public void DoCoins(int i)
        {
            if (Player.inventory[i].stack != 100 || (Player.inventory[i].type != 71 && Player.inventory[i].type != 72 && Player.inventory[i].type != 73))
            {
                return;
            }
            Player.inventory[i].SetDefaults(Player.inventory[i].type + 1);
            for (int j = 0; j < 54; j++)
            {
                if (Player.inventory[j].netID == Player.inventory[i].netID && j != i && Player.inventory[j].type == Player.inventory[i].type && Player.inventory[j].stack < Player.inventory[j].maxStack)
                {
                    Player.inventory[j].stack++;
                    Player.inventory[i].SetDefaults();
                    Player.inventory[i].active = false;
                    Player.inventory[i].TurnToAir();
                    DoCoins(j);
                }
            }
        }

        public bool useVoidBag()
        {
            for (int i = 0; i < 58; i++)
            {
                if (Player.inventory[i].stack > 0 && Player.inventory[i].type == 4131)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
