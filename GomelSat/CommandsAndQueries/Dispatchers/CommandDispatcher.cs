using System;
using System.Linq;
using Common.CommandQueryTools;

namespace CommandsAndQueries.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly object[] commandHandlers;

        public CommandDispatcher(object[] commandHandlers)
        {
            this.commandHandlers = commandHandlers;
        }

        public TCommandResponse Dispatch<TCommand, TCommandResponse>(TCommand command) where TCommand : ICommand<TCommandResponse>
        {
            foreach (var handler in commandHandlers.OfType<ICommandHandler<TCommand, TCommandResponse>>())
            {
                var response = handler.Handle(command);
                return response;
            }

            throw new Exception(String.Format("Please connect command {0} to dispatcher", command));
        }
    }
}
