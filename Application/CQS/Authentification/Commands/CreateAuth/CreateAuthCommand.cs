using Application.Abstractions.Messaging;

namespace Application.CQS.Authentification.Commands.CreateAuth
{
    public record CreateAuthCommand(string Username,string Password) : ICommand<bool>
    {
    }
}
