using Application.CQS.Chat.Commands.CreateMessage;
using MediatR;
using Shared.DataTransferObject.Messenger;
using Shared.Infrastructure.Backend.Interceptor.Abstraction;

namespace Infrastructure.Handler.Data.InternalDataInterceptor.Invoker
{
    public class SqlLiteDatabaseHandlerInvoker : IInternalDataInterceptorApplicationInvoker
    {
        private readonly ISender sender;

        public SqlLiteDatabaseHandlerInvoker(ISender sender) 
        {
            this.sender = sender;
        }
        public Task CreateFriendRequest(params UserFriendshipRequestDTO[] data)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveAcceptFriendRequest(params MessengerUserDTO[] data)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveFriendRequest(params UserFriendshipRequestDTO[] data)
        {
            throw new NotImplementedException();
        }

        public async Task ReceiveMessage(params MessageDTO[] data)
        {
            var groupedByChat = data.GroupBy(x => x.ChatId).ToDictionary(x => x.Key, y => y.ToList());
            foreach(var chat in groupedByChat)
            {
                foreach(var message in chat.Value)
                {
                    var command = new CreateMessageCommand(chat.Key.Value,message.OwnerUuid, new List<MessageDTO>() { message });
                    var response = await sender.Send(command, CancellationToken.None);
                }
            }
        }

        public Task SendMessage(params MessageDTO[] data)
        {
            throw new NotImplementedException();
        }
    }
}
