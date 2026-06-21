namespace Assignment.Models.Entities
{
    public class BookLoan
    {
        public int LoanId { get; set; }

        public int BookId { get; set; }

        public int MemberId { get; set; }

        public DateTime CheckoutDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public decimal OverdueFine { get; set; }

        public string LoanStatus { get; set; }
    }
}