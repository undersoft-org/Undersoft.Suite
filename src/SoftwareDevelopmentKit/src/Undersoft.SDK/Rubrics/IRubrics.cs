namespace Undersoft.SDK.Rubrics
{
    using Series;
    using Undersoft.SDK.Instant;
    using Undersoft.SDK.Instant.Series;

    public interface IRubrics : ISeries<MemberRubric>
    {
        int BinarySize { get; }

        int[] BinarySizes { get; }

        IInstantSeries Series { get; set; }

        IRubrics KeyRubrics { get; set; }

        RubricSqlMappings Mappings { get; set; }

        int[] Ordinals { get; }

        byte[] GetBytes(IInstant figure);

        byte[] GetUniqueBytes(IInstant figure, uint seed = 0);

        void Update();
    }
}
