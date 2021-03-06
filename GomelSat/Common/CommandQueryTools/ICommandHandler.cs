﻿namespace Common.CommandQueryTools
{
    public interface ICommandHandler<in TCommand, out TCommandResponse> where TCommand: ICommand<TCommandResponse>
    {
        TCommandResponse Handle(TCommand command);
    }
}
