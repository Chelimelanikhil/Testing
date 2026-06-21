using System.ComponentModel.DataAnnotations;

namespace Assignment.Models.Requests
{
    public class CheckoutBookRequest
    {
        [Required]
        public int MemberId { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public DateTime DueDate { get; set; }
    }
}