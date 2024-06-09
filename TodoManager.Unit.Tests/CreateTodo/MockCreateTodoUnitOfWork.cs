using TodoManager.Api;
using TodoManager.Api.CreateTodo;

namespace TodoManager.Unit.Tests.CreateTodo;

public sealed class MockCreateTodoUnitOfWork
    : MockAsyncUnitOfWork<MockCreateTodoUnitOfWork>, ICreateTodoUnitOfWork
{
    public int TodoCount { get; private set; } = 1;
    public Todo? CapturedTodo { get; set; }

    public Task<int> AddTodoAsync(Todo newTodo)
    {
        CapturedTodo = newTodo;
        return Task.FromResult(TodoCount++);
    }
}