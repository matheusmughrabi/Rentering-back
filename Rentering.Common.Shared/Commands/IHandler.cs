namespace Rentering.Common.Shared.Commands
{
    public interface IHandler<T> where T : Command
    {
        ICommandResult Handle(T command);
    }
}
