using Infrastructure.Handler.Data.InternalDataInterceptor.Abstraction;
using Infrastructure.Handler.Data.InternalDataInterceptor.Invoker;
using Shared.DataTransferObject.Messenger;
#if ANDROID
using Infrastructure.Handler.Data.InternalDataInterceptor.Invoker.Notification.Android;
#else
#endif

namespace Infrastructure.Handler.Data.InternalDataInterceptor
{
    public class InternalDataInterceptorApplication : IInternalDataInterceptorApplication
    {
        private List<IInternalDataInterceptorApplicationInvoker> _interceptors;
        public List<IInternalDataInterceptorApplicationInvoker> Invoker { get => _interceptors; private set => _interceptors = value; }

        List<IInternalDataInterceptorApplicationInvoker> IInternalDataInterceptorApplication.Invoker => throw new NotImplementedException();

        public InternalDataInterceptorApplication()
        {
            Invoker = new List<IInternalDataInterceptorApplicationInvoker>();  

        }

        public void Add(IInternalDataInterceptorApplicationInvoker invoker)
        {
            if ((Invoker.Find(x => x.Equals(invoker)) != null))
                return;
            Invoker.Add(invoker);
        }
        public void Remove(IInternalDataInterceptorApplicationInvoker invoker)
        {
            if (Invoker.Find(x=> x.Equals(invoker)) == null)
                return;
            Invoker.Remove(invoker);
        }
        public IInternalDataInterceptorApplicationInvoker Get<T>() where T : IInternalDataInterceptorApplicationInvoker
        {
            var foundItem = Invoker.Find(x => x.GetType() == typeof(T));
            return foundItem;
        }
        private async Task<DataInterceptorApplicationInvokerResponseModel> ExecAction<T>(Func<T[], Task> func, T[] param) where T : class, new()
        {
            DataInterceptorApplicationInvokerResponseModel dataInterceptorApplicationInvokerResponseModel = new DataInterceptorApplicationInvokerResponseModel();
            dataInterceptorApplicationInvokerResponseModel.Start();
            try
            {
                await func(param);

                dataInterceptorApplicationInvokerResponseModel.IsSuccess = true;
            }
            catch (Exception ex)
            {

                dataInterceptorApplicationInvokerResponseModel.Exception = ex;
            }
            dataInterceptorApplicationInvokerResponseModel.Stop();
            return dataInterceptorApplicationInvokerResponseModel;
        }
        #region BusinessContext

        public async Task<InternalDataInterceptorApplicationInvokeResponseModel> ReceiveMessage(params MessageDTO[] data)
        {
            var response = new InternalDataInterceptorApplicationInvokeResponseModel(Invoker);
            foreach (var item in Invoker)
            {
                response.ExecResponseDictionary[item] = await ExecAction<MessageDTO>(item.ReceiveMessage, data);
            }
            return response;
        }

        public async Task<InternalDataInterceptorApplicationInvokeResponseModel> SendMessage(params MessageDTO[] data)
        {
            var response = new InternalDataInterceptorApplicationInvokeResponseModel(Invoker);
            foreach (var item in Invoker)
            {
                response.ExecResponseDictionary[item] = await ExecAction<MessageDTO>(item.SendMessage, data);
            }
            return response;
        }

        public async Task<InternalDataInterceptorApplicationInvokeResponseModel> CreateFriendRequest(params UserFriendshipRequestDTO[] data)
        {
            var response = new InternalDataInterceptorApplicationInvokeResponseModel(Invoker);
            foreach (var item in Invoker)
            {
                response.ExecResponseDictionary[item] = await ExecAction<UserFriendshipRequestDTO>(item.CreateFriendRequest,data);
            }
            return response;
        }
        public async Task<InternalDataInterceptorApplicationInvokeResponseModel> ReceiveAcceptFriendRequest(params MessengerUserDTO[] data)
        {
            var response = new InternalDataInterceptorApplicationInvokeResponseModel(Invoker);
            foreach (var item in Invoker)
            {
                response.ExecResponseDictionary[item] = await ExecAction<MessengerUserDTO>(item.ReceiveAcceptFriendRequest, data);
            }
            return response;
        }

        public async Task<InternalDataInterceptorApplicationInvokeResponseModel> ReceiveFriendRequest(params UserFriendshipRequestDTO[] data)
        {
            var response = new InternalDataInterceptorApplicationInvokeResponseModel(Invoker);
            foreach (var item in Invoker)
            {
                response.ExecResponseDictionary[item] = await ExecAction<UserFriendshipRequestDTO>(item.ReceiveFriendRequest, data);
            }
            return response;
        }


        #endregion

    }
}
