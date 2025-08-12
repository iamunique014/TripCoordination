namespace TripCoordination.ViewModel
{
    public class TripFlatRow
    {
        public int TripID { get; set; }
        public string CreatorName { get; set; } // Organizer first name
        public string CreatorSurname { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public DateTime DepartureDate { get; set; }
        public bool IsFull { get; set; }
        public int Seats { get; set; }
        public int? DestinationID { get; set; }
        public string DestinationName { get; set; }
        public int TripDestinationTownID { get; set; }
    }
}
