namespace Undersoft.SDK.Instant.Series.Querying
{
    using System;

    [Serializable]
    public class InstantSeriesSort
    {
        #region Fields

        public IInstantSeries InstantSeriesGenerator;
        public InstantSeriesSortTerms Terms;

        #endregion

        #region Constructors

        public InstantSeriesSort(IInstantSeries series)
        {
            this.InstantSeriesGenerator = series;
            Terms = new InstantSeriesSortTerms(series);
        }

        #endregion
    }
}