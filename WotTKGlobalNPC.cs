using Terraria;
using Terraria.ModLoader;

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
            if (Main.myPlayer == player.whoAmI && item.type == 5096 && WotTKConfig.Instance.ChangeVanillaWeaponsToMace)
                player.AddBuff(336, 420, false);
        }
    }
}
