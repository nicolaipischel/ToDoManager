namespace TodoManager.Api;

public interface IAsyncReadOnlyUnitOfWork : IAsyncDisposable { }

public interface IAsyncUnitOfWork : IAsyncDisposable
{
    Task SaveChangesAsync(CancellationToken ct = default);
}