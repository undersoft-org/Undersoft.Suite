using Undersoft.SDK.Instant;

namespace Undersoft.SDK.Rubrics
{
    using Series;
    using System.Collections.Generic;
    using System.Linq;
    using Undersoft.SDK.Extracting;
    using Undersoft.SDK.Instant.Series;
    using Undersoft.SDK.Series.Base;
    using Undersoft.SDK.Uniques.Hashing;


    public partial class MemberRubrics : ListingBase<MemberRubric>, IRubrics
    {
        private int binarySize;
        private int[] binarySizes;
        private int[] ordinals;

        public MemberRubrics() : base() { }

        public MemberRubrics(IEnumerable<MemberRubric> collection) : base(collection) { }

        public MemberRubrics(IList<MemberRubric> collection) : base(collection) { }

        public int BinarySize
        {
            get => binarySize;
        }

        public int[] BinarySizes
        {
            get => binarySizes;
        }

        public IInstantSeries Series { get; set; }

        public IRubrics KeyRubrics { get; set; }

        public RubricSqlMappings Mappings { get; set; }

        public int[] Ordinals
        {
            get => ordinals;
        }

        public override ISeriesItem<MemberRubric> EmptyItem()
        {
            return new RubricItem();
        }

        public override ISeriesItem<MemberRubric>[] EmptyTable(int size)
        {
            return new RubricItem[size];
        }

        public override ISeriesItem<MemberRubric>[] EmptyVector(int size)
        {
            return new RubricItem[size];
        }

        public unsafe byte[] GetBytes(IInstant figure)
        {
            int size = figure.GetSize();
            byte* figurePtr = stackalloc byte[size * 2];
            byte* bufferPtr = figurePtr + size;
            figure.StructureTo(figurePtr);
            int destOffset = 0;
            foreach (var rubric in AsValues())
            {
                int l = rubric.RubricSize;
                Extract.CopyBlock(bufferPtr, destOffset, figurePtr, rubric.RubricOffset, l);
                destOffset += l;
            }
            byte[] b = new byte[destOffset];
            fixed (byte* bp = b)
                Extract.CopyBlock(bp, bufferPtr, destOffset);
            return b;
        }

        public unsafe byte[] GetUniqueBytes(IInstant figure, uint seed = 0)
        {
            int size = figure.GetSize();
            byte* figurePtr = stackalloc byte[size * 2];
            byte* bufferPtr = figurePtr + size;
            figure.StructureTo(figurePtr);
            int destOffset = 0;
            foreach (var rubric in AsValues())
            {
                int l = rubric.RubricSize;
                Extract.CopyBlock(bufferPtr, destOffset, figurePtr, rubric.RubricOffset, l);
                destOffset += l;
            }
            ulong hash = Hasher64.ComputeKey(bufferPtr, destOffset, seed);
            byte[] b = new byte[8];
            fixed (byte* bp = b)
                *(ulong*)bp = hash;
            return b;
        }

        public unsafe long GetUniqueKey(IInstant figure, uint seed = 0)
        {
            int size = figure.GetSize();
            byte* figurePtr = stackalloc byte[size * 2];
            byte* bufferPtr = figurePtr + size;
            figure.StructureTo(figurePtr);
            int destOffset = 0;
            foreach (var rubric in AsValues())
            {
                int l = rubric.RubricSize;
                Extract.CopyBlock(bufferPtr, destOffset, figurePtr, rubric.RubricOffset, l);
                destOffset += l;
            }
            return (long)Hasher64.ComputeKey(bufferPtr, destOffset, seed);
        }

        public override ISeriesItem<MemberRubric> NewItem(ISeriesItem<MemberRubric> value)
        {
            return new RubricItem(value);
        }

        public override ISeriesItem<MemberRubric> NewItem(MemberRubric value)
        {
            return new RubricItem(value);
        }

        public override ISeriesItem<MemberRubric> NewItem(object key, MemberRubric value)
        {
            return new RubricItem(key, value);
        }

        public override ISeriesItem<MemberRubric> NewItem(long key, MemberRubric value)
        {
            return new RubricItem(key, value);
        }

        public void SetUniqueKey(IInstant figure, uint seed = 0)
        {
            figure.Id = GetUniqueKey(figure, seed);
        }

        public void Update()
        {
            ordinals = AsValues().Select(o => o.RubricId).ToArray();

            binarySizes = AsValues().Select(o => o.RubricSize).ToArray();

            binarySize = AsValues().Sum(b => b.RubricSize);

            if (KeyRubrics != null)
            {
                KeyRubrics.Update();
                KeyRubrics.ForEach(
                    (k) =>
                    {
                        var r = this[k];
                        r.IsKey = true;
                        r.IsIdentity = true;
                    }
                );
            }

            AsValues()
                .Where(r => r.IsKey || r.RubricType is IIdentifiable)
                .ForEach(r => r.IsUnique = true)
                .ToArray();
        }

        public bool Visible { get; set; }
    }
}
