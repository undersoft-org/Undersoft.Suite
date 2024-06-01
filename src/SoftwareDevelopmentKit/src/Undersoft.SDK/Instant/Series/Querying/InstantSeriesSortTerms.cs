namespace Undersoft.SDK.Instant.Series.Querying
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;

    [Serializable]
    public class InstantSeriesSortTerms : Collection<InstantSeriesSortTerm>, ICollection
    {
        [NonSerialized]
        private IInstantSeries series;

        public InstantSeriesSortTerms() { }

        public InstantSeriesSortTerms(IInstantSeries series)
        {
            this.series = series;
        }

        public IInstantSeries InstantSeriesCreator
        {
            get { return series; }
            set { series = value; }
        }

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(ICollection<InstantSeriesSortTerm> terms)
        {
            foreach (InstantSeriesSortTerm term in terms)
            {
                term.InstantSeriesCreator = InstantSeriesCreator;
                term.Index = ((IList)this).Add(term);
            }
        }

        public void Add(IInstantSeriesSortTerm item)
        {
            Add(new InstantSeriesSortTerm(item.RubricName, item.Direction.ToString(), item.RubricId));
        }

        public new int Add(InstantSeriesSortTerm value)
        {
            value.InstantSeriesCreator = InstantSeriesCreator;
            value.Index = ((IList)this).Add(value);
            return value.Index;
        }

        public object AddNew()
        {
            return (object)((IBindingList)this).AddNew();
        }

        public InstantSeriesSortTerms Clone()
        {
            InstantSeriesSortTerms mx = (InstantSeriesSortTerms)this.MemberwiseClone();
            return mx;
        }

        public bool Contains(IInstantSeriesSortTerm item)
        {
            return Contains(
                new InstantSeriesSortTerm(item.RubricName, item.Direction.ToString(), item.RubricId)
            );
        }

        public void CopyTo(IInstantSeriesSortTerm[] array, int arrayIndex)
        {
            Array.Copy(this.Cast<IInstantSeriesSortTerm>().ToArray(), array, Count);
        }

        public InstantSeriesSortTerm Find(InstantSeriesSortTerm data)
        {
            foreach (InstantSeriesSortTerm lDetailValue in this)
                if (lDetailValue == data)
                    return lDetailValue;
            return null;
        }

        public List<InstantSeriesSortTerm> Get()
        {
            return this.AsEnumerable().Select(c => c).ToList();
        }

        public List<InstantSeriesSortTerm> Get(List<string> RubricNames)
        {
            return this.AsEnumerable().Where(c => RubricNames.Contains(c.RubricName)).ToList();
        }

        public InstantSeriesSortTerm[] GetTerms(string RubricName)
        {
            return this.AsEnumerable().Where(c => c.RubricName == RubricName).ToArray();
        }

        public bool Have(string RubricName)
        {
            return this.AsEnumerable().Where(c => c.RubricName == RubricName).Any();
        }

        public int IndexOf(object value)
        {
            for (int i = 0; i < Count; i++)
                if (this[i] == value)
                    return i;
            return -1;
        }

        public void Remove(ICollection<InstantSeriesSortTerm> value)
        {
            foreach (InstantSeriesSortTerm term in value)
                Remove(term);
        }

        public bool Remove(IInstantSeriesSortTerm item)
        {
            return Remove(new InstantSeriesSortTerm(item.RubricName, item.Direction.ToString(), item.RubricId));
        }

        public void Renew(ICollection<InstantSeriesSortTerm> terms)
        {
            bool diffs = false;
            if (Count != terms.Count)
            {
                diffs = true;
            }
            else
            {
                foreach (InstantSeriesSortTerm term in terms)
                {
                    if (Have(term.RubricName))
                    {
                        int same = 0;
                        foreach (InstantSeriesSortTerm myterm in GetTerms(term.RubricName))
                        {
                            if (myterm.Compare(term))
                                same++;
                        }

                        if (same == 0)
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
                foreach (InstantSeriesSortTerm term in terms)
                    term.Index = ((IList)this).Add(term);
            }
        }

        public void SetRange(InstantSeriesSortTerm[] data)
        {
            for (int i = 0; i < data.Length; i++)
                this[i] = data[i];
        }
    }
}
