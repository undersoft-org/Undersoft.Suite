namespace Undersoft.SDK.Workflows
{
    using Series;
    using System.Linq;
    using Undersoft.SDK.Invoking;
    using Undersoft.SDK.Utilities;

    public class Workout
    {
        public WorkAspect Aspect;
        public WorkCase Case;
        public WorkItem Work;
        public Workspace Workspace;

        public Workout(
            bool safeClose,
            string className,
            string methodName,
            out object result,
            params object[] input
        ) : this(1, safeClose, InstanceUtilities.New(className), methodName, input)
        {
            result = Work.GetOutput();
        }

        public Workout(IInvoker method) : this(1, false, method) { }

        public Workout(IInvoker method, bool safe, params object[] input)
            : this(1, safe, method, input) { }

        public Workout(IInvoker method, params object[] input) : this(1, false, method, input) { }

        public Workout(int workersCount, bool safeClose, ISeries<IInvoker> _methods)
        {
            Case = new WorkCase();
            Aspect = Case.Aspect("FirstWorkNow");
            foreach (var am in _methods)
                Aspect.AddWork(am);
            Aspect.Allocate(workersCount);

            Workspace = Aspect.Workspace;
            foreach (WorkItem am in Aspect)
                am.Start(am.Arguments);

            Aspect.Workspace.Close(safeClose);
        }

        public Workout(int workersCount, bool safeClose, IInvoker method, params object[] input)
        {
            Case = new WorkCase();
            Aspect = Case.Aspect("FirstWorkNow").AddWork(method).Aspect.Allocate(workersCount);

            Workspace = Aspect.Workspace;
            Work = Aspect.AsValues().FirstOrDefault();
            Case.Run(method.Name, input);
            Workspace.Close(safeClose);
        }

        public Workout(
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

            Workspace = Aspect.Workspace;
            Work = Aspect.AsValues().FirstOrDefault();
            Case.Run(am.Name, input);
            Workspace.Close(safeClose);
        }

        public Workout(
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

            Workspace = Aspect.Workspace;
            Work = Aspect.AsValues().FirstOrDefault();
            Work.FlowTo(Case.AsValues().Skip(1).FirstOrDefault().AsValues().FirstOrDefault());
            Case.Run(method.Name, method.Arguments);
            Workspace.Close(safeClose);
        }

        public Workout(
            object classObject,
            string methodName,
            out object result,
            params object[] input
        ) : this(1, false, classObject, methodName, input)
        {
            result = Work.GetOutput();
        }

        public Workout(string className, string methodName, params object[] input)
            : this(1, false, InstanceUtilities.New(className), methodName, input) { }

        public static Workout Run<TClass>()
        {
            return new Workout(new Invoker<TClass>());
        }

        public static Workout Run<TClass>(bool safeThread, params object[] input)
        {
            return new Workout(new Invoker<TClass>(), safeThread, input);
        }

        public static Workout Run<TClass>(object[] constructorParams, params object[] input)
        {
            return new Workout(new Invoker<TClass>(constructorParams), input);
        }

        public static Workout Run<TClass>(
            string methodName,
            object[] constructorParams,
            params object[] input
        )
        {
            return new Workout(new Invoker<TClass>(methodName, constructorParams), input);
        }

        public static Workout Run<TClass>(string methodName, params object[] input)
        {
            return new Workout(new Invoker<TClass>(methodName), input);
        }

        public static Workout Run<TClass>(
            string methodName,
            Type[] parameterTypes,
            object[] constructorParams,
            params object[] input
        )
        {
            return new Workout(
                new Invoker<TClass>(methodName, parameterTypes, constructorParams),
                input
            );
        }

        public static Workout Run<TClass>(
            string methodName,
            Type[] parameterTypes,
            params object[] input
        )
        {
            return new Workout(new Invoker<TClass>(methodName, parameterTypes), input);
        }

        public static Workout Run<TClass>(
            Type[] parameterTypes,
            object[] constructorParams,
            params object[] input
        )
        {
            return new Workout(new Invoker<TClass>(parameterTypes, constructorParams), input);
        }

        public static Workout Run<TClass>(Type[] parameterTypes, params object[] input)
        {
            return new Workout(new Invoker<TClass>(parameterTypes), input);
        }

        public void Close(bool safeClose = false)
        {
            Aspect.Workspace.Close(safeClose);
        }

        public void Run()
        {
            Workspace.Run(Work);
        }

        public void Run(params object[] input)
        {
            Work.SetInput(input);
            Workspace.Run(Work);
        }
    }
}
