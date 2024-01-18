﻿using Domain.Entities.Users;
using Infrastructure.Repository.Primitives;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Infrastructure.Abstractions;

namespace Infrastructure.Repository.Concrete
{
    internal class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<List<FriendshipRequest>> GetAllFriendshipRequests(Expression<Func<User, bool>> expression)
        {
            //Will be optimized in future, because GetAsync generates too much not useful overhead (e.g. roles etc.) that are not used in this operation or operation related processes
            var value = await GetAsync(expression);
            return value.FriendshipRequestsWhereIamRequester.ToList();
        }

        /*public static Func<ApplicationDbContext, Expression<Func<DatabaseEntity.User, bool>>, DatabaseEntity.User> GetUserCompiledQuery =
EF.CompileQuery((ApplicationDbContext context, Expression<Func<DatabaseEntity.User, bool>> expression) =>
   context.Users
          .Include(i => i.UserTypeUu)
          .Include(i => i.UserRelationToRoles)
              .ThenInclude(ur => ur.RoleUu)
          .Include(i => i.UserFriendUserUus)
              .ThenInclude(uf => uf.FriendUserUu)
                  .ThenInclude(ut => ut.UserTypeUu)
          .AsNoTracking()
          .Where(expression)
          .FirstOrDefault());*/

        public async override Task<User> GetAsync(Expression<Func<User, bool>> expression)
        {
            var value = await DbSet
                                    .Include(i => i.UserType)
                                    .Include(i => i.UserHasRelationToRoles)
                                    .ThenInclude(ur => ur.Role)
                                    .Include(x => x.FriendshipsThatIAccepted)
                                        .ThenInclude(x => x.UserFriend)
                                    .Include(x => x.FriendshipsThatIRequested)
                                        .ThenInclude(x => x.User)
                                    .Include(i => i.FriendshipRequestsWhereIamRequester)//all request that user requested to other users
                                            .ThenInclude(tu => tu.TargetUser)
                                    .Include(i => i.FriendshipRequestsWhereIamTarget)//all request that user requested to other users
                                            .ThenInclude(tu => tu.RequesterUser)
                                    .AsNoTracking()
                                    .AsSingleQuery()
                                    .FirstOrDefaultAsync(expression);
            return value;
        }

        public async Task<bool> IsEmailAlreadyInUse(string email)
        {
            return await DbSet.AsNoTracking()
                         .Where(x => x.Email.EmailValue == email.ToLower())
                         .AnyAsync();
        }

        public async Task<bool> IsPhoneAlreadyInUse(string phone)
        {
            return await DbSet.AsNoTracking()
                         .Where(x => x.Phone.PhoneNumb == phone.ToLower())
                         .AnyAsync();
        }

        public async Task<bool> IsUserRegistered(string email)
        {
            return await DbSet.AsNoTracking()
                         .Where(x => x.Email.EmailValue == email.ToLower() && x.ActivationDateTime != null)
                         .AnyAsync();
        }

        public async override Task<ICollection<User>> ListAsync(Expression<Func<User, bool>> expression)
        {
            var value = await DbSet.ExpressionQuery(expression)
                                    .Include(i => i.UserType)
                                    .Include(i => i.UserHasRelationToRoles)
                                    .ThenInclude(ur => ur.Role)
                                    .Include(x => x.FriendshipsThatIAccepted)
                                        .ThenInclude(x => x.UserFriend)
                                    .Include(x => x.FriendshipsThatIRequested)
                                        .ThenInclude(x => x.User)
                                    .Include(i => i.FriendshipRequestsWhereIamRequester)//all request that user requested to other users
                                            .ThenInclude(tu => tu.TargetUser)
                                    .Include(i => i.FriendshipRequestsWhereIamTarget)//all request that user requested to other users
                                            .ThenInclude(tu => tu.RequesterUser)
                                    .AsNoTracking()
                                    .AsSingleQuery()
                                    .ToListAsync();
            return value;
        }
        public override async Task<RepositoryResponse<ICollection<User>>> ListAsyncWithMeta(Expression<Func<User, bool>> expression = null)
        {
            int count = await CountMaxAsync(expression);
            var value = await DbSet.ExpressionQuery(expression)
                                    .Include(i => i.UserType)
                                    .Include(i => i.UserHasRelationToRoles)
                                    .ThenInclude(ur => ur.Role)
                                    .Include(x => x.FriendshipsThatIAccepted)
                                        .ThenInclude(x => x.UserFriend)
                                    .Include(x => x.FriendshipsThatIRequested)
                                        .ThenInclude(x => x.User)
                                    .Include(i => i.FriendshipRequestsWhereIamRequester)//all request that user requested to other users
                                            .ThenInclude(tu => tu.TargetUser)
                                    .Include(i => i.FriendshipRequestsWhereIamTarget)//all request that user requested to other users
                                            .ThenInclude(tu => tu.RequesterUser)
                                    .AsNoTracking()
                                    .AsSingleQuery()
                                    .ToListAsync();

            var data = value;
            var meta = new Primitives.Meta { TotalItems = count };

            return new RepositoryResponse<ICollection<User>>(data, ref meta);
        }
    }
}
