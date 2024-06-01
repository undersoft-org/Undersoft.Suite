namespace Undersoft.SDK.Series.Universe
{
    using System.Collections.Generic;
    using Undersoft.SDK.Series.Universe.Base;
    using vanEmdeBoasTree;

    public class UniverseScope : Universe
    {
        public static int NULL_KEY = -1;

        private static int MINIMUM_UNIVERSE_SIZE_U4 = 4;

        private byte level;

        private int max;
        private int min;

        private byte nodeId;
        private int registryId;

        private int parentSqrt;
        private int childSqrt;

        internal Universe sigmaNode;
        private IList<vEBTreeLevel> levels;
        private ISeries<Universe> scopes;
        private ISeries<Universe> sigmaScopes;

        private int size;

        public UniverseScope(
            int Size,
            ISeries<Universe> Scopes,
            ISeries<Universe> SigmaScopes,
            IList<vEBTreeLevel> Levels,
            byte Level,
            byte NodeIndex,
            int DeckIndex
        )
        {
            min = NULL_KEY;
            max = NULL_KEY;
            sigmaNode = null;

            scopes = Scopes;
            sigmaScopes = SigmaScopes;
            levels = Levels;

            size = Size;
            parentSqrt = ParentSqrt(size);
            childSqrt = ChildSqrt(size);

            nodeId = NodeIndex;
            level = Level;
            registryId = DeckIndex;
        }

        public override int IndexMax
        {
            get { return max; }
        }

        public override int IndexMin
        {
            get { return min; }
        }

        public override int Size
        {
            get { return size; }
        }

        public static int ChildSqrt(int number)
        {
            double exponent = Math.Floor(Math.Log(number) / Math.Log(2.0) / 2.0);
            return (int)Math.Pow(2.0, exponent);
        }

        public static int ParentSqrt(int number)
        {
            double exponent = Math.Ceiling(Math.Log(number) / Math.Log(2.0) / 2.0);
            return (int)Math.Pow(2.0, exponent);
        }

        public override void Add(int offsetBase, int offsetFactor, int indexOffset, int x)
        {
            if (min == x || max == x)
            {
                return;
            }

            if (min == NULL_KEY)
            {
                FirstAdd(
                    offsetBase + offsetFactor * parentSqrt,
                    offsetFactor * parentSqrt,
                    indexOffset * parentSqrt + highest(x),
                    x
                );
                return;
            }

            if (x < min)
            {
                int tmp = x;
                x = min;
                min = tmp;
            }

            if (size != MINIMUM_UNIVERSE_SIZE_U4)
            {
                Universe scopesItem;
                int x_highest = highest(x);

                int scopesKey = offsetBase + indexOffset * parentSqrt + x_highest;

                if (!scopes.TryGet(scopesKey, out scopesItem))
                {
                    if (parentSqrt == MINIMUM_UNIVERSE_SIZE_U4)
                    {
                        if (sigmaNode == null)
                        {
                            sigmaNode = new UniverseTetraValue(-1);
                            sigmaNode.FirstAdd(x_highest);
                        }
                        else
                        {
                            sigmaNode.Add(x_highest);
                        }
                        scopesItem = new UniverseTetraValue(-1);
                        scopes.Add(scopesKey, scopesItem);
                        scopesItem.FirstAdd(lowest(x));
                    }
                    else
                    {
                        if (sigmaNode == null)
                        {
                            sigmaNode = new UniverseScope(
                                parentSqrt,
                                scopes,
                                sigmaScopes,
                                levels,
                                (byte)(level + 1),
                                (byte)(2 * nodeId),
                                registryId
                            );
                            sigmaNode.FirstAdd(x_highest);
                        }
                        else
                        {
                            sigmaNode.Add(x_highest);
                        }

                        scopesItem = new UniverseScope(
                            parentSqrt,
                            scopes,
                            sigmaScopes,
                            levels,
                            (byte)(level + 1),
                            (byte)(2 * nodeId + 1),
                            registryId * levels[level].Nodes[nodeId].NodeSize + x_highest
                        );
                        scopes.Add(scopesKey, scopesItem);
                        scopesItem.FirstAdd(
                            offsetBase + offsetFactor * parentSqrt,
                            offsetFactor * parentSqrt,
                            indexOffset * parentSqrt + x_highest,
                            lowest(x)
                        );
                    }
                }
                else
                {
                    scopesItem.Add(
                        offsetBase + offsetFactor * parentSqrt,
                        offsetFactor * parentSqrt,
                        indexOffset * parentSqrt + x_highest,
                        lowest(x)
                    );
                }
            }

            if (max < x)
            {
                max = x;
            }
        }

        public override void Add(int x)
        {
            if (min == x || max == x)
            {
                return;
            }

            if (x < min)
            {
                int tmp = x;
                x = min;
                min = tmp;
            }

            if (size != MINIMUM_UNIVERSE_SIZE_U4)
            {
                int x_highest = highest(x);

                Universe sigmaScopesItem;

                int sigmaScopesKey =
                    levels[level].Nodes[nodeId].IndexOffset
                    + levels[level].Nodes[nodeId].NodeSize * registryId
                    + x_highest;

                if (!sigmaScopes.TryGet(sigmaScopesKey, out sigmaScopesItem))
                {
                    if (parentSqrt == MINIMUM_UNIVERSE_SIZE_U4)
                    {
                        if (sigmaNode == null)
                        {
                            sigmaNode = new UniverseTetraValue(-1);
                            sigmaNode.FirstAdd(x_highest);
                        }
                        else
                        {
                            sigmaNode.Add(x_highest);
                        }

                        sigmaScopesItem = new UniverseTetraValue(-1);
                        sigmaScopes.Add(sigmaScopesKey, sigmaScopesItem);
                        sigmaScopesItem.FirstAdd(lowest(x));
                    }
                    else
                    {
                        if (sigmaNode == null)
                        {
                            sigmaNode = new UniverseScope(
                                parentSqrt,
                                scopes,
                                sigmaScopes,
                                levels,
                                (byte)(level + 1),
                                (byte)(2 * nodeId),
                                registryId
                            );
                            sigmaNode.FirstAdd(x_highest);
                        }
                        else
                        {
                            sigmaNode.Add(x_highest);
                        }

                        sigmaScopesItem = new UniverseScope(
                            parentSqrt,
                            scopes,
                            sigmaScopes,
                            levels,
                            (byte)(level + 1),
                            (byte)(2 * nodeId + 1),
                            registryId * levels[level].Nodes[nodeId].NodeSize + x_highest
                        );
                        sigmaScopes.Add(sigmaScopesKey, sigmaScopesItem);
                        sigmaScopesItem.FirstAdd(lowest(x));
                    }
                }
                else
                {
                    sigmaScopesItem.Add(lowest(x));
                }
            }

            if (max < x)
            {
                max = x;
            }
        }

        public override bool Contains(int offsetBase, int offsetFactor, int indexOffset, int x)
        {
            if (x == min || x == max)
            {
                return true;
            }
            else
            {
                if (size == MINIMUM_UNIVERSE_SIZE_U4 || x < min || x > max)
                {
                    return false;
                }
                else
                {
                    int scopesKey = offsetBase + indexOffset * parentSqrt + highest(x);
                    Universe scopesItem;
                    if (!scopes.TryGet(scopesKey, out scopesItem))
                        return false;
                    return scopesItem.Contains(
                        offsetBase + offsetFactor * parentSqrt,
                        offsetFactor * parentSqrt,
                        indexOffset * parentSqrt + highest(x),
                        lowest(x)
                    );
                }
            }
        }

        public override bool Contains(int x)
        {
            return Contains(0, 1, 0, x);
        }

        public override void FirstAdd(int offsetBase, int offsetFactor, int indexOffset, int x)
        {
            min = x;
            max = x;
        }

        public override void FirstAdd(int x)
        {
            min = x;
            max = x;
        }

        public override int Next(int offsetBase, int offsetFactor, int indexOffset, int x)
        {
            if (min != NULL_KEY && x < min)
            {
                return min;
            }

            Universe scopesItem;
            int x_highest = highest(x);
            int scopesKey;

            scopesKey = offsetBase + indexOffset * parentSqrt + x_highest;

            int maximumLow = NULL_KEY;
            if (scopes.TryGet(scopesKey, out scopesItem))
            {
                maximumLow = scopesItem.IndexMax;
            }

            if (maximumLow != NULL_KEY && lowest(x) < maximumLow)
            {
                int _offset = scopesItem.Next(
                    offsetBase + offsetFactor * parentSqrt,
                    offsetFactor * parentSqrt,
                    indexOffset * parentSqrt + x_highest,
                    lowest(x)
                );

                return index(x_highest, _offset);
            }

            if (sigmaNode == null)
            {
                return NULL_KEY;
            }

            int successorCluster = sigmaNode.Next(x_highest);

            if (successorCluster == NULL_KEY)
            {
                return NULL_KEY;
            }

            scopesKey = offsetBase + indexOffset * parentSqrt + successorCluster;

            scopes.TryGet(scopesKey, out scopesItem);
            int offset = scopesItem.IndexMin;

            return index(successorCluster, offset);
        }

        public override int Next(int x)
        {
            if (min != NULL_KEY && x < min)
            {
                return min;
            }

            int x_highest = highest(x);

            Universe sigmaScopesItem;

            vEBTreeNode nodeTypeInfo = levels[level].Nodes[nodeId];
            int sigmaScopesKey =
                nodeTypeInfo.IndexOffset + nodeTypeInfo.NodeSize * registryId + x_highest;

            int maximumLow = NULL_KEY;

            if (sigmaScopes.TryGet(sigmaScopesKey, out sigmaScopesItem))
            {
                maximumLow = sigmaScopesItem.IndexMax;
            }

            if (maximumLow != NULL_KEY && lowest(x) < maximumLow)
            {
                int _offset = sigmaScopesItem.Next(lowest(x));
                return index(x_highest, _offset);
            }

            if (sigmaNode == null)
            {
                return NULL_KEY;
            }

            int successorCluster = sigmaNode.Next(x_highest);
            if (successorCluster == NULL_KEY)
            {
                return NULL_KEY;
            }

            sigmaScopesKey =
                nodeTypeInfo.IndexOffset + nodeTypeInfo.NodeSize * registryId + successorCluster;

            sigmaScopes.TryGet(sigmaScopesKey, out sigmaScopesItem);
            int offset = sigmaScopesItem.IndexMin;
            return index(successorCluster, offset);
        }

        public override int Previous(int offsetBase, int offsetFactor, int indexOffset, int x)
        {
            if (max != NULL_KEY && x > max)
            {
                return max;
            }

            Universe scopesItem;
            int x_highest = highest(x);
            int scopesKey;

            scopesKey = offsetBase + indexOffset * parentSqrt + x_highest;

            int minimumLow = NULL_KEY;
            if (scopes.TryGet(scopesKey, out scopesItem))
            {
                minimumLow = scopesItem.IndexMin;
            }

            if (minimumLow != NULL_KEY && lowest(x) > minimumLow)
            {
                int _offset = scopesItem.Previous(
                    offsetBase + offsetFactor * parentSqrt,
                    offsetFactor * parentSqrt,
                    indexOffset * parentSqrt + x_highest,
                    lowest(x)
                );
                return index(x_highest, _offset);
            }

            if (sigmaNode == null)
            {
                return NULL_KEY;
            }

            int predecessorCluster = sigmaNode.Previous(x_highest);
            if (predecessorCluster == NULL_KEY)
            {
                if (min != NULL_KEY && x > min)
                {
                    return min;
                }

                return NULL_KEY;
            }
            scopesKey = offsetBase + indexOffset * parentSqrt + predecessorCluster;

            scopes.TryGet(scopesKey, out scopesItem);
            int offset = scopesItem.IndexMax;
            return index(predecessorCluster, offset);
        }

        public override int Previous(int x)
        {
            if (max != NULL_KEY && x > max)
            {
                return max;
            }

            int x_highest = highest(x);

            Universe sigmaScopesItem;

            vEBTreeNode nodeTypeInfo = levels[level].Nodes[nodeId];
            int sigmaScopesKey =
                nodeTypeInfo.IndexOffset + nodeTypeInfo.NodeSize * registryId + x_highest;

            int minimumLow = NULL_KEY;
            if (sigmaScopes.TryGet(sigmaScopesKey, out sigmaScopesItem))
            {
                minimumLow = sigmaScopesItem.IndexMin;
            }

            if (minimumLow != NULL_KEY && lowest(x) > minimumLow)
            {
                int _offset = sigmaScopesItem.Previous(lowest(x));
                return index(x_highest, _offset);
            }

            if (sigmaNode == null)
            {
                return NULL_KEY;
            }

            int predecessorCluster = sigmaNode.Previous(x_highest);
            if (predecessorCluster == NULL_KEY)
            {
                if (min != NULL_KEY && x > min)
                {
                    return min;
                }

                return NULL_KEY;
            }

            sigmaScopesKey =
                nodeTypeInfo.IndexOffset + nodeTypeInfo.NodeSize * registryId + predecessorCluster;

            sigmaScopes.TryGet(sigmaScopesKey, out sigmaScopesItem);
            int offset = sigmaScopesItem.IndexMax;
            return index(predecessorCluster, offset);
        }

        public override bool Remove(int offsetBase, int offsetFactor, int indexOffset, int x)
        {
            if (min == max)
            {
                if (min != x)
                    return false;
                min = NULL_KEY;
                max = NULL_KEY;
                sigmaNode = null;
                return true;
            }

            Universe scopesItem;
            int x_highest;
            int scopesKey;

            if (min == x)
            {
                int firstCluster = sigmaNode.IndexMin;

                scopesKey = offsetBase + indexOffset * parentSqrt + firstCluster;
                scopes.TryGet(scopesKey, out scopesItem);
                x = index(firstCluster, scopesItem.IndexMin);

                min = x;
            }

            x_highest = highest(x);
            scopesKey = offsetBase + indexOffset * parentSqrt + x_highest;

            if (!scopes.TryGet(scopesKey, out scopesItem))
                return false;

            scopesItem.Remove(
                offsetBase + offsetFactor * parentSqrt,
                offsetFactor * parentSqrt,
                indexOffset * parentSqrt + x_highest,
                lowest(x)
            );

            if (scopesItem.IndexMin == NULL_KEY)
            {
                scopes.Remove(scopesKey);

                sigmaNode.Remove(highest(x));

                if (x == max)
                {
                    int sigmaNodeMaximum = sigmaNode.IndexMax;

                    if (sigmaNodeMaximum == NULL_KEY)
                    {
                        max = min;
                        sigmaNode = null;
                    }
                    else
                    {
                        scopesKey = offsetBase + indexOffset * parentSqrt + sigmaNodeMaximum;
                        scopes.TryGet(scopesKey, out scopesItem);

                        int maximumKey = scopesItem.IndexMax;
                        max = index(sigmaNodeMaximum, maximumKey);
                    }
                }
            }
            else if (x == max)
            {
                scopesKey = offsetBase + indexOffset * parentSqrt + highest(x);
                scopes.TryGet(scopesKey, out scopesItem);
                int maximumKey = scopesItem.IndexMax;

                max = index(highest(x), maximumKey);
            }
            return true;
        }

        public override bool Remove(int x)
        {
            if (min == max)
            {
                if (min != x)
                    return true;
                min = NULL_KEY;
                max = NULL_KEY;
                sigmaNode = null;
                return true;
            }

            Universe sigmaScopesItem;
            vEBTreeNode nodeTypeInfo = levels[level].Nodes[nodeId];
            ;
            int sigmaScopesKey;

            int x_highest;

            if (min == x)
            {
                int first = sigmaNode.IndexMin;

                sigmaScopesKey =
                    nodeTypeInfo.IndexOffset + nodeTypeInfo.NodeSize * registryId + first;
                sigmaScopes.TryGet(sigmaScopesKey, out sigmaScopesItem);
                x = index(first, sigmaScopesItem.IndexMin);

                min = x;
            }

            x_highest = highest(x);

            sigmaScopesKey =
                nodeTypeInfo.IndexOffset + nodeTypeInfo.NodeSize * registryId + x_highest;
            if (!sigmaScopes.TryGet(sigmaScopesKey, out sigmaScopesItem))
                return false;
            sigmaScopesItem.Remove(lowest(x));

            if (sigmaScopesItem.IndexMin == NULL_KEY)
            {
                sigmaScopes.Remove(sigmaScopesKey);

                sigmaNode.Remove(highest(x));

                if (x == max)
                {
                    int sigmaNodeMaximum = sigmaNode.IndexMax;

                    if (sigmaNodeMaximum == NULL_KEY)
                    {
                        max = min;
                        sigmaNode = null;
                    }
                    else
                    {
                        sigmaScopesKey =
                            nodeTypeInfo.IndexOffset
                            + nodeTypeInfo.NodeSize * registryId
                            + sigmaNodeMaximum;
                        sigmaScopes.TryGet(sigmaScopesKey, out sigmaScopesItem);
                        int maximumKey = sigmaScopesItem.IndexMax;

                        max = index(sigmaNodeMaximum, maximumKey);
                    }
                }
            }
            else if (x == max)
            {
                sigmaScopesKey =
                    nodeTypeInfo.IndexOffset + nodeTypeInfo.NodeSize * registryId + highest(x);
                sigmaScopes.TryGet(sigmaScopesKey, out sigmaScopesItem);
                int maximumKey = sigmaScopesItem.IndexMax;

                max = index(highest(x), maximumKey);
            }
            return true;
        }

        private int highest(int x)
        {
            return x / childSqrt;
        }

        private int index(int x, int y)
        {
            return x * childSqrt + y;
        }

        private int lowest(int x)
        {
            return x & childSqrt - 1;
        }
    }
}
