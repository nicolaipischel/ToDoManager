using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using TodoManager.Api;
using TodoManager.Api.CreateTodo;

namespace TodoManager.Unit.Tests.CreateTodo;

public class CreateTodoTests
{
    private MockLogger _logger;
    private MockCreateTodoUnitOfWork _unitOfWork;
    private CreateTodoEndpoint _endpoint;
    private CreateTodoDto _validCreateTodo;

    public CreateTodoTests()
    {
        _logger = new MockLogger();
        _unitOfWork = new MockCreateTodoUnitOfWork();
        _endpoint = new CreateTodoEndpoint(_unitOfWork, _logger);

        _validCreateTodo = new CreateTodoDto("Bring out trash");
    }

    [Fact]
    public async Task CreateTodo_Given_ValidTodo_Then_ReturnsCreated()
    {
        // Act
        var result = await _endpoint.CreateTodoAsync(_validCreateTodo, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Created<TodoDto>>();
    }

    [Fact]
    public async Task CreateTodo_Given_ValidTodo_Then_SaveChangesIsCalled()
    {
        // Act
        _ = await _endpoint.CreateTodoAsync(_validCreateTodo, CancellationToken.None);

        // Assert
        _unitOfWork.SaveChangesMustHaveBeenCalled();
    }

    [Fact]
    public async Task CreateTodo_Given_ValidTodo_Then_NewTodoIsAdded()
    {
        // Arrange
        var expectedTodo = new Todo { Title = _validCreateTodo.Title };

        // Act
        _ = await _endpoint.CreateTodoAsync(_validCreateTodo, CancellationToken.None);

        // Assert
        _unitOfWork.CapturedTodo.Should().BeEquivalentTo(
            expectedTodo,
            x => x
                .Excluding(todo => todo.CreatedAt)
                .Excluding(todo => todo.UpdatedAt)
                .Excluding(todo => todo.Id));
    }

    [Fact]
    public async Task CreateTodo_Given_TodoWithoutTitle_Then_ReturnsBadRequest()
    {
        // Arrange
        var noTitleTodo = new CreateTodoDto(Title: string.Empty);

        // Act
        var result = await _endpoint.CreateTodoAsync(noTitleTodo, CancellationToken.None);

        // Assert
        result.Should().BeOfType<BadRequest>();
    }
}