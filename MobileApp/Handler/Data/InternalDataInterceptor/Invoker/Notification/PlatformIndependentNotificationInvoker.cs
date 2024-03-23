using Shared.DataTransferObject.Messenger;
using Shared.Infrastructure.Backend.Interceptor.Abstraction;

namespace Infrastructure.Handler.Data.InternalDataInterceptor.Invoker.Notification
{
    public class PlatformIndependentNotificationInvoker : IInternalDataInterceptorApplicationInvoker
    {
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

        public Task ReceiveMessage(params MessageDTO[] data)
        {
            throw new NotImplementedException();
        }

        public Task SendMessage(params MessageDTO[] data)
        {
            throw new NotImplementedException();
        }
    }
}
