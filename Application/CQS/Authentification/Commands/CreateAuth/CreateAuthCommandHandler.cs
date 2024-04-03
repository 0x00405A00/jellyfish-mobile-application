using Application.Abstractions.Messaging;
using Domain.ValueObjects;
using Infrastructure.Api;
using Infrastructure.Authentification;
using Shared.Infrastructure.Backend;

namespace Application.CQS.Authentification.Commands.CreateAuth
{
    internal sealed class CreateAuthCommandHandler : ICommandHandler<CreateAuthCommand, bool>
    {
        private readonly IJellyfishBackendApiDecorator jellyfishBackendApiDecorator;
        private readonly IAuthentificationService authentificationService;
        private readonly ILocalStorageService localStorageService;

        public CreateAuthCommandHandler(
            IAuthentificationService authentificationService,
            ILocalStorageService localStorageService)
        {
            this.jellyfishBackendApiDecorator = jellyfishBackendApiDecorator;
            this.authentificationService = authentificationService;
            this.localStorageService = localStorageService;
        }
        public async Task<Result<bool>> Handle(CreateAuthCommand request, CancellationToken cancellationToken)
        {
            if(String.IsNullOrWhiteSpace(request.Username) || String.IsNullOrWhiteSpace(request.Password))
            {
                //invalid credentials, abort lógin immediantly
                return Result<bool>.Failure("invalid login credentials");
            }
            var result = await authentificationService.Authentificate(request.Username, request.Password,cancellationToken) ;
            if(!result)
            {
                return Result<bool>.Failure("login failed");
            }
            return Result<bool>.Success(true);
        }
    }
}
