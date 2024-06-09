namespace TodoManager.Api.CreateTodo;

public sealed class CreateTodoEndpoint : IMinimalApiEndpoint
{
    private readonly ICreateTodoUnitOfWork _unitOfWork;
    private readonly ILogger _logger;

    public CreateTodoEndpoint(ICreateTodoUnitOfWork unitOfWork, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public void MapEndpoint(WebApplication app) =>
        app.MapPost("/api/todos/new", CreateTodoAsync)
            .Produces<TodoDto>(StatusCodes.Status201Created)
            .Produces<Dictionary<string, string>>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithName(nameof(CreateTodoAsync))
            .WithOpenApi();

    public async Task<IResult> CreateTodoAsync(CreateTodoDto dto, CancellationToken ct)
    {
        var todo = new Todo { Title = dto.Title };

        await _unitOfWork.AddTodoAsync(todo);
        await _unitOfWork.SaveChangesAsync(ct);

        return TypedResults.Created($"/todos/{todo.Id}", todo.ToDto());
    }
}

public sealed record CreateTodoDto(string Title);