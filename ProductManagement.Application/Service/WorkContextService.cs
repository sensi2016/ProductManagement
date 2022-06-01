

using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ProductManagement.Application.Service
{
    public class WorkContextService : IWorkContextService,IRegisterScoped
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public WorkContextService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }


        public string UserName => _contextAccessor.HttpContext.User.Identity.Name;
   
        public int? UserId
        {
            get
            {
                var userId = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (int.TryParse(userId, out var resul))
                {
                    return resul;
                }

                return null;
            }
        }

        public int? RoleId
        {
            get
            {
                var RoleId = _contextAccessor.HttpContext.User.Claims.Where(d => d.Type == "RoleId").FirstOrDefault().Value;

                if (int.TryParse(RoleId, out var resul))
                {
                    return resul;
                }

                return null;
            }
        }
     
    }
}
