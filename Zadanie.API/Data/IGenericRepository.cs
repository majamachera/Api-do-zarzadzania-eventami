using System.Threading.Tasks;

namespace Zadanie.API.Data
{
    public interface IGenericRepository
    {
        void DelateAsync<T>(T entity) where T : class;
        Task<bool> SaveAllAsync();
    }
}