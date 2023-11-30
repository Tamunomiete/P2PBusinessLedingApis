using System.ComponentModel.DataAnnotations;

namespace P2PBusinessLedingApis.DAL.Models.Users
{
    public class tbl_Roles
    {
        [Key]
        public int RoleId { get; set; }

        public string RoleName { get; set; }
    }
}
