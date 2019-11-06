using EFCore.Dominio;
using System.Threading.Tasks;

namespace EFCore.Repositorio
{
    public interface IEFCoreRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;

        Task<bool> SavesChangeAsync();

        Task<Heroi[]> getAllHerois(bool incluirbatalha);
        Task<Heroi> getHeroiById(int id, bool incluirbatalha = false);
        Task<Heroi[]> getHeroisByNome(string nome, bool incluirbatalha);


        Task<Batalha[]> getAllBatalhas(bool incluirHeroi);
        Task<Batalha> getBatalhaById(int id, bool incluirHeroi = false);
        Task<Batalha[]> getBatalhaByNome(string nome, bool incluirHeroi);
    }
}
