namespace Undersoft.SDK.Rubrics.Attributes
{
    using Undersoft.SDK.Instant.Attributes;

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public class RubricAttribute : InstantAttribute
    {
        public RubricAttribute()
        {
        }
    }
}
