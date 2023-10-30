﻿using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using WotTK.Content.Items.Weapons.Melee.Mace;

namespace WotTK
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
            switch (npc.type)
            {
                case NPCID.GoblinWarrior:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GoblinSmasher>(), 50));
                    break;
            }
        }
    }
}
