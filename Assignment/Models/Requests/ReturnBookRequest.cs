using System.ComponentModel.DataAnnotations;

namespace Assignment.Models.Requests
{
    public class ReturnBookRequest
    {
        [Required]
        public int LoanId { get; set; }

        [Required]
        public int MemberId { get; set; }

        [Required]
        public string ReturnCondition { get; set; }
    }
}