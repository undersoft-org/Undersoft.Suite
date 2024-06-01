namespace Undersoft.SDK.Workflows.Notes
{
    using Undersoft.SDK.Workflows;
    using Uniques;

    public class WorkNote : Origin
    {
        public object[] Parameters;
        public WorkNoteBox SenderBox;

        public WorkNote(
            WorkItem sender,
            WorkItem recipient,
            WorkNoteEvoker Out,
            WorkNoteEvokers In,
            params object[] Params
        )
        {
            Parameters = Params;

            if (recipient != null)
            {
                Recipient = recipient;
                RecipientName = Recipient.Worker.Name;
            }

            Sender = sender;
            SenderName = Sender.Worker.Name;

            if (Out != null)
                EvokerOut = Out;

            if (In != null)
                EvokersIn = In;
        }

        public WorkNote(string sender, params object[] Params) : this(sender, null, null, null, Params)
        { }

        public WorkNote(
            string sender,
            string recipient,
            WorkNoteEvoker Out,
            WorkNoteEvokers In,
            params object[] Params
        )
        {
            SenderName = sender;
            Parameters = Params;

            if (recipient != null)
                RecipientName = recipient;

            if (Out != null)
                EvokerOut = Out;

            if (In != null)
                EvokersIn = In;
        }

        public WorkNote(string sender, string recipient, WorkNoteEvoker Out, params object[] Params)
            : this(sender, recipient, Out, null, Params) { }

        public WorkNote(string sender, string recipient, params object[] Params)
            : this(sender, recipient, null, null, Params) { }

        public WorkNoteEvoker EvokerOut { get; set; }

        public WorkNoteEvokers EvokersIn { get; set; }

        public WorkItem Recipient { get; set; }

        public string RecipientName { get; set; }

        public WorkItem Sender { get; set; }

        public string SenderName { get; set; }
    }
}
