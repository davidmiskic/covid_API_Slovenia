namespace CovidAPI.Models
{
    public class DayCase
    {
        public string Id { get; set; }
        public string Region { get; set; }
        public string ActiveDay { get; set; }
        public string Vac1st { get; set; }
        public string Vac2nd { get; set; }
        public string Deceased { get; set; }
    }
}