namespace BlogProject.API.Models.Domain
{
    public class BlogPost
    {
        public Guid Id { get; set; }

        public String Title { get; set; }

        public String ShortDescription  { get; set; }

        public String Content { get; set; }

        public String FeatureImageUrl { get; set; }

        public String UrlHandle { get; set; }

        public DateTime PublishDate { get; set; }

        public String Author { get; set; }

        public bool IsVisible { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
