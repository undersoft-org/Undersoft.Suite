namespace Undersoft.SDK.Workflows
{
    public class WorkAspect<TAspect> : WorkAspect where TAspect : class
    {
        public WorkAspect() : base(typeof(TAspect).FullName) { }

        public override WorkItem Work<TEvent>(string methodname = null) where TEvent : class
        {
            return base.Work<TEvent>(methodname);
        }

        public override WorkItem Work<TEvent>(Type[] arguments) where TEvent : class
        {
            return base.Work<TEvent>(arguments);
        }

        public override WorkItem Work<TEvent>(params object[] consrtuctorParams)
            where TEvent : class
        {
            return base.Work<TEvent>(consrtuctorParams);
        }

        public override WorkItem Work<TEvent>(Type[] arguments, params object[] consrtuctorParams)
            where TEvent : class
        {
            return base.Work<TEvent>(arguments, consrtuctorParams);
        }
    }
}
