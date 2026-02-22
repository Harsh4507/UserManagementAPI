using Microsoft.AspNetCore.Mvc.Filters;

namespace UserManagement.Filters
{
    public class CustomActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Code to execute before the action method is called
            // For example, you can log the request or modify the context
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Code to execute after the action method is called
            // For example, you can log the response or handle exceptions
        }
    }
}
