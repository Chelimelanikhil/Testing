namespace Assignment.Models.Entities
{
    public class Member
    {
        public int MemberId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string MembershipType { get; set; }

        public decimal OutstandingFine { get; set; }

        public DateTime MembershipExpiryDate { get; set; }

        public bool IsActive { get; set; }
    }
}