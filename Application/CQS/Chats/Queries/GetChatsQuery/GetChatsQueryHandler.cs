using Application.Abstractions.Messaging;
using Domain.Entities.Chats;
using Domain.ValueObjects;
using Infrastructure.Abstractions;

namespace Application.CQS.Chats.Queries.GetChatsQuery
{
    internal sealed class GetChatsQueryHandler : IQueryHandler<GetChatsQuery, ICollection<Chat>>
    {
        private readonly IChatRepository _chatRepository;
        public GetChatsQueryHandler(
            IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }
        public async Task<Result<ICollection<Chat>>> Handle(GetChatsQuery request, CancellationToken cancellationToken)
        {
            var data = await _chatRepository.ListAsync();
            if (data == null)
            {
                return Result<ICollection<Chat>>.Failure("chat not found");
            }

            return Result<ICollection<Chat>>.Success(null);
        }
    }
}
