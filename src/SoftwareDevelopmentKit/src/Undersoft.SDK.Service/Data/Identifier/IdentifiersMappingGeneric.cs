using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.SDK.Service.Data.Identifier;

using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Store;

public class IdentifiersMapping<TObject> : IIdentifiersMapping where TObject : class, IOrigin, IInnerProxy
{
    private string TABLE_NAME = typeof(TObject).Name + "Identifiers";
    private readonly ModelBuilder _modelBuilder;
    private readonly EntityTypeBuilder<TObject> _entityBuilder;
    private readonly EntityTypeBuilder<Identifier<TObject>> _identifierBuilder;

    public IdentifiersMapping(ModelBuilder builder)
    {
        _modelBuilder = builder;
        _entityBuilder = _modelBuilder.Entity<TObject>();
        _identifierBuilder = _modelBuilder.Entity<Identifier<TObject>>();
    }

    public ModelBuilder Configure()
    {
        _identifierBuilder.ToTable(TABLE_NAME, DataStoreSchema.IdentifierSchema);

        _identifierBuilder.HasIndex(k => k.Key);
        _identifierBuilder.HasIndex(k => k.ObjectId);

        _identifierBuilder.HasOne(a => a.Object)
                          .WithMany("Identifiers")
                          .HasForeignKey(k => k.ObjectId)
                          .IsRequired()
                          .OnDelete(DeleteBehavior.Restrict);

        _entityBuilder.HasMany("Identifiers")
                      .WithOne(nameof(Identifier<TObject>.Object))
                      .HasForeignKey(nameof(Identifier<TObject>.ObjectId))
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Restrict);

        _entityBuilder.Navigation("Identifiers").AutoInclude();

        return _modelBuilder;
    }
}