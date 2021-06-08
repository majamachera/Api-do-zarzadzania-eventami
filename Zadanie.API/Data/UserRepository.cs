using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zadanie.API.Models;
namespace Zadanie.API.Data
{
    public class UserRepository : GenericRepository, IUserRepository

    {
        private readonly DataContext _context;
        public UserRepository(DataContext context) : base(context)
        {
            _context = context;
        }
        /// <summary>
        /// Zabezpieczenie aby tylko 
        /// jeden użytkownik o tym samym emailu mógł 
        /// się zapisać do danego wydarzenia
        /// </summary>
        public async Task<bool> EmailExistAsync(int id, string email)
        {
            if ((await _context.User.FirstOrDefaultAsync(x => x.EventId == id)) != null)
            {
                if (await _context.User.AnyAsync(x => x.Email == email))
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        /// <summary>
        /// Pobieranie użytkownika po id
        /// </summary>
        public async Task<User> GetUserAsync(int idp)
        {
            var thisuser = await _context.User.FirstOrDefaultAsync(x => x.Id == idp);
            return thisuser;
        }
        /// <summary>
        /// pobieranie listy użytkowników dla konkretnego eventu
        /// </summary>
        public async Task<IEnumerable<User>> GetUsersAsync()
        {

            var user = await _context.User.ToListAsync();
            return user;
        }
        /// <summary>
        /// Zabezpieczenie aby użytkownik wybrał istniejące id eventu
        /// </summary>

        public async Task<bool> IdEventExistAsync(int id)
        {
            if (await _context.Events.AnyAsync(x => x.Id == id))
            {

                return true;
            }
            return false;

        }

    }
}