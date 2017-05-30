namespace Quotes.Core
{
    public enum QuoteType
    {
        Love,
        Life,
        Funny,
        Inspirational
    }
    public class QuoteModel
    {
        //[Key]
        public int Id { get; set; }

        public QuoteType QuoteType { get; set; }

        public string Quote { get; set; }
        public string Author { get; set; }
    }
}