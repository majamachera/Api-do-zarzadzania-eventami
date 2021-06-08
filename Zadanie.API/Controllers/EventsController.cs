using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Zadanie.API.Data;
using Microsoft.EntityFrameworkCore;
using Zadanie.API.Models;
using AutoMapper;
using Zadanie.API.Dtos;
using System.Text;

namespace Zadanie.API.Controllers
{
    [ApiController]
    [Route("")]

    public class EventsController : ControllerBase
    {
        private readonly IEventRepository _repo;
        private readonly IMapper _mapper;

        public EventsController(IEventRepository repo, IMapper mapper)
        {

            _mapper = mapper;
            _repo = repo;


        }
        
        [HttpGet("events")]
        public async Task<IActionResult> GetEventsAsync()
        {
            var events = await _repo.GetEventsAsync();
            var mappedevents = _mapper.Map<IEnumerable<EventDto>>(events);
            return Ok(mappedevents);
        }

        [HttpGet("event/{id}")]
        public async Task<IActionResult> GetEventAsync(int id)
        {
            var thisEvent = await _repo.GetEventAsync(id);
            var mappedThisEvent = _mapper.Map<EventDto>(thisEvent);
            return Ok(mappedThisEvent);

        }
        [HttpDelete("event/{id}")]
        public async Task<IActionResult> DelateEventAsync(int id)
        {
            var thisEvent = await _repo.GetEventAsync(id);
            _repo.DelateAsync(thisEvent);
            if (await _repo.SaveAllAsync())
            {
                return Ok();
            }
            return BadRequest("błąd w usunięciu");
        }
        [HttpPost("/event")]
        public async Task<IActionResult> AddEventAsync(AddEventDto addEventDto)
        {
            

            addEventDto.Name = addEventDto.Name.ToLower();
            if (await _repo.EventExistAsync(addEventDto.Name))
                return BadRequest("Wydarzenie o takiej nazwie już istnieje");
            if (!(_repo.EventTypeExist(addEventDto.Type)))
                return BadRequest("Taki numer typu wydarzenia nie istnieje, do wyboru jest 1-Event, 2-Seminarium, 3-Meeting");
            var EventtoCreate = new Events
            {
                AddDate = addEventDto.AddDate
            };

            var CreatedEvent = await _repo.AddEventAsync(EventtoCreate, addEventDto.Name, addEventDto.Date, addEventDto.Type);

            return StatusCode(201);

        }



    }
}