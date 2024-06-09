namespace TodoManager.Api;

public sealed record Todo
{
    public int Id { get; init; }
    public required string Title { get; init; } = string.Empty;
    public bool IsComplete { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}

public sealed record TodoDto
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public bool IsComplete { get; init; }
}

public static class TodoMappingExtensions
{
    public static TodoDto ToDto(this Todo todo)
    {
        return new()
        {
            Id = todo.Id,
            Title = todo.Title,
            IsComplete = todo.IsComplete,
        };
    }
}