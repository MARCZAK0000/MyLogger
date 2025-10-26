# CustomLogger

Lightweight and colorful logging library for .NET 9 with Dependency Injection integration.

## 📋 Table of Contents

- [Features](#-features)
- [Installation](#-installation)
- [Usage](#-usage)
  - [Dependency Injection](#dependency-injection)
  - [Factory Pattern](#factory-pattern)
  - [Logging Examples](#logging-examples)
- [Log Levels](#-log-levels)
- [Console Colors](#-console-colors)
- [API](#-api)
- [Examples](#-examples)
- [License](#-license)

## ✨ Features

- **Typed Logger** - Generic `ICustomLogger<T>` interface for better log source tracking
- **Colorful Output** - Different colors for different log levels in console    
- **Exception Handling** - Detailed exception logging with full stack trace
- **Thread-safe** - Safe logging in multi-threaded environments
- **Dependency Injection** - Full integration with Microsoft.Extensions.DependencyInjection
- **Factory Pattern** - Ability to create loggers without DI
- **Argument Formatting** - Support for parameterized messages

## 📦 Installation

### NuGet Package
```bash
Install-Package MARCZAK0000.CustomLogger
```

### .NET CLI
```bash
dotnet add package MARCZAK0000.CustomLogger
```

## 🚀 Usage

### Dependency Injection

```csharp
using CustomLogger.Extension;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

// Register CustomLogger
builder.Services.AddCustomLogger();

var host = builder.Build();

// Usage in a class
public class MyService
{
    private readonly ICustomLogger<MyService> _logger;

    public MyService(ICustomLogger<MyService> logger)
    {
        _logger = logger;
    }

    public void DoSomething()
    {
        _logger.LogInformation("Starting operation");
        try
        {
            // Your logic
            _logger.LogDebug("Operation completed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during operation execution");
        }
    }
}
```

### Factory Pattern

```csharp
using CustomLogger.Model;
using CustomLogger.Abstraction;

// Creating logger without DI
var factory = new CustomLoggerFactory();
var logger = factory.CreateLogger<Program>();

logger.LogInformation("Hello World!");

// Remember to dispose resources
factory.Dispose();
```

### Logging Examples

```csharp
// Basic logging
logger.LogInformation("Application started");

// Logging with parameters
logger.LogInformation("Processing file: {fileName} with size {size} bytes", 
                     "document.pdf", 1024);

// Error logging with exception
try
{
    // code that might throw exception
}
catch (Exception ex)
{
    logger.LogError(ex, "Error processing file {fileName}", fileName);
}

// Critical logging
logger.LogCritical("Critical system error - application will be terminated");
```

## 📊 Log Levels

| Level | Method | Color | Description |
|-------|--------|-------|-------------|
| **Trace** | `LogTrace()` | Gray | Very detailed debug information |
| **Debug** | `LogDebug()` | Green | Debug information |
| **Information** | `LogInformation()` | Gray | General application flow information |
| **Warning** | `LogWarning()` | Yellow | Warnings about potential issues |
| **Error** | `LogError()` | Orange | Errors that don't stop the application |
| **Critical** | `LogCritical()` | Red | Critical errors requiring immediate attention |

## 🎨 Console Colors

CustomLogger uses ANSI codes for console output coloring:

- 🔴 **Red** - Critical
- 🟠 **Orange** - Error  
- 🟡 **Yellow** - Warning
- 🟢 **Green** - Debug
- ⚪ **Gray** - Trace, Information

## 🔧 API

### ICustomLogger\<T>

```csharp
public interface ICustomLogger<T> where T : class
{
    // Trace
    void LogTrace(string message);
    void LogTrace(string message, params object[] args);
    
    // Information
    void LogInformation(string message);
    void LogInformation(string message, params object[] args);
    
    // Warning
    void LogWarning(string message);
    void LogWarning(string message, params object[] args);
    
    // Debug
    void LogDebug(string message);
    void LogDebug(string message, params object[] args);
    
    // Error
    void LogError(string message);
    void LogError(string message, params object[] args);
    void LogError(Exception ex, string message);
    void LogError(Exception ex, string message, params object[] args);
    
    // Critical
    void LogCritical(string message);
    void LogCritical(string message, params object[] args);
    void LogCritical(Exception ex, string message);
    void LogCritical(Exception ex, string message, params object[] args);
}
```

### Output Format

```
[LEVEL] 2024-10-26T14:30:15.1234567Z - Your message
```

Example:
```
[INFO] 2024-10-26T14:30:15.1234567Z - Application started
[ERROR] 2024-10-26T14:30:16.1234567Z - Database connection error - Exception: ...
```

## 📝 Examples

### Complete Console Example

```csharp
using CustomLogger.Extension;
using CustomLogger.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddCustomLogger();

var host = builder.Build();
var logger = host.Services.GetRequiredService<ICustomLogger<Program>>();

logger.LogInformation("=== CustomLogger Demonstration ===");

// Different log levels
logger.LogTrace("This is a trace message");
logger.LogDebug("This is a debug message");
logger.LogInformation("This is information");
logger.LogWarning("This is a warning");
logger.LogError("This is an error");
logger.LogCritical("This is a critical error");

// Logging with parameters
var userName = "John Doe";
var loginTime = DateTime.Now;
logger.LogInformation("User {user} logged in at {time}", userName, loginTime);

// Exception logging
try
{
    throw new InvalidOperationException("Sample exception");
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occurred during processing");
}

await host.RunAsync();
```

### Web API Example

```csharp
using CustomLogger.Extension;
using CustomLogger.Abstraction;

var builder = WebApplication.CreateBuilder(args);

// Add CustomLogger
builder.Services.AddCustomLogger();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly ICustomLogger<WeatherController> _logger;

    public WeatherController(ICustomLogger<WeatherController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("Fetching weather data");
        
        try
        {
            // Your business logic
            var weather = GetWeatherData();
            
            _logger.LogDebug("Returned {count} weather records", weather.Count);
            return Ok(weather);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching weather data");
            return StatusCode(500, "Internal server error");
        }
    }
}

app.Run();
```

## 📋 Requirements

- .NET 9.0
- Microsoft.Extensions.DependencyInjection 9.0.8+
- Microsoft.Extensions.DependencyInjection.Abstractions 9.0.8+


## 👤 Author

**MARCZAK0000**

---

💡 **Tip**: This logger is ideal for console applications and development environments where colorful output makes debugging easier. In production environments, consider using more advanced logging solutions.