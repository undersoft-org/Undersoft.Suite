using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace Undersoft.SDK.Service.Data.Query
{
    [Serializable]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public class Filter<TEntity> : ICloneable
    {
        public string typeName;
        [NonSerialized] private Type type;

        private FilterExpression<TEntity> filterExpression;

        public Filter()
        {
        }
        public Filter(Expression<Func<TEntity, bool>> expressionItem, LogicOperand linkOperand = LogicOperand.And)
        {
            ExpressionItem = expressionItem;
            Logic = linkOperand;
        }
        public Filter(MemberRubric rubric, MathOperand compareOperand, object compareValue, LogicOperand linkOperand = LogicOperand.And)
        {
            Property = rubric.Name;
            Operand = compareOperand;
            Value = compareValue;
            Logic = linkOperand;
            PropertyType = rubric.RubricType;
            Rubric = rubric;
        }
        public Filter(string propertyName, MathOperand compareOperand, object compareValue, LogicOperand linkOperand = LogicOperand.And)
        {
            Property = propertyName;
            Operand = compareOperand;
            Value = compareValue;
            Logic = linkOperand;
        }
        public Filter(string propertyName, string compareOperand, object compareValue, string linkLogic = "And")
        {
            Property = propertyName;
            Enum.TryParse(compareOperand, true, out MathOperand tempOperand);
            if (tempOperand == MathOperand.None)
                tempOperand = FilterOperand.ParseMathOperand(compareOperand);
            Operand = tempOperand;
            Value = compareValue;
            Enum.TryParse(linkLogic, true, out LogicOperand tempLogic);
            Logic = tempLogic;
        }
        public Filter(FilterItem item) : this(item.Property, item.Operand, item.Value, item.Logic)
        {
        }
        [JsonIgnore]
        public Expression<Func<TEntity, bool>> ExpressionItem { get; set; }
        [JsonIgnore]
        public MemberRubric Rubric { get; set; }

        public string Property { get; set; }

        [JsonIgnore]
        public Type PropertyType
        {
            get
            {
                if (type == null && typeName != null)
                    type = Type.GetType(typeName);
                return type;
            }
            set
            {
                type = value;
                typeName = value.FullName;
            }
        }

        public MathOperand Operand { get; set; }

        public object Value { get; set; }

        public string Data { get; set; }

        public LogicOperand Logic { get; set; }

        public void Assign(FilterExpression<TEntity> filterExpression)
        {
            var fe = filterExpression;
            this.filterExpression = fe;
            if (fe.Rubrics.TryGet(Property, out MemberRubric rubric))
            {
                Rubric = rubric;
                PropertyType = rubric.RubricType;
                ExpressionItem = fe.ConvertItem(this);
            }
        }

        public object Clone()
        {
            Filter<TEntity> clone = (Filter<TEntity>)MemberwiseClone();
            clone.Rubric = Rubric;
            return clone;
        }

        public Filter<TEntity> Clone(object value)
        {
            Filter<TEntity> clone = (Filter<TEntity>)MemberwiseClone();
            clone.Rubric = Rubric;
            clone.Value = value;
            return clone;
        }

        public bool Compare(Filter<TEntity> term)
        {
            if (Property != term.Property)
                return false;
            if (!Value.Equals(term.Value))
                return false;
            if (!Operand.Equals(term.Operand))
                return false;
            if (!Logic.Equals(term.Logic))
                return false;

            return true;
        }

    }
}
