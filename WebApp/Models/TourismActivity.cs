
using Microsoft.AspNetCore.Identity;

namespace WebApp.Models
{
    public class TourismActivity
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string? UserId { get; set; }
        public ActivityType ActivityType { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public City? City { get; set; }
        public IdentityUser? User { get; set; }
    }
}
