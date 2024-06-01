namespace Undersoft.SDK.Series
{
    using Invoking;
    using Undersoft.SDK.Updating;

    public interface ITracedSeries
    {
        IUpdater Updater { get; }

        IInvoker NoticeChange { get; }

        IInvoker NoticeChanging { get; }
    }
}
