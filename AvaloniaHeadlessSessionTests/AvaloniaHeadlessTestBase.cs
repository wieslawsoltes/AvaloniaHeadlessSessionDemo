using Avalonia.Headless;
using AvaloniaHeadlessSessionDemo;

namespace AvaloniaHeadlessSessionTests;

public abstract class AvaloniaHeadlessTestBase
{
    // Sync method
    protected void RunInHeadlessSession(Action action)
    {
        using var session = HeadlessUnitTestSession.StartNew(typeof(App));
        session.Dispatch(() =>
        {
            action();
            return true;
        }, CancellationToken.None).GetAwaiter().GetResult();
    }

    // Sync method with return value
    protected T RunInHeadlessSession<T>(Func<T> func)
    {
        using var session = HeadlessUnitTestSession.StartNew(typeof(App));
        return session.Dispatch(func, CancellationToken.None).GetAwaiter().GetResult();
    }

    // Async method
    protected async Task RunInHeadlessSession(Func<Task> action)
    {
        using var session = HeadlessUnitTestSession.StartNew(typeof(App));
        await session.Dispatch(async () =>
        {
            await action();
            return true;
        }, CancellationToken.None);
    }

    // Async method with return value
    protected async Task<T> RunInHeadlessSession<T>(Func<Task<T>> func)
    {
        using var session = HeadlessUnitTestSession.StartNew(typeof(App));
        return await session.Dispatch(func, CancellationToken.None);
    }
}
