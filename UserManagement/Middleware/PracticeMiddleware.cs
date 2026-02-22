namespace UserManagement.Middleware
{
    public class PracticeMiddleware
    {
        public readonly RequestDelegate _next;
        public PracticeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            try {
                //Code in this middleware that will be executed before hitting next middleware
                await _next(context);
                //Code in this middleware that will be executed after hitting next middleware
            }

            catch (Exception ex)
            {

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var result = new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "Internal Server Error"
                };

                await context.Response.WriteAsJsonAsync(result);
            }
        }
    }
}
