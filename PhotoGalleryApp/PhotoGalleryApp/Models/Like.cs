namespace PhotoGalleryApp.Models
{
    public class Like
    {
        public int Id { get; set; } 
        public string UserId { get; set; }
        public User User { get; set; }

        public int PhotoId { get; set; }
        public Photo Photo { get; set; }
    }
}
