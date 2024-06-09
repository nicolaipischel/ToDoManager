using TodoManager.Api;
using Xunit.Sdk;

namespace TodoManager.Unit.Tests;

public abstract class MockAsyncUnitOfWork<T> : IAsyncUnitOfWork
    where T : MockAsyncUnitOfWork<T>
{
    public int SaveChangesCallCount { get; private set; }

    public int DisposeCallCount { get; private set; }
    public Exception? ExceptionOnSaveChanges { get; set; }

    public T SaveChangesMustHaveBeenCalled()
    {
        if (SaveChangesCallCount != 1)
        {
            throw new XunitException(
                $"SaveChangesAsync must have been called exactly once, but it was called {SaveChangesCallCount} times.");
        }

        return (T)this;
    }

    private void IncrementSaveChangesCallCount()
    {
        checked { SaveChangesCallCount++; }
    }

    private void ThrowExceptionIfNecessary()
    {
        if (ExceptionOnSaveChanges != null)
        {
            throw ExceptionOnSaveChanges;
        }
    }

    private void SaveChangesInternal()
    {
        IncrementSaveChangesCallCount();
        ThrowExceptionIfNecessary();
    }

    public ValueTask DisposeAsync()
    {
        checked
        {
            DisposeCallCount++;
        }

        return default;
    }

    public virtual T MustBeDisposed()
    {
        if (DisposeCallCount < 1)
        {
            throw new XunitException($"\"{GetType().Name}\" was not disposed.");
        }

        return (T)this;
    }

    public virtual T SaveChangesMustNotHaveBeenCalled()
    {
        if (SaveChangesCallCount != 0)
        {
            throw new XunitException(
                $"SaveChangesAsync must not have been called, but it was called {SaveChangesCallCount} {(SaveChangesCallCount == 1 ? "time" : "times")}.");
        }

        return (T)this;
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
    {
        SaveChangesInternal();
        return Task.CompletedTask;
    }
}