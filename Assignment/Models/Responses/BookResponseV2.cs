namespace Assignment.Models.Responses
{
    public class BookResponseV2
    {
        public int BookId { get; set; }

        public string ISBN { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Publisher { get; set; }

        public int? PublishedYear { get; set; }

        public string Genre { get; set; }

        public int AvailableCopies { get; set; }
    }
}