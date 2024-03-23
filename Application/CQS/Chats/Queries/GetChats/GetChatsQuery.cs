using Application.Abstractions.Messaging;
using Shared.DataTransferObject.Messenger;

namespace Application.CQS.Chats.Queries.GetChatsQuery
{
    public record GetChatsQuery() : IQuery<List<ChatDTO>>
    {
    }
}
