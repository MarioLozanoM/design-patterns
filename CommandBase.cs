public interface ICommand
{
    void Execute();
    void Undo();
}

public interface ICommandInvoker
{
    void ExecuteCommand(ICommand command);
    bool UndoLastCommand();
}

public class CommandInvoker : ICommandInvoker
{
    private readonly Stack<ICommand> _commandHistory = new Stack<ICommand>();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _commandHistory.Push(command);
    }

    public bool UndoLastCommand()
    {
        if (_commandHistory.Count > 0)
        {
            ICommand lastCommand = _commandHistory.Pop();
            lastCommand.Undo();
            return true;
        }
        return false;
    }
}