using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VQueries.Domain.Aggregates.Translator;

namespace VQueries.Infra.Configures;

public class TranslatorEntityConfiguration : IEntityTypeConfiguration<Translator>
{
    public void Configure(EntityTypeBuilder<Translator> builder)
    {
        //maybe OwnsMany better -- one translator - many runes alphs
        builder.OwnsOne(b => b.Runes);
        // for relatoinsships between tables
        throw new NotImplementedException();
    }
}
