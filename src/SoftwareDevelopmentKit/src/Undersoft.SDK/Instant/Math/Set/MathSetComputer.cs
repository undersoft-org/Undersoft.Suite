using Undersoft.SDK.Instant.Series;

namespace Undersoft.SDK.Instant.Math.Set
{
    public abstract class MathSetComputer
    {
        public IInstantSeries[] DataParameters = new IInstantSeries[1];
        public int ParametersCount = 0;
        public int RowOffset = 0;
        public int RowChunk = 0;

        public abstract void Compute();

        public int GetColumnCount(int paramid)
        {
            return DataParameters[paramid].Rubrics.Count;
        }

        public int GetIndexOf(IInstantSeries v)
        {
            for (int i = 0; i < ParametersCount; i++)
                if (DataParameters[i] == v)
                    return 1 + i;
            return -1;
        }

        public int GetRowCount(int paramid)
        {
            if (RowChunk == 0)
                return DataParameters[paramid].Count;
            else
            {
                var count = DataParameters[paramid].Count - RowOffset;
                return RowChunk > count ? count : RowChunk;
            }
        }

        public int GetRowOffset()
        {
            return RowOffset;
        }

        public int Put(IInstantSeries v)
        {
            int index = GetIndexOf(v);
            if (index < 0)
            {
                DataParameters[ParametersCount] = v;
                return ++ParametersCount;
            }
            else
            {
                DataParameters[index] = v;
            }
            return index;
        }

        public void SetParams(IInstantSeries p, int offset = 0, int chunk = 0)
        {
            Put(p);

            RowOffset = offset;
            RowChunk = chunk;
        }

        public void SetParams(IInstantSeries p, int index, int offset, int chunk)
        {
            if (index < ParametersCount)
            {
                RowOffset = offset;
                RowChunk = chunk;

                if (ReferenceEquals(DataParameters[index], p))
                    return;
                else
                    DataParameters[index] = p;
            }
        }

        public void SetParams(IInstantSeries[] p, int paramCount)
        {
            DataParameters = p;
            ParametersCount = paramCount;
        }
    }
}
