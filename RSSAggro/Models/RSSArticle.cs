namespace RSSAggro.Models
{
    public class RSSArticle
    {
        public string? Content { get; set; }

        public string? Title { get; set; }

        public string? Author { get; set; }

        public DateTimeOffset? DateCreated { get; set; }


        public RSSArticle(string content, string title, string author, DateTimeOffset dateCreated)
        {
            Content = content;
            Title = title;  
            Author = author;
            DateCreated = dateCreated;
        }
    }
}
