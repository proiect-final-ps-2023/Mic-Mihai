using Microsoft.AspNetCore.Identity;
using PhotoGalleryApp.Models;

public class User : IdentityUser
{
    public virtual ICollection<Album> Albums { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ICollection<Like> Likes { get; set; }
}
