using System;
using System.Collections.Generic;
using Terraria.ID;

namespace WotTK.Common.QuestSystem;

public class QuestRegistry
{
    private static Dictionary<string, Quest> registry = new();

    public static Quest RegisterQuest(Quest quest)
    {
        if (!registry.TryAdd(quest.Id, quest))
        {
            throw new ArgumentException($"Quest {quest.Id} has already been registered");
        }

        return quest;
    }

    public static Quest GetQuestById(string questId)
    {
        return registry.GetValueOrDefault(questId);
    }

    public static IEnumerable<string> GetAllQuestIDs()
    {
        return registry.Keys;
    }
    
    // Example
    public static Quest ExampleQuest = RegisterQuest(new Quest("example", [
        new FetchTask(ItemID.DirtBlock, 10, "dirt"),
        SlayTask.FromType(NPCID.BlueSlime, 5, "slimes")
    ], [
        new ItemReward(ItemID.GoldBar, 20, "gold")
    ]));
}