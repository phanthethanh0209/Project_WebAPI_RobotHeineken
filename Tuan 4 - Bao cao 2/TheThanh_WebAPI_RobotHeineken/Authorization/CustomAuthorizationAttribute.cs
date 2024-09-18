using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TheThanh_WebAPI_RobotHeineken.Authorization
{
    public class CustomAuthorizationAttribute
    {
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
        public class CustomAuthorizeAttribute : TypeFilterAttribute
        {
            public CustomAuthorizeAttribute(string permission) : base(typeof(PermissionFilter))
            {
                Arguments = new object[] { new PermissionRequirement(permission) };
            }
        }

        public class PermissionFilter : IAuthorizationFilter // kiểm tra quyền truy cập dựa trên quyền (permission) được chỉ định.
        {
            private readonly IAuthorizationService _authorization;
            private readonly PermissionRequirement _requirement;

            public PermissionFilter(IAuthorizationService authorization, PermissionRequirement requirement)
            {
                _authorization = authorization;
                _requirement = requirement;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                // Kiểm tra xem người dùng có được xác thực hay chưa
                if (!context.HttpContext.User.Identity.IsAuthenticated)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                // Thực hiện kiểm tra quyền của người dùng với quyền yêu cầu (_requirement)
                AuthorizationResult result = _authorization.AuthorizeAsync(context.HttpContext.User, null, _requirement).Result;
                if (!result.Succeeded)
                {
                    context.Result = new ForbidResult();
                }
            }
        }

    }
}
