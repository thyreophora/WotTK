using Microsoft.Xna.Framework;
using Terraria;

namespace WotTK
{
    public static class WotTKUtils
    {
        public static Vector2 SafeDirectionTo(
          this Entity entity,
          Vector2 destination,
          Vector2? fallback = null)
        {
            if (!fallback.HasValue)
                fallback = new Vector2?(Vector2.Zero);
            return Utils.SafeNormalize(destination - entity.Center, fallback.Value);
        }
        public static NPC ClosestNPCAt(
         this Vector2 origin,
         float maxDistanceToCheck,
         bool ignoreTiles = true,
         bool bossPriority = false)
        {
            NPC npc = null;
            float num1 = maxDistanceToCheck;
            if (bossPriority)
            {
                bool flag1 = false;
                for (int index = 0; index < Main.npc.Length; ++index)
                {
                    if ((!flag1 || Main.npc[index].boss || Main.npc[index].type == 114) && Main.npc[index].CanBeChasedBy(null, false))
                    {
                        float num2 = Main.npc[index].width / 2f + Main.npc[index].height / 2f;
                        bool flag2 = true;
                        if (num2 < num1 && !ignoreTiles)
                            flag2 = Collision.CanHit(origin, 1, 1, Main.npc[index].Center, 1, 1);
                        if (Vector2.Distance(origin, Main.npc[index].Center) < num1 + num2 & flag2)
                        {
                            if (Main.npc[index].boss || Main.npc[index].type == 114)
                                flag1 = true;
                            num1 = Vector2.Distance(origin, Main.npc[index].Center);
                            npc = Main.npc[index];
                        }
                    }
                }
            }
            else
            {
                for (int index = 0; index < Main.npc.Length; ++index)
                {
                    if (Main.npc[index].CanBeChasedBy((object)null, false))
                    {
                        float num3 = Main.npc[index].width / 2f + Main.npc[index].height / 2f;
                        bool flag = true;
                        if ((double)num3 < (double)num1 && !ignoreTiles)
                            flag = Collision.CanHit(origin, 1, 1, Main.npc[index].Center, 1, 1);
                        if ((double)Vector2.Distance(origin, Main.npc[index].Center) < (double)num1 + (double)num3 & flag)
                        {
                            num1 = Vector2.Distance(origin, Main.npc[index].Center);
                            npc = Main.npc[index];
                        }
                    }
                }
            }
            return npc;
        }
    }
}
