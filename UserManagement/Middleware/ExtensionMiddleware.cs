namespace UserManagement.Middleware
{
    public static class ExtensionMiddleware
    {
        public static IApplicationBuilder CustomMiddleware(this IApplicationBuilder builder) 
        {
            return builder.UseMiddleware<PracticeMiddleware>();
        }
    }
}
