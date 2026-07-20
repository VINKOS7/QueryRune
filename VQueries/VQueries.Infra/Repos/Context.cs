using Microsoft.EntityFrameworkCore;
using Dotseed.Context;
using MediatR;

using VQueries.Infra.Configures;
using VQueries.Domain.Aggregates.Translator;

namespace VQueries.Infra.Repos;

//without event-bus
public class Context : UnitOfWorkContext
{
    public DbSet<Translator> Translators { get; set; }
    public Context(DbContextOptions options, IMediator mediator) : base(options, mediator) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // modelBuilder.ApplyConfiguration(new TranslatorEntityConfiguration());
    }

    public override async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        await base.SaveEntitiesAsync(cancellationToken);

        // await _integrationEventLogService.PublishStoredIntegrationEventsAsync();

        return true;
    }
}