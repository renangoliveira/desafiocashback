using CashbackDomain.Model;
using CashbackRepository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CashbackRepository
{
    public class CashbackRepositoryIn : ICashbackRepository
    {
        public readonly CashbackContext _context; 

        public CashbackRepositoryIn(CashbackContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public async Task<Venda[]> GetAllVendasAsyncByRevendedor(string username)
        {            
            IQueryable<Venda> query = _context.Vendas;            

            query = query.AsNoTracking().Where(c => c.Username == username);

            return (await query.ToArrayAsync());
        }

        public async Task<Revendedor> GetRevendedorAsync(string username)
        {
            IQueryable<Revendedor> query = _context.Revendedores;
            query = query.AsNoTracking().Where(c => c.Email == username);

            return await query.FirstOrDefaultAsync();
        }        

        public async Task<Venda> GetVendasAsyncByID(int CodigoID, string Username)
        {
            IQueryable<Venda> query = _context.Vendas;
            query = query.AsNoTracking().Where(c => c.CodigoId == CodigoID && c.Username == Username);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Revendedor> GetRevendedorAsyncByCPF(string Cpf)
        {
            IQueryable<Revendedor> query = _context.Revendedores;
            query = query.AsNoTracking().Where(c => c.CPF == Cpf);

            return await query.FirstOrDefaultAsync(); ;
        }
    }
}