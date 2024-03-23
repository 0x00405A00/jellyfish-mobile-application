using Application.Abstractions.Messaging;
using Shared.DataTransferObject.Messenger;

namespace Application.CQS.Chats.Commands.CreateChatCommand
{
    public record CreateChatCommand(
        Guid ChatOwnerUuid,
        string? ChatName,
        string? ChatDescription,
        ICollection<Guid> Members,
        string? Picture,
        string? PictureMimeType) : ICommand<ChatDTO>;
}
