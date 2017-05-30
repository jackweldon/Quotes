namespace Quotes
{
    public enum QuoteType
    {
        Love = 1,
        Life = 2,
        Funny = 3,
        Happy = 4
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

///TMtDThMbLCUhcj8%