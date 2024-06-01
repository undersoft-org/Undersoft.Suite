namespace Undersoft.SDK.Instant
{
    using Rubrics;

    public interface IInstantCreator
    {
        Type BaseType { get; set; }

        string Name { get; set; }

        IRubrics Rubrics { get; }

        int Size { get; }

        Type Type { get; set; }

        object New();
    }
}
