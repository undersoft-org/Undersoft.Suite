using Microsoft.OData.Client;
using Undersoft.SDK.Series;
using System;
using System.Threading;
using Undersoft.SDK.Service.Data.Repository;

namespace Undersoft.SDK.Service.Data.Remote.Repository;

public class RemoteSynchronizer : IRemoteSynchronizer
{
    const int WAIT_LINKER_TIMEOUT = 10000;
    const int WAIT_RESULT_TIMEOUT = 10000;
    readonly ManualResetEventSlim linkerAccess = new ManualResetEventSlim(true, 128);
    readonly ISeries<IRepository> repositories = new Registry<IRepository>();
    readonly ManualResetEventSlim resultAccess = new ManualResetEventSlim(true, 128);
    int linkers;

    public RemoteSynchronizer() { }

    public void AddRepository(IRepository repository)
    {
        repositories.Put(repository);
    }

    public void OnLinked(object sender, LoadCompletedEventArgs args)
    {
        ReleaseLinker();
    }

    public void AcquireLinker()
    {
        Interlocked.Increment(ref linkers);
        resultAccess.Reset();
        if (!linkerAccess.Wait(WAIT_LINKER_TIMEOUT))
            throw new TimeoutException("Wait read timeout");
    }

    public void ReleaseLinker()
    {
        if (0 == Interlocked.Decrement(ref linkers))
            resultAccess.Set();
    }

    public void AcquireResult()
    {
        if (!resultAccess.Wait(WAIT_RESULT_TIMEOUT))
            throw new TimeoutException("Wait write Timeout");
        linkerAccess.Reset();
    }

    public void ReleaseResult()
    {
        linkerAccess.Set();
    }
}
