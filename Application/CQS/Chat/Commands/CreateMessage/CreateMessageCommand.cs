using Application.Abstractions.Messaging;
using Domain.Errors;
using Domain.Extension;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Shared.DataTransferObject.Messenger;

namespace Application.CQS.Chat.Commands.CreateMessage
{
    public record CreateMessageCommand(Guid ChatId, Guid MessageCreatorId, List<MessageDTO> Messages) : ICommand<List<MessageDTO>>;
    internal sealed class CreateMessageCommandHandler : ICommandHandler<CreateMessageCommand, List<MessageDTO>>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMessageRepository _messageRepository;

        public CreateMessageCommandHandler(IChatRepository chatRepository,
                                           IMessageRepository messageRepository)
        {
            this._chatRepository = chatRepository;
            this._messageRepository = messageRepository;
        }
        public async Task<Result<List<MessageDTO>>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Chats.Chat chat = await _chatRepository.GetAsync(x => x.Id == request.ChatId.ToIdentification<ChatId>());
            if (chat is null)
            {
                //create chat
            }
            throw new NotImplementedException();
        }
    }
}
