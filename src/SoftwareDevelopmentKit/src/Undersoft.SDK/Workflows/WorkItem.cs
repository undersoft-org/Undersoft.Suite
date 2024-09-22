namespace Undersoft.SDK.Workflows
{
    using Notes;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Undersoft.SDK;
    using Undersoft.SDK.Invoking;
    using Uniques;

    public class WorkItem : Origin, IInvoker, IWorker
    {
        public IUnique Empty => Uscn.Empty;

        public WorkItem(IInvoker operation)
        {
            Name = operation.Name;
            Worker = new Worker(operation.Name, operation);
            Worker.Work = this;
            Box = new WorkNoteBox(Worker.Name);
            Box.Work = this;

            Id = Name.UniqueKey();
            TypeId = Unique.NewId;
        }

        public WorkItem(Worker worker)
        {
            Name = worker.Name;
            Worker = worker;
            Worker.Work = this;
            Box = new WorkNoteBox(Worker.Name);
            Box.Work = this;

            Id = Name.UniqueKey();
            TypeId = Unique.NewId;
        }

        public string Name { get; set; }

        public string QualifiedName { get; set; }

        public string MethodName => Info.Name;

        public override string TypeName
        {
            get => Type.FullName;
        }

        public Type ReturnType => Info.ReturnType;

        public Type Type => TargetObject.GetType();

        public AssemblyName AssemblyName => Type.Assembly.GetName();

        public Worker Worker { get; set; }

        public WorkAspect Aspect { get; set; }

        public WorkAspects Case { get; set; }

        public WorkNoteBox Box { get; set; }

        public Arguments Arguments
        {
            get => Worker.Process.Arguments;
            set => Worker.Process.Arguments = value;
        }

        public MethodInfo Info
        {
            get => Worker.Process.Info;
            set => Worker.Process.Info = value;
        }

        public ParameterInfo[] Parameters
        {
            get => Worker.Process.Parameters;
            set => Worker.Process.Parameters = value;
        }
        public object[] ValueArray
        {
            get => Arguments.ValueArray;
            set => Worker.Process.ValueArray = value;
        }

        public InvokerDelegate MethodInvoker => Process.MethodInvoker;

        public Delegate Method => Process.Method;

        public WorkAspect Post(params object[] input)
        {
            Worker.SetInput(input);
            Aspect.Run(this);
            return Aspect;
        }

        public object Invoke(params object[] parameters)
        {
            Post(parameters);
            return null;
        }

        public Task<object> InvokeAsync(params object[] parameters)
        {
            return Task.Run(() =>
            {
                return Invoke(parameters);
            });
        }

        public Task<T> InvokeAsync<T>(params object[] parameters) where T : class
        {
            return Task.Run(() =>
            {
                Invoke(parameters);
                return default(T);
            });
        }

        public WorkNoteEvokers Evokers
        {
            get => Worker.Evokers;
            set => Worker.Evokers = value;
        }

        public string WorkerName
        {
            get => Worker.Name;
            set => Worker.Name = value;
        }

        public IInvoker Process
        {
            get => Worker.Process;
            set => Worker.Process = value;
        }

        public object TargetObject
        {
            get => Process.TargetObject;
            set => Process.TargetObject = value;
        }

        public WorkAspect FlowTo<T>(string methodName = null)
        {
            if (methodName == null)
                methodName = typeof(T).GetMethods().FirstOrDefault(m => m.IsPublic).Name;

            FlowTo(typeof(T).FullName, methodName);
            return Aspect;
        }

        public WorkAspect FlowTo<T>(Func<T, Delegate> methodName) where T : class, new()
        {
            string name = null;
            if (methodName == null)
                name = typeof(T).GetMethods().FirstOrDefault(m => m.IsPublic).Name;
            else
            {
                name = methodName(new T()).Method.Name;
            }

            FlowTo(typeof(T).FullName, name);
            return Aspect;
        }

        public WorkAspect FlowTo(WorkItem Recipient)
        {
            long recipientKey = Recipient.Name.UniqueKey();

            var relationWorks = Aspect.Where(l => l.Worker.Evokers.ContainsKey(l.Name.UniqueKey(recipientKey)))
                .ToArray();

            var evokers = relationWorks.Select(l => l.Evokers.Get(l.Name.UniqueKey(recipientKey))).ToArray();

            foreach (var noteEvoker in evokers)
            {
                noteEvoker.RelatedWorks.Put(this);
                noteEvoker.RelatedWorkNames.Put(Name);
            }

            Worker.FlowTo(Recipient, relationWorks.Concat(new[] { this }).ToArray());

            return Aspect;
        }

        public WorkAspect FlowTo(WorkItem Recipient, params WorkItem[] RelationWorks)
        {
            Worker.FlowTo(Recipient, RelationWorks.Concat(new[] { this }).ToArray());
            return Aspect;
        }

        public WorkAspect FlowTo(string recipientClass, string methodName)
        {
            var recipientName = recipientClass + "." + methodName;
            var aspect = Case.Where(m => m.ContainsKey(recipientName)).FirstOrDefault();
            var recipient = aspect.Get(recipientName);

            long recipientKey = recipient.Name.UniqueKey();

            var relationWorks = Aspect.Where(l => l.Worker.Evokers.ContainsKey(l.Name.UniqueKey(recipientKey)))
                .ToArray();

            var evokers = relationWorks.Select(l => l.Evokers.Get(l.Name.UniqueKey(recipientKey)))
                .ToArray();
            foreach (var noteEvoker in evokers)
            {
                noteEvoker.RelatedWorks.Put(this);
                noteEvoker.RelatedWorkNames.Put(Name);
            }

            Worker.FlowTo(recipient, relationWorks.Concat(new[] { this }).ToArray());
            return Aspect;
        }

        public WorkAspect FlowTo(string RecipientName, params string[] RelationNames)
        {
            Worker.FlowTo(RecipientName, RelationNames.Concat(new[] { Name }).ToArray());
            return Aspect;
        }

        public WorkAspect FlowFrom<T>(string methodName = null)
        {
            if (methodName == null)
                methodName = typeof(T).GetMethods().FirstOrDefault(m => m.IsPublic).Name;

            FlowFrom(typeof(T).FullName, methodName);
            return Aspect;
        }

        public WorkAspect FlowFrom<T>(Func<T, Delegate> methodName) where T : class, new()
        {
            string name = null;
            if (methodName == null)
                name = typeof(T).GetMethods().FirstOrDefault(m => m.IsPublic).Name;
            else
            {
                name = methodName(new T()).Method.Name;
            }

            FlowFrom(typeof(T).FullName, name);
            return Aspect;
        }

        public WorkAspect FlowFrom(WorkItem Sender)
        {
            Sender.FlowTo(this);
            return Aspect;
        }

        public WorkAspect FlowFrom(WorkItem Sender, params WorkItem[] RelationWorks)
        {
            Sender.FlowTo(this, RelationWorks);
            return Aspect;
        }

        public WorkAspect FlowFrom(string senderCless, string methodName)
        {
            var senderName = senderCless + "." + methodName;
            var aspect = Case.Where(m => m.ContainsKey(senderName)).FirstOrDefault();
            var sender = aspect.Get(senderName);

            sender.FlowTo(this);
            return Aspect;
        }

        public WorkAspect FlowFrom(string SenderName, params string[] RelationNames)
        {
            var aspect = Case.AsValues().Where(m => m.ContainsKey(SenderName)).FirstOrDefault();
            var sender = aspect.Get(SenderName);

            sender.FlowTo(Name, RelationNames);
            return Aspect;
        }

        public virtual WorkAspect AddWork<T>() where T : class
        {
            return Aspect.AddWork<T>();
        }

        public virtual WorkAspect AddWork<T>(Type[] arguments) where T : class
        {
            return Aspect.AddWork<T>(arguments);
        }

        public virtual WorkAspect AddWork<T>(params object[] consrtuctorParams) where T : class
        {
            return Aspect.AddWork<T>(consrtuctorParams);
        }

        public virtual WorkAspect AddWork<T>(Type[] arguments, params object[] consrtuctorParams)
            where T : class
        {
            return Aspect.AddWork<T>(arguments, consrtuctorParams);
        }

        public virtual WorkAspect AddWork<T>(Func<T, string> method) where T : class
        {
            return Aspect.AddWork<T>(method);
        }

        public virtual WorkAspect AddWork<T>(Func<T, string> method, params Type[] arguments)
            where T : class
        {
            return Aspect.AddWork<T>(method, arguments);
        }

        public virtual WorkAspect AddWork<T>(
            Func<T, string> method,
            params object[] constructorParams
        ) where T : class
        {
            return Aspect.AddWork<T>(method, constructorParams);
        }

        public virtual WorkItem Work<T>() where T : class
        {
            return Aspect.Work<T>();
        }

        public virtual WorkItem Work<T>(Type[] arguments) where T : class
        {
            return Aspect.Work<T>(arguments);
        }

        public virtual WorkItem Work<T>(params object[] consrtuctorParams) where T : class
        {
            return Aspect.Work<T>(consrtuctorParams);
        }

        public virtual WorkItem Work<T>(Type[] arguments, params object[] consrtuctorParams)
            where T : class
        {
            return Aspect.Work<T>(arguments, consrtuctorParams);
        }

        public virtual WorkItem Work<T>(Func<T, string> method) where T : class
        {
            return Aspect.Work<T>(method);
        }

        public virtual WorkItem Work<T>(Func<T, string> method, params Type[] arguments)
            where T : class
        {
            return Aspect.Work<T>(method, arguments);
        }

        public virtual WorkItem Work<T>(Func<T, string> method, params object[] constructorParams)
            where T : class
        {
            return Aspect.Work<T>(method, constructorParams);
        }

        public WorkAspect Allocate(int laborsCount = 1)
        {
            return Aspect.Allocate(laborsCount);
        }

        public object GetInput()
        {
            return ((IWorker)Worker).GetInput();
        }

        public void SetInput(object value)
        {
            ((IWorker)Worker).SetInput(value);
        }

        public object GetOutput()
        {
            return ((IWorker)Worker).GetOutput();
        }

        public void SetOutput(object value)
        {
            ((IWorker)Worker).SetOutput(value);
        }

        public Task Fire(params object[] parameters)
        {
            return Process.Fire(parameters);
        }

        public object Execute(object target, params object[] parameters)
        {
            return Process.Invoke(target, parameters);
        }

        public Task<object> ExecuteAsync(object target, params object[] parameters)
        {
            return Process.InvokeAsync(target, parameters);
        }

        public Task<T> ExecuteAsync<T>(object target, params object[] parameters) where T : class
        {
            return Process.InvokeAsync<T>(target, parameters);
        }

        public Task Fire(bool firstAsTarget, object target, params object[] parameters)
        {
            return Process.Fire(firstAsTarget, target, parameters);
        }

        public object Invoke(bool firstAsTarget, object target, params object[] parameters)
        {
            return Process.Invoke(firstAsTarget, target, parameters);
        }

        public Task<object> InvokeAsync(
            bool firstAsTarget,
            object target,
            params object[] parameters
        )
        {
            return Process.InvokeAsync(firstAsTarget, target, parameters);
        }

        public Task<T> InvokeAsync<T>(bool firstAsTarget, object target, params object[] parameters) where T : class
        {
            return Process.InvokeAsync<T>(firstAsTarget, target, parameters);
        }

        public virtual async Task<object> InvokeAsync(Arguments arguments)
        {
            return await Process.InvokeAsync(arguments);
        }

        public virtual async Task<object> InvokeAsync(
            bool withTarget,
            object target,
            Arguments arguments
        )
        {
            return await Process.InvokeAsync(withTarget, target, arguments);
        }
    }
}
