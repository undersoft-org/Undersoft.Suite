namespace Undersoft.SDK.Workflows.Notes
{
    using System.Collections.Generic;
    using Series;

    public class WorkNoteTopic : Registry<WorkNote>
    {
        public WorkNoteBox RecipientBox;

        public WorkNoteTopic(string senderName, IList<WorkNote> notelist, WorkNoteBox recipient = null) : base(true)
        {
            if (recipient != null)
            {
                RecipientBox = recipient;
            }
            if (notelist != null && notelist.Count > 0)
            {
                foreach (WorkNote evocation in notelist)
                {
                    evocation.SenderName = SenderName;
                    Notes = evocation;
                }
            }
        }

        public WorkNoteTopic(string senderName, WorkNote note, WorkNoteBox recipient = null) : base(true)
        {
            if (recipient != null)
            {
                RecipientBox = recipient;
            }
            SenderName = senderName;
            Notes = note;
        }

        public WorkNoteTopic(string senderName, WorkNoteBox recipient = null) : base(true)
        {
            if (recipient != null)
                RecipientBox = recipient;
            SenderName = senderName;
        }

        public WorkNoteTopic(string senderName, WorkNoteBox recipient = null, params object[] parameters) : base(true)
        {
            if (recipient != null)
                RecipientBox = recipient;
            SenderName = senderName;
            if (parameters != null)
            {
                if (parameters[0].GetType() == typeof(Dictionary<string, object>))
                {
                    WorkNote result = new WorkNote(senderName, parameters);
                    Notes = result;
                }
            }
        }

        public WorkNote Notes
        {
            get
            {
                WorkNote _result = null;
                TryDequeue(out _result);
                return _result;
            }
            set
            {
                value.SenderName = SenderName;
                Enqueue(DateTime.Now.ToBinary(), value);
                if (RecipientBox != null)
                    RecipientBox.QualifyToEvoke();
            }
        }

        public string SenderName { get; set; }

        public void Notify(IList<WorkNote> noteList)
        {
            foreach (WorkNote result in noteList)
                Notes = result;
        }

        public void Notify(WorkNote note)
        {
            Notes = note;
        }

        public void Notify(params object[] parameters)
        {
            if (parameters != null)
            {
                WorkNote result = new WorkNote(SenderName);
                result.Parameters = parameters;
                Notes = result;
            }
        }

        public void Notify(string senderName, params object[] parameters)
        {
            SenderName = senderName;
            if (parameters != null)
            {
                WorkNote result = new WorkNote(senderName);
                result.Parameters = parameters;
                Notes = result;
            }
        }
    }
}
