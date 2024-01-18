using Domain.ValueObjects;
using MediatR;

namespace Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
    public interface ISearchQuery<TResponse> : IQuery<TResponse>
    {
    }
}
