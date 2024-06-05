using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using WotTK.Common.Players;
using WotTK.Common.QuestSystem;

namespace WotTK.Common.Commands;

public class QuestCommand : ModCommand
{
    public override string Command => "quest";
    public override CommandType Type => CommandType.Chat;
    
    public override void Action(CommandCaller caller, string input, string[] args)
    {
        void PrintHelp()
        {
            caller.Reply("quest list (all|mine)" +
                         "quest start <id>" +
                         "quest finish <id>");
        }
        
        switch (args[0].ToLower())
        {
            case "list":
                switch (args[1])
                {
                    case "all":
                        caller.Reply(string.Join(", ", QuestRegistry.GetAllQuestIDs()));
                    break;
                    case "mine":
                        caller.Reply(string.Join(", ", caller.Player.GetModPlayer<WotTKPlayer>().GetActiveQuests().Select(quest => quest.Id)));
                        break;
                    default:
                        PrintHelp();
                        break;
                }
                break;
            case "start":
            {
                Quest quest = QuestRegistry.GetQuestById(args[1]);
                if (quest is not null)
                {
                    caller.Player.GetModPlayer<WotTKPlayer>().StartQuest(quest);
                }
                else
                {
                    caller.Reply("quest not found", Color.Red);
                }
            }
                break;
            case "finish":
            {
                Quest quest = QuestRegistry.GetQuestById(args[1]);
                if (quest is not null)
                {
                    caller.Player.GetModPlayer<WotTKPlayer>().TryFinishQuest(quest);
                }
                else
                {
                    caller.Reply("quest not found", Color.Red);
                }
            }
                break;
            default:
                PrintHelp();
                break;
        }
    }
}