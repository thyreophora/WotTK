using Terraria.ID;

namespace WotTK.Common.QuestSystem;

public class QuestSystem
{
    // Example
    public static Quest ExampleQuest = new Quest("example quest", [
        new FetchTask(ItemID.DirtBlock, 10, "dirt"),
        new SlayTask(NPCID.GreenSlime, 5, "slimes")
    ], [
        new ItemReward(ItemID.GoldBar, 20, "gold")
    ]);
}