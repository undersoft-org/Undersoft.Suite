using Undersoft.SDK.Invoking;

namespace Undersoft.SDK.Workflows
{
    using Notes;
    using Series;
    using System.Collections.Generic;

    public class WorkAspects : Registry<WorkAspect>
    {
        public WorkAspects(string name = null, WorkNotes notes = null) : base(true)
        {
            Name = name != null ? name : "ThreadGraph";
            Notes = Notes != null ? notes : new WorkNotes();
            Methods = new WorkMethods();
        }

        public string Name { get; set; }

        public WorkMethods Methods { get; set; }

        public WorkNotes Notes { get; set; }

        public WorkAspect Get(string key)
        {
            WorkAspect result = null;
            TryGet(key, out result);
            return result;
        }

        public override void Add(WorkAspect aspect)
        {
            aspect.Case = this;
            aspect.Workspace = new Workspace(aspect);
            Put(aspect.Name, aspect);
        }

        public override void Add(IEnumerable<WorkAspect> aspects)
        {
            foreach (var aspect in aspects)
            {
                aspect.Case = this;
                aspect.Workspace = new Workspace(aspect);
                Put(aspect.Name, aspect);
            }
        }

        public override bool Add(object key, WorkAspect value)
        {
            value.Case = this;
            value.Workspace = new Workspace(value);
            Put(key, value);
            return true;
        }

        public void Add(object key, IEnumerable<WorkItem> value)
        {
            WorkAspect msn = new WorkAspect(key.ToString(), value);
            msn.Case = this;
            msn.Workspace = new Workspace(msn);
            Put(key, msn);
        }

        public WorkAspect Add(object key, IEnumerable<IInvoker> value)
        {
            WorkAspect msn = new WorkAspect(key.ToString(), value);
            msn.Case = this;
            msn.Workspace = new Workspace(msn);
            Put(key, msn);
            return msn;
        }

        public void Add(object key, IInvoker value)
        {
            List<IInvoker> cml = new List<IInvoker>() { value };
            WorkAspect msn = new WorkAspect(key.ToString(), cml);
            msn.Case = this;
            msn.Workspace = new Workspace(msn);
            Put(key, msn);
        }

        public override WorkAspect this[object key]
        {
            get
            {
                TryGet(key, out WorkAspect result);
                return result;
            }
            set
            {
                value.Case = this;
                value.Workspace = new Workspace(value);
                Put(key, value);
            }
        }
    }
}
