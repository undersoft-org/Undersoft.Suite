using FluentValidation;
using System.Collections;
using System.Linq.Expressions;

namespace Undersoft.SDK.Service.Application.GUI.View;

using Undersoft.SDK;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Data.Store;

public class ViewValidator<TModel> : AbstractValidator<IViewData<TModel>>, IValidator<IViewData<TModel>> where TModel : class, IOrigin, IInnerProxy
{
    protected static readonly string[]? SupportedLanguages;

    protected IServicer _servicer;

    public ViewValidator(IServicer servicer)
    {
        _servicer = servicer;
    }

    protected void ValidationScope<T>(OperationType commandMode, Action actions)
    {
        ValidationScope(typeof(T), commandMode, actions);
    }

    protected void ValidationScope(Type type, OperationType commandMode, Action actions)
    {
        WhenAsync(
            (cmd, cancel) =>
                Task.Run(
                    () =>
                    {
                        return ((int)cmd.Operation & (int)commandMode) > 0
                            && GetType().UnderlyingSystemType.IsAssignableTo(type);
                    },
                    cancel
                ),
            actions
        );
    }

    protected void ValidationScope(OperationType GenericDataMode, Action actions)
    {
        WhenAsync(
            (cmd, cancel) =>
                Task.Run(
                    () =>
                    {
                        return ((int)cmd.Operation & (int)GenericDataMode) > 0;
                    },
                    cancel
                ),
            actions
        );
    }

    protected void ValidateRequired(params Expression<Func<IViewData<TModel>, object?>>[] members)
    {
        foreach (var member in members)
        {
            RuleFor(member)
                .NotEmpty()
                .WithMessage($"{member.GetMemberName()} is required!");
        }
    }

    protected void ValidateEmail(params Expression<Func<IViewData<TModel>, string?>>[] members)
    {
        foreach (var member in members)
        {
            RuleFor(member).EmailAddress().WithMessage($"Invalid email address!");
        }
    }

    protected void ValidateLength(
        int min,
        int max,
        params Expression<Func<IViewData<TModel>, string?>>[] members
    )
    {
        foreach (var member in members)
        {
            RuleFor(member)
                .MinimumLength(min)
                .WithMessage($"{member.GetMemberName()} minimum length is {min} characters.")
                .MaximumLength(max)
                .WithMessage($"{member.GetMemberName()} maximum length is {max} characters");
        }
    }

    protected void ValidateCount(
        int min,
        int max,
        params Expression<Func<IViewData<TModel>, ICollection>>[] members
    )
    {
        foreach (var member in members)
        {
            RuleFor(member)
                .Must(a => a.Count >= min)
                .WithMessage($"{member.GetMemberName()} count below minimum {min}!")
                .Must(a => a.Count <= max)
                .WithMessage($"{member.GetMemberName()} count above maximum {max}!");
        }
    }

    protected void ValidateEnum(params Expression<Func<IViewData<TModel>, string?>>[] members)
    {
        foreach (var member in members)
        {
            RuleFor(member).IsInEnum().WithMessage($"Incorrect value");
        }
    }

    protected void ValidateNotEqual(
        object item,
        params Expression<Func<IViewData<TModel>, object?>>[] members
    )
    {
        foreach (var member in members)
        {
            RuleFor(member).NotEqual(item, EqualityComparer<object?>.Default).WithMessage($"{member.GetMemberName()} not equal: {item}");
        }
    }

    protected void ValidateNotEqual(
        Expression<Func<IViewData<TModel>, object?>> first,
        Expression<Func<IViewData<TModel>, object?>> second
    )
    {
        RuleFor(first).NotEqual(second, EqualityComparer<object?>.Default).WithMessage($"{first.GetMemberName()} not equal {second.GetMemberName()}");
    }

    protected void ValidateEqual(
        object item,
        params Expression<Func<IViewData<TModel>, object?>>[] members
    )
    {
        foreach (var member in members)
        {
            RuleFor(member).Equal(item, EqualityComparer<object?>.Default).WithMessage($"{member.GetMemberName()} equal: {item}");
        }
    }

    protected void ValidateEqual(
        Expression<Func<IViewData<TModel>, object?>> first,
        Expression<Func<IViewData<TModel>, object?>> second
    )
    {
        RuleFor(first).Equal(second, EqualityComparer<object?>.Default).WithMessage($"{first.GetMemberName()} equal {second.GetMemberName()}");
    }

    protected void ValidateLanguage(params Expression<Func<IViewData<TModel>, object?>>[] members)
    {
        foreach (var member in members)
        {
            RuleFor(member)
                .Must(SupportedLanguages.Contains)
                .WithMessage($"{member.GetMemberName()} language must conform to ISO 639-1.");
        }
    }

    protected void ValidateExist<TStore, TDto>(
        Func<TModel, Expression<Func<TDto, bool>>> dataExpression
    )
        where TDto : class, IOrigin, IInnerProxy
        where TStore : IDataServiceStore
    {
        var _repository = _servicer.RemoteSet<TStore, TDto>();

        RuleFor(e => e.Model)
            .MustAsync(
                async (cmd, cancel) =>
                {
                    return await _repository.Exist(dataExpression.Invoke(cmd));
                }
            )
            .WithMessage($"{typeof(TDto).Name} does not exists");
    }

    protected void ValidateNotExist<TStore, TDto>(
        Func<TModel, Expression<Func<TDto, bool>>> dataExpression
    )
        where TDto : class, IOrigin, IInnerProxy
        where TStore : IDataServiceStore
    {
        var _repository = _servicer.RemoteSet<TStore, TDto>();

        RuleFor(e => e.Model)
            .MustAsync(
                async (cmd, cancel) =>
                {
                    return await _repository.NotExist(dataExpression.Invoke(cmd));
                }
            )
            .WithMessage($"{typeof(TDto).Name} already exists");
    }
}
