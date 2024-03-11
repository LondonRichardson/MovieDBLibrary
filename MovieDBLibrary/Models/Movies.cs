namespace MovieDBLibrary.Models
{
    public class Movies
    {
        public int MovieID { get; set; }
        public string Title { get; set; }
        public int YearReleased { get; set; }
        public string Director { get; set; }
        public string LeadActor { get; set; }
        public string Cast { get; set; }
        public string GrossRevenue { get; set; }
        public string MaturityRating { get; set; }
        public string ImageUrl { get; set; }

        public Movies()
        {

        }
    }
}
