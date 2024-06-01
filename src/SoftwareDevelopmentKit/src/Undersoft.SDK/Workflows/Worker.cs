namespace Undersoft.SDK.Workflows
{
    using Series;
    using Uniques;
    using Notes;
    using Undersoft.SDK;
    using Undersoft.SDK.Invoking;

    public class Worker : Origin, IWorker
    {
        public Registry<object> Input { get; set; } = new Registry<object>(true);
        public Registry<object> Output { get; set; } = new Registry<object>(true);

        public Worker RootWorker { get; set; }

        private Worker() { }

        public Worker(string Name, IInvoker Method) : this()
        {
            Process = Method;
            this.Name = Name;
            TypeId = Process.Id;
            Id = Unique.NewId.UniqueKey(TypeId);
            RootWorker = this;
        }

        public IUnique Empty => new Uscn();

        public WorkNoteEvokers Evokers { get; set; } = new WorkNoteEvokers();

        public object GetInput()
        {
            Input.TryDequeue(out object entry);
            InputId = RootWorker.InputId++;
            return entry;
        }

        public void SetInput(object value)
        {
            Input.Enqueue(Unique.NewId, value);
        }

        public object GetOutput()
        {
            Output.TryDequeue(out object entry);
            return entry;
        }

        public void SetOutput(object value)
        {
            Output.Enqueue(Unique.NewId, value);
            OutputId = RootWorker.OutputId++;
        }

        public bool CanSetOutput()
        {
            return InputId == RootWorker.OutputId;
        }

        public Worker Clone()
        {
            var _worker = new Worker(Name, Process);
            RootWorker = this;
            _worker.Input = Input;
            _worker.Output = Output;
            _worker.Evokers = Evokers;
            _worker.Work = Work;
            return _worker;
        }

        public WorkItem Work { get; set; }

        public string Name { get; set; }

        public IInvoker Process { get; set; }

        public int OutputId { get; set; }

        public int InputId { get; set; }

        public WorkAspect FlowTo<T>(string methodName = null)
        {
            return Work.FlowTo<T>(methodName);
        }

        public WorkAspect FlowTo<T>(Func<T, Delegate> methodName) where T : class, new()
        {
            return Work.FlowTo(methodName);
        }

        public WorkAspect FlowTo(WorkItem recipient)
        {
            Evokers.Add(new WorkNoteEvoker(Work, recipient, Work));
            return Work.Aspect;
        }

        public WorkAspect FlowTo(WorkItem Recipient, params WorkItem[] RelationWorks)
        {
            Evokers.Add(new WorkNoteEvoker(Work, Recipient, RelationWorks));
            return Work.Aspect;
        }

        public WorkAspect FlowTo(string RecipientName, string methodName)
        {
            Evokers.Add(new WorkNoteEvoker(Work, RecipientName, Name));
            return Work.Aspect;
        }

        public WorkAspect FlowTo(string RecipientName, params string[] RelationNames)
        {
            Evokers.Add(new WorkNoteEvoker(Work, RecipientName, RelationNames));
            return Work.Aspect;
        }

        public WorkAspect FlowFrom<T>(string methodName = null)
        {
            return Work.FlowFrom<T>(methodName);
        }

        public WorkAspect FlowFrom<T>(Func<T, Delegate> methodName) where T : class, new()
        {
            return Work.FlowFrom(methodName);
        }

        public WorkAspect FlowFrom(WorkItem sender)
        {
            Work.FlowFrom(sender);
            return Work.Aspect;
        }

        public WorkAspect FlowFrom(WorkItem Sender, params WorkItem[] RelationWorks)
        {
            Work.FlowFrom(Sender, RelationWorks);
            return Work.Aspect;
        }

        public WorkAspect FlowFrom(string SenderName, string methodName)
        {
            Work.FlowFrom(SenderName, methodName);
            return Work.Aspect;
        }

        public WorkAspect FlowFrom(string SenderName, params string[] RelationNames)
        {
            Work.FlowFrom(SenderName, RelationNames);
            return Work.Aspect;
        }
    }
}
