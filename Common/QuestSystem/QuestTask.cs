using System;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WotTK.Common.Players;

namespace WotTK.Common.QuestSystem;

public abstract class QuestTask
{
    public string QuestId { get; private set; }
    public string Id { get; }
    public abstract string TitleKey { get; }
    public abstract string DescriptionKey { get; }

    public QuestTask(string id)
    {
        if (Regex.IsMatch(id, @"[^a-zA-Z0-9_]"))
        {
            throw new ArgumentException("only latin letters, digits and underscore characters allowed as quest task id", nameof(id));
        }
        
        Id = id;
    }

    public void Init(Quest quest)
    {
        QuestId = quest.Id;
    }
    
    public virtual void AddPlayer(WotTKPlayer player) {}
    public virtual void RemovePlayer(WotTKPlayer player) {}
    public abstract bool IsDone(WotTKPlayer player);
    public virtual bool Apply(WotTKPlayer player) { return IsDone(player); }
    public abstract void Draw(SpriteBatch sb, Vector2 position);
}