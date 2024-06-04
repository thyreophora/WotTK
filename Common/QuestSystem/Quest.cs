using System;
using System.Linq;
using WotTK.Common.Players;

namespace WotTK.Common.QuestSystem;

public class Quest
{
    public string Id { get; }
    public string TitleKey => $"{Id}.Title";
    public string DescriptionKey => $"{Id}.Description";
    
    private QuestTask[] tasks;
    private QuestReward[] rewards;

    public Quest(string id, QuestTask[] tasks, QuestReward[] rewards)
    {
        foreach (QuestTask task in tasks)
        {
            if (task.QuestId is not null)
            {
                throw new ArgumentException($"Provided task {task.Id} for quest {id} has already been registered in quest {task.QuestId}", nameof(tasks));
            }
        }
        
        foreach (QuestReward reward in rewards)
        {
            if (reward.QuestId is not null)
            {
                throw new ArgumentException($"Provided reward for quest {id} has already been registered in quest {reward.QuestId}", nameof(tasks));
            }
        }

        Id = id;
        
        this.tasks = tasks.ToArray();
        this.rewards = rewards.ToArray();

        foreach (QuestTask task in this.tasks)
        {
            task.Init(this);
        }

        foreach (QuestReward reward in rewards)
        {
            reward.Init(this);
        }
    }

    public void AddPlayer(WotTKPlayer player)
    {
        foreach (QuestTask task in tasks)
        {
            task.AddPlayer(player);
        }
    }

    public void RemovePlayer(WotTKPlayer player)
    {
        foreach (QuestTask task in tasks)
        {
            task.RemovePlayer(player);
        }
    }

    public bool CanBeFinished(WotTKPlayer player)
    {
        return tasks.All(task => task.IsDone(player));
    }

    public bool Finish(WotTKPlayer player)
    {
        if (!CanBeFinished(player)) return false;

        foreach (QuestTask task in tasks)
        {
            task.Apply(player);
        }

        foreach (QuestReward reward in rewards)
        {
            reward.Grant(player);
        }

        return true;
    }
}