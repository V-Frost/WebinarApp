using System.ComponentModel.DataAnnotations;

namespace WebinarApp.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // "Admin" або "User"
        public string Category { get; set; } // Зберігається тільки для адміністратора
    }
}
