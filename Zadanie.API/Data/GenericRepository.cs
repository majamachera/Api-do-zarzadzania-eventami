using System.Threading.Tasks;

namespace Zadanie.API.Data
{
    public class GenericRepository : IGenericRepository
    {
        private readonly DataContext _context;
        public GenericRepository(DataContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Zapis
        /// </summary>
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        /// <summary>
        /// Usówanie
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="T"></typeparam>
        public void DelateAsync<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
    }
}