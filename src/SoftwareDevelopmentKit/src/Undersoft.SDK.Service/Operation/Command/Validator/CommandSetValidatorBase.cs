using FluentValidation;
using System.Globalization;
using System.Linq.Expressions;

namespace Undersoft.SDK.Service.Operation.Command.Validator;

using Proxies;
using Undersoft.SDK.Service.Data.Query;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Operation.Command;

public abstract class CommandSetValidatorBase<TCommand> : AbstractValidator<TCommand>
    where TCommand : ICommandSet
{
    protected static readonly string[] SupportedLanguages;

    protected readonly IServicer _servicer;

    static CommandSetValidatorBase()
    {
        SupportedLanguages = CultureInfo
            .GetCultures(CultureTypes.NeutralCultures)
            .Select(c => c.TwoLetterISOLanguageName)
            .Distinct()
            .ToArray();
    }

    public CommandSetValidatorBase(IServicer servicer)
    {
        _servicer = servicer;
    }

    protected virtual void ValidateLimit(int min, int max)
    {
        RuleFor(a => a)
            .Must(a => a.Commands.Count() >= min)
            .WithMessage($"Items count below minimum quantity")
            .Must(a => a.Commands.Count() <= max)
            .WithMessage($"Items count above maximum quantity");
    }

    protected void ValidateRequired<T>(params Expression<Func<T, object>>[] properties)
    {
        foreach (var property in properties)
        {
            RuleForEach(a => a.Commands)
                .ChildRules(
                    c =>
                        c.RuleFor(a => property.Compile()((T)a.Contract))
                            .NotEmpty()
                            .WithMessage(a => $"{property.GetMemberName()} is required!")
                );
        }
    }

    protected void ValidateLanguage<T>(params Expression<Func<T, object>>[] properties)
    {
        foreach (var property in properties)
        {
            RuleForEach(a => a.Commands)
                .ChildRules(
                    c =>
                        c.RuleFor(a => property.Compile()((T)a.Contract))
                            .Must(SupportedLanguages.Contains)
                            .WithMessage("Agreement language must conform to ISO 639-1.")
                );
        }
    }

    protected void ValidateEqual<T>(object item, params Expression<Func<T, object>>[] properties)
    {
        foreach (var property in properties)
        {
            RuleForEach(a => a.Commands)
                .ChildRules(
                    c =>
                        c.RuleFor(a => property.Compile()((T)a.Contract))
                            .Equal(item)
                            .WithMessage($"{property.GetMemberName()} is equal: {item}")
                );
        }
    }

    protected void ValidateNotEqual<T>(object item, params Expression<Func<T, object>>[] properties)
    {
        foreach (var property in properties)
        {
            RuleForEach(a => a.Commands)
                .ChildRules(
                    c =>
                        c.RuleFor(a => property.Compile()((T)a.Contract))
                            .NotEqual(item)
                            .WithMessage($"{property.GetMemberName()} is not equal: {item}")
                );
        }
    }

    protected void ValidateLength<T>(int min, int max, params Expression<Func<T, string>>[] properties)
    {
        foreach (var property in properties)
        {
            RuleForEach(a => a.Commands)
                .ChildRules(
                    c =>
                        c.RuleFor(r => r.Contract.ValueOf(property.GetMemberName()).ToString())
                            .MinimumLength(min)
                            .WithMessage($"{property.GetMemberName()} minimum text rubricCount: {max} characters")
                            .MaximumLength(max)
                            .WithMessage($"{property.GetMemberName()} maximum text rubricCount: {max} characters")
                );
        }
    }

    protected void ValidateEnum<T>(params Expression<Func<T, object>>[] properties)
    {
        foreach (var property in properties)
        {
            RuleForEach(a => a.Commands)
                .ChildRules(
                    c =>
                        c.RuleFor(a => property.Compile()((T)a.Contract))
                            .IsInEnum()
                            .WithMessage($"Incorrect {property.GetMemberName()} number")
                );
        }
    }

    protected void ValidateEmail<T>(params Expression<Func<T, string>>[] properties)
    {
        foreach (var property in properties)
        {
            RuleForEach(a => a.Commands)
                .ChildRules(
                    c =>
                        c.RuleFor(a => property.Compile()((T)a.Contract))
                            .EmailAddress()
                            .WithMessage($"Invalid {property.GetMemberName()} address.")
                );
        }
    }

    protected void ValidateExist<TStore, TEntity>(
        LinkOperand operand,
        params Expression<Func<TEntity, object>>[] properties
    )
        where TEntity : class, IOrigin, IInnerProxy
        where TStore : IDataServerStore
    {
        var _repository = _servicer.StoreSet<TStore, TEntity>();

        RuleForEach(a => a.Commands)
            .ChildRules(
                c =>
                    c.RuleFor(r => r.Contract)
                        .MustAsync(
                            async (cmd, cancel) =>
                            {
                                return await _repository.Exist(
                                    buildPredicate<TEntity>(
                                        (IInnerProxy)cmd,
                                        operand,
                                        properties
                                    )
                                );
                            }
                        )
                        .WithMessage($"{typeof(TEntity).Name} already exists")
            );
    }

    protected void ValidateNotExist<TStore, TEntity>(
        LinkOperand operand,
        params Expression<Func<TEntity, object>>[] properties
    )
        where TEntity : class, IOrigin, IInnerProxy
        where TStore : IDataServerStore
    {
        var _repository = _servicer.StoreSet<TStore, TEntity>();

        RuleForEach(a => a.Commands)
            .ChildRules(
                c =>
                    c.RuleFor(r => r.Contract)
                        .MustAsync(
                            async (cmd, cancel) =>
                            {
                                return await _repository.NotExist(
                                    buildPredicate<TEntity>(
                                        (IInnerProxy)cmd,
                                        operand,
                                        properties
                                    )
                                );
                            }
                        )
                        .WithMessage($"{typeof(TEntity).Name} does not exists")
            );
    }

    private Expression<Func<TEntity, bool>> buildPredicate<TEntity>(
        IInnerProxy dataInput,
        LinkOperand operand,
        params Expression<Func<TEntity, object>>[] properties
    ) where TEntity : IInnerProxy
    {
        Expression<Func<TEntity, bool>> predicate =
            operand == LinkOperand.And ? predicate = e => true : predicate = e => false;
        foreach (var item in properties)
        {
            predicate =
                operand == LinkOperand.And
                   ? predicate.And(e => item.Compile()(e) == dataInput.Proxy[item.GetMemberName()])
                    : predicate.Or(e => item.Compile()(e) == dataInput.Proxy[item.GetMemberName()]);
        }
        return predicate;
    }
}
