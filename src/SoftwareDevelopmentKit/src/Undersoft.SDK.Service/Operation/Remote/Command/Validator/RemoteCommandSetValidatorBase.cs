using FluentValidation;
using System.Globalization;
using System.Linq.Expressions;

namespace Undersoft.SDK.Service.Operation.Remote.Command.Validator;

using Proxies;
using Undersoft.SDK.Service.Data.Query;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Operation.Remote.Command;

public abstract class RemoteCommandSetValidatorBase<TCommand> : AbstractValidator<TCommand>
    where TCommand : IRemoteCommandSet
{
    protected static readonly string[] SupportedLanguages;

    protected readonly IServicer _servicer;

    static RemoteCommandSetValidatorBase()
    {
        SupportedLanguages = CultureInfo
            .GetCultures(CultureTypes.NeutralCultures)
            .Select(c => c.TwoLetterISOLanguageName)
            .Distinct()
            .ToArray();
    }

    public RemoteCommandSetValidatorBase(IServicer servicer)
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

    protected void ValidateRequired(params string[] propertyNames)
    {
        foreach (string propertyName in propertyNames)
        {
            RuleForEach(a => a.Commands)
                .ChildRules(
                    c =>
                        c.RuleFor(r => r.Model.ValueOf(propertyName))
                            .NotEmpty()
                            .WithMessage(a => $"{propertyName} is required!")
                );
        }
    }

    protected void ValidateLanguage(params string[] propertyNames)
    {
        foreach (string propertyName in propertyNames)
        {
            RuleForEach(a => a.Commands)
                .ChildRules(
                    c =>
                        c.RuleFor(r => r.Model.ValueOf(propertyName))
                            .Must(SupportedLanguages.Contains)
                            .WithMessage("Agreement language must conform to ISO 639-1.")
                );
        }
    }

    protected void ValidateEqual(object item, params string[] propertyNames)
    {
        foreach (string propertyName in propertyNames)
        {
            RuleForEach(a => a.Commands)
                .ChildRules(
                    c =>
                        c.RuleFor(r => r.Model.ValueOf(propertyName))
                            .Equal(item)
                            .WithMessage($"{propertyName} is equal: {item}")
                );
        }
    }

    protected void ValidateNotEqual(object item, params string[] propertyNames)
    {
        foreach (string propertyName in propertyNames)
        {
            RuleForEach(a => a.Commands)
                .ChildRules(
                    c =>
                        c.RuleFor(r => r.Model.ValueOf(propertyName))
                            .NotEqual(item)
                            .WithMessage($"{propertyName} is not equal: {item}")
                );
        }
    }

    protected void ValidateLength(int min, int max, params string[] propertyNames)
    {
        foreach (string propertyName in propertyNames)
        {
            RuleForEach(a => a.Commands)
                .ChildRules(
                    c =>
                        c.RuleFor(r => r.Model.ValueOf(propertyName).ToString())
                            .MinimumLength(min)
                            .WithMessage($"{propertyName} minimum text rubricCount: {max} characters")
                            .MaximumLength(max)
                            .WithMessage($"{propertyName} maximum text rubricCount: {max} characters")
                );
        }
    }

    protected void ValidateEnum(params string[] propertyNames)
    {
        foreach (string propertyName in propertyNames)
        {
            RuleForEach(a => a.Commands)
                .ChildRules(
                    c =>
                        c.RuleFor(r => r.Model.ValueOf(propertyName))
                            .IsInEnum()
                            .WithMessage($"Incorrect {propertyName} number")
                );
        }
    }

    protected void ValidateEmail(params string[] emailPropertyNames)
    {
        foreach (string emailPropertyName in emailPropertyNames)
        {
            RuleForEach(a => a.Commands)
                .ChildRules(
                    c =>
                        c.RuleFor(r => r.Model.ValueOf(emailPropertyName).ToString())
                            .EmailAddress()
                            .WithMessage($"Invalid {emailPropertyName} address.")
                );
        }
    }

    protected void ValidateExist<TStore, TEntity>(
        LinkOperand operand,
        params string[] propertyNames
    )
        where TEntity : class, IOrigin, IInnerProxy
        where TStore : IDataServerStore
    {
        var _repository = _servicer.StoreSet<TStore, TEntity>();

        RuleForEach(a => a.Commands)
            .ChildRules(
                c =>
                    c.RuleFor(r => r.Model)
                        .MustAsync(
                            async (cmd, cancel) =>
                            {
                                return await _repository.Exist(
                                    buildPredicate<TEntity>(
                                        (IInnerProxy)cmd,
                                        operand,
                                        propertyNames
                                    )
                                );
                            }
                        )
                        .WithMessage($"{typeof(TEntity).Name} already exists")
            );
    }

    protected void ValidateNotExist<TStore, TEntity>(
        LinkOperand operand,
        params string[] propertyNames
    )
        where TEntity : class, IOrigin, IInnerProxy
        where TStore : IDataServerStore
    {
        var _repository = _servicer.StoreSet<TStore, TEntity>();

        RuleForEach(a => a.Commands)
            .ChildRules(
                c =>
                    c.RuleFor(r => r.Model)
                        .MustAsync(
                            async (cmd, cancel) =>
                            {
                                return await _repository.NotExist(
                                    buildPredicate<TEntity>(
                                        (IInnerProxy)cmd,
                                        operand,
                                        propertyNames
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
        params string[] propertyNames
    ) where TEntity : IInnerProxy
    {
        Expression<Func<TEntity, bool>> predicate =
            operand == LinkOperand.And ? predicate = e => true : predicate = e => false;
        foreach (var item in propertyNames)
        {
            predicate =
                operand == LinkOperand.And
                    ? predicate.And(e => e.Proxy[item] == dataInput.Proxy[item])
                    : predicate.Or(e => e.Proxy[item] == dataInput.Proxy[item]);
        }
        return predicate;
    }

    protected virtual void ValidateLimit<T>(int min, int max)
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
                        c.RuleFor(a => property.Compile()((T)a.Model))
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
                        c.RuleFor(a => property.Compile()((T)a.Model))
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
                        c.RuleFor(a => property.Compile()((T)a.Model))
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
                        c.RuleFor(a => property.Compile()((T)a.Model))
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
                        c.RuleFor(r => r.Model.ValueOf(property.GetMemberName()).ToString())
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
                        c.RuleFor(a => property.Compile()((T)a.Model))
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
                        c.RuleFor(a => property.Compile()((T)a.Model))
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
                    c.RuleFor(r => r.Model)
                        .MustAsync(
                            async (cmd, cancel) =>
                            {
                                return await _repository.Exist(
                                    buildPredicate(
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
                    c.RuleFor(r => r.Model)
                        .MustAsync(
                            async (cmd, cancel) =>
                            {
                                return await _repository.NotExist(
                                    buildPredicate(
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
