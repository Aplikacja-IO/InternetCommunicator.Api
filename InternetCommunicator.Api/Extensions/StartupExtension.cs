using InternetCommunicator.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;


namespace InternetCommunicator.Api.Extensions
{
    public static class StartupExtension
    {
        public static void RepositoryConfiguration(this IServiceCollection services)
        {
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICompanyUserRepository, CompanyUserRepository>();
            services.AddScoped<IComponentRepository, ComponentRepository>();
            services.AddScoped<IComponentSubscriberRepository, ComponentSubscriberRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IGroupMembershipRepository, GroupMembershipRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IRegisterUserRepository, RegisterUserRepository>();
            services.AddScoped<IGroupMembershipRepository, GroupMembershipRepository>();
        }
    }
}
