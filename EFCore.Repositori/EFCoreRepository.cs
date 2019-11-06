using System.Linq;
using System.Threading.Tasks;
using EFCore.Dominio;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Repositorio
{
    public class EFCoreRepository : IEFCoreRepository
    {
        private readonly HeroiContext _context;

        public EFCoreRepository(HeroiContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SavesChangeAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Heroi[]> getAllHerois(bool incluirbatalha = false)
        {
            IQueryable<Heroi> query = _context.Herois
                .Include(h => h.Identidade)
                .Include(h => h.Armas);

            if(incluirbatalha)
                query.Include(h => h.HeroisBatalhas).ThenInclude(heroib => heroib.Batalha);

            query = query.AsNoTracking().OrderBy(h => h.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Heroi> getHeroiById(int id, bool incluirbatalha = false)
        {
            IQueryable<Heroi> query = _context.Herois
                .Include(h => h.Identidade)
                .Include(h => h.Armas);

            query = query.AsNoTracking().OrderBy(h => h.Id);

            return await query.FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<Heroi[]> getHeroisByNome(string nome, bool incluirbatalha = false)
        {
            IQueryable<Heroi> query = _context.Herois
                .Include(h => h.Identidade)
                .Include(h => h.Armas);

            if (incluirbatalha)
                query.Include(h => h.HeroisBatalhas).ThenInclude(heroib => heroib.Batalha);

            query = query.AsNoTracking().Where(h => h.Nome.Contains(nome)).OrderBy(h => h.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Batalha[]> getAllBatalhas(bool incluirHeroi = false)
        {
            IQueryable<Batalha> query = _context.Batalhas;

            if (incluirHeroi)
                query.Include(h => h.HeroisBatalhas).ThenInclude(heroib => heroib.Heroi);

            query = query.AsNoTracking().OrderBy(h => h.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Batalha> getBatalhaById(int id, bool incluirHeroi = false)
        {
            IQueryable<Batalha> query = _context.Batalhas;

            if (incluirHeroi)
                query.Include(h => h.HeroisBatalhas).ThenInclude(heroib => heroib.Heroi);

            query = query.AsNoTracking().OrderBy(h => h.Id);

            return await query.FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<Batalha[]> getBatalhaByNome(string nome, bool incluirHeroi)
        {
            IQueryable<Batalha> query = _context.Batalhas;

            if (incluirHeroi)
                query.Include(h => h.HeroisBatalhas).ThenInclude(heroib => heroib.Heroi);

            query = query.AsNoTracking().Where(h => h.Nome.Contains(nome)).OrderBy(h => h.Id);

            return await query.ToArrayAsync();
        }
    }
}
