using System.Collections.ObjectModel;
using Undersoft.SDK.Series.Base;

namespace Undersoft.SDK.Estimating
{
    public class EstimatorSeries : RegistryBase<EstimatorItem>
    {
        public EstimatorSeries() : base() { }

        public EstimatorSeries(IEnumerable<EstimatorItem> range) : base(range) { }
    }    
}
