using Application.Abstractions.Messaging;
using Domain.Entities.Chats;

namespace Application.CQS.Chats.Queries.GetChatsQuery
{
    public record GetChatsQuery() : IQuery<ICollection<Chat>>
    {
    }
}
