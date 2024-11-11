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
                // Check nếu request đến area Admin
                if (context.Request.Path.StartsWithSegments("/Admin"))
                {
                    var token = context.Session.GetString("JWTToken");
                    var userRole = context.Session.GetString("UserRole");
                    Console.WriteLine($"Middleware Debug Path: {context.Request.Path}");
                    Console.WriteLine($"Middleware Debug Token: {token}");
                    Console.WriteLine($"Middleware Debug Role: {userRole}");
                    // Nếu chưa đăng nhập hoặc không phải Admin
                    if (string.IsNullOrEmpty(token) || userRole != "Admin")
                    {
                        // Lưu URL hiện tại để redirect sau khi đăng nhập
                        var returnUrl = context.Request.Path + context.Request.QueryString;
                        context.Response.Redirect($"/Auth/Login?returnUrl={returnUrl}");
                        return; // Dừng pipeline ở đây
                    }
                }

                // Nếu xác thực thành công hoặc không phải admin area, tiếp tục pipeline
                await _next(context);
            }
            catch (Exception)
            {
                // Log lỗi nếu cần
                context.Response.Redirect("/Auth/Login");
                return;
            }
        }
    }
}
