using ACC.ExternalClients.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ACC.ExternalClients.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAccExternalClients(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<ChatbaseOptions>(config.GetSection(ChatbaseOptions.SectionName));
        services.AddScoped<IChatbaseClient, ChatbaseClient>();
        return services;
    }
}
