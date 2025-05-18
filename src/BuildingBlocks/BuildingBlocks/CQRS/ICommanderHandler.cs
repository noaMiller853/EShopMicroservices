using MediatR;

namespace BuildingBlocks.CQRS;

public interface ICommanderHandler<in TCommand>
    : ICommanderHandler<TCommand, Unit>
    where TCommand : ICommand<Unit> // Added constraint to ensure TCommand implements ICommand<Unit>
{
}

public interface ICommanderHandler<in TCommand, TResponse>
    : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse> // Added constraint to ensure TCommand implements ICommand<TResponse>
    where TResponse : notnull
{
}
