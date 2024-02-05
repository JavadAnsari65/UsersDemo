using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsersDemo.Infrastructure.Entities
{
    public class UserEntity
    {
        [Key]
        public int UserId { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string UserEmail { get; set; }
        public string Mobile { get; set; }
        public string Image { get; set; }

    }
}
