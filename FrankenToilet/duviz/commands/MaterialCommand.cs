using GameConsole;
using plog;

namespace FrankenToilet.duviz.commands;

public class NotFoundReplace : ICommand, IConsoleLogger
{
    public Logger Log { get; } = new Logger("Lobby command");

    public string Name
    {
        get
        {
            return "Not found";
        }
    }

    public string Description
    {
        get
        {
            return "Sets every material in the level to a missing texture";
        }
    }

    public string Command
    {
        get
        {
            return "notFound";
        }
    }

    public void Execute(Console con, string[] args)
    {
        Meow.ReplaceEverything();
    }
}