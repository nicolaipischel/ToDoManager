using Microsoft.EntityFrameworkCore;

namespace TodoManager.Api;

public abstract class EfAsyncUnitOfWork<T> : IAsyncUnitOfWork
    where T : DbContext
{
    protected EfAsyncUnitOfWork(T dbContext) => DbContext = dbContext;

    protected T DbContext { get; }

    public virtual Task SaveChangesAsync(CancellationToken ct = default) =>
        DbContext.SaveChangesAsync(ct);

    // The Dependency Injection will handle disposal - you do not call Dispose directly.
    // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1#overview-of-dependency-injection.
    public async ValueTask DisposeAsync() => await Task.CompletedTask;
}