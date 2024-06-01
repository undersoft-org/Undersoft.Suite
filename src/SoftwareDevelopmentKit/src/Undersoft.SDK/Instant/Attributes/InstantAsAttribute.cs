using System.Runtime.InteropServices;

namespace Undersoft.SDK.Instant.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class InstantAsAttribute : InstantAttribute
    {
        public int SizeConst;
        public UnmanagedType Value;

        public InstantAsAttribute(UnmanagedType unmanaged)
        {
            Value = unmanaged;
        }
    }
}
