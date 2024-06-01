using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Undersoft.SDK.Estimating
{
    public class EstimatorSeries : KeyedCollection<long, EstimatorItem>
    {
        public EstimatorSeries()
        {

        }

        public EstimatorSeries(IList<EstimatorItem> range) 
        {
           
        }

        public void AddRange(IEnumerable<EstimatorItem> range)
        {
            foreach (EstimatorItem de in range)
            {
                this.Add(de);
            }
        }

        protected override long GetKeyForItem(EstimatorItem item)
        {
            return item.Id;
        }
    }
}
