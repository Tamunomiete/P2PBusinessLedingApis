namespace P2PBusinessLedingApis.DAL.Models.Users.ResponseObj
{
    public class CreateUserResult
    {
        public bool IsSuccessful { get; set; }
        public string? Code { get; set; }
        public string? Message { get; set; }
    }
}
