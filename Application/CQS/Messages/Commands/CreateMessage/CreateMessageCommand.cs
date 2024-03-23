using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.Messages;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Shared.DataTransferObject.Messenger;
using Domain.Extension;
using Domain.Primitives.Ids;
using Domain.Primitives;

namespace Application.CQS.Messages.Commands.CreateMessage
{
    public record CreateMessageCommand(MessageDTO[] Messages) : ICommand<List<MessageDTO>>
    {
    }
    internal sealed class CreateMessageCommandHandler : ICommandHandler<CreateMessageCommand, List<MessageDTO>>
    {
        private readonly IMapper mapper;
        private readonly IChatRepository chatRepository;
        private readonly IMessageRepository messageRepository;

        public CreateMessageCommandHandler(
            IMapper mapper,
            IChatRepository chatRepository,
            IMessageRepository messageRepository)
        {
            this.mapper = mapper;
            this.chatRepository = chatRepository;
            this.messageRepository = messageRepository;
        }
        public async Task<Result<List<MessageDTO>>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            if(request.Messages.Length == 0)
            {
                return Result<List<MessageDTO>>.Failure("invalid request");
            }
            
            var groupedByChatId = request.Messages.GroupBy(x => IdentificationExtension.ToIdentification<ChatId>(x.ChatId??Guid.Empty)).ToDictionary(x=>x.Key, x=>x.ToList());

            var foundChatsInDatabase = (await chatRepository.ListAsync(x=> groupedByChatId.ContainsKey(x.Id)))?.ToList();

            List<Message> data;
            foreach (var group in groupedByChatId)
            {
                var chatId = group.Key;
                if(foundChatsInDatabase.Find(x=>x.Id == chatId) == null)
                {
                    //create new chat

                    chatRepository.Add();
                }
                var messagesForChat = group.Value;
                foreach (var item in messagesForChat)
                {

                }
            }
            var dataMapper = mapper.Map<List<MessageDTO>>(data);
            return Result<List<MessageDTO>>.Success(dataMapper);    
        }
    }
}
