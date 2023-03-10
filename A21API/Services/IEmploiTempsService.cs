using A21API.Models;

namespace A21API.Services
{
    public interface IEmploiTempsService
    {
        public Task<List<EmploiTemps>> GetEmploiTemps();

        public Task<EmploiTemps> GetEmploiTemps(int id);

        public Task<bool> DeleteEmploiTemps(int id);

        public Task<EmploiTemps> SaveEmploiTemps(EmploiTemps emploiTemps);

        public List<string> getErrors();
    }
}