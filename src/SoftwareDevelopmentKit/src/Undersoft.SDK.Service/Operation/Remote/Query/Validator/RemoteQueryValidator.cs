using FluentValidation;
using System.Collections;
using System.Linq.Expressions;

namespace Undersoft.SDK.Service.Operation.Remote.Query.Validator;

using Undersoft.SDK;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Store;

public class RemoteQueryValidator<TDto, TModel> : RemoteQueryValidatorBase<RemoteQuery<TDto, TModel>>
    where TDto : class, IOrigin, IInnerProxy
    where TModel : class, IOrigin, IInnerProxy
{
    public RemoteQueryValidator(IServicer servicer) : base(servicer) { }

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

    protected void ValidateRequired(params Expression<Func<RemoteQuery<TDto, TModel>, object>>[] members)
    {
        foreach (var member in members)
        {
            RuleFor(member)
                .NotEmpty()
                .WithMessage($"{member.Parameters.FirstOrDefault().Name} is required!");
        }
    }

    protected void ValidateEmail(params Expression<Func<RemoteQuery<TDto, TModel>, string>>[] members)
    {
        foreach (var member in members)
        {
            RuleFor(member).EmailAddress().WithMessage($"Invalid email address.");
        }
    }

    protected void ValidateLength(
        int min,
        int max,
        params Expression<Func<RemoteQuery<TDto, TModel>, string>>[] members
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
        params Expression<Func<RemoteQuery<TDto, TModel>, ICollection>>[] members
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

    protected void ValidateEnum(params Expression<Func<RemoteQuery<TDto, TModel>, string>>[] members)
    {
        foreach (var member in members)
        {
            RuleFor(member).IsInEnum().WithMessage($"Incorrect enum value");
        }
    }

    protected void ValidateNotEqual(
        object item,
        params Expression<Func<RemoteQuery<TDto, TModel>, object>>[] members
    )
    {
        foreach (var member in members)
        {
            RuleFor(member).NotEqual(item).WithMessage($"value not equal: {item}");
        }
    }

    protected void ValidateEqual(
        object item,
        params Expression<Func<RemoteQuery<TDto, TModel>, object>>[] members
    )
    {
        foreach (var member in members)
        {
            RuleFor(member).Equal(item).WithMessage($"value equal: {item}");
        }
    }

    protected void ValidateLanguage(params Expression<Func<RemoteQuery<TDto, TModel>, object>>[] members)
    {
        foreach (var member in members)
        {
            RuleFor(member)
                .Must(SupportedLanguages.Contains)
                .WithMessage("Agreement language must conform to ISO 639-1.");
        }
    }

    protected void ValidateExist<TStore>(Func<TDto, Expression<Func<TDto, bool>>> query)
        where TStore : IDataServiceStore
    {
        var _repository = _servicer.RemoteSet<TStore, TDto>();

        RuleFor(e => e.Data)
            .MustAsync(
                async (cmd, cancel) =>
                {
                    return await _repository.Exist(query.Invoke(cmd));
                }
            )
            .WithMessage($"{typeof(TDto).Name} does not exists");
    }

    protected void ValidateNotExist<TStore>(Func<TDto, Expression<Func<TDto, bool>>> query)
        where TStore : IDataServiceStore
    {
        var _reposiotry = _servicer.RemoteSet<TStore, TDto>();

        RuleFor(e => e.Data)
            .MustAsync(
                async (cmd, cancel) =>
                {
                    return await _reposiotry.NotExist(query.Invoke(cmd));
                }
            )
            .WithMessage($"{typeof(TDto).Name} already exists");
    }
}
