using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zadanie.API.Models;

namespace Zadanie.API.Data
{
    public interface IUserRepository : IGenericRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserAsync(int idp);
        Task<bool> EmailExistAsync(int id, string email);
        Task<bool> IdEventExistAsync(int id);

    }
}