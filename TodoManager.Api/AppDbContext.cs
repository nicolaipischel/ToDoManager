using Microsoft.EntityFrameworkCore;

namespace TodoManager.Api;

public sealed class AppDbContext : DbContext
{
    private readonly TimeProvider _timeProvider;

    public AppDbContext(DbContextOptions<AppDbContext> options, TimeProvider timeProvider) : base(options)
    {
        _timeProvider = timeProvider;
    }

    public DbSet<Todo> Todos => Set<Todo>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}