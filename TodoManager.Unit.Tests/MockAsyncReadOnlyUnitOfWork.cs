using TodoManager.Api;
using Xunit.Sdk;

namespace TodoManager.Unit.Tests;

public abstract class MockAsyncReadOnlyUnitOfWork<T> : IAsyncReadOnlyUnitOfWork
    where T : MockAsyncReadOnlyUnitOfWork<T>
{
    public int DisposeCallCount { get; private set; }

    public ValueTask DisposeAsync()
    {
        checked
        {
            DisposeCallCount++;
        }

        return default;
    }

    public virtual T  MustBeDisposed()
    {
        if (DisposeCallCount < 1)
        {
            throw new XunitException($"\"{GetType().Name}\" was not disposed.");
        }

        return (T)this;
    }
}