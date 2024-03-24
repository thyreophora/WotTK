using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using WotTK.Common.Sets;

namespace WotTK.Common.Globals;

public sealed class NPCLevels : GlobalNPC
{
    public int Level { get; private set; }
    
    public override bool InstancePerEntity { get; } = true;

    public override void OnSpawn(NPC npc, IEntitySource source) {
        var range = NPCSets.Level[NPCID.FromNetId(npc.type)];

        if (range.Item1 == 0 || range.Item2 == 0) {
            return;
        }

        Level = Main.rand.Next(range.Item1, range.Item2 + 1);
    }

    public override void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter) {
        binaryWriter.Write(Level);
    }

    public override void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader) {
        Level = binaryReader.ReadInt32();
    }
}
