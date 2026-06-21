namespace Assignment.Models.Responses
{
    public class BookResponse
    {
        public int BookId { get; set; }

        public string ISBN { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public int AvailableCopies { get; set; }

        public bool IsAvailable { get; set; }
    }
}