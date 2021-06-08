using System;
using System.Collections.Generic;

namespace Zadanie.API.Models
{
    public class Events
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public DateTime AddDate { get; set; }
        public DateTime Date {get; set;}
        public enum EventType
        {
        Event = 1,
        Seminarium = 2,
        Meeting = 3
        }
        public EventType Type {get;set;}
        public ICollection<User> User { get; set; }

    }
}