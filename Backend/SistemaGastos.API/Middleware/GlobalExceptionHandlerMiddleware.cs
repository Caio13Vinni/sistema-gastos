namespace SistemaGastos.API.Middleware;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Uma exceção não tratada ocorreu");

            var response = context.Response;
            response.ContentType = "application/json";

            switch (exception)
            {
                case KeyNotFoundException ex:
                    response.StatusCode = StatusCodes.Status404NotFound;
                    await response.WriteAsJsonAsync(new { erro = ex.Message });
                    break;

                case ArgumentException ex:
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    await response.WriteAsJsonAsync(new { erro = ex.Message });
                    break;

                case InvalidOperationException ex:
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    await response.WriteAsJsonAsync(new { erro = ex.Message });
                    break;

                default:
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    await response.WriteAsJsonAsync(new { erro = "Erro interno do servidor" });
                    break;
            }
        }
    }
}