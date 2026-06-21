namespace Assignment.Models.Responses
{
    public class BookResponseV1
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public bool IsAvailable { get; set; }
    }
}