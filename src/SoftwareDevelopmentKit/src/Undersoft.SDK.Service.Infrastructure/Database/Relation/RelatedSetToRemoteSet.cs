using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.SDK.Service.Infrastructure.Database.Relation;

using Undersoft.SDK.Service.Data.Object;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Data.Remote;
using Undersoft.SDK.Proxies;

public class RelatedSetToRemoteSet<TLeft, TRight>
    where TLeft : class, IOrigin, IInnerProxy
    where TRight : class, IOrigin, IInnerProxy
{
    private readonly string RELATION_TABLE_NAME;
    private readonly string LEFT_TABLE_NAME = typeof(TLeft).Name + "s";
    private readonly string RIGHT_TABLE_NAME = typeof(TRight).Name + "s";
    private readonly string LEFT_NAME = typeof(TLeft).Name + "s";
    private readonly string RIGHT_NAME = typeof(TRight).Name.Replace(typeof(TLeft).Name, "") + "s";
    private readonly string LEFT_SCHEMA = null;
    private readonly string RIGHT_SCHEMA = null;

    private readonly ExpandSite _expandSite;
    private readonly ModelBuilder _modelBuilder;
    private readonly EntityTypeBuilder<TLeft> _firstBuilder;
    private readonly EntityTypeBuilder<TRight> _secondBuilder;
    private readonly EntityTypeBuilder<RemoteLink<TLeft, TRight>> _relationBuilder;

    public RelatedSetToRemoteSet(
        ModelBuilder modelBuilder,
        ExpandSite expandSite = ExpandSite.None,
        string dbSchema = null
    ) : this(modelBuilder, null, null, expandSite, dbSchema, dbSchema) { }

    public RelatedSetToRemoteSet(
        ModelBuilder modelBuilder,
        string leftName,
        ExpandSite expandSite = ExpandSite.None,
        string dbSchema = null
    ) : this(modelBuilder, leftName, null, expandSite, dbSchema, dbSchema)
    { }

    public RelatedSetToRemoteSet(
        ModelBuilder modelBuilder,
        string leftName,
        string leftTableName,
        ExpandSite expandSite = ExpandSite.None,
        string parentSchema = null,
        string childSchema = null
    )
    {
        _modelBuilder = modelBuilder;
        _firstBuilder = _modelBuilder.Entity<TLeft>();
        _secondBuilder = _modelBuilder.Entity<TRight>();
        _relationBuilder = _modelBuilder.Entity<RemoteLink<TLeft, TRight>>();
        _expandSite = expandSite;

        if (leftTableName != null)
            LEFT_TABLE_NAME = leftTableName;
        if (leftName != null)
            LEFT_NAME = leftName;
        if (parentSchema != null)
            LEFT_SCHEMA = parentSchema;

        RELATION_TABLE_NAME = LEFT_NAME;
    }

    public ModelBuilder Configure()
    {
        _relationBuilder.ToTable(RELATION_TABLE_NAME, DataStoreSchema.RelationSchema);
        _relationBuilder.HasKey(k => new { k.LeftEntityId, k.RightEntityId });

        _relationBuilder
            .HasOne(a => a.LeftEntity)
            .WithMany(RELATION_TABLE_NAME)
            .HasForeignKey(a => a.LeftEntity);

        _firstBuilder
            .HasMany<RemoteLink<TLeft, TRight>>(RELATION_TABLE_NAME)
            .WithOne(p => p.LeftEntity)
            .HasForeignKey(k => k.LeftEntityId);

        _firstBuilder.Navigation(RELATION_TABLE_NAME);

        return _modelBuilder;
    }
}
