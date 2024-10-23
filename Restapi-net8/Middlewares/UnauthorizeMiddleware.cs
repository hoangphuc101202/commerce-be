public class UnauthorizeMiddleware{
    private readonly RequestDelegate _next;
    public UnauthorizeMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
       await _next(context);

        if (context.Response.StatusCode == 401)
        {
            context.Response.ContentType = "application/json";
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(new { 
                message = "Unauthorized access",
                data = "",
                error = ""
                });
            await context.Response.WriteAsync(result);
        }
    }
}