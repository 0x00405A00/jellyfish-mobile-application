using Application.Abstractions.Messaging;
using Domain.ValueObjects;
using Domain.Entities.Chats;
using Infrastructure.Abstractions;

namespace Application.CQS.Chats.Commands.CreateChatCommand
{
    internal sealed class CreateChatCommandHandler : ICommandHandler<CreateChatCommand, Chat>
    {
        public CreateChatCommandHandler()
        {
        }

        public Task<Result<Chat>> Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
