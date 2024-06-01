using System.Diagnostics;
using System.Text;

namespace Undersoft.SDK.Estimating
{
    using Undersoft.SDK.Series;

    public class AdaptiveResonainceTheoryEstimator
    {
        public List<string> NameList { get; set; }

        public int ItemSize { get; set; }

        public EstimatorSeries Items { get; set; }

        public List<EstimatorCluster> Clusters { get; set; }

        public List<EstimatorHyperCluster> HyperClusters { get; set; }

        private Registry<EstimatorCluster> ItemsToClusters;

        private Dictionary<EstimatorCluster, EstimatorHyperCluster> ClustersToHyperClusters;

        public double bValue = 0.2f;

        public double pValue = 0.6f;

        public double p2Value = 0.3f;

        public const int rangeLimit = 1;

        public int IterationLimit = 50;

        private string tempHardFileName = "surveyResults.art";

        public AdaptiveResonainceTheoryEstimator()
        {
            NameList = new List<string>();
            Items = new EstimatorSeries();
            Clusters = new List<EstimatorCluster>();
            HyperClusters = new List<EstimatorHyperCluster>();
            ItemsToClusters = new Registry<EstimatorCluster>();
            ClustersToHyperClusters = new Dictionary<EstimatorCluster, EstimatorHyperCluster>();

            LoadFile(tempHardFileName);
            Items = NormalizeItemList(Items);
            Create();
        }

        public void Create()
        {
            Clusters.Clear();
            HyperClusters.Clear();
            ItemsToClusters.Clear();
            ClustersToHyperClusters.Clear();

            for (int i = 0; i < Items.Count; i++)
            {
                AssignCluster(Items[i]);
            }

            for (int i = 0; i < HyperClusters.Count; i++)
            {
                HyperClusters[i].GetHyperClusterItems();
            }
        }

        public void Create(ICollection<EstimatorItem> itemCollection)
        {
            Items.AddRange(itemCollection);

            Clusters.Clear();
            HyperClusters.Clear();
            ItemsToClusters.Clear();
            ClustersToHyperClusters.Clear();

            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].Id = i;
                AssignCluster(Items[i]);
            }

            for (int i = 0; i < HyperClusters.Count; i++)
            {
                HyperClusters[i].GetHyperClusterItems();
            }
        }

        public void Append(ICollection<EstimatorItem> itemCollection)
        {
            int currentCount = Items.Count;

            Items.AddRange(itemCollection);

            for (int i = currentCount; i < Items.Count; i++)
            {
                Items[i].Id = i;
                AssignCluster(Items[i]);
            }

            for (int i = 0; i < HyperClusters.Count; i++)
            {
                HyperClusters[i].GetHyperClusterItems();
            }
        }

        public void Append(EstimatorItem item)
        {
            item.Id = Items.Count;
            Items.Add(item);
            AssignCluster(item);

            for (int i = 0; i < HyperClusters.Count; i++)
            {
                HyperClusters[i].GetHyperClusterItems();
            }
        }

        public void AssignCluster(EstimatorItem item)
        {
            int iterationCounter = IterationLimit;
            bool isAssignementChanged = true;
            double itemVectorMagnitude = CalculateVectorMagnitude(item.Vector);

            while (isAssignementChanged && iterationCounter > 0)
            {
                isAssignementChanged = false;

                List<KeyValuePair<EstimatorCluster, double>> clusterToProximityList =
                    new List<KeyValuePair<EstimatorCluster, double>>();
                double proximityThreshold = itemVectorMagnitude / (bValue + rangeLimit * ItemSize);

                for (int i = 0; i < Clusters.Count; i++)
                {
                    double clusterVectorMagnitude = CalculateVectorMagnitude(
                        Clusters[i].ClusterVector
                    );
                    double proximity =
                        CaulculateVectorIntersectionMagnitude(
                            item.Vector,
                            Clusters[i].ClusterVector
                        ) / (bValue + clusterVectorMagnitude);
                    if (proximity > proximityThreshold)
                    {
                        clusterToProximityList.Add(
                            new KeyValuePair<EstimatorCluster, double>(Clusters[i], proximity)
                        );
                    }
                }

                if (clusterToProximityList.Count > 0)
                {
                    clusterToProximityList.Sort((x, y) => -1 * x.Value.CompareTo(y.Value));

                    for (int i = 0; i < clusterToProximityList.Count; i++)
                    {
                        EstimatorCluster newCluster = clusterToProximityList[i].Key;
                        double vigilance =
                            CaulculateVectorIntersectionMagnitude(
                                newCluster.ClusterVector,
                                item.Vector
                            ) / itemVectorMagnitude;
                        if (vigilance >= pValue)
                        {
                            if (ItemsToClusters.ContainsKey(item.Id))
                            {
                                EstimatorCluster previousCluster = ItemsToClusters[item.Id];
                                if (ReferenceEquals(newCluster, previousCluster))
                                    break;
                                if (previousCluster.RemoveItemFromCluster(item) == false)
                                {
                                    Clusters.Remove(previousCluster);
                                }
                            }
                            newCluster.AddItemToCluster(item);
                            ItemsToClusters[item.Id] = newCluster;
                            isAssignementChanged = true;
                            break;
                        }
                    }
                }

                if (ItemsToClusters.ContainsKey(item.Id) == false)
                {
                    EstimatorCluster newCluster = new EstimatorCluster(item);
                    Clusters.Add(newCluster);
                    ItemsToClusters.Add(item.Id, newCluster);
                    isAssignementChanged = true;
                }

                iterationCounter--;
            }

            AssignHyperCluster();
        }

        public void AssignHyperCluster()
        {
            int iterationCounter = IterationLimit;
            bool isAssignementChanged = true;

            while (isAssignementChanged && iterationCounter > 0)
            {
                isAssignementChanged = false;
                for (int j = 0; j < Clusters.Count; j++)
                {
                    List<KeyValuePair<EstimatorHyperCluster, double>> hyperClusterToProximityList =
                        new List<KeyValuePair<EstimatorHyperCluster, double>>();
                    EstimatorCluster cluster = Clusters[j];
                    double clusterVectorMagnitude = CalculateVectorMagnitude(cluster.ClusterVector);
                    double proximityThreshold =
                        clusterVectorMagnitude / (bValue + rangeLimit * ItemSize);

                    for (int i = 0; i < HyperClusters.Count; i++)
                    {
                        double hyperClusterVectorMagnitude = CalculateVectorMagnitude(
                            HyperClusters[i].HyperClusterVector
                        );
                        double proximity =
                            CaulculateVectorIntersectionMagnitude(
                                cluster.ClusterVector,
                                HyperClusters[i].HyperClusterVector
                            ) / (bValue + hyperClusterVectorMagnitude);
                        if (proximity > proximityThreshold)
                        {
                            hyperClusterToProximityList.Add(
                                new KeyValuePair<EstimatorHyperCluster, double>(
                                    HyperClusters[i],
                                    proximity
                                )
                            );
                        }
                    }

                    if (hyperClusterToProximityList.Count > 0)
                    {
                        hyperClusterToProximityList.Sort((x, y) => -1 * x.Value.CompareTo(y.Value));

                        for (int i = 0; i < hyperClusterToProximityList.Count; i++)
                        {
                            EstimatorHyperCluster newHyperCluster = hyperClusterToProximityList[i].Key;
                            double vigilance =
                                CaulculateVectorIntersectionMagnitude(
                                    newHyperCluster.HyperClusterVector,
                                    cluster.ClusterVector
                                ) / clusterVectorMagnitude;
                            if (vigilance >= p2Value)
                            {
                                if (ClustersToHyperClusters.ContainsKey(cluster))
                                {
                                    EstimatorHyperCluster previousHyperCluster = ClustersToHyperClusters[
                                        cluster
                                    ];
                                    if (ReferenceEquals(newHyperCluster, previousHyperCluster))
                                        break;
                                    if (
                                        previousHyperCluster.RemoveClusterFromHyperCluster(cluster)
                                        == false
                                    )
                                    {
                                        HyperClusters.Remove(previousHyperCluster);
                                    }
                                }
                                newHyperCluster.AddClusterToHyperCluster(cluster);
                                ClustersToHyperClusters[cluster] = newHyperCluster;
                                isAssignementChanged = true;

                                break;
                            }
                        }
                    }

                    if (ClustersToHyperClusters.ContainsKey(cluster) == false)
                    {
                        EstimatorHyperCluster newHyperCluster = new EstimatorHyperCluster(cluster);
                        HyperClusters.Add(newHyperCluster);
                        ClustersToHyperClusters.Add(cluster, newHyperCluster);
                        isAssignementChanged = true;
                    }
                }

                iterationCounter--;
            }
        }

        public EstimatorItem SimilarTo(EstimatorItem item)
        {
            StringBuilder outputText = new StringBuilder();
            double tempItemSimilarSum = 0;
            double itemSimilarSum = 0;
            EstimatorItem itemSimilar = null;
            EstimatorCluster cluster = null;

            ItemsToClusters.TryGet(item.Id, out cluster);
            if (cluster == null) { }
            else
            {
                EstimatorSeries clusterItemList = cluster.ClusterItems;
                for (int i = 0; i < clusterItemList.Count; i++)
                {
                    if (!ReferenceEquals(item, clusterItemList[i]))
                    {
                        tempItemSimilarSum =
                            CaulculateVectorIntersectionMagnitude(
                                item.Vector,
                                clusterItemList[i].Vector
                            ) / CalculateVectorMagnitude(clusterItemList[i].Vector);
                        if (itemSimilarSum == 0 || itemSimilarSum < tempItemSimilarSum)
                        {
                            itemSimilarSum = tempItemSimilarSum;
                            itemSimilar = clusterItemList[i];
                        }
                    }
                }

                if (itemSimilar != null)
                {
                    outputText.Append(
                        " Most similiar taste have item " + itemSimilar.Name + "\r\n\r\n"
                    );
                }
                else
                {
                    outputText.Append(" There is no similiar item " + item.Name + "\r\n\r\n");
                }
            }
            Debug.WriteLine(outputText.ToString());

            return itemSimilar;
        }

        public EstimatorItem SimilarInGroupsTo(EstimatorItem item)
        {
            StringBuilder outputText = new StringBuilder();
            double tempItemSimilarSum = 0;
            double itemSimilarSum = 0;
            EstimatorItem itemSimilar = null;
            EstimatorCluster cluster = null;

            ItemsToClusters.TryGet(item.Id, out cluster);
            if (cluster == null) { }
            else
            {
                EstimatorHyperCluster hyperCluster = ClustersToHyperClusters[cluster];
                EstimatorSeries hyperClusterItemList = hyperCluster.GetHyperClusterItems();
                for (int i = 0; i < hyperClusterItemList.Count; i++)
                {
                    if (!ReferenceEquals(item, hyperClusterItemList[i]))
                    {
                        tempItemSimilarSum =
                            CaulculateVectorIntersectionMagnitude(
                                item.Vector,
                                hyperClusterItemList[i].Vector
                            ) / CalculateVectorMagnitude(hyperClusterItemList[i].Vector);
                        if (itemSimilarSum == 0 || itemSimilarSum < tempItemSimilarSum)
                        {
                            itemSimilarSum = tempItemSimilarSum;
                            itemSimilar = hyperClusterItemList[i];
                        }
                    }
                }

                if (itemSimilar != null)
                {
                    outputText.Append(
                        " Most similiar taste in hyper cluster have item "
                            + itemSimilar.Name
                            + "\r\n\r\n"
                    );
                }
                else
                {
                    outputText.Append(
                        " There is no simiilar item in hyper cluster " + item.Name + "\r\n\r\n"
                    );
                }
            }
            Debug.WriteLine(outputText.ToString());

            return itemSimilar;
        }

        public EstimatorItem SimilarInOtherGroupsTo(EstimatorItem item)
        {
            StringBuilder outputText = new StringBuilder();
            double tempItemSimilarSum = 0;
            double itemSimilarSum = 0;
            EstimatorItem itemSimilar = null;

            if (!ItemsToClusters.TryGet(item.Id, out EstimatorCluster cluster)) { }
            else
            {
                EstimatorHyperCluster hyperCluster = ClustersToHyperClusters[cluster];
                for (int j = 0; j < hyperCluster.Clusters.Count; j++)
                {
                    if (!ReferenceEquals(cluster, hyperCluster.Clusters[j]))
                    {
                        EstimatorSeries clusterItemList = hyperCluster.Clusters[
                            j
                        ].ClusterItems;
                        for (int i = 0; i < clusterItemList.Count; i++)
                        {
                            tempItemSimilarSum =
                                CaulculateVectorIntersectionMagnitude(
                                    item.Vector,
                                    clusterItemList[i].Vector
                                ) / CalculateVectorMagnitude(clusterItemList[i].Vector);
                            if (itemSimilarSum == 0 || itemSimilarSum < tempItemSimilarSum)
                            {
                                itemSimilarSum = tempItemSimilarSum;
                                itemSimilar = clusterItemList[i];
                            }
                        }
                    }
                }

                if (itemSimilar != null)
                {
                    outputText.Append(
                        " Most similiar taste in hyper cluster (other clusters) have item "
                            + itemSimilar.Name
                            + "\r\n\r\n"
                    );
                }
                else
                {
                    outputText.Append(
                        " There is no simiilar item in hyper cluster (other clusters) "
                            + item.Name
                            + "\r\n\r\n"
                    );
                }
            }
            Debug.WriteLine(outputText.ToString());

            return itemSimilar;
        }

        public static double[] CalculateIntersection(EstimatorSeries input, double[] output)
        {
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = input[0].Vector[i];
                for (int j = 1; j < input.Count; j++)
                {
                    output[i] = Math.Min(output[i], input[j].Vector[i]);
                }
            }
            return output;
        }

        public static double[] CalculateSummary(EstimatorSeries input, double[] output)
        {
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = 0;
                for (int j = 0; j < input.Count; j++)
                {
                    output[i] += input[j].Vector[i];
                }
            }

            return output;
        }

        public static double[] UpdateIntersectionByLast(EstimatorSeries input, double[] output)
        {
            int n = input.Count - 1;
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = Math.Min(output[i], input[n].Vector[i]);
            }
            return output;
        }

        public static double[] UpdateSummaryByLast(EstimatorSeries input, double[] output)
        {
            int n = input.Count - 1;
            for (int i = 0; i < output.Length; i++)
            {
                output[i] += input[n].Vector[i];
            }
            return output;
        }

        public static double[] CalculateClusterIntersection(List<EstimatorCluster> input, double[] output)
        {
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = input[0].ClusterVector[i];
                for (int j = 1; j < input.Count; j++)
                {
                    output[i] = Math.Min(output[i], input[j].ClusterVector[i]);
                }
            }
            return output;
        }

        public static double[] CalculateClusterSummary(List<EstimatorCluster> input, double[] output)
        {
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = 0;
                for (int j = 0; j < input.Count; j++)
                {
                    output[i] += input[j].ClusterVector[i];
                }
            }

            return output;
        }

        public static double[] UpdateClusterIntersectionByLast(List<EstimatorCluster> input, double[] output)
        {
            int n = input.Count - 1;
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = Math.Min(output[i], input[n].ClusterVector[i]);
            }
            return output;
        }

        public static double[] UpdateClusterSummaryByLast(List<EstimatorCluster> input, double[] output)
        {
            int n = input.Count - 1;
            for (int i = 0; i < output.Length; i++)
            {
                output[i] += input[n].ClusterVector[i];
            }
            return output;
        }

        public static EstimatorSeries NormalizeItemList(EstimatorSeries featureItemList)
        {
            EstimatorSeries normalizedItemList = new EstimatorSeries();

            int length;
            for (int i = 0; i < featureItemList.Count; i++)
            {
                length = featureItemList[0].Vector.Length;
                double[] featureVector = new double[length];
                for (int j = 0; j < length; j++)
                {
                    featureVector[j] = featureItemList[i].Vector[j] / 10.00;
                }
                normalizedItemList.Add(
                    new EstimatorItem(
                        featureItemList[i].Id,
                        (string)featureItemList[(int)i].Name,
                        featureVector
                    )
                );
            }
            return normalizedItemList;
        }

        static public double CalculateVectorMagnitude(double[] vector)
        {
            double result = 0;
            for (int i = 0; i < vector.Length; ++i)
            {
                result += vector[i];
            }
            return result;
        }

        static public double CaulculateVectorIntersectionMagnitude(
            double[] vector1,
            double[] vector2
        )
        {
            double result = 0;

            for (int i = 0; i < vector1.Length; ++i)
            {
                result += Math.Min(vector1[i], vector2[i]);
            }

            return result;
        }

        public void LoadFile(string fileLocation)
        {
            string line;
            NameList.Clear();
            NameList.Add("Name");

            StreamReader file = new StreamReader(fileLocation);

            while ((line = file.ReadLine()) != null)
            {
                if (line == "Items")
                {
                    break;
                }
            }

            if (line == null)
            {
                throw new Exception("ART File does not have a section marked Items!");
            }
            else
            {
                while ((line = file.ReadLine()) != null)
                {
                    if (line == "--")
                    {
                        break;
                    }
                    else
                    {
                        NameList.Add(line);
                    }
                }
                ItemSize = NameList.Count - 1;

                int featureItemId = 0;
                while ((line = file.ReadLine()) != null)
                {
                    string featureName = line;
                    line = file.ReadLine();
                    double[] featureVector = new double[ItemSize];
                    int i = 0;
                    while ((line != null) && (line != "--"))
                    {
                        featureVector[i] = Int32.Parse(line);
                        ++i;
                        line = file.ReadLine();
                    }

                    if (line == "--")
                    {
                        if (i != ItemSize)
                        {
                            for (int j = i; j < ItemSize; ++j)
                            {
                                featureVector[j] = 0;
                            }
                        }
                        Items.Add(new EstimatorItem(featureItemId, featureName, featureVector));
                        featureItemId++;
                    }
                }
            }

            file.Close();
        }
    }
}
