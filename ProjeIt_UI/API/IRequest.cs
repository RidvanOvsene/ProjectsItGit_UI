using System.Threading.Tasks;

namespace ProjeIt_UI.Api
{
    public interface IRequest
    {
        Task<T> GetAsync<T>(string token, string controllerAdress);
        Task<T> GetPageAsync<T>(string PageKey, string token, string controllerAdress);
        Task<bool> PostAsync<T>(string token, string controllerAdress, T model);
        Task<T> PostAndGetAsync<T>(string token, string controllerAdress, T model);
    }
}
