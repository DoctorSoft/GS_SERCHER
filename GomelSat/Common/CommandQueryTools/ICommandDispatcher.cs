using System.Security.Cryptography.X509Certificates;

namespace Common.CommandQueryTools
{
    public interface ICommandDispatcher
    {
        TCommandResponse Dispatch<TCommand, TCommandResponse>(TCommand command) where TCommand : ICommand<TCommandResponse>;
    }
}
