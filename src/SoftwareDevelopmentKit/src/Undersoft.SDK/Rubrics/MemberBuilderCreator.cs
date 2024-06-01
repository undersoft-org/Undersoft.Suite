namespace Undersoft.SDK.Rubrics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Undersoft.SDK.Proxies;
    using Undersoft.SDK.Series;

    public class MemberBuilderCreator
    {
        public ISeries<MemberBuilder> Create(Type modelType)
        {
            return Create(PrepareMembers(GetMembers(modelType)));
        }

        public ISeries<MemberBuilder> Create(MemberRubric[] memberRubrics)
        {
            Registry<MemberBuilder> rubricBuilders = new Registry<MemberBuilder>();

            memberRubrics.ForEach(
                (member) =>
                {
                    if (!rubricBuilders.TryGet(member.RubricName, out MemberBuilder fieldProperty))
                        rubricBuilders.Put(new MemberBuilder(member));
                    else
                        fieldProperty.SetMember(member);
                }
            );
            rubricBuilders.ForEach(
                (fp, x) =>
                {
                    fp.Member.RubricId = x;
                    fp.Member.FieldId = x;
                }
            );

            return rubricBuilders;
        }

        public MemberRubric[] PrepareMembers(IEnumerable<MemberInfo> membersInfo)
        {
            return membersInfo
                .Select(
                    m =>
                        !(m is MemberRubric rubric)
                            ? m.MemberType == MemberTypes.Field
                                ? new MemberRubric((FieldInfo)m)
                                : m.MemberType == MemberTypes.Property
                                    ? new MemberRubric((PropertyInfo)m)
                                    : null
                            : rubric
                )
                .Where(p => p != null)
                .ToArray();
        }

        public MemberInfo[] GetMembers(Type modelType)
        {
            return modelType
                .GetMembers(BindingFlags.Instance | BindingFlags.Public)
                .Where(
                    m =>

                            m.MemberType == MemberTypes.Field
                            ||
                                m.MemberType == MemberTypes.Property
                                && ((PropertyInfo)m).CanRead && ((PropertyInfo)m).CanWrite
                                && ((PropertyInfo)m).PropertyType != typeof(IProxy)


                )
                .ToArray();
        }
    }
}
