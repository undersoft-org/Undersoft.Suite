namespace Undersoft.SDK.Workflows.Notes
{
    using Series;
    using System.Collections.Generic;
    using System.Linq;
    using Undersoft.SDK.Workflows;

    public class WorkNoteBox : Registry<WorkNoteTopic>
    {
        public WorkNoteBox(string Recipient) : base(true)
        {
            RecipientName = Recipient;
            Evokers = new WorkNoteEvokers();
        }

        public WorkNoteEvokers Evokers { get; set; }

        public WorkItem Work { get; set; }

        public string RecipientName { get; set; }

        public void Notify(params WorkNote[] notes)
        {
            if (notes != null && notes.Any())
            {
                foreach (WorkNote note in notes)
                {
                    WorkNoteTopic queue = null;
                    if (note.SenderName != null)
                    {
                        if (!ContainsKey(note.SenderName))
                        {
                            queue = new WorkNoteTopic(note.SenderName, this);
                            if (Add(note.SenderName, queue))
                            {
                                if (note.EvokerOut != null)
                                    Evokers.Add(note.EvokerOut);
                                queue.Notify(note);
                            }
                        }
                        else if (TryGet(note.SenderName, out queue))
                        {
                            if (notes != null && notes.Length > 0)
                            {
                                if (note.EvokerOut != null)
                                    Evokers.Add(note.EvokerOut);
                                queue.Notify(note);
                            }
                        }
                    }
                }
            }
        }

        public void Notify(WorkNote note)
        {
            if (note.SenderName != null)
            {
                WorkNoteTopic queue = null;
                if (!ContainsKey(note.SenderName))
                {
                    queue = new WorkNoteTopic(note.SenderName, this);
                    if (Add(note.SenderName, queue))
                    {
                        if (note.EvokerOut != null)
                            Evokers.Add(note.EvokerOut);
                        queue.Notify(note);
                    }
                }
                else if (TryGet(note.SenderName, out queue))
                {
                    if (note.EvokerOut != null)
                        Evokers.Add(note.EvokerOut);
                    queue.Notify(note);
                }
            }
        }

        public void Notify(string key, params WorkNote[] notes)
        {
            WorkNoteTopic queue = null;
            if (!ContainsKey(key))
            {
                queue = new WorkNoteTopic(key, this);
                if (Add(key, queue) && notes != null && notes.Length > 0)
                {
                    foreach (WorkNote note in notes)
                    {
                        if (note.EvokerOut != null)
                            Evokers.Add(note.EvokerOut);
                        note.SenderName = key;
                        queue.Notify(note);
                    }
                }
            }
            else if (TryGet(key, out queue))
            {
                if (notes != null && notes.Length > 0)
                {
                    foreach (WorkNote note in notes)
                    {
                        if (note.EvokerOut != null)
                            Evokers.Add(note.EvokerOut);
                        note.SenderName = key;
                        queue.Notify(note);
                    }
                }
            }
        }

        public void Notify(string key, WorkNote value)
        {
            value.SenderName = key;
            WorkNoteTopic queue = null;
            if (!ContainsKey(key))
            {
                queue = new WorkNoteTopic(key, this);
                if (Add(key, queue))
                {
                    if (value.EvokerOut != null)
                        Evokers.Add(value.EvokerOut);
                    queue.Notify(value);
                }
            }
            else if (TryGet(key, out queue))
            {
                if (value.EvokerOut != null)
                    Evokers.Add(value.EvokerOut);
                queue.Notify(value);
            }
        }

        public void Notify(string key, object ioqueues)
        {
            WorkNoteTopic queue = null;
            if (!ContainsKey(key))
            {
                queue = new WorkNoteTopic(key, this);
                if (Add(key, queue) && ioqueues != null)
                {
                    queue.Notify(ioqueues);
                }
            }
            else if (TryGet(key, out queue))
            {
                if (ioqueues != null)
                {
                    queue.Notify(ioqueues);
                }
            }
        }

        public WorkNote TakeNote(string key)
        {
            WorkNoteTopic _ioqueue = null;
            if (TryGet(key, out _ioqueue))
                return _ioqueue.Dequeue();
            return null;
        }

        public IList<WorkNote> TakeNotes(IList<string> keys)
        {
            return AsItems()
                .Where(q => keys.Contains(q.Value.SenderName))
                .Select(v => v.Value.Notes)
                .ToList();
        }

        public object[] GetParams(string key)
        {
            WorkNoteTopic _ioqueue = null;
            WorkNote temp = null;
            if (TryGet(key, out _ioqueue))
                if (_ioqueue.TryDequeue(out temp))
                    return temp.Parameters;
            return null;
        }

        public bool MeetsRequirements(IList<string> keys)
        {
            return AsItems()
                .Where(q => keys.Contains(q.Value.SenderName))
                .All(v => v.Value.Count > 0);
        }

        public void QualifyToEvoke()
        {
            List<WorkNoteEvoker> toEvoke = new List<WorkNoteEvoker>();
            foreach (WorkNoteEvoker relay in Evokers.AsValues())
            {
                if (relay.RelatedWorkNames.All(r => ContainsKey(r)))
                    if (relay.RelatedWorkNames.All(r => this[r].AsValues().Any()))
                    {
                        toEvoke.Add(relay);
                    }
            }

            if (toEvoke.Any())
            {
                foreach (WorkNoteEvoker evoke in toEvoke)
                {
                    if (MeetsRequirements(evoke.RelatedWorkNames))
                    {
                        IList<WorkNote> notes = TakeNotes(evoke.RelatedWorkNames);

                        if (notes.All(a => a != null))
                        {
                            var parameters = new List<object>();
                            foreach (WorkNote note in notes)
                            {
                                foreach (var parameter in note.Parameters)
                                {
                                    parameters.Add(parameter);
                                }
                            }

                            Work.Invoke(parameters.ToArray());
                        }
                    }
                }
            }
        }
    }
}
