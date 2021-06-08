using System;
using System.Collections.Generic;
using Zadanie.API.Models;
using static Zadanie.API.Models.Events;

namespace Zadanie.API.Dtos
{
    public class EventDto
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public DateTime AddDate { get; set; }
        public DateTime Date{get; set;}
        public EventType Type { get; set; }
        public ICollection<UserDto> User { get; set; }
    }
   
}