using System.ComponentModel.DataAnnotations;

namespace P2PBusinessLedingApis.DAL.Models.Users
{
    public class tbl_users
    {
        [Key]
        [Required]
        [StringLength(50)]
        public string UserId { get; set; }

        [Required]
        [StringLength(255)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [StringLength(20)]
        public string? PhoneNumber { get; set; }

        public string? Roles { get; set; }

        public string? BVN { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? IPAddress { get; set; }
    }
}
