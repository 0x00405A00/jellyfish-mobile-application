﻿using Application.Abstractions.Messaging;
using Domain.ValueObjects;
using Domain.Entities.Chats;
using Infrastructure.Abstractions;
using AutoMapper;
using Domain.Extension;
using Domain.Primitives.Ids;
using Shared.DataTransferObject.Messenger;
using Shared.MimePart;
using Domain.Errors;

namespace Application.CQS.Chats.Commands.CreateChatCommand
{
    internal sealed class CreateChatCommandHandler : ICommandHandler<CreateChatCommand, ChatDTO>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public CreateChatCommandHandler(
            IMapper mapper,
            IChatRepository chatRepository,
            IUserRepository userRepository,
            IMediaService mediaService)
        {
            _mapper = mapper;
            _chatRepository = chatRepository;
            this._userRepository = userRepository;
            this.mediaService = mediaService;
        }


        public async Task<Result<ChatDTO>> Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            var createdBy = await _userRepository.GetAsync(x => x.Id == request.ChatOwnerUuid.ToIdentification<UserId>());
            ICollection<Domain.Entities.Users.User> members = null;
            List<ChatRelationToUser> chatMembers = new List<ChatRelationToUser>();

            if (!request.Members.Contains(request.ChatOwnerUuid))
                request.Members.Add(request.ChatOwnerUuid);

            var memberIds = request.Members.ToIdentification<UserId>();

            members = await _userRepository.ListAsync(x => memberIds.Contains(x.Id));
            Result<ChatDTO> result = null;
            if (members != null && members.Count != memberIds.Count)
            {
                List<Error> errors = new List<Error>();
                foreach (var memberId in memberIds)
                {
                    var foundMemberInDb = members.Any(x => x.Id == memberId);
                    if (!foundMemberInDb)
                    {
                        var err = new Error($"{memberId} not found", Error.ERROR_CODE.BadRequest);
                        errors.Add(err);
                    }
                }
                result = Result<ChatDTO>.Failure("chat member not found", errors);
                return result;
            }
            Domain.Entities.Chats.Chat chat = null;
            try
            {
                var chatId = Domain.Entities.Chats.Chat.NewId();

                foreach (var member in members)
                {
                    bool isAdmin = request.ChatOwnerUuid == member.Id.ToGuid();
                    var chatMember = ChatRelationToUser.Create(
                        ChatRelationToUser.NewId(),
                        member.Id,
                        chatId,
                        isAdmin,
                        DateTime.UtcNow.ToTypedDateTime(),
                        createdBy.Id,
                        null,
                        null,
                        null,
                        null);
                    chatMembers.Add(chatMember);
                }
                Picture? picture = null;
                if (!String.IsNullOrWhiteSpace(request.Picture))
                {
                    byte[] pictureBinary = Convert.FromBase64String(request.Picture);
                    var path = mediaService.CreateChatPicture(chatId.ToGuid(), MimeExtension.GetFileExtension(request.PictureMimeType), pictureBinary, cancellationToken);
                    picture = Picture.Parse(pictureBinary, path, request.PictureMimeType);
                }

                chat = Domain.Entities.Chats.Chat.Create(
                    chatId,
                    request.ChatName,
                    request.ChatDescription,
                    picture,
                    chatMembers,
                    DateTime.UtcNow.ToTypedDateTime(),
                    createdBy.Id,
                    null,
                    null,
                    null,
                    null);

                chat.SetCreated(createdBy.Id);

            }
            catch (Exception ex)
            {
                return Result<ChatDTO>.Failure("chat create failed");
            }
            _chatRepository.Add(chat);
            var dto = _mapper.Map<ChatDTO>(chat);
            return Result<ChatDTO>.Success(dto);
        }

    }
}
