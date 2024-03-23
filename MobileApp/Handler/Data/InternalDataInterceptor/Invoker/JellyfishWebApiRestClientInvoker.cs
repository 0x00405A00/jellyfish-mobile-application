using Presentation.Controls;
using Shared.DataTransferObject.Messenger;
using Shared.Infrastructure.Backend.Api;
using Shared.Infrastructure.Backend.Interceptor.Abstraction;

namespace Infrastructure.Handler.Data.InternalDataInterceptor.Invoker
{
    public class JellyfishWebApiRestClientInvoker : IInternalDataInterceptorApplicationInvoker
    {
        public JellyfishWebApiRestClientInvoker()
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

        public Task ReceiveMessage(params MessageDTO[] data)
        {
            List<MessageDTO> messages =  data.ToList();
            messages.ForEach(message =>
            {
                MainThread.InvokeOnMainThreadAsync(() =>
                {

                    NotificationHandler.ToastNotify(message.Text);
                });
            });
            return Task.CompletedTask;
        }

        public Task SendMessage(params MessageDTO[] data)
        {
            throw new NotImplementedException();
        }
    }
}
