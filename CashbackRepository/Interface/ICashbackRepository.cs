using CashbackDomain.Model;
using System.Threading.Tasks;

namespace CashbackRepository.Interface
{
    public interface ICashbackRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        // Vendas
        Task<Venda[]> GetAllVendasAsyncByRevendedor(string username);

        Task<Venda> GetVendasAsyncByID(int CodigoID, string Username);

        // Revendedor
        Task<Revendedor> GetRevendedorAsync(string username);
        Task<Revendedor> GetRevendedorAsyncByCPF(string Cpf);
    }
}
