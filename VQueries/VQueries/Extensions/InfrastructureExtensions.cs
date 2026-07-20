using VQueries.Infra.Repos.TranslatorRepo;
using VQueries.Domain.Aggregates.Translator;

namespace VQueries.Api.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ITranslatorRepo, TranslatorRepo>();

        return services;
    }
}
