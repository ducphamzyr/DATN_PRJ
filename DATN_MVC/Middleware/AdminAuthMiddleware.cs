namespace DATN_MVC.Middleware
{
    public class AdminAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AdminAuthMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (context.Request.Path.StartsWithSegments("/Admin"))
                {
                    var token = context.Session.GetString("JWTToken");
                    var userRole = context.Session.GetString("UserRole");

                    if (string.IsNullOrEmpty(token) || userRole != "Admin")
                    {
                        context.Response.Redirect("/Auth/Login");
                        return;
                    }
                }

                if (_next != null)
                {
                    await _next(context);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
