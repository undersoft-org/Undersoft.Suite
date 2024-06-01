namespace Undersoft.SDK.Series.Universe.vanEmdeBoasTree
{
    using System.Collections.Generic;

    public class vEBTreeLevel
    {
        public vEBTreeLevel()
        {
            Level = 0;
            BaseOffset = 0;
            Nodes = null;
        }

        public int BaseOffset { get; set; }

        public byte Count { get; set; }

        public byte Level { get; set; }

        public IList<vEBTreeNode> Nodes { get; set; }
    }
}
