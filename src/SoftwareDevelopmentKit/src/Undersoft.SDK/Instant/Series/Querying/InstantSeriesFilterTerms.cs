namespace Undersoft.SDK.Instant.Series.Querying
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;

    [Serializable]
    public enum OperandType
    {
        Equal,
        EqualOrMore,
        EqualOrLess,
        More,
        Less,
        Like,
        NotLike,
        Contains,
        None
    }

    [Serializable]
    public enum LogicType
    {
        And,
        Or
    }

    [Serializable]
    public enum FilterStage
    {
        None,
        First,
        Second,
        Third
    }

    [Serializable]
    public class InstantSeriesFilterTerms : Collection<InstantSeriesFilterTerm>, ICollection
    {
        [NonSerialized]
        private IInstantSeries series;

        public InstantSeriesFilterTerms() { }

        public InstantSeriesFilterTerms(IInstantSeries series)
        {
            this.InstantSeriesCreator = series;
        }

        public IInstantSeries InstantSeriesCreator
        {
            get { return series; }
            set { series = value; }
        }

        public bool IsReadOnly => throw new NotImplementedException();

        public new int Add(InstantSeriesFilterTerm value)
        {
            value.InstantSeriesCreator = series;
            value.Index = ((IList)this).Add(value);
            return value.Index;
        }

        public void Add(ICollection<InstantSeriesFilterTerm> terms)
        {
            foreach (InstantSeriesFilterTerm term in terms)
            {
                term.InstantSeriesCreator = InstantSeriesCreator;
                term.Index = Add(term);
            }
        }

        public void Add(IInstantSeriesFilterTerm item)
        {
            Add(new InstantSeriesFilterTerm(item.RubricName, item.Operand, item.Value, item.Logic, item.Stage));
        }

        public object AddNew()
        {
            return (object)((IBindingList)this).AddNew();
        }

        public InstantSeriesFilterTerms Clone()
        {
            InstantSeriesFilterTerms ft = new InstantSeriesFilterTerms();
            foreach (InstantSeriesFilterTerm t in this)
            {
                InstantSeriesFilterTerm _t = new InstantSeriesFilterTerm(t.RubricName, t.Operand, t.Value, t.Logic, t.Stage);
                ft.Add(_t);
            }

            return ft;
        }

        public bool Contains(IInstantSeriesFilterTerm item)
        {
            return Contains(
                new InstantSeriesFilterTerm(item.RubricName, item.Operand, item.Value, item.Logic, item.Stage)
            );
        }

        public bool Contains(string RubricName)
        {
            return this.AsEnumerable().Where(c => c.RubricName == RubricName).Any();
        }

        public void CopyTo(IInstantSeriesFilterTerm[] array, int arrayIndex)
        {
            Array.Copy(this.AsQueryable().Cast<IInstantSeriesFilterTerm>().ToArray(), array, Count);
        }

        public InstantSeriesFilterTerm Find(InstantSeriesFilterTerm data)
        {
            foreach (InstantSeriesFilterTerm lDetailValue in this)
                if (lDetailValue == data)
                    return lDetailValue;
            return null;
        }

        public List<InstantSeriesFilterTerm> Get(int stage)
        {
            FilterStage filterStage = (FilterStage)Enum.ToObject(typeof(FilterStage), stage);
            return this.AsEnumerable().Where(c => filterStage.Equals(c.Stage)).ToList();
        }

        public List<InstantSeriesFilterTerm> Get(List<string> RubricNames)
        {
            return this.AsEnumerable()
                .Where(c => RubricNames.Contains(c.FilterRubric.RubricName))
                .ToList();
        }

        public InstantSeriesFilterTerm[] Get(string RubricName)
        {
            return this.AsEnumerable().Where(c => c.RubricName == RubricName).ToArray();
        }

        public int IndexOf(object value)
        {
            for (int i = 0; i < Count; i++)
                if (ReferenceEquals(this[i], value))
                    return i;
            return -1;
        }

        public void Remove(ICollection<InstantSeriesFilterTerm> value)
        {
            foreach (InstantSeriesFilterTerm term in value)
                Remove(term);
        }

        public bool Remove(IInstantSeriesFilterTerm item)
        {
            return Remove(
                new InstantSeriesFilterTerm(item.RubricName, item.Operand, item.Value, item.Logic, item.Stage)
            );
        }

        public void Renew(ICollection<InstantSeriesFilterTerm> terms)
        {
            bool diffs = false;
            if (Count != terms.Count)
            {
                diffs = true;
            }
            else
            {
                foreach (InstantSeriesFilterTerm term in terms)
                {
                    if (Contains(term.RubricName))
                    {
                        int same = 0;
                        foreach (InstantSeriesFilterTerm myterm in Get(term.RubricName))
                        {
                            if (!myterm.Compare(term))
                                same++;
                        }

                        if (same != 0)
                        {
                            diffs = true;
                            break;
                        }
                    }
                    else
                    {
                        diffs = true;
                        break;
                    }
                }
            }

            if (diffs)
            {
                Clear();
                foreach (InstantSeriesFilterTerm term in terms)
                    Add(term);
            }
        }

        public void Reset()
        {
            this.Clear();
        }

        public void SetRange(InstantSeriesFilterTerm[] data)
        {
            for (int i = 0; i < data.Length; i++)
                this[i] = data[i];
        }
    }
}
