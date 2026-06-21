namespace Assignment.Models.Responses
{
    public class LoanResponse
    {
        public int LoanId { get; set; }

        public string BookTitle { get; set; }

        public string MemberName { get; set; }

        public DateTime CheckoutDate { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsOverdue { get; set; }

        public decimal OverdueFine { get; set; }
    }
}