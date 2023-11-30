namespace P2PBusinessLedingApis.DAL.Models.Users.RequestObj
{
    public class GetAllUserData
    {
        public bool IsSuccessful { get; set; }
        public string? Code { get; set; }
        public string? Message { get; set; }
        public List<tbl_users> ? UsersData { get; set; }
    }
}
