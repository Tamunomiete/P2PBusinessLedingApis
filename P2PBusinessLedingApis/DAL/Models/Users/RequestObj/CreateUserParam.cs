namespace P2PBusinessLedingApis.DAL.Models.Users.RequestObj
{
    public class CreateUserParam
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string Roles { get; set; }

        public string? BVN { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

    }
}
