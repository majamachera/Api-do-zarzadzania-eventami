using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zadanie.API.Models;
using static Zadanie.API.Models.Events;

namespace Zadanie.API.Data
{

    public interface IEventRepository : IGenericRepository
    {
        Task<IEnumerable<Events>> GetEventsAsync();
        Task<Events> GetEventAsync(int idp);
        Task<Events> AddEventAsync(Events thisevent, string name, DateTime date, EventType type);
        Task<bool> EventExistAsync(string name);
        bool EventTypeExist(EventType type);




    }

}