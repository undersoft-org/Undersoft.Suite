namespace Undersoft.SDK.Workflows.Notes
{
    using System.Linq;
    using Series;
    using Undersoft.SDK.Workflows;
    using Uniques;

    public class WorkNoteEvoker : Registry<WorkItem>, IUnique
    {
        public ISeries<WorkItem> RelatedWorks = new Catalog<WorkItem>();
        public ISeries<string> RelatedWorkNames = new Catalog<string>();

        public WorkNoteEvoker(WorkItem sender, WorkItem recipient, params WorkItem[] relayWorks) : base(true)
        {
            Sender = sender;
            SenderName = sender.Worker.Name;
            Recipient = recipient;
            RecipientName = recipient.Worker.Name;
            Id = SenderName.UniqueKey(RecipientName.UniqueKey());
            TypeId = RecipientName.UniqueKey();
            RelatedWorks.Add(relayWorks);
            RelatedWorkNames.Add(RelatedWorks.Select(rn => rn.Worker.Name));
        }

        public WorkNoteEvoker(WorkItem sender, WorkItem recipient, params string[] relayNames) : base(true)
        {
            Sender = sender;
            SenderName = sender.Name;
            Recipient = recipient;
            RecipientName = recipient.Name;
            Id = SenderName.UniqueKey(RecipientName.UniqueKey());
            TypeId = RecipientName.UniqueKey();
            RelatedWorkNames.Add(relayNames);
            var namekeys = relayNames.ForEach(s => s.UniqueKey());
            RelatedWorks.Add(
                Sender.Case
                    .AsValues()
                    .Where(m => m.Any(k => namekeys.Contains(k.Id)))
                    .SelectMany(os => os.AsValues())
                    .ToList()
            );
        }

        public WorkNoteEvoker(WorkItem sender, string recipientName, params WorkItem[] relayWorks) : base(true)
        {
            Sender = sender;
            SenderName = sender.Name;
            RecipientName = recipientName;
            Id = SenderName.UniqueKey(RecipientName.UniqueKey());
            TypeId = RecipientName.UniqueKey();
            var rcpts = Sender.Case
                .AsValues()
                .Where(m => m.ContainsKey(recipientName))
                .SelectMany(os => os.AsValues())
                .ToArray();
            Recipient = rcpts.FirstOrDefault();
            RelatedWorks.Add(relayWorks);
            RelatedWorkNames.Add(RelatedWorks.Select(rn => rn.Worker.Name));
        }

        public WorkNoteEvoker(WorkItem sender, string recipientName, params string[] relayNames) : base(true)
        {
            Sender = sender;
            SenderName = sender.Worker.Name;
            var rcpts = Sender.Case
                .AsValues()
                .Where(m => m.ContainsKey(recipientName))
                .SelectMany(os => os.AsValues())
                .ToArray();
            Recipient = rcpts.FirstOrDefault();
            RecipientName = recipientName;
            Id = SenderName.UniqueKey(RecipientName.UniqueKey());
            TypeId = RecipientName.UniqueKey();
            RelatedWorkNames.Add(relayNames);
            var namekeys = relayNames.ForEach(s => s.UniqueKey());
            RelatedWorks.Add(
                Sender.Case
                    .AsValues()
                    .Where(m => m.Any(k => namekeys.Contains(k.Id)))
                    .SelectMany(os => os.AsValues())
                    .ToList()
            );
        }

        public IUnique Empty => new Usid();

        public string EvokerName { get; set; }

        public EvokerType EvokerType { get; set; }

        public WorkItem Recipient { get; set; }

        public string RecipientName { get; set; }

        public WorkItem Sender { get; set; }

        public string SenderName { get; set; }
    }
}
