using System;
using System.ComponentModel.DataAnnotations;
using static Zadanie.API.Models.Events;

namespace Zadanie.API.Dtos
{
    public class AddEventDto
    {
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        public string Name { get; set; }
        public DateTime AddDate { get; set; }
        [Required(ErrorMessage = "Data Eventu jest wymagana")]
        public DateTime Date { get; set; }
        public EventType Type { get; set; }

        public AddEventDto()
        {
            AddDate = DateTime.Now.Date;

        }
    }
}