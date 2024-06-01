namespace Undersoft.SDK.Series.Universe
{
    using System.Collections;
    using System.Collections.Generic;
    using Undersoft.SDK.Series.Universe.Base;
    using Undersoft.SDK.Uniques;
    using vanEmdeBoasTree;

    public class Universe<V> : IUniverse<V>, IUnique
    {
        private IList<vEBTreeLevel> levels;
        private ISeries<V> registry;
        private Universe root;
        private ISeries<Universe> scopes;
        private ISeries<Universe> sigmaScopes;
        private int size;
        private Uscn serialcode;

        public Universe() : this(int.MaxValue, false) { }

        public Universe(int size, bool safeThread)
        {
            Initialize(size);
        }

        public int Count => registry.Count;

        public int IndexMax
        {
            get { return root.IndexMax; }
        }

        public int IndexMin
        {
            get { return root.IndexMin; }
        }

        public int Size { get; }

        public void Initialize(int range = 0, bool safeThred = false)
        {
            scopes = new Registry<Universe>();
            sigmaScopes = new Registry<Universe>();

            if (range == 0 || range > int.MaxValue)
            {
                range = int.MaxValue;
            }
            if (!safeThred)
                registry = new Registry<V>(false, range);
            else
                registry = new Registry<V>(false, range);

            size = range;

            CreateLevels(range);

            root = new UniverseScope(range, scopes, sigmaScopes, levels, 0, 0, 0);
        }

        public bool Add(int key, V obj)
        {
            if (registry.Add(key, obj))
            {
                root.Add(0, 1, 0, key);
                return true;
            }
            return false;
        }

        public bool Contains(int key)
        {
            return registry.ContainsKey(key);
        }

        public V Get(int key)
        {
            return registry.Get(key);
        }

        public IEnumerator<ISeriesItem<V>> GetEnumerator()
        {
            return new UniverseEnumerator<V>(this);
        }

        public int Next(int key)
        {
            return root.Next(0, 1, 0, key);
        }

        public int Previous(int key)
        {
            return root.Previous(0, 1, 0, key);
        }

        public bool Remove(int key)
        {
            if (registry.TryRemove(key))
            {
                root.Remove(0, 1, 0, key);
                return true;
            }
            return false;
        }

        public bool Set(int key, V value)
        {
            return registry.Set(key, value) != null;
        }

        public bool TryAdd(int key)
        {
            root.Add(0, 1, 0, key);
            return true;
        }

        public bool TryContains(int key)
        {
            return root.Contains(0, 1, 0, key);
        }

        public bool TryRemove(int key)
        {
            root.Remove(0, 1, 0, key);
            return true;
        }

        private void BuildSigmaScopes(
            int range,
            byte level,
            byte nodeTypeIndex,
            int nodeCounter,
            int clusterSize
        )
        {
            int parentSqrt = UniverseScope.ParentSqrt(range);

            if (levels == null)
            {
                levels = new List<vEBTreeLevel>();
            }
            if (levels.Count <= level)
            {
                levels.Add(new vEBTreeLevel());
            }
            if (levels[level].Nodes == null)
            {
                levels[level].Nodes = new List<vEBTreeNode>();
                levels[level].Nodes.Add(new vEBTreeNode());
            }
            else
            {
                levels[level].Nodes.Add(new vEBTreeNode());
            }

            levels[level].Nodes[nodeTypeIndex].NodeCounter = nodeCounter;
            levels[level].Nodes[nodeTypeIndex].NodeSize = parentSqrt;

            if (parentSqrt > 4)
            {
                BuildSigmaScopes(
                    parentSqrt,
                    (byte)(level + 1),
                    (byte)(2 * nodeTypeIndex),
                    nodeCounter,
                    parentSqrt
                );

                BuildSigmaScopes(
                    parentSqrt,
                    (byte)(level + 1),
                    (byte)(2 * nodeTypeIndex + 1),
                    nodeCounter * parentSqrt,
                    parentSqrt
                );
            }
        }

        private void CreateLevels(int range)
        {
            if (levels == null)
            {
                int parentSqrt = UniverseScope.ParentSqrt(size);
                BuildSigmaScopes(range, 0, 0, 1, parentSqrt);
            }

            int baseOffset = 0;
            for (int i = 1; i < levels.Count; i++)
            {
                levels[i].BaseOffset = baseOffset;
                for (int j = 0; j < levels[i].Nodes.Count - 1; j++)
                {
                    levels[i].Nodes[j].IndexOffset = baseOffset;
                    baseOffset += levels[i].Nodes[j].NodeCounter * levels[i].Nodes[j].NodeSize;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new UniverseEnumerator<V>(this);
        }

        public byte[] GetBytes()
        {
            return serialcode.GetBytes();
        }

        public byte[] GetIdBytes()
        {
            return serialcode.GetIdBytes();
        }

        public bool Equals(IUnique other)
        {
            return serialcode.Equals(other);
        }

        public int CompareTo(IUnique other)
        {
            return serialcode.CompareTo(other);
        }

        public long Id
        {
            get => serialcode.Id;
            set => serialcode.Id = value;
        }
        public long TypeId
        {
            get => serialcode.TypeId;
            set => serialcode.TypeId = value;
        }

    }
}
