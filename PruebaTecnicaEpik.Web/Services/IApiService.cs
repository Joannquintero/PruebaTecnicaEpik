using PruebaTecnicaEpik.Models.Models.Request;
using PruebaTecnicaEpik.Web.Models;
using System.Threading.Tasks;

namespace PruebaTecnicaEpik.Web.Services
{
    public interface IApiService
    {
        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller);
        Task<Response> CreatePersonaAsync(string urlBase, string servicePrefix, string controller, PersonaRequest model);
        Task<Response> PutAsync<T>(string urlBase, string servicePrefix, string controller, T model);
        Task<Response> DeleteAsync(string urlBase, string servicePrefix, string controller);
        Task<Response> GetAsync<T>(string urlBase, string servicePrefix, string controller);
    }
}
