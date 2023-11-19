using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using WotTK.Content.Items.Materials;
using WotTK.Content.Items.Weapons.Magic.Staffs;
using WotTK.Content.Items.Weapons.Melee.Mace;
using WotTK.Utilities;

namespace WotTK.Common.Globals
{
    public class WotTKGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool canGetExp = true;
        public override void OnKill(NPC npc)
        {
            Player player = Main.LocalPlayer;
            Item item = player.HeldItem;
            if (Main.myPlayer == player.whoAmI && item.type == ItemID.HamBat && WotTKConfig.Instance.ChangeVanillaWeaponsToMace)
                player.AddBuff(336, 420, false);
        }
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            Conditions.NotExpert notExpert = new Conditions.NotExpert();
            switch (npc.type)
            {
                case NPCID.GoblinWarrior:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GoblinSmasher>(), 50));
                    break;
                case NPCID.BloodZombie:
                case NPCID.Drippler:
                    npcLoot.Add(ItemDropRule.ByCondition(new WotTKUtils.IItemDropRuleByFunc(() => NPC.downedSlimeKing), ModContent.ItemType<MediumLeather>(), 3));
                    break;
                case NPCID.ZombieEskimo:
                case NPCID.TheBride:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WoolCloth>(), 1));
                    break;
                case NPCID.EyeofCthulhu:
                    npcLoot.Add(ItemDropRule.ByCondition(notExpert, ModContent.ItemType<SmokedEyeShooter>(), 2));
                    break;
                case NPCID.Deerclops:
                    npcLoot.Add(ItemDropRule.ByCondition(notExpert, ModContent.ItemType<FreezingShard>(), 4));
                    break;
            }
        }
    }
}
