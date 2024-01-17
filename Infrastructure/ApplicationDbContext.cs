﻿using Domain.Entities.Auths;
using Domain.Entities.Chats;
using Domain.Entities.Mails;
using Domain.Entities.Messages;
using Domain.Entities.Roles;
using Domain.Entities.Users;
using Domain.Primitives;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shared.Infrastructure.EFCore;
using Shared.Infrastructure.EFCore.Converter;
using Shared.Infrastructure.EFCore.Interceptors;

namespace Infrastructure
{
    public class ApplicationDbContext: DbContext
    {
        #region Const
        public const string ConnectionStringAlias = "JellyfishSqlLiteDatabase";
        public static string ConnectionString = "Data Source=Database.db";//hardcoded connection string, because cli tool dotnet ef migrations/database cant consume IConfiguration-Service from DI

        #endregion
        #region Consumed DI Services
        public DbSaveChangesInterceptor DbContextAuditLogInterceptor { get; }
        private readonly IConfiguration _configuration;
        private ILogger<ApplicationDbContext> _logger;

        public DbContextOptions<ApplicationDbContext> Options { get; }
        #endregion
        #region DbSets
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserHasRelationToRole> UserHasRelationToRoles { get; set; }
        public DbSet<FriendshipRequest> FriendshipRequests { get; set; }
        public DbSet<UserHasRelationToFriend> UserFriends { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Auth> Auths { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatRelationToUser> ChatRelationToUsers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageOutbox> MessageOutboxes { get; set; }
        public DbSet<EmailSendingType> EmailTypes { get; set; }
        public DbSet<MailOutbox> MailOutboxes { get; set; }
        public DbSet<MailOutboxRecipient> MailOutboxRecipients { get; set; }
        public DbSet<MailOutboxAttachment> MailOutboxAttachments { get; set; }
        #endregion
        #region Ctor

        public ApplicationDbContext(
                IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Options = options;
        }
        public ApplicationDbContext(
                IConfiguration configuration, DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Options = options;
            _configuration = configuration;
        }
        public ApplicationDbContext(
                IConfiguration configuration, DbSaveChangesInterceptor dbContextAuditLogInterceptor)
        {
            _configuration = configuration;
            DbContextAuditLogInterceptor = dbContextAuditLogInterceptor;
        }

        public ApplicationDbContext(
                IConfiguration configuration, DbContextOptions<ApplicationDbContext> options, DbSaveChangesInterceptor dbContextAuditLogInterceptor)
            : base(options)
        {
            _configuration = configuration;
            Options = options;
            DbContextAuditLogInterceptor = dbContextAuditLogInterceptor;
        }
        #endregion
        #region Model
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        {
            if(_configuration is not null)
            {
                ConnectionString = _configuration.GetConnectionString(ConnectionStringAlias);
            }
            var loggerFactory = LoggerFactory.Create(x=>x.AddConsole());
            _logger = loggerFactory.CreateLogger<ApplicationDbContext>();
            optionsBuilder.UseSqlite(ConnectionString);
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
            optionsBuilder.EnableSensitiveDataLogging(true);
            optionsBuilder.AddInterceptors(new DatabaseReaderInterceptor());
            //optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

#warning Before any start, check if u changed the entity structure in IEntityConfiguration classes. When any changes are done, please migrate to database 
#warning Documentation: https://learn.microsoft.com/de-de/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //pay attention to dependency order (chicken egg problem)

            //Initial Migration: PS C:\Users\Mika\source\repos\jellyfish-backend-ddd\Presentation> dotnet ef migrations add InitialCreate --context ApplicationDbContext
            //Current Problem: A key cannot be configured on 'Entity<Identification>' because it is a derived type.The key must be configured on the root type 'Entity'.If you did not intend for 'Entity' to be included in the model, ensure that it is not referenced by a DbSet property on your context, referenced in a configuration call to ModelBuilder, or referenced from a navigation on a type that is included in the model.

            _logger.LogInformation($"try to create database entities...");
            modelBuilder.CreateModels();
            _logger.LogInformation($"database entities created");
            _logger.LogInformation($"try to create database functions...");
            modelBuilder.CreateDbFunctions();
            _logger.LogInformation($"database functions created");
            base.OnModelCreating(modelBuilder);
            _logger.LogInformation($"try to data seed initial data...");
            modelBuilder.CreateInitialDataSeed();
            _logger.LogInformation($"initial data seed created");
            _logger.LogWarning($"!!! BE CAREFULL IN PRODUCTION WITH SAMPLE DATA !!!");
            _logger.LogWarning($"try to add sample data...");
            modelBuilder.CreateSampleData();
            _logger.LogWarning($"sample data is added");
            _logger.LogWarning($"!!! BE CAREFULL IN PRODUCTION WITH SAMPLE DATA !!!");
            _logger.LogWarning($"try to add owned types...");
            modelBuilder.CreateOwnedTypes();
            _logger.LogWarning($"owned types are added");

            //modelBuilder.ApplyConfigurationsFromAssembly ignore any order so dependencies which are order depend could not be created (app runs in exception)
            //Data seeding: Schema initial data with: https://learn.microsoft.com/de-de/ef/core/modeling/data-seeding
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .Properties<CustomDateTime>()
                .HaveConversion<CustomDateTimeConverter>();

            configurationBuilder
                .Properties<UserId>()
                .HaveConversion<UserIdConverter>();

            configurationBuilder
                .Properties<UserTypeId>()
                .HaveConversion<UserTypeIdConverter>();

            configurationBuilder
                .Properties<ChatId>()
                .HaveConversion<ChatIdConverter>();

            configurationBuilder
                .Properties<MessageId>()
                .HaveConversion<MessageIdConverter>();

            configurationBuilder
                .Properties<RoleId>()
                .HaveConversion<RoleIdConverter>();

            configurationBuilder
                .Properties<PhoneNumber>()
                .HaveConversion<PhoneNumberConverter>();

            configurationBuilder
                .Properties<Email>()
                .HaveConversion<EmailConverter>();
        }
        #endregion
    }
}
