using FluentValidation;
using System.Collections;
using System.Linq.Expressions;

namespace Undersoft.SDK.Service.Operation.Command.Validator;

using Undersoft.SDK;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Operation.Command;

public class CommandSetValidator<TDto> : CommandSetValidatorBase<CommandSet<TDto>> where TDto : class, IOrigin, IInnerProxy
{
    public CommandSetValidator(IServicer ultimateService) : base(ultimateService) { }

    protected void ValidationScope<T>(OperationType commandMode, Action validations)
    {
        ValidationScope(typeof(T), commandMode, validations);
    }

    protected void ValidationScope(Type type, OperationType commandMode, Action validations)
    {
        WhenAsync(
            async (cmd, cancel) =>
                await Task.Run(
                    () =>
                        ((int)cmd.CommandMode & (int)commandMode) > 0
                        && GetType().UnderlyingSystemType.IsAssignableTo(type),
                    cancel
                ),
            validations
        );
    }

    protected void ValidationScope(OperationType commandMode, Action validations)
    {
        WhenAsync(
            async (cmd, cancel) =>
                await Task.Run(() => ((int)cmd.CommandMode & (int)commandMode) > 0, cancel),
            validations
        );
    }

    protected override void ValidateLimit(int min, int max)
    {
        RuleFor(a => a)
            .Must(a => a.Count >= min)
            .WithMessage($"Items count below minimum quantity")
            .Must(a => a.Count <= max)
            .WithMessage($"Items count above maximum quantity");
    }

    protected void ValidateRequired(params Expression<Func<Command<TDto>, object>>[] members)
    {
        members
            .ForEach(
                member =>
                    RuleForEach(v => v)
                        .ChildRules(
                            a =>
                                a.RuleFor(member)
                                    .NotEmpty()
                                    .WithMessage(
                                        $"property {member.Parameters.LastOrDefault().Name} is required!"
                                    )
                        )
            )
            .ToArray();
    }

    protected void ValidateEmail(params Expression<Func<Command<TDto>, string>>[] members)
    {
        members
            .ForEach(
                member =>
                    RuleForEach(v => v)
                        .ChildRules(
                            a =>
                                a.RuleFor(member)
                                    .EmailAddress()
                                    .WithMessage($"Invalid email address.")
                        )
            )
            .ToArray();
    }

    protected void ValidateLength(
        int min,
        int max,
        params Expression<Func<Command<TDto>, object>>[] members
    )
    {
        members.ForEach(
            (member) =>
            {
                RuleForEach(v => v)
                    .ChildRules(
                        a =>
                            a.RuleFor(member)
                                .Must(
                                    (text) =>
                                    {
                                        if (text == null)
                                            return false;
                                        var length = text.ToString().Length;
                                        return !(length < min) || !(length > max);
                                    }
                                )
                                .WithMessage(
                                    $"text rubricCount above range limit min:{min} - max:{max} characters"
                                )
                    );
            }
        );
    }

    protected void ValidateCount(
        int min,
        int max,
        params Expression<Func<Command<TDto>, object>>[] members
    )
    {
        members.ForEach(
            (member) =>
            {
                RuleForEach(v => v)
                    .ChildRules(
                        a =>
                            a.RuleFor(member)
                                .Must(
                                    (collection) =>
                                    {
                                        var count = ((ICollection)collection).Count;
                                        return !(count < min) || !(count > max);
                                    }
                                )
                                .WithMessage(
                                    $"Items count above range limit min:{min} - max:{max} characters"
                                )
                    );
            }
        );
    }

    protected void ValidateEnum(params Expression<Func<Command<TDto>, string>>[] members)
    {
        members
            .ForEach(
                member =>
                    RuleForEach(v => v)
                        .ChildRules(
                            a => a.RuleFor(member).IsInEnum().WithMessage($"Incorrect enum value")
                        )
            )
            .ToArray();
    }

    protected void ValidateNotEqual(
        object item,
        params Expression<Func<Command<TDto>, object>>[] members
    )
    {
        members
            .ForEach(
                member =>
                    RuleForEach(v => v)
                        .ChildRules(
                            a =>
                                a.RuleFor(member)
                                    .NotEqual(item)
                                    .WithMessage($"value not equal: {item}")
                        )
            )
            .ToArray();
    }

    protected void ValidateEqual(
        object item,
        params Expression<Func<Command<TDto>, object>>[] members
    )
    {
        members
            .ForEach(
                member =>
                    RuleForEach(v => v)
                        .ChildRules(
                            a => a.RuleFor(member).Equal(item).WithMessage($"value equal: {item}")
                        )
            )
            .ToArray();
    }

    protected void ValidateLanguage(params Expression<Func<Command<TDto>, object>>[] members)
    {
        members
            .ForEach(
                member =>
                    RuleForEach(v => v)
                        .ChildRules(
                            a =>
                                a.RuleFor(member)
                                    .Must(SupportedLanguages.Contains)
                                    .WithMessage("Agreement language must conform to ISO 639-1.")
                        )
            )
            .ToArray();
    }

    protected void ValidateExist<TStore, TEntity>(
        Func<TDto, Expression<Func<TEntity, bool>>> command,
        string message = null
    )
        where TEntity : class, IOrigin, IInnerProxy
        where TStore : IDataServerStore
    {
        var _repository = _servicer.StoreSet<TStore, TEntity>();
        RuleForEach(e => e)
            .MustAsync(async (cmd, cancel) => await _repository.Exist(command(cmd.Contract)))
            .WithMessage(
                $"{typeof(TEntity).Name} does not exists " + message != null
                    ? "with " + message
                    : null
            );
    }

    protected void ValidateNotExist<TStore, TEntity>(
        Func<TDto, Expression<Func<TEntity, bool>>> command,
        string message = null
    )
        where TEntity : class, IOrigin, IInnerProxy
        where TStore : IDataServerStore
    {
        var _repository = _servicer.StoreSet<TStore, TEntity>();
        RuleForEach(e => e)
            .MustAsync(async (cmd, cancel) => await _repository.NotExist(command(cmd.Contract)))
            .WithMessage(
                $"{typeof(TEntity).Name} already exists " + message != null
                    ? "with " + message
                    : null
            );
    }
}
