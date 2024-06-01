namespace Undersoft.SDK.Workflows
{
    using System.Collections.Generic;
    using System.Linq;
    using Invoking;
    using Uniques;
    using Notes;

    public class WorkCase<TRule> : WorkCase where TRule : class
    {
        public WorkCase() : base(new WorkAspects(typeof(TRule).FullName)) { }

        public WorkAspect<TAspect> Aspect<TAspect>() where TAspect : class
        {
            if (!TryGet(typeof(TAspect).FullName, out WorkAspect aspect))
            {
                aspect = new WorkAspect<TAspect>();
                Add(aspect);
            }
            return aspect as WorkAspect<TAspect>;
        }
    }

    public class WorkCase : WorkAspects
    {
        public WorkCase(IEnumerable<IInvoker> methods, WorkAspects @case = null)
            : base(
                @case == null ? $"Case_{Unique.NewId}" : @case.Name,
                @case == null ? new WorkNotes() : @case.Notes
            )
        {
            Add($"Aspect_{Unique.NewId}", methods);
            Open();
        }

        public WorkCase(WorkAspects @case) : base(@case.Name, @case.Notes)
        {
            Add(@case.AsValues());
        }

        public WorkCase() : base($"Case_{Unique.NewId}", new WorkNotes()) { }

        public WorkAspect Aspect(IInvoker method, WorkAspect aspect)
        {
            if (aspect != null)
            {
                if (!TryGet(aspect.Name, out WorkAspect _aspect))
                {
                    Add(_aspect);
                    _aspect.AddWork(method);
                }
                return aspect;
            }
            return null;
        }

        public WorkAspect Aspect(IInvoker method, string name)
        {
            if (!TryGet(name, out WorkAspect aspect))
            {
                aspect = new WorkAspect(name);
                Add(aspect);
                aspect.AddWork(method);
            }
            return aspect;
        }

        public WorkAspect Aspect(string name)
        {
            if (!TryGet(name, out WorkAspect aspect))
            {
                aspect = new WorkAspect(name);
                Add(aspect);
            }
            return aspect;
        }

        public void Open()
        {
            Setup();
        }

        public void Setup()
        {
            foreach (WorkAspect aspect in AsValues())
            {
                if (aspect.Workspace == null)
                {
                    aspect.Workspace = new Workspace(aspect);
                }
                if (!aspect.Workspace.Ready)
                {
                    aspect.Allocate();
                }
            }
        }

        public void Run(string workName, params object[] input)
        {
            WorkItem[] works = AsValues()
                .Where(m => m.ContainsKey(workName))
                .SelectMany(w => w.AsValues())
                .ToArray();

            foreach (WorkItem work in works)
                work.Invoke(input);
        }

        public void Run(IDictionary<string, object[]> worksAndParams)
        {
            foreach (KeyValuePair<string, object[]> workAndParam in worksAndParams)
            {
                object input = workAndParam.Value;
                string workName = workAndParam.Key;
                WorkItem[] works = AsValues()
                    .Where(m => m.ContainsKey(workName))
                    .SelectMany(w => w.AsValues())
                    .ToArray();

                foreach (WorkItem work in works)
                    work.Execute(input);
            }
        }
    }

    public class InvalidWorkException : Exception
    {
        #region Constructors


        public InvalidWorkException(string message) : base(message) { }

        #endregion
    }
}
