namespace BlueLight.NoteItem.Models.Relationship
{
    public class Blog
    {
        public Blog()
        {
            this.Posts = new List<Post>();
        }
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; set; }
    }
}
