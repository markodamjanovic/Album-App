
namespace AlbumApp.Models
{
    public class Album
    {
        public int Id {get; set;}
        public int UserId { get; set; }
        public string PhotoPath { get; set; }
        public string Description { get; set; }
    }
}