namespace Undersoft.SDK.Workflows.Notes
{
    using System.Collections.Generic;
    using System.Linq;
    using Series;
    using Undersoft.SDK.Workflows;

    public class WorkNoteEvokers : Registry<WorkNoteEvoker>
    {
        public WorkNoteEvokers() : base(true) { }

        public bool Contains(IEnumerable<WorkItem> objectives)
        {
            return AsValues()
                .Any(t => t.RelatedWorks.Any(ro => objectives.All(o => ReferenceEquals(ro, o))));
        }

        public bool Contains(IEnumerable<string> relayNames)
        {
            return AsValues().Any(t => t.RelatedWorkNames.SequenceEqual(relayNames));
        }

        public WorkNoteEvoker this[string relatedWorkName]
        {
            get
            {
                return AsValues()
                    .FirstOrDefault(c => c.RelatedWorkNames.Contains(relatedWorkName));
            }
        }
        public WorkNoteEvoker this[WorkItem relatedWork]
        {
            get
            {
                return AsValues().FirstOrDefault(c => c.RelatedWorks.Contains(relatedWork));
            }
        }
    }
}
