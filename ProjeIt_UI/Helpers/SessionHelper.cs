using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ProjeIt_UI.Helpers
{
    public class SessionHelper : ISessionHelper
    {
        private readonly IHttpContextAccessor HttpContextAccessor;

        public SessionHelper(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public void SetSeesionModel(object SeesionModel, string SessionName)
        {
            HttpContextAccessor.HttpContext.Session.SetString(SessionName, JsonConvert.SerializeObject(SeesionModel));
        }

        public string GetSessionModel(string SessionKey)
        {
            return HttpContextAccessor.HttpContext.Session.GetString(SessionKey);
        }
    }
}
