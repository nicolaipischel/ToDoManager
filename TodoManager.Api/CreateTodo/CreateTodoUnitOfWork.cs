namespace TodoManager.Api.CreateTodo;

public sealed class CreateTodoUnitOfWork : EfAsyncUnitOfWork<AppDbContext>, ICreateTodoUnitOfWork
{
    public CreateTodoUnitOfWork(AppDbContext context) : base(context) { }

    public Task<int> AddTodoAsync(Todo newTodo) => throw new NotImplementedException();
}