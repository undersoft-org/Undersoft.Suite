namespace Undersoft.SDK.Workflows
{
    using Invoking;
    using Notes;
    using System.Collections.Generic;
    using System.Linq;
    using Uniques;

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
                if (aspect.Space == null)
                {
                    aspect.Space = new Workspace(aspect);
                }
                if (!aspect.Space.Ready)
                {
                    aspect.Allocate();
                }
            }
        }

        public void Run(string name, params object[] args)
        {
            WorkItem[] items = AsValues()
                .Where(m => m.ContainsKey(name))
                .SelectMany(w => w.AsValues())
                .ToArray();

            foreach (WorkItem item in items)
                item.Invoke(args);
        }

        public void Run(IDictionary<string, object[]> methodsWithArgs)
        {
            foreach (KeyValuePair<string, object[]> methodAndArgs in methodsWithArgs)
            {
                object input = methodAndArgs.Value;
                string workName = methodAndArgs.Key;
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
