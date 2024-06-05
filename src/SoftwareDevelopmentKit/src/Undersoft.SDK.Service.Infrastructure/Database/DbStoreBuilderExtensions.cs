using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.SDK.Service.Infrastructure.Database;

using Data.Entity;
using System.Linq.Expressions;
using Undersoft.SDK;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Identifier;
using Undersoft.SDK.Service.Infrastructure.Database.Relation;

public abstract class EntityTypeMapping<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class
{
    protected ModelBuilder ModelBuilder = default!;

    public virtual void SetBuilder(ModelBuilder modelBuilder)
    {
        this.ModelBuilder = modelBuilder;
    }

    public abstract void Configure(EntityTypeBuilder<TEntity> builder);
}

public static class DbStoreBuilderExtensions
{
    public static ModelBuilder ApplyIdentity<TContext>(this ModelBuilder builder)
    {
        foreach (var type in builder.Model.GetEntityTypes().ToList())
        {
            var clr = type.ClrType;
            if (clr != null && clr.GetInterfaces().Contains(typeof(IEntity)))
            {
                var model = builder.Entity(clr);
                model.HasKey("Id");
                model.HasIndex("Index");
                model.Property("CodeNo").HasMaxLength(32).IsConcurrencyToken(true);
            }
        }
        return builder;
    }

    public static ModelBuilder ApplyMapping<TEntity>(
        this ModelBuilder builder,
        EntityTypeMapping<TEntity> entityBuilder
    ) where TEntity : class
    {
        entityBuilder.SetBuilder(builder);
        builder.ApplyConfiguration(entityBuilder);
        return builder;
    }

    public static ModelBuilder ApplyIdentifiers(this ModelBuilder builder, Type type)
    {
        return new IdentifiersMapping(type, builder).Configure();
    }

    public static ModelBuilder ApplyIdentifiers<TEntity>(this ModelBuilder builder)
        where TEntity : class, IOrigin, IInnerProxy
    {
        return new IdentifiersMapping<TEntity>(builder).Configure();
    }

    public static ModelBuilder RelateSetToSet<TLeft, TRight>(
        this ModelBuilder builder,
        ExpandSite expandSite = ExpandSite.None,
        bool autoinclude = false,
        string? dbSchema = null
    )
        where TLeft : class, IOrigin, IInnerProxy
        where TRight : class, IOrigin, IInnerProxy
    {
        return new RelatedSetToSet<TLeft, TRight>(builder, expandSite, dbSchema).Configure(
            autoinclude
        );
    }

    public static ModelBuilder RelateSetToSet<TLeft, TRight>(
        this ModelBuilder builder,
        Expression<Func<TRight, object?>>? leftMember,
        Expression<Func<TLeft, object?>>? rightMember,
        ExpandSite expandSite = ExpandSite.None,
        bool autoinclude = false,
        string? parentSchema = null,
        string? childSchema = null
    )
        where TLeft : class, IOrigin, IInnerProxy
        where TRight : class, IOrigin, IInnerProxy
    {
        return new RelatedSetToSet<TLeft, TRight>(
            builder,
            leftMember.GetMemberName(),
            rightMember.GetMemberName(),
            expandSite
        ).Configure(autoinclude);
    }

    public static ModelBuilder RelateSetToSet<TLeft, TRight>(
        this ModelBuilder builder,
        Expression<Func<TRight, object?>>? leftMember,
        string? LeftTableName,
        Expression<Func<TLeft, object?>>? rightMember,
        string? rightTableName,
        ExpandSite expandSite = ExpandSite.None,
        bool autoinclude = false,
        string? parentSchema = null,
        string? childSchema = null
    )
        where TLeft : class, IOrigin, IInnerProxy
        where TRight : class, IOrigin, IInnerProxy
    {
        return new RelatedSetToSet<TLeft, TRight>(
            builder,
            leftMember.GetMemberName(),
            LeftTableName,
            rightMember.GetMemberName(),
            rightTableName,
            expandSite,
            parentSchema,
            childSchema
        ).Configure(autoinclude);
    }

    public static ModelBuilder RelateSetToSet<TLeft, TRight>(
        this ModelBuilder builder,
        string? leftName,
        string? rightName,
        ExpandSite expandSite = ExpandSite.None,
        bool autoinclude = false,
        string? dbSchema = null
    )
        where TLeft : class, IOrigin, IInnerProxy
        where TRight : class, IOrigin, IInnerProxy
    {
        return new RelatedSetToSet<TLeft, TRight>(
            builder,
            leftName,
            rightName,
            expandSite,
            dbSchema
        ).Configure(autoinclude);
    }

    public static ModelBuilder RelateSetToSet<TLeft, TRight>(
        this ModelBuilder builder,
        string? leftName,
        string? leftTableName,
        string? rightName,
        string? rightTableName,
        ExpandSite expandSite = ExpandSite.None,
        bool autoinclude = false,
        string? parentSchema = null,
        string? childSchema = null
    )
        where TLeft : class, IOrigin, IInnerProxy
        where TRight : class, IOrigin, IInnerProxy
    {
        return new RelatedSetToSet<TLeft, TRight>(
            builder,
            leftName,
            leftTableName,
            rightName,
            rightTableName,
            expandSite,
            parentSchema,
            childSchema
        ).Configure(autoinclude);
    }

    public static ModelBuilder RalateSetToSetExplicitly<TLeft, TRight>(
        this ModelBuilder builder,
        string leftName,
        string rightName,
        ExpandSite expandSite = ExpandSite.None,
        string? dbSchema = null
    )
        where TLeft : class, IOrigin, IInnerProxy
        where TRight : class, IOrigin, IInnerProxy
    {
        return new RelatedSetToSetExplicitly<TLeft, TRight>(
            builder,
            leftName,
            rightName,
            expandSite,
            dbSchema
        ).Configure();
    }

    public static ModelBuilder RalateSetToSetExplicitly<TLeft, TRight>(
        this ModelBuilder builder,
        Expression<Func<TRight, object?>>? leftMember,
         Expression<Func<TLeft, object?>>? righMember,
        ExpandSite expandSite = ExpandSite.None,
        string? dbSchema = null
    )
        where TLeft : class, IOrigin, IInnerProxy
        where TRight : class, IOrigin, IInnerProxy
    {
        return new RelatedSetToSetExplicitly<TLeft, TRight>(
            builder,
            leftMember.GetMemberName(),
            righMember.GetMemberName(),
            expandSite,
            dbSchema
        ).Configure();
    }

    public static ModelBuilder RalateSetToSetExplicitly<TLeft, TRight>(
        this ModelBuilder builder,
        string? leftName,
        string? leftTableName,
        string? rightName,
        string? rightTableName,
        ExpandSite expandSite = ExpandSite.None,
        string? parentSchema = null,
        string? childSchema = null
    )
        where TLeft : class, IOrigin, IInnerProxy
        where TRight : class, IOrigin, IInnerProxy
    {
        return new RelatedSetToSetExplicitly<TLeft, TRight>(
            builder,
            leftName,
            leftTableName,
            rightName,
            rightTableName,
            expandSite,
            parentSchema,
            childSchema
        ).Configure();
    }

    public static ModelBuilder RelateOneToSet<TLeft, TRight>(
        this ModelBuilder builder,
        ExpandSite expandSite = ExpandSite.None,
        bool autoinclude = false,
        string? dbSchema = null
    )
        where TLeft : class, IOrigin, IInnerProxy
        where TRight : class, IOrigin, IInnerProxy
    {
        return new RelatedOneToSet<TLeft, TRight>(builder, expandSite).Configure(autoinclude);
    }

    public static ModelBuilder RelateOneToSet<TLeft, TRight>(
        this ModelBuilder builder,
        string? leftName,
        string? rightName,
        ExpandSite expandSite = ExpandSite.None,
        bool autoinclude = false,
        string? dbSchema = null
    )
        where TLeft : class, IOrigin, IInnerProxy
        where TRight : class, IOrigin, IInnerProxy
    {
        return new RelatedOneToSet<TLeft, TRight>(
            builder,
            leftName,
            rightName,
            expandSite,
            dbSchema
        ).Configure(autoinclude);
    }

    public static ModelBuilder RelateOneToSet<TLeft, TRight>(
        this ModelBuilder builder,
        Expression<Func<TRight, object?>>? leftMember,
        Expression<Func<TLeft, object?>>? rightMember,
        ExpandSite expandSite = ExpandSite.None,
        bool autoinclude = false,
        string? dbSchema = null
    )
        where TLeft : class, IOrigin, IInnerProxy
        where TRight : class, IOrigin, IInnerProxy
    {
        return new RelatedOneToSet<TLeft, TRight>(
            builder,
            leftMember.GetMemberName(),
            rightMember.GetMemberName(),
            expandSite,
            dbSchema
        ).Configure(autoinclude);
    }

    public static ModelBuilder RelateOneToSet<TLeft, TRight>(
        this ModelBuilder builder,
        string leftName,
        string leftTableName,
        string rightName,
        string rightTableName,
        ExpandSite expandSite = ExpandSite.None,
        bool autoinclude = false,
        string? parentSchema = null,
        string? childSchema = null
    )
        where TLeft : class, IOrigin, IInnerProxy
        where TRight : class, IOrigin, IInnerProxy
    {
        return new RelatedOneToSet<TLeft, TRight>(
            builder,
            leftName,
            leftTableName,
            rightName,
            rightTableName,
            expandSite
        ).Configure(autoinclude);
    }

    public static ModelBuilder RelateOneToOne<TLeft, TRight>(
        this ModelBuilder builder,
        ExpandSite expandSite = ExpandSite.None,
        bool autoinclude = false,
        string dbSchema = null
    )
        where TLeft : class, IOrigin, IInnerProxy
        where TRight : class, IOrigin, IInnerProxy
    {
        return new RelatedOneToOne<TLeft, TRight>(builder, expandSite, dbSchema).Configure(
            autoinclude
        );
    }

    public static ModelBuilder RelateOneToOne<TLeft, TRight>(
        this ModelBuilder builder,
        string? leftName,
        string? rightName,
        ExpandSite expandSite = ExpandSite.None,
        bool autoinclude = false,
        string? dbSchema = null
    )
        where TLeft : class, IOrigin, IInnerProxy
        where TRight : class, IOrigin, IInnerProxy
    {
        return new RelatedOneToOne<TLeft, TRight>(
            builder,
            leftName,
            rightName,
            expandSite,
            dbSchema
        ).Configure(autoinclude);
    }

    public static ModelBuilder RelateOneToOne<TLeft, TRight>(
        this ModelBuilder builder,
        Expression<Func<TRight, object?>>? leftMember,
        Expression<Func<TLeft, object?>>? rightMember,
        ExpandSite expandSite = ExpandSite.None,
        bool autoinclude = false,
        string? dbSchema = null
    )
        where TLeft : class, IOrigin, IInnerProxy
        where TRight : class, IOrigin, IInnerProxy
    {
        return new RelatedOneToOne<TLeft, TRight>(
            builder,
            leftMember.GetMemberName(),
            rightMember.GetMemberName(),
            expandSite,
            dbSchema
        ).Configure(autoinclude);
    }

    public static ModelBuilder RelateOneToOne<TLeft, TRight>(
        this ModelBuilder builder,
        string? leftName,
        string? leftTableName,
        string? rightName,
        string? rightTableName,
        ExpandSite expandSite = ExpandSite.None,
        bool autoinclude = false,
        string? parentSchema = null,
        string? childSchema = null
    )
        where TLeft : class, IOrigin, IInnerProxy
        where TRight : class, IOrigin, IInnerProxy
    {
        return new RelatedOneToOne<TLeft, TRight>(
            builder,
            leftName,
            leftTableName,
            rightName,
            rightTableName,
            expandSite,
            parentSchema,
            childSchema
        ).Configure(autoinclude);
    }
}
