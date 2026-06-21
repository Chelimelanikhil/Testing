using System.ComponentModel.DataAnnotations;

namespace Assignment.Models.Requests
{
    public class SearchBooksRequest
    {
        public string? Title { get; set; }

        public string? Author { get; set; }

        public string? Genre { get; set; }

        [Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;

        [Range(1, 50)]
        public int PageSize { get; set; } = 10;
    }
}