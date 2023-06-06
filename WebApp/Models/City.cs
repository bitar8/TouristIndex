﻿
namespace WebApp.Models
{
    public class City
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public Country? Country { get; set; }
        public List<TourismActivity> TourismActivities { get; set; } = new();
    }
}
