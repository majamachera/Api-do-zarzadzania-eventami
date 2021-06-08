using System;
using System.Collections.Generic;



namespace Zadanie.API.Models
{
    public class User
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public string Email {get; set;}
        public Events Event { get; set; }
        public int EventId { get; set; }
    }
}