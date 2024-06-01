namespace Undersoft.SSC.Domain.Entities
{
    public class Default : DataObject, IEntity
    {
        public virtual EntitySet<Activity>? Activities { get; set; }

        public virtual EntitySet<Member>? Members { get; set; }

        public virtual EntitySet<Resource>? Resources { get; set; }

        public virtual EntitySet<Schedule>? Schedules { get; set; }

        public virtual EntitySet<Application>? Applications { get; set; }

        public virtual EntitySet<Service>? Services { get; set; }

        public virtual EntitySet<Detail>? Details { get; set; }

        public virtual EntitySet<Setting>? Settings { get; set; }
    }
}