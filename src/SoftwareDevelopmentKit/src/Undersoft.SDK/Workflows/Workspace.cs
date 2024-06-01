namespace Undersoft.SDK.Workflows
{
    using Notes;
    using Series;
    using System.Collections;
    using System.Threading;

    public class Workspace : IWorkspace
    {
        private static readonly int WAIT_WRITE_TIMEOUT = 5000;

        private ManualResetEventSlim postAccess = new ManualResetEventSlim(true, 128);
        private SemaphoreSlim postPass = new SemaphoreSlim(1);
        private object inlock = new object();
        private object outlock = new object();

        private void acquirePostAccess()
        {
            do
            {
                if (!postAccess.Wait(WAIT_WRITE_TIMEOUT))
                    continue;
                postAccess.Reset();
            } while (!postPass.Wait(0));
        }

        private void releasePostAccess()
        {
            postPass.Release();
            postAccess.Set();
        }

        public WorkNotes Notes;
        public bool Ready;
        public WorkAspects Case;
        public WorkAspect Aspect;
        private Thread[] workers;
        private int WorkersCount => Aspect.WorkersCount;
        private Registry<Worker> Works = new Registry<Worker>();

        public Workspace(WorkAspect aspect)
        {
            Aspect = aspect;
            Case = Aspect.Case;
            Notes = Case.Notes;
            Ready = false;
        }

        public void Close(bool SafeClose)
        {
            foreach (Thread worker in workers)
            {
                Run(null);

                if (SafeClose && worker.ThreadState == ThreadState.Running)
                    worker.Join();
            }
            Ready = false;
        }

        public WorkAspect Allocate(int workersCount = 0)
        {
            if (workersCount > 0)
                Aspect.WorkersCount = workersCount;

            workers = new Thread[WorkersCount];
            for (int i = 0; i < WorkersCount; i++)
            {
                workers[i] = new Thread(Activate);
                workers[i].IsBackground = true;
                workers[i].Priority = ThreadPriority.AboveNormal;
                workers[i].Start();
            }

            Ready = true;
            return Aspect;
        }

        public void Run(WorkItem work)
        {
            lock (inlock)
            {
                if (work != null)
                {
                    Works.Enqueue(work.Worker.Clone());
                    Monitor.Pulse(inlock);
                }
                else
                {
                    Works.Enqueue(DateTime.Now.Ticks, null);
                    Monitor.Pulse(inlock);
                }
            }
        }

        public void Reset(int workersCount = 0)
        {
            Close(true);
            Allocate(workersCount);
        }

        public void Activate()
        {

            for (; ; )
            {
                Worker worker;
                object input;

                lock (inlock)
                {
                    while (!Works.TryDequeue(out worker))
                    {
                        Monitor.Wait(inlock);
                    }
                }

                lock (outlock)
                {
                    if (worker != null)
                    {
                        input = worker.GetInput();
                    }
                    else
                        return;
                }

                object output;

                if (input != null)
                {
                    if (input is IList)
                        output = worker.Process.Invoke((object[])input);
                    else
                        output = worker.Process.Invoke(input);
                }
                else
                {
                    output = worker.Process.Invoke();
                }

                while (!worker.CanSetOutput())
                {
                    Thread.Sleep(10);
                }
                lock (outlock)
                {
                    Outpost(worker, output);
                }
            }
        }

        private Worker Clone(Worker worker)
        {
            Worker _worker = new Worker(worker.Name, worker.Process);
            _worker.Input = worker.Input;
            _worker.Evokers = worker.Evokers;
            _worker.Work = worker.Work;
            return _worker;
        }

        private void Outpost(Worker worker, object output)
        {
            if (output != null)
            {
                worker.SetOutput(output);

                if (worker.Evokers != null && worker.Evokers.Count > 0)
                {
                    int l = worker.Evokers.Count;
                    if (l > 0)
                    {
                        var notes = new WorkNote[l];
                        for (int i = 0; i < worker.Evokers.Count; i++)
                        {
                            WorkNote note = new WorkNote(
                                worker.Work,
                                worker.Evokers[i].Recipient,
                                worker.Evokers[i],
                                null,
                                output
                            );
                            note.SenderBox = worker.Work.Box;
                            notes[i] = note;
                        }

                        Notes.Send(notes);
                    }
                }
            }
        }
    }
}
