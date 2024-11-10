public class ForbidenMiddleware{
    private readonly RequestDelegate _next;
    public ForbidenMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
       await _next(context);

        if (context.Response.StatusCode == 403)
        {
            context.Response.ContentType = "application/json";
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(new { 
                message = "Forbiden access to this resource",
                data = "",
                error = ""
                });
            await context.Response.WriteAsync(result);
        }
    }
}