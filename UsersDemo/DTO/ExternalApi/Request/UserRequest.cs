using System.ComponentModel.DataAnnotations;

namespace UsersDemo.DTO.ExternalApi.Request
{
    public class UserRequest
    {
        [Required]
        [MaxLength(256)] // حداکثر طول نام کاربری
        public string Username { get; set; }
        [StringLength(20, MinimumLength = 8, ErrorMessage = "The length of the password must be at least 8 characters.")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[!@#$%^&*])[a-zA-Z\d!@#$%^&*]+$", ErrorMessage = "The password must contain letters, numbers and symbols.")]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string UserEmail { get; set; }
        [RegularExpression(@"^\d{11}$", ErrorMessage = "The mobile number must be an 11-digit numeric string.")]
        public string Mobile { get; set; }
        public string Image { get; set; }
    }
}
