using Microsoft.Extensions.Logging;
using Xunit.Sdk;

namespace TodoManager.Unit.Tests;

public sealed class MockLogger : ILogger
{
    public Exception? CapturedException { get; private set; }
    public LogLevel CapturedLogLevel { get; private set; }
    public string CapturedMessage { get; private set; } = string.Empty;
    public int LogCallCount { get; private set; }

    public void LogMustHaveBeenCalled()
    {
        if (LogCallCount != 1)
        {
            var msg = $"{nameof(Log)} must have been called exactly once, but it was called {LogCallCount} times.";
            throw new XunitException(msg);
        }
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        CapturedException = exception;
        CapturedLogLevel = logLevel;
        CapturedMessage = formatter(state, exception);
        LogCallCount++;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        throw new NotImplementedException();
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        throw new NotImplementedException();
    }
}