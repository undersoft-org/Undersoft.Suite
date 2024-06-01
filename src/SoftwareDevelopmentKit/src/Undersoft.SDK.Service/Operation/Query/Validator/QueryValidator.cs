using FluentValidation;
using System.Collections;
using System.Linq.Expressions;

namespace Undersoft.SDK.Service.Operation.Query.Validator;

using Undersoft.SDK;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Store;

public class QueryValidator<TEntity, TDto> : QueryValidatorBase<Query<TEntity, TDto>>
    where TDto : class, IOrigin, IInnerProxy
    where TEntity : class, IOrigin, IInnerProxy
{
    public QueryValidator(IServicer ultimateService) : base(ultimateService) { }

    protected void ValidationScope<T>(OperationType operationType, Action actions)
    {
        ValidationScope(typeof(T), operationType, actions);
    }

    protected void ValidationScope(Type type, OperationType operationType, Action actions)
    {
        WhenAsync(
            (cmd, cancel) =>
                Task.Run(
                    () =>
                    {
                        return ((int)cmd.OperationType & (int)operationType) > 0
                            && GetType().UnderlyingSystemType.IsAssignableTo(type);
                    },
                    cancel
                ),
            actions
        );
    }

    protected void ValidationScope(OperationType operationType, Action actions)
    {
        WhenAsync(
            (cmd, cancel) =>
                Task.Run(
                    () =>
                    {
                        return ((int)cmd.OperationType & (int)operationType) > 0;
                    },
                    cancel
                ),
            actions
        );
    }

    protected void ValidateRequired(params Expression<Func<Query<TEntity, TDto>, object>>[] members)
    {
        foreach (var member in members)
        {
            RuleFor(member)
                .NotEmpty()
                .WithMessage($"{member.Parameters.FirstOrDefault().Name} is required!");
        }
    }

    protected void ValidateEmail(params Expression<Func<Query<TEntity, TDto>, string>>[] members)
    {
        foreach (var member in members)
        {
            RuleFor(member).EmailAddress().WithMessage($"Invalid email address.");
        }
    }

    protected void ValidateLength(
        int min,
        int max,
        params Expression<Func<Query<TEntity, TDto>, string>>[] members
    )
    {
        foreach (var member in members)
        {
            RuleFor(member)
                .MinimumLength(min)
                .WithMessage($"minimum text rubricCount: {max} characters")
                .MaximumLength(max)
                .WithMessage($"maximum text rubricCount: {max} characters");
        }
    }

    protected void ValidateCount(
        int min,
        int max,
        params Expression<Func<Query<TEntity, TDto>, ICollection>>[] members
    )
    {
        foreach (var member in members)
        {
            RuleFor(member)
                .Must(a => a.Count >= min)
                .WithMessage($"Items count below minimum quantity")
                .Must(a => a.Count <= max)
                .WithMessage($"Items count above maximum quantity");
        }
    }

    protected void ValidateEnum(params Expression<Func<Query<TEntity, TDto>, string>>[] members)
    {
        foreach (var member in members)
        {
            RuleFor(member).IsInEnum().WithMessage($"Incorrect enum value");
        }
    }

    protected void ValidateNotEqual(
        object item,
        params Expression<Func<Query<TEntity, TDto>, object>>[] members
    )
    {
        foreach (var member in members)
        {
            RuleFor(member).NotEqual(item).WithMessage($"value not equal: {item}");
        }
    }

    protected void ValidateEqual(
        object item,
        params Expression<Func<Query<TEntity, TDto>, object>>[] members
    )
    {
        foreach (var member in members)
        {
            RuleFor(member).Equal(item).WithMessage($"value equal: {item}");
        }
    }

    protected void ValidateLanguage(params Expression<Func<Query<TEntity, TDto>, object>>[] members)
    {
        foreach (var member in members)
        {
            RuleFor(member)
                .Must(SupportedLanguages.Contains)
                .WithMessage("Agreement language must conform to ISO 639-1.");
        }
    }

    protected void ValidateExist<TStore>(Func<TEntity, Expression<Func<TEntity, bool>>> command)
        where TStore : IDataServerStore
    {
        var _repository = _servicer.StoreSet<TStore, TEntity>();

        RuleFor(e => e.Data)
            .MustAsync(
                async (cmd, cancel) =>
                {
                    return await _repository.Exist(command.Invoke(cmd));
                }
            )
            .WithMessage($"{typeof(TEntity).Name} does not exists");
    }

    protected void ValidateNotExist<TStore>(Func<TEntity, Expression<Func<TEntity, bool>>> command)
        where TStore : IDataServerStore
    {
        var _reposiotry = _servicer.StoreSet<TStore, TEntity>();

        RuleFor(e => e.Data)
            .MustAsync(
                async (cmd, cancel) =>
                {
                    return await _reposiotry.NotExist(command.Invoke(cmd));
                }
            )
            .WithMessage($"{typeof(TEntity).Name} already exists");
    }
}
