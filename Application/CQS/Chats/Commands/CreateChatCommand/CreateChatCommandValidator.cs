using FluentValidation;

namespace Application.CQS.Chats.Commands.CreateChatCommand
{
    public class CreateChatCommandValidator : AbstractValidator<CreateChatCommand>
    {
        public CreateChatCommandValidator()
        {

        }
    }
}
