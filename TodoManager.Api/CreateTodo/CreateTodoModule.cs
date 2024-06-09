namespace TodoManager.Api.CreateTodo;

public static class CreateTodoModule
{
    public static IServiceCollection AddNewTodoModule(this IServiceCollection services) =>
        services
            .AddScoped<ICreateTodoUnitOfWork, CreateTodoUnitOfWork>()
            .AddSingleton<CreateTodoEndpoint>();
}