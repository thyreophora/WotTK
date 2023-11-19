using Microsoft.Xna.Framework;
using Mono.Cecil;
using System.Collections.ObjectModel;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using WotTK.Content.Items.Weapons.Magic.Staffs;
using WotTK.Content.Items.Weapons.Melee.Mace;

namespace WotTK.Common.Globals
{
    public class WotTKGlobalItem : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (WotTKConfig.Instance.ChangeVanillaWeaponsToMace)
            {
                switch (item.type)
                {
                    case ItemID.ZombieArm:
                        SetMaceDefaults(item, ModContent.ProjectileType<ZombieArm>());
                        break;
                    case ItemID.PurpleClubberfish:
                        SetMaceDefaults(item, ModContent.ProjectileType<PurpleClubberfish>());
                        break;
                    case ItemID.SlapHand:
                        SetMaceDefaults(item, ModContent.ProjectileType<SlapHand>());
                        break;
                    case ItemID.WaffleIron:
                        SetMaceDefaults(item, ModContent.ProjectileType<WaffleIron>());
                        break;
                    case ItemID.HamBat:
                        SetMaceDefaults(item, ModContent.ProjectileType<HamBat>());
                        break;
                    case ItemID.TentacleSpike:
                        SetMaceDefaults(item, ModContent.ProjectileType<TentacleSpike>());
                        break;
                }
            }
        }
        private void SetMaceDefaults(Item item, int proj, int mult = 2)
        {
            item.shoot = proj;
            item.shootSpeed = 0;
            item.DamageType = PaladinDamageType.Instance;
            item.useTime *= mult;
            item.useAnimation *= mult;
            item.damage *= mult;
            item.useStyle = ItemUseStyleID.Shoot;
            item.noUseGraphic = true;
            item.noMelee = true;
        }
        /*public override bool PreDrawTooltip(Item item, ReadOnlyCollection<TooltipLine> lines, ref int x, ref int y)
        {
            foreach (TooltipLine line in lines) 
            { 
                if (line.Mod == "Terraria" && line.Name == "ItemName")
                {
                    //Main.Text
                    //Main.spriteBatch.DrawString(Main.)
                    return false;
                }
            }
            return true;
        }*/
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            //LeadingConditionRule rule = new LeadingConditionRule(new DropsEnabled());
            switch (item.type)
            {
                case ItemID.EyeOfCthulhuBossBag:
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SmokedEyeShooter>(), 2));
                    break;
                case ItemID.DeerclopsBossBag:
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<FreezingShard>(), 4));
                    break;
            }
        }
    }
}
