using Undersoft.SDK.Series;

namespace Undersoft.SDK.Estimating
{
    public class EstimatorHyperCluster : Identifiable
    {
        public double[] HyperClusterVector { get; set; }

        public Listing<EstimatorCluster> Clusters { get; set; }

        public EstimatorSeries HyperClusterItems { get; set; }

        public double[] HyperClusterVectorSummary { get; set; }

        public EstimatorHyperCluster(EstimatorCluster cluster)
        {
            HyperClusterVector = cluster.ClusterVector[..cluster.ClusterVector.Length];
            HyperClusterVectorSummary = cluster.ClusterVectorSummary[..cluster.ClusterVectorSummary.Length];
            Clusters = new Listing<EstimatorCluster>([cluster]);
        }

        public bool RemoveClusterFromHyperCluster(EstimatorCluster cluster)
        {
            if (Clusters.Remove(cluster) == true)
            {
                if (Clusters.Count > 0)
                {
                    AdaptiveResonainceTheoryEstimator.CalculateClusterIntersection(Clusters, HyperClusterVector);
                    AdaptiveResonainceTheoryEstimator.CalculateClusterSummary(Clusters, HyperClusterVectorSummary);
                }
            }
            return Clusters.Count > 0;
        }

        public void AddClusterToHyperCluster(EstimatorCluster cluster)
        {
            Clusters.Add(cluster);
            AdaptiveResonainceTheoryEstimator.UpdateClusterIntersectionByLast(Clusters, HyperClusterVector);
            AdaptiveResonainceTheoryEstimator.UpdateClusterSummaryByLast(Clusters, HyperClusterVectorSummary);
        }


        public EstimatorSeries GetHyperClusterItems()
        {
            EstimatorSeries updatedItemList = new EstimatorSeries();

            for (int i = 0; i < Clusters.Count; i++)
            {
                for (int j = 0; j < Clusters[i].ClusterItems.Count; j++)
                {
                    updatedItemList.Add(Clusters[i].ClusterItems[j]);
                }
            }
            HyperClusterItems = updatedItemList;

            return HyperClusterItems;
        }

    }

}
