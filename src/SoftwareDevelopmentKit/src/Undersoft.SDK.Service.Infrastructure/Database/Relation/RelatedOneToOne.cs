using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.SDK.Service.Infrastructure.Database.Relation;
public class RelatedOneToOne<TParent, TChild>
    where TParent : class, IOrigin, IInnerProxy
    where TChild : class, IOrigin, IInnerProxy
{
    private readonly string PARENT_TABLE_NAME = typeof(TParent).Name + "s";
    private readonly string CHILD_TABLE_NAME = typeof(TChild).Name + "s";
    private readonly string PARENT_NAME = typeof(TParent).Name;
    private readonly string CHILD_NAME = typeof(TChild).Name.Replace(typeof(TParent).Name, "");
    private readonly string PARENT_SCHEMA = null;
    private readonly string CHILD_SCHEMA = null;

    private readonly ExpandSite _expandSite;
    private readonly ModelBuilder _modelBuilder;
    private readonly EntityTypeBuilder<TParent> _firstBuilder;
    private readonly EntityTypeBuilder<TChild> _secondBuilder;

    public RelatedOneToOne(
        ModelBuilder modelBuilder,
        ExpandSite expandSite = ExpandSite.None,
        string? parentSchema = null
    ) : this(modelBuilder, null, null, null, null, expandSite, parentSchema, parentSchema) { }

    public RelatedOneToOne(
        ModelBuilder modelBuilder,
        string? parentName,
        string? childName,
        ExpandSite expandSite = ExpandSite.None,
        string? parentSchema = null
    )
        : this(
            modelBuilder,
            parentName,
            null,
            childName,
            null,
            expandSite,
            parentSchema,
            parentSchema
        )
    { }

    public RelatedOneToOne(
        ModelBuilder modelBuilder,
        string? parentName,
        string? parentTableName,
        string? childName,
        string? childTableName,
        ExpandSite expandSite = ExpandSite.None,
        string? parentSchema = null,
        string? childSchema = null
    )
    {
        _modelBuilder = modelBuilder;
        _firstBuilder = modelBuilder.Entity<TParent>();
        _secondBuilder = modelBuilder.Entity<TChild>();
        _expandSite = expandSite;

        if (parentTableName != null)
            PARENT_TABLE_NAME = parentTableName;
        if (childTableName != null)
            CHILD_TABLE_NAME = childTableName;
        if (parentName != null)
            PARENT_NAME = parentName;
        if (childName != null)
            CHILD_NAME = childName;
        if (parentSchema != null)
            PARENT_SCHEMA = parentSchema;
        if (childSchema != null)
            CHILD_SCHEMA = childSchema;
    }

    public ModelBuilder Configure(bool autoinclude = false)
    {
        if (PARENT_SCHEMA != null && CHILD_SCHEMA != null)
        {
            _firstBuilder.ToTable(PARENT_TABLE_NAME, PARENT_SCHEMA);
            _secondBuilder.ToTable(CHILD_TABLE_NAME, CHILD_SCHEMA);
        }

        _firstBuilder
            .HasOne<TChild>(CHILD_NAME)
            .WithOne(PARENT_NAME)
            .HasForeignKey<TChild>(PARENT_NAME + "Id");

        _secondBuilder
            .HasOne<TParent>(PARENT_NAME)
            .WithOne(CHILD_NAME)
            .HasForeignKey<TChild>(CHILD_NAME + "Id");

        if (_expandSite != ExpandSite.None)
        {
            if ((_expandSite & (ExpandSite.OnRight | ExpandSite.WithMany)) > 0)
                if (!autoinclude)
                    _firstBuilder.Navigation(CHILD_NAME);
                else
                    _firstBuilder.Navigation(CHILD_NAME).AutoInclude();
            else
                 if (!autoinclude)
                _secondBuilder.Navigation(PARENT_NAME);
            else
                _secondBuilder.Navigation(PARENT_NAME).AutoInclude();
        }

        return _modelBuilder;
    }
}
