namespace PerfumeStoreAPI.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        // Password hash ko expose nahi karenge for security reasons
        public string Role { get; set; } = "DefaultRole"; // Default role if null
        public string Token { get; set; } = "DefaultToken"; // Default token if null
    }
}
