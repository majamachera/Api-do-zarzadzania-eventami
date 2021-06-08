using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zadanie.API.Models;
using static Zadanie.API.Models.Events;

namespace Zadanie.API.Data
{
    public class EventRepository : GenericRepository, IEventRepository
    {
        private readonly DataContext _context;
        public EventRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Dodawanie eventu
        /// </summary>

        public async Task<Events> AddEventAsync(Events thisevent, string name, DateTime date, EventType type)
        {


            thisevent.Type = type;
            thisevent.Date = date;
            thisevent.Name = name;
            await _context.Events.AddAsync(thisevent);
            await _context.SaveChangesAsync();
            return thisevent;
        }
        /// <summary>
        /// Sprawdzanie czy taki event już istnieje po nazwie
        /// </summary>
        public async Task<bool> EventExistAsync(string name)
        {
            if (await _context.Events.AnyAsync(x => x.Name == name))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Pobieranie eventu po id
        /// </summary>

        public async Task<Events> GetEventAsync(int idp)
        {
            var thisevent = await _context.Events.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == idp);
            return thisevent;
        }
        /// <summary>
        /// Pobieranie listy Eventów
        /// </summary>
        public async Task<IEnumerable<Events>> GetEventsAsync()
        {
            var events = await _context.Events.Include(x => x.User).ToListAsync();
            return events;
        }
        /// <summary>
        /// Sprawdzanie czy jest taki numer typu wydarzenia
        /// </summary>
        public bool EventTypeExist(EventType type)
        {
            switch (type)
            {
                case EventType.Event:
                    return true;
                case EventType.Seminarium:
                    return true;
                case EventType.Meeting:
                    return true;
                default:
                    return false;
            }
        }



    }
}