namespace P2PBusinessLedingApis.REPO.UserRepo
{
    public class ValidateLoginResult
    {
        public bool IsSuccessful { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
