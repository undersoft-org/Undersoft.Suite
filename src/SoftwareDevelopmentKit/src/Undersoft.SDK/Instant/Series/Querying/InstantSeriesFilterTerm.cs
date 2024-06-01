namespace Undersoft.SDK.Instant.Series.Querying
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using Rubrics;

    [Serializable]
    public class InstantSeriesFilterTerm : ICloneable, IInstantSeriesFilterTerm
    {
        public string valueTypeName;

        [NonSerialized]
        private IInstantSeries series;

        [NonSerialized]
        private Type valueType;

        public InstantSeriesFilterTerm()
        {
            Stage = FilterStage.First;
        }

        public InstantSeriesFilterTerm(IInstantSeries series)
        {
            Stage = FilterStage.First;
            this.series = series;
        }

        public InstantSeriesFilterTerm(
            IInstantSeries series,
            string filterColumn,
            string operand,
            object value,
            string logic = "And",
            int stage = 1
        )
        {
            RubricName = filterColumn;
            OperandType tempOperand1;
            Enum.TryParse(operand, true, out tempOperand1);
            Operand = tempOperand1;
            Value = value;
            LogicType tempLogic;
            Enum.TryParse(logic, true, out tempLogic);
            Logic = tempLogic;
            this.series = series;
            if (series != null)
            {
                MemberRubric[] filterRubrics = this.series.Rubrics
                    .AsValues()
                    .Where(c => c.RubricName == RubricName)
                    .ToArray();
                if (filterRubrics.Length > 0)
                {
                    FilterRubric = filterRubrics[0];
                    ValueType = FilterRubric.RubricType;
                }
            }

            Stage = (FilterStage)Enum.ToObject(typeof(FilterStage), stage);
        }

        public InstantSeriesFilterTerm(
            MemberRubric filterColumn,
            OperandType operand,
            object value,
            LogicType logic = LogicType.And,
            FilterStage stage = FilterStage.First
        )
        {
            Operand = operand;
            Value = value;
            Logic = logic;
            ValueType = filterColumn.RubricType;
            RubricName = filterColumn.RubricName;
            FilterRubric = filterColumn;
            Stage = stage;
        }

        public InstantSeriesFilterTerm(
            string filterColumn,
            OperandType operand,
            object value,
            LogicType logic = LogicType.And,
            FilterStage stage = FilterStage.First
        )
        {
            RubricName = filterColumn;
            Operand = operand;
            Value = value;
            Logic = logic;
            Stage = stage;
        }

        public InstantSeriesFilterTerm(
            string filterColumn,
            string operand,
            object value,
            string logic = "And",
            int stage = 1
        )
        {
            RubricName = filterColumn;
            OperandType tempOperand1;
            Enum.TryParse(operand, true, out tempOperand1);
            Operand = tempOperand1;
            Value = value;
            LogicType tempLogic;
            Enum.TryParse(logic, true, out tempLogic);
            Logic = tempLogic;
            Stage = (FilterStage)Enum.ToObject(typeof(FilterStage), stage);
        }

        public IInstantSeries InstantSeriesCreator
        {
            get { return series; }
            set
            {
                series = value;
                if (FilterRubric == null && value != null)
                {
                    MemberRubric[] filterRubrics = series.Rubrics
                        .AsValues()
                        .Where(c => c.RubricName == RubricName)
                        .ToArray();
                    if (filterRubrics.Length > 0)
                    {
                        FilterRubric = filterRubrics[0];
                        ValueType = FilterRubric.RubricType;
                    }
                }
            }
        }

        public MemberRubric FilterRubric { get; set; }

        [DisplayName("Pos")]
        public int Index { get; set; }

        public LogicType Logic { get; set; }

        public OperandType Operand { get; set; }

        public string RubricName { get; set; }

        public FilterStage Stage { get; set; } = FilterStage.First;

        public object Value { get; set; }

        public Type ValueType
        {
            get
            {
                if (valueType == null && valueTypeName != null)
                    valueType = Type.GetType(valueTypeName);
                return valueType;
            }
            set
            {
                valueType = value;
                valueTypeName = value.FullName;
            }
        }

        public object Clone()
        {
            InstantSeriesFilterTerm clone = (InstantSeriesFilterTerm)this.MemberwiseClone();
            clone.FilterRubric = FilterRubric;
            return clone;
        }

        public InstantSeriesFilterTerm Clone(object value)
        {
            InstantSeriesFilterTerm clone = (InstantSeriesFilterTerm)this.MemberwiseClone();
            clone.FilterRubric = FilterRubric;
            clone.Value = value;
            return clone;
        }

        public bool Compare(InstantSeriesFilterTerm term)
        {
            if (RubricName != term.RubricName)
                return false;
            if (!Value.Equals(term.Value))
                return false;
            if (!Operand.Equals(term.Operand))
                return false;
            if (!Stage.Equals(term.Stage))
                return false;
            if (!Logic.Equals(term.Logic))
                return false;

            return true;
        }
    }
}
