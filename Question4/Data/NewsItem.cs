using System;

namespace Question4.Data
{
    public class NewsItem
    {
        public string Headline { get; set; }
        public string Text { get; set; }
        public int RelevanceScore { get; set; }
        public DateTime Date { get; set; }
        public string Source { get; set; }
        public string[] Tags { get; set; }
    }

}
