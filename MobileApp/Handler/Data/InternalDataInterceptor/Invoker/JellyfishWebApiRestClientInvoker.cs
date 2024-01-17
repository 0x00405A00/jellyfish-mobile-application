using MobileApp.Controls;
using MobileApp.Handler.Data.InternalDataInterceptor.Abstraction;
using Shared.DataTransferObject.Messenger;
using Shared.Infrastructure.Backend.Api;

namespace MobileApp.Handler.Data.InternalDataInterceptor.Invoker
{
    public class JellyfishWebApiRestClientInvoker : IInternalDataInterceptorApplicationInvoker
    {
        private readonly JellyfishBackendApi _jellyfishWebApiRestClient;
        public JellyfishWebApiRestClientInvoker(JellyfishBackendApi jellyfishWebApiRestClient)
        {
            _jellyfishWebApiRestClient = jellyfishWebApiRestClient;
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
