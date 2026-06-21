using System.ComponentModel.DataAnnotations;

namespace Assignment.Models.Requests
{
    public class RegisterMemberRequest
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string MembershipType { get; set; }
    }
}