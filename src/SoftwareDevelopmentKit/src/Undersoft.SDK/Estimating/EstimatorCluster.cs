namespace Undersoft.SDK.Estimating
{
    public class EstimatorCluster : Identifiable
    {
        public double[] ClusterVector { get; set; }

        public EstimatorSeries ClusterItems { get; set; }

        public double[] ClusterVectorSummary { get; set; }

        public EstimatorCluster(EstimatorItem item)
        {
            ClusterVector = item.Vector[..item.Vector.Length];
            ClusterVectorSummary = item.Vector[..item.Vector.Length];
            ClusterItems = new EstimatorSeries([item]);
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
