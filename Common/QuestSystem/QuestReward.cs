using System;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WotTK.Common.Players;

namespace WotTK.Common.QuestSystem;

public abstract class QuestReward
{
    public string QuestId { get; private set; }
    public string Id { get; }
    public abstract string TitleKey { get; }
    public abstract string DescriptionKey { get; }
    
    public abstract void Grant(WotTKPlayer player);
    public abstract void Draw(SpriteBatch sb, Vector2 position);
    
    public QuestReward(string id)
    {
        if (Regex.IsMatch(id, @"[^a-zA-Z0-9_]"))
        {
            throw new ArgumentException("only latin letters, digits and underscore characters allowed as quest reward id", nameof(id));
        }
        
        Id = id;
    }
    
    public void Init(Quest quest)
    {
        QuestId = quest.Id;
    }
}