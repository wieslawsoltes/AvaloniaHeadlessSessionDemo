using Avalonia.Headless;
using AvaloniaHeadlessSessionDemo;

namespace AvaloniaHeadlessSessionTests;

public abstract class AvaloniaHeadlessTestBase : IDisposable
{
    private readonly HeadlessUnitTestSession _session;

    public AvaloniaHeadlessTestBase()
    {
        _session = Task.Run(() => HeadlessUnitTestSession.StartNew(typeof(App))).Result;
    }

    public void Dispose()
    {
        _session.Dispose();
    }

    protected Task RunInHeadlessSession(Action action)
    {
        return _session.Dispatch(() =>
        {
            action();
            return true;
        }, CancellationToken.None);
    }

    protected Task<T> RunInHeadlessSession<T>(Func<T> func)
    {
        return _session.Dispatch(func, CancellationToken.None);
    }

    protected Task RunInHeadlessSession(Func<Task> action)
    {
        return _session.Dispatch(async () =>
        {
            await action();
            return true;
        }, CancellationToken.None);
    }

    protected Task<T> RunInHeadlessSession<T>(Func<Task<T>> func)
    {
        return _session.Dispatch(func, CancellationToken.None);
    }
}
