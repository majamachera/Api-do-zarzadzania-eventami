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
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public readonly IEventRepository _eventrepo;



        public UserController(IUserRepository repository, IEventRepository eventrepo, IMapper mapper)
        {

            _eventrepo = eventrepo;
            _mapper = mapper;
            _repository = repository;


        }
        [HttpGet("/users")]
        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await _repository.GetUsersAsync();
            var mappedusers = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(mappedusers);
        }
        [HttpPost("/user")]
        public async Task<IActionResult> AddUserAsync(AddUserDto addUserDto)

        {

            //Sprawdzenie czy jest wydarzenie o takim Id 
            if (!(await _repository.IdEventExistAsync(addUserDto.EventId)))
                return BadRequest("Nie ma wydarzenia o takim ID podaj poprawne");
            Events allusers = await _eventrepo.GetEventAsync(addUserDto.EventId);
            var how = allusers.User.Count;
            var i = 25;
            //Sprawdzenie czy jest mniej niż 25 uczestników
            if (how >= i)
                return BadRequest("Na to wydarzenie nie można się już zapisać, jest już" + i + "uczestników");
            if (!(addUserDto.Email.Contains("@")))
                return BadRequest("Ten email jest niepoprawny");
            if (await _repository.EmailExistAsync(addUserDto.EventId, addUserDto.Email))
                return BadRequest("Użytkownik z takim emailem już istnieje");
            var eventFromRepo = await _eventrepo.GetEventAsync(addUserDto.EventId);
            var UserForAdd = new User
            {
                Name = addUserDto.Name,
                Email = addUserDto.Email

            };
            var usermap = _mapper.Map<User>(UserForAdd);
            eventFromRepo.User.Add(usermap);

            if (await _repository.SaveAllAsync())
            {
                var user = _mapper.Map<UserDto>(usermap);
                return CreatedAtRoute("GetUser", new { id = usermap.Id }, user);
            }
            return BadRequest("nie udało");
        }
        [HttpDelete("user/{id}")]
        public async Task<IActionResult> DelateUserAsync(int id)
        {
            var thisUser = await _repository.GetUserAsync(id);
            _repository.DelateAsync(thisUser);
            if (await _repository.SaveAllAsync())
            {
                return Ok();
            }
            return BadRequest("błąd w usunięciu");
        }
        [HttpGet("id", Name = "GetUser")]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            var user = await _repository.GetUserAsync(id);
            var UserforReturn = _mapper.Map<AddUserDto>(user);
            return Ok(UserforReturn);
        }

    }
}