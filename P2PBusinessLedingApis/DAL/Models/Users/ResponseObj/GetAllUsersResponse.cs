namespace P2PBusinessLedingApis.DAL.Models.Users.ResponseObj
{
    public class GetAllUsersResponse
    {
        public string? ResponseCode { get; set; }
        public string? ResponseDescription { get; set; }
        public List<tbl_users>? Users { get; set; }
    }
}
