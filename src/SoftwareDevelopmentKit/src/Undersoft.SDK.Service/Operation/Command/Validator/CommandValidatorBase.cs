﻿using FluentValidation;
using System.Globalization;
using System.Linq.Expressions;

namespace Undersoft.SDK.Service.Operation.Command.Validator;

using Proxies;
using Undersoft.SDK.Service.Data.Query;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Operation.Command;

public abstract class CommandValidatorBase<TCommand> : AbstractValidator<TCommand>
    where TCommand : ICommand
{
    protected static readonly string[] SupportedLanguages;

    protected readonly IServicer uservice;

    static CommandValidatorBase()
    {
        SupportedLanguages = CultureInfo
            .GetCultures(CultureTypes.NeutralCultures)
            .Select(c => c.TwoLetterISOLanguageName)
            .Distinct()
            .ToArray();
    }

    public CommandValidatorBase(IServicer ultimateService)
    {
        uservice = ultimateService;
    }

    protected void ValidateRequired<T>(params Expression<Func<T, object>>[] properties)
    {
        foreach (var property in properties)
        {
            RuleFor(a => property.Compile()((T)a.Contract))
                .NotEmpty()
                .WithMessage(a => $"{property.GetMemberName()} is required!");
        }
    }

    protected void ValidateLanguage<T>(params Expression<Func<T, object>>[] properties)
    {
        foreach (var property in properties)
        {
            RuleFor(a => property.Compile()((T)a.Contract))
                 .Must(SupportedLanguages.Contains)
                .WithMessage("Language must conform to ISO 639-1.");
        }
    }

    protected void ValidateNotEqual<T>(object item, params Expression<Func<T, object>>[] properties)
    {
        foreach (var property in properties)
        {
            RuleFor(e => property.Compile()((T)e.Contract))
                .NotEqual(item)
                .WithMessage($"{property.GetMemberName()} is not equal: {item}");
        }
    }

    protected void ValidateEqual<T>(object item, params Expression<Func<T, object>>[] properties)
    {
        foreach (var property in properties)
        {
            RuleFor(e => property.Compile()((T)e.Contract))
                .Equal(item)
                .WithMessage($"{property.GetMemberName()} is equal: {item}");
        }
    }

    protected void ValidateLength<T>(int min, int max, params Expression<Func<T, string>>[] properties)
    {
        foreach (var property in properties)
        {
            RuleFor(e => property.Compile()((T)e.Contract))
                .MinimumLength(min)
                .WithMessage($"{property.GetMemberName()} minimum text rubricCount: {max} characters")
                .MaximumLength(max)
                .WithMessage($"{property.GetMemberName()} maximum text rubricCount: {max} characters");
        }
    }

    protected void ValidateEnum<T>(params Expression<Func<T, object>>[] properties)
    {
        foreach (var property in properties)
        {
            RuleFor(e => property.Compile()((T)e.Contract))
                .IsInEnum()
                .WithMessage($"Incorrect {property.GetMemberName()} number");
        }
    }

    protected void ValidateEmail<T>(params Expression<Func<T, string>>[] properties)
    {
        foreach (var emailProperty in properties)
        {
            RuleFor(e => emailProperty.Compile()((T)e.Contract))
                .EmailAddress()
                .When(a => !string.IsNullOrEmpty(emailProperty.Compile()((T)a.Contract).ToString()))
                .WithMessage($"Invalid {emailProperty.GetMemberName()} address.");
        }
    }

    protected void ValidateExist<TStore, TEntity>(
        LinkOperand operand,
        params Expression<Func<TEntity, object>>[] properties
    )
        where TEntity : class, IOrigin, IInnerProxy
        where TStore : IDataServerStore
    {
        RuleFor(e => e)
            .MustAsync(
                async (cmd, cancel) =>
                {
                    return await uservice
                        .StoreSet<TStore, TEntity>()
                        .Exist(
                            buildPredicate<TEntity>((IInnerProxy)cmd.Contract, operand, properties)
                        );
                }
            )
            .WithMessage($"{typeof(TEntity).Name} already exists");
    }

    protected void ValidateNotExist<TStore, TEntity>(
        LinkOperand operand,
        params Expression<Func<TEntity, object>>[] properties
    )
        where TEntity : class, IOrigin, IInnerProxy
        where TStore : IDataServerStore
    {
        RuleFor(e => e)
            .MustAsync(
                async (cmd, cancel) =>
                {
                    return await uservice
                        .StoreSet<TStore, TEntity>()
                        .NotExist(
                            buildPredicate<TEntity>((IInnerProxy)cmd.Contract, operand, properties)
                        );
                }
            )
            .WithMessage($"{typeof(TEntity).Name} does not exists");
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
