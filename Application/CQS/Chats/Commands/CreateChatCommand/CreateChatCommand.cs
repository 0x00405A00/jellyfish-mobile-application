using Application.Abstractions.Messaging;
using Domain.Entities.Chats;
using Domain.ValueObjects;

namespace Application.CQS.Chats.Commands.CreateChatCommand
{
    public record CreateChatCommand() : ICommand<Chat>
    {
    }
}
