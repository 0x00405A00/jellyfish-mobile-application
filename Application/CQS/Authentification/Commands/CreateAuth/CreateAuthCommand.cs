using Application.Abstractions.Messaging;
using Domain.ValueObjects;
using Infrastructure.Api;
using Infrastructure.Storage;
using Shared.DataTransferObject;
using Shared.Infrastructure.Backend;

namespace Application.CQS.Authentification.Commands.CreateAuth
{
    public record CreateAuthCommand(string Username,string Password) : ICommand<bool>
    {
    }
    internal sealed class CreateAuthCommandHandler : ICommandHandler<CreateAuthCommand, bool>
    {
        private readonly IJellyfishBackendApiDecorator jellyfishBackendApiDecorator;
        private readonly ILocalStorageService localStorageService;

        public CreateAuthCommandHandler(
            IJellyfishBackendApiDecorator jellyfishBackendApiDecorator,
            ILocalStorageService localStorageService)
        {
            this.jellyfishBackendApiDecorator = jellyfishBackendApiDecorator;
            this.localStorageService = localStorageService;
        }
        public async Task<Result<bool>> Handle(CreateAuthCommand request, CancellationToken cancellationToken)
        {
            if(String.IsNullOrWhiteSpace(request.Username) || String.IsNullOrWhiteSpace(request.Password))
            {
                //invalid credentials, abort lógin immediantly
                return Result<bool>.Failure("invalid login credentials");
            }
            
            return Result<bool>.Success(true);
        }
    }
}
