namespace TodoManager.Api.CreateTodo;

public interface ICreateTodoUnitOfWork : IAsyncUnitOfWork
{
    Task<int> AddTodoAsync(Todo newTodo);
}