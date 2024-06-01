namespace Undersoft.SDK.Instant.Series.Querying
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using SDK.Uniques;

    public class InstantSeriesFilterExpression
    {
        private System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo();

        private Expression<Func<IInstant, bool>> Expression { get; set; }

        public Expression<Func<IInstant, bool>> Query
        {
            get { return CreateExpression(Stage); }
        }

        public InstantSeriesFilterTerms Conditions;
        public int Stage { get; set; }

        public InstantSeriesFilterExpression()
        {
            Conditions = new InstantSeriesFilterTerms();
            nfi.NumberDecimalSeparator = ".";
            Stage = 1;
        }

        public Expression<Func<IInstant, bool>> this[int stage]
        {
            get { return CreateExpression(stage); }
        }

        public Expression<Func<IInstant, bool>> CreateExpression(int stage = 1)
        {
            Expression<Func<IInstant, bool>> exps = null;
            List<InstantSeriesFilterTerm> fts = Conditions.Get(stage);
            Expression = null;
            LogicType previousLogic = LogicType.And;
            foreach (InstantSeriesFilterTerm ft in fts)
            {
                exps = null;
                if (ft.Operand != OperandType.Contains)
                {
                    if (Expression != null)
                        if (previousLogic != LogicType.Or)
                            Expression = Expression.And(CaseConditions(ft, exps));
                        else
                            Expression = Expression.Or(CaseConditions(ft, exps));
                    else
                        Expression = CaseConditions(ft, exps);
                    previousLogic = ft.Logic;
                }
                else
                {
                    HashSet<int> list = new HashSet<int>((ft.Value.GetType() == typeof(string)) ? ft.Value.ToString()
                            .Split(';')
                            .Select(p => Convert.ChangeType(p, ft.FilterRubric.RubricType).GetHashCode()) :
                        (ft.Value.GetType() == typeof(List<object>)) ? ((List<object>)ft.Value)
                        .Select(p => Convert.ChangeType(p, ft.FilterRubric.RubricType).GetHashCode()) : null);

                    if (list != null && list.Count > 0)
                        exps = (r => list.Contains(r[ft.FilterRubric.RubricName].GetHashCode()));

                    if (Expression != null)
                        if (previousLogic != LogicType.Or)
                            Expression = Expression.And(exps);
                        else
                            Expression = Expression.Or(exps);
                    else
                        Expression = exps;
                    previousLogic = ft.Logic;
                }
            }

            return Expression;
        }

        private Expression<Func<IInstant, bool>> CaseConditions(InstantSeriesFilterTerm filterTerm,
            Expression<Func<IInstant, bool>> expression)
        {
            var ft = filterTerm;
            var ex = expression;
            if (ft.Value != null)
            {
                object Value = filterTerm.Value;
                OperandType Operand = ft.Operand;
                bool isNumeric = ft.FilterRubric.RubricType == typeof(IUnique) ||
                                 ft.FilterRubric.RubricType == typeof(string) ||
                                 ft.FilterRubric.RubricType == typeof(DateTime) ||
                                 ft.FilterRubric.RubricType == typeof(Enum)
                    ? false
                    : true;

                if (Operand != OperandType.Like && Operand != OperandType.NotLike)
                {
                    switch (Operand)
                    {
                        case OperandType.Equal:

                            ex = (r => r[ft.FilterRubric.FieldId] != null
                                ? isNumeric ? r[ft.FilterRubric.FieldId].ComparableInt64(ft.FilterRubric.RubricType)
                                    .Equals(Value.ComparableInt64(ft.FilterRubric.RubricType)) :
                                r[ft.FilterRubric.FieldId].ComparableDouble(ft.FilterRubric.RubricType)
                                    .Equals(Value.ComparableDouble(ft.FilterRubric.RubricType))
                                : false);
                            break;

                        case OperandType.EqualOrMore:

                            ex = (r => r[ft.FilterRubric.FieldId] != null
                                ? isNumeric ? r[ft.FilterRubric.FieldId].ComparableInt64(ft.FilterRubric.RubricType)
                                              >=
                                              (Value.ComparableInt64(ft.FilterRubric.RubricType)) :
                                r[ft.FilterRubric.FieldId].ComparableDouble(ft.FilterRubric.RubricType)
                                >=
                                (Value.ComparableDouble(ft.FilterRubric.RubricType))
                                : false);
                            break;

                        case OperandType.More:

                            ex = (r => r[ft.FilterRubric.FieldId] != null
                                ? isNumeric ? r[ft.FilterRubric.FieldId].ComparableInt64(ft.FilterRubric.RubricType)
                                              >
                                              (Value.ComparableInt64(ft.FilterRubric.RubricType)) :
                                r[ft.FilterRubric.FieldId].ComparableDouble(ft.FilterRubric.RubricType)
                                >
                                (Value.ComparableDouble(ft.FilterRubric.RubricType))
                                : false);
                            break;

                        case OperandType.EqualOrLess:

                            ex = (r => r[ft.FilterRubric.FieldId] != null
                                ? isNumeric ? r[ft.FilterRubric.FieldId].ComparableInt64(ft.FilterRubric.RubricType)
                                              <=
                                              (Value.ComparableInt64(ft.FilterRubric.RubricType)) :
                                r[ft.FilterRubric.FieldId].ComparableDouble(ft.FilterRubric.RubricType)
                                <=
                                (Value.ComparableDouble(ft.FilterRubric.RubricType))
                                : false);
                            break;

                        case OperandType.Less:

                            ex = (r => r[ft.FilterRubric.FieldId] != null
                                ? isNumeric ? r[ft.FilterRubric.FieldId].ComparableInt64(ft.FilterRubric.RubricType)
                                              <
                                              (Value.ComparableInt64(ft.FilterRubric.RubricType)) :
                                r[ft.FilterRubric.FieldId].ComparableDouble(ft.FilterRubric.RubricType)
                                <
                                (Value.ComparableDouble(ft.FilterRubric.RubricType))
                                : false);
                            break;
                        default:
                            break;
                    }
                }
                else if (Operand != OperandType.NotLike)

                    ex = (r => r[ft.FilterRubric.FieldId] != null
                        ? Convert.ChangeType(r[ft.FilterRubric.FieldId], ft.FilterRubric.RubricType).ToString()
                            .Contains(Convert.ChangeType(Value, ft.FilterRubric.RubricType).ToString())
                        : false);
                else
                    ex = (r => r[ft.FilterRubric.FieldId] != null
                        ? !Convert.ChangeType(r[ft.FilterRubric.FieldId], ft.FilterRubric.RubricType).ToString()
                            .Contains(Convert.ChangeType(Value, ft.FilterRubric.RubricType).ToString())
                        : false);
            }

            return ex;
        }
    }
}