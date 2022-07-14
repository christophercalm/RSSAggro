namespace RSSAggro.Models
{
    public class RSSArticle
    {
        public string? Content { get; set; }

        public string? Title { get; set; }

        public string? Author { get; set; }


        public RSSArticle(string content, string title, string author)
        {
            Content = content;
            Title = title;  
            Author = author;
        }
    }
}
