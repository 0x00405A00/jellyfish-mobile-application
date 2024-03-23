using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.Chats;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Shared.DataTransferObject.Messenger;

namespace Application.CQS.Chats.Queries.GetChatsQuery
{
    internal sealed class GetChatsQueryHandler : IQueryHandler<GetChatsQuery, List<ChatDTO>>
    {
        private readonly IMapper mapper;
        private readonly IChatRepository _chatRepository;
        public GetChatsQueryHandler(
            IMapper mapper,
            IChatRepository chatRepository)
        {
            this.mapper = mapper;
            _chatRepository = chatRepository;
        }
        public async Task<Result<List<ChatDTO>>> Handle(GetChatsQuery request, CancellationToken cancellationToken)
        {
            var data = await _chatRepository.ListAsync();
            if (data == null)
            {
                return Result<List<ChatDTO>>.Failure("no chats existing");
            }
            var mappedData = mapper.Map<List<ChatDTO>>(data);
            return Result<List<ChatDTO>>.Success(mappedData);
        }
    }
}
