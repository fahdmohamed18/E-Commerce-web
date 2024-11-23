namespace Natsu.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string? ImageUrl { get; set; } // Nullable image URL
    }
}
