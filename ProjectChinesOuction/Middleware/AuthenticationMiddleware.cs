using ProjectChinesOuction.Models;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using ProjectChinesOuction.BLL;

namespace ProjectChinesOuction
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthenticationMiddleware> _logger;
  
        public AuthenticationMiddleware(RequestDelegate next, ILogger<AuthenticationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var identity = context.User.Identity as ClaimsIdentity;
                if (identity == null)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }
                EnumRoleUser role;
        
                var userClaims = identity.Claims;
                Enum.TryParse(userClaims.FirstOrDefault(x => x.Type == "Role")?.Value ?? "", out role);
                int userId;
                int.TryParse(userClaims.FirstOrDefault(x => x.Type == "userId")?.Value ?? "", out userId);

                var user = new User
                {

                    Role = role,
                    UserId=userId
                
                };

                context.Items["User"] = user;
                await _next(context);

            }
               catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the middleware.");
                
            }
        }
    
    }
}
