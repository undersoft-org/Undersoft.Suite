namespace Undersoft.SDK.Logging
{
    using System.Threading;

    public class LogMessage
    {
        private static long autoId = DateTime.Now.Ticks;

        public LogMessage()
        {
            Id = Interlocked.Increment(ref autoId);
        }

        public long Id { get; set; }

        public int Level { get; set; }

        public string Message { get; set; }

        public int Milliseconds { get; set; }

        public DateTime Time { get; set; }

        public string Type { get; set; }
    }
}
