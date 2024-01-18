using Infrastructure.Handler.Data.InternalDataInterceptor.Abstraction;
using Shared.DataTransferObject.Messenger;

namespace Infrastructure.Handler.Data.InternalDataInterceptor.Invoker
{
    public class SqlLiteDatabaseHandlerInvoker : IInternalDataInterceptorApplicationInvoker
    {
        public SqlLiteDatabaseHandlerInvoker() 
        {
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
            foreach (var dataItem in data)
            {
                try
                {
                    
                }
                catch (Exception ex)
                {

                }

            }
        }

        public Task SendMessage(params MessageDTO[] data)
        {
            throw new NotImplementedException();
        }
    }
}
