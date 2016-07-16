namespace Common.CommandQueryTools
{
    public interface IVoidCommandHandler<in TCommand> : ICommandHandler<TCommand, VoidCommandResponse> where TCommand : IVoidCommand
    {
    }
}
