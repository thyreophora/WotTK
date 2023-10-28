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
            
        }
    }
}
