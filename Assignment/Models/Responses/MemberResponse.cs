namespace Assignment.Models.Responses
{
    public class MemberResponse
    {
        public int MemberId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string MembershipType { get; set; }

        public decimal OutstandingFine { get; set; }

        public int ActiveLoans { get; set; }
    }
}