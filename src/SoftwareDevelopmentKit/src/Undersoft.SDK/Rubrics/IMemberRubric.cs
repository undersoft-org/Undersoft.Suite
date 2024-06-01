using System.Reflection;

namespace Undersoft.SDK.Rubrics
{
    public interface IMemberRubric
    {
        bool Editable { get; set; }

        MemberInfo MemberInfo { get; }

        object[] RubricAttributes { get; set; }

        int RubricId { get; set; }

        int RubricOrdinal { get; set; }

        string RubricName { get; set; }

        string DisplayName { get; set; }

        int RubricOffset { get; set; }

        int RubricSize { get; set; }

        Type RubricType { get; set; }

        bool Visible { get; set; }
    }
}
