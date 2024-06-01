using System;

namespace Undersoft.SDK.Rubrics.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class KeyRubricAttribute : IdentityRubricAttribute
    {
        public KeyRubricAttribute() { }
    }
}
