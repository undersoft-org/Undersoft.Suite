namespace Undersoft.SDK.Workflows
{
    using Series;
    using System.Linq;
    using Undersoft.SDK.Invoking;
    using Undersoft.SDK.Utilities;

    public class Work<T> : Work
    {
        public Work(params object[] input) : base(new Invoker<T>(), input) { }
        public Work(bool safeClose, params object[] input) : base(new Invoker<T>(), safeClose, input) { }

        public Work(Func<T, Delegate> method, params object[] input) : base(new Invoker<T>(method), input) { }
        public Work(Func<T, Delegate> method, bool safeClose, params object[] input) : base(new Invoker<T>(method), safeClose, input) { }
    }

    public class Work
    {
        public WorkAspect Aspect;
        public WorkCase Case;
        public WorkItem Item;
        public Workspace Space;

        public Work(
            bool safeClose,
            string className,
            string methodName,
            out object result,
            params object[] input
        ) : this(1, safeClose, InstanceUtilities.New(className), methodName, input)
        {
            result = Item.GetOutput();
        }

        public Work(IInvoker method) : this(1, false, method) { }

        public Work(IInvoker method, bool safeClose, params object[] input)
            : this(1, safeClose, method, input) { }

        public Work(IInvoker method, params object[] input) : this(1, false, method, input) { }

        public Work(int workersCount, bool safeClose, ISeries<IInvoker> _methods)
        {
            Case = new WorkCase();
            Aspect = Case.Aspect("FirstWorkNow");
            foreach (var am in _methods)
                Aspect.AddWork(am);
            Aspect.Allocate(workersCount);

            Space = Aspect.Space;
            foreach (WorkItem am in Aspect)
                am.Post(am.Arguments);

            Aspect.Space.Close(safeClose);
        }

        public Work(int workersCount, bool safeClose, IInvoker method, params object[] input)
        {
            Case = new WorkCase();
            Aspect = Case.Aspect("FirstWorkNow").AddWork(method).Aspect.Allocate(workersCount);

            Space = Aspect.Space;
            Case.Run(method.Name, input);
            Space.Close(safeClose);
        }

        public Work(
            int workersCount,
            bool safeClose,
            object classObject,
            string methodName,
            params object[] input
        )
        {
            IInvoker am = new Invoker(classObject, methodName);
            Case = new WorkCase();
            Aspect = Case.Aspect("FirstWorkNow").AddWork(am).Aspect.Allocate(workersCount);

            Space = Aspect.Space;
            Item = Aspect.AsValues().FirstOrDefault();
            Case.Run(am.Name, input);
            Space.Close(safeClose);
        }

        public Work(
            int workersCount,
            int evokerCount,
            bool safeClose,
            IInvoker method,
            IInvoker evoker
        )
        {
            Case = new WorkCase();
            Aspect = Case.Aspect("FirstWorkNow").AddWork(method).Aspect.Allocate(workersCount);
            Case.Aspect("SecondWorkNow").AddWork(evoker).Aspect.Allocate(evokerCount);

            Space = Aspect.Space;
            Item = Aspect.AsValues().FirstOrDefault();
            Item.FlowTo(Case.AsValues().Skip(1).FirstOrDefault().AsValues().FirstOrDefault());
            Case.Run(method.Name, method.Arguments);
            Space.Close(safeClose);
        }

        public Work(
            object classObject,
            string methodName,
            out object result,
            params object[] input
        ) : this(1, false, classObject, methodName, input)
        {
            result = Item.GetOutput();
        }

        public Work(string className, string methodName, params object[] input)
            : this(1, false, InstanceUtilities.New(className), methodName, input) { }

        public static Work Run<TClass>()
        {
            return new Work(new Invoker<TClass>());
        }

        public static Work Run<TClass>(bool safeThread, params object[] input)
        {
            return new Work(new Invoker<TClass>(), safeThread, input);
        }

        public static Work Run<TClass>(object[] constructorParams, params object[] input)
        {
            return new Work(new Invoker<TClass>(constructorParams), input);
        }

        public static Work Run<TClass>(
            string methodName,
            object[] constructorParams,
            params object[] input
        )
        {
            return new Work(new Invoker<TClass>(methodName, constructorParams), input);
        }

        public static Work Run<TClass>(string methodName, params object[] input)
        {
            return new Work(new Invoker<TClass>(methodName), input);
        }

        public static Work Run<TClass>(
            string methodName,
            Type[] parameterTypes,
            object[] constructorParams,
            params object[] input
        )
        {
            return new Work(
                new Invoker<TClass>(methodName, parameterTypes, constructorParams),
                input
            );
        }

        public static Work Run<TClass>(
            string methodName,
            Type[] parameterTypes,
            params object[] input
        )
        {
            return new Work(new Invoker<TClass>(methodName, parameterTypes), input);
        }

        public static Work Run<TClass>(
            Type[] parameterTypes,
            object[] constructorParams,
            params object[] input
        )
        {
            return new Work(new Invoker<TClass>(parameterTypes, constructorParams), input);
        }

        public static Work Run<TClass>(Type[] parameterTypes, params object[] input)
        {
            return new Work(new Invoker<TClass>(parameterTypes), input);
        }

        public void Close(bool safeClose = false)
        {
            Aspect.Space.Close(safeClose);
        }

        public void Run()
        {
            Space.Run(Item);
        }

        public void Run(params object[] input)
        {
            Item.SetInput(input);
            Space.Run(Item);
        }
    }
}
