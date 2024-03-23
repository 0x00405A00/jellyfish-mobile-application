using Application.CQS.Messenger.Chat.Command.CreateMessage;
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
            /*var command = new CreateMessageCommand(data);
            await sender.Send(command);*/
        }

        public Task SendMessage(params MessageDTO[] data)
        {
            throw new NotImplementedException();
        }
    }
}
