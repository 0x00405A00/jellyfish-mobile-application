using Microsoft.AspNetCore.Components.Authorization;
using Shared.ApiDataTransferObject;
using Shared.ApiDataTransferObject.Object;
using Shared.DataFilter.Presentation;
using Shared.DataTransferObject;
using Shared.Infrastructure.Backend.Api;
using System.Net;

namespace Infrastructure.Api
{
    public class JellyfishBackendApiDecorator : IJellyfishBackendApiDecorator
    {
        private readonly IJellyfishBackendApi jellyfishBackendApi;

        public JellyfishBackendApiDecorator(IJellyfishBackendApi jellyfishBackendApi)
        {
            this.jellyfishBackendApi = jellyfishBackendApi;
        }
        public Task<JellyfishBackendApi.JellyfishBackendApiResponse<UserDTO>> Activate(string base64Token, UserActivationDataTransferModel userActivationDataTransferModel, CancellationToken cancellationToken)
        {
            return jellyfishBackendApi.Activate(base64Token,userActivationDataTransferModel,cancellationToken);
        }

        public void AddErrorHandler(JellyfishBackendApi.ErrorOutputEventHandler eventHandler)
        {
            jellyfishBackendApi.AddErrorHandler(eventHandler);
        }

        public Task<JellyfishBackendApi.JellyfishBackendApiResponse<UserDTO>> AddUser(UserDTO user, CancellationToken cancellationToken)
        {
            return jellyfishBackendApi.AddUser(user,cancellationToken);
        }

        public Task<JellyfishBackendApi.JellyfishBackendApiResponse<List<Guid>>> AssignRoles(UserDTO user, List<RoleDTO> roleDTOs, CancellationToken cancellationToken)
        {
            return jellyfishBackendApi.AssignRoles(user,roleDTOs,cancellationToken);
        }

        public Task<AuthDTO> Authentificate(string userName, string password, CancellationToken cancellationToken)
        {
            return jellyfishBackendApi.Authentificate(userName,password,cancellationToken);
        }

        public Task<JellyfishBackendApi.JellyfishBackendApiResponse<Guid>> ChangePassword(Guid userId, PasswordChangeDTO passwordChangeDTO, CancellationToken cancellationToken)
        {

            return jellyfishBackendApi.ChangePassword(userId,passwordChangeDTO,cancellationToken);
        }

        public Task<bool> ConnectionTest(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<JellyfishBackendApi.JellyfishBackendApiResponse<UserDTO>> DeleteUserProfilePicture(UserDTO userDTO, CancellationToken cancellationToken)
        {
            return DeleteUserProfilePicture(userDTO,cancellationToken);
        }

        public Task<JellyfishBackendApi.JellyfishBackendApiResponse<UserDTO>> EditUser(UserDTO user, CancellationToken cancellationToken)
        {
            return jellyfishBackendApi.EditUser(user,cancellationToken);
        }

        public Task<UserDTO?> GetCurrentUser(AuthenticationState authenticationState, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<JellyfishBackendApi.JellyfishBackendApiResponse<List<RoleDTO>>> GetRoles(CancellationToken cancellationToken)
        {
            return GetRoles(cancellationToken);
        }

        public Task<JellyfishBackendApi.JellyfishBackendApiResponse<UserDTO>> GetUser(Guid userId, CancellationToken cancellationToken)
        {
            return GetUser(userId,cancellationToken);
        }

        public Task<JellyfishBackendApi.JellyfishBackendApiResponse<List<UserDTO>>> GetUsers(SearchParamsBody? searchParamsBody, CancellationToken cancellationToken)
        {
            return GetUsers(searchParamsBody,cancellationToken);
        }

        public Task<JellyfishBackendApi.JellyfishBackendApiResponse<List<UserDTO>>> GetUsers(string searchText, int selectedPage, int maxItemsPerPage, bool filterOnlyDeletedUsers, CancellationToken cancellationToken)
        {
            return GetUsers(searchText,selectedPage,maxItemsPerPage,filterOnlyDeletedUsers,cancellationToken);
        }

        public Task<JellyfishBackendApi.JellyfishBackendApiResponse<List<UserTypeDTO>>> GetUserTypes(CancellationToken cancellationToken)
        {
            return GetUserTypes(cancellationToken);
        }

        public Task<HttpStatusCode> Logout(CancellationToken cancellationToken)
        {
            return Logout(cancellationToken);
        }

        public Task<AuthDTO> RefreshAuthentification(string token, string refreshToken, CancellationToken cancellationToken)
        {
            return RefreshAuthentification(token,refreshToken,cancellationToken);
        }

        public Task<JellyfishBackendApi.JellyfishBackendApiResponse<UserDTO>> Register(RegisterUserDTO registerDataTransferModel, CancellationToken cancellationToken)
        {
            return Register(registerDataTransferModel,cancellationToken);
        }

        public Task<WebApiHttpRequestResponseModel<ApiDataTransferObject<UserDTO>>> Register(ApiDataTransferObject<RegisterUserDTO> registerDataTransferModel, CancellationToken cancellationToken)
        {
            return Register(registerDataTransferModel,cancellationToken);
        }

        public Task<JellyfishBackendApi.JellyfishBackendApiResponse<Guid>> RemoveUser(UserDTO user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<WebApiHttpRequestResponseModel<T1>> Request<T1, T2>(string url, global::RestSharp.Method method, CancellationToken cancellationToken, T2 bodyObject, List<KeyValuePair<string, string>> query = null, List<KeyValuePair<string, string>> headers = null, bool donttryagain = true)
        {
            return Request<T1, T2>(url,method,cancellationToken,bodyObject,query,headers,donttryagain);
        }

        public Task<global::RestSharp.RestResponse> Request<T2>(string url, global::RestSharp.Method method, CancellationToken cancellationToken, object bodyObject = null, List<KeyValuePair<string, string>> query = null, List<KeyValuePair<string, string>> headers = null, bool donttryagain = true)
        {
            return Request<T2>(url,method,cancellationToken,bodyObject,query,headers,donttryagain);
        }

        public Task<JellyfishBackendApi.JellyfishBackendApiResponse<bool>> ResetPassword(PasswordResetDataTransferModel passwordResetDataTransferModel, CancellationToken cancellationToken)
        {
            return ResetPassword(passwordResetDataTransferModel,cancellationToken);
        }

        public Task<JellyfishBackendApi.JellyfishBackendApiResponse<bool>> ResetPasswordRequest(PasswordResetRequestDTO passwordResetRequestDTO, CancellationToken cancellationToken)
        {
            return ResetPasswordRequest(passwordResetRequestDTO,cancellationToken);
        }

        public Task<JellyfishBackendApi.JellyfishBackendApiResponse<List<Guid>>> RevokeRoles(UserDTO user, List<RoleDTO> roleDTOs, CancellationToken cancellationToken)
        {
            return RevokeRoles(user,roleDTOs,cancellationToken);
        }

        public Task<JellyfishBackendApi.JellyfishBackendApiResponse<T2>> TypedRequest<T, T2>(string url, global::RestSharp.Method method, T? data, CancellationToken cancellationToken, PaginationBase paginationBase = null)
        {
            return TypedRequest<T, T2>(url,method,data,cancellationToken,paginationBase);
        }

        public Task<JellyfishBackendApi.JellyfishBackendApiResponse<UserDTO>> UpdateUserProfilePicture(UserDTO user, CancellationToken cancellationToken)
        {
            return UpdateUserProfilePicture(user, cancellationToken) ;
        }
    }
}
