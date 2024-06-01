using Undersoft.SDK.Estimating;

namespace Undersoft.SDK.Estimating
{
    public class EstimatorCluster
    {
        public double[] ClusterVector { get; set; }


        public EstimatorSeries ClusterItems { get; set; }


        public double[] ClusterVectorSummary { get; set; }

        public EstimatorCluster(EstimatorItem item)
        {
            ClusterVector = new double[item.Vector.Length];
            Array.Copy(item.Vector, ClusterVector, item.Vector.Length);
            ClusterVectorSummary = new double[item.Vector.Length];
            Array.Copy(item.Vector, ClusterVectorSummary, item.Vector.Length);
            ClusterItems = new EstimatorSeries();
            ClusterItems.Add(item);                                                                                                             
        }

        public bool RemoveItemFromCluster(EstimatorItem item)
        {
            if (ClusterItems.Remove(item) == true)
            {
                if (ClusterItems.Count > 0)  
                {
                    AdaptiveResonainceTheoryEstimator.CalculateIntersection(ClusterItems, ClusterVector);
                    AdaptiveResonainceTheoryEstimator.CalculateSummary(ClusterItems, ClusterVectorSummary);

                }
            }
            return ClusterItems.Count > 0;
        }

        public void AddItemToCluster(EstimatorItem item)
        {
            if (!ClusterItems.Contains(item))
            {
                ClusterItems.Add(item);
                AdaptiveResonainceTheoryEstimator.UpdateIntersectionByLast(ClusterItems, ClusterVector);
                AdaptiveResonainceTheoryEstimator.UpdateSummaryByLast(ClusterItems, ClusterVectorSummary);
            }
        }
    }

}
