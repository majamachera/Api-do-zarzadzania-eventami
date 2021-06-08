using System.ComponentModel.DataAnnotations;
using Zadanie.API.Models;
namespace Zadanie.API.Dtos
{
    public class AddUserDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email jest wymagany")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Podaj numer ID eventu, na który chcesz się zapisać")]
        public int EventId { get; set; }
    }
}