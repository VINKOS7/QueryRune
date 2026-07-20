using VQueries.Api.Services.AFST;

namespace VQueries.Api.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<HttpClient>(); // yes, i know about D from SOLID and it is broken this principle. But is not comers code, maybe fix later
        services.AddScoped<IAFST<IDictionary<char, string>>, AFST<IDictionary<char, string>>>(sp => new AFST<IDictionary<char, string>>(new Dictionary<char, string>()));

        return services;
    }
}