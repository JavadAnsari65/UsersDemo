namespace UsersDemo.DTO.InternalApi.Request
{
    public class UserRequest
    {
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
