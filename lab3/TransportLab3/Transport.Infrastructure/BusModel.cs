namespace Transport.Infrastructure.Models
{
    public class BusModel
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public int Capacity { get; set; }

        // Зв'язок один-до-одного з DriverModel
        public int DriverId { get; set; }
        public DriverModel Driver { get; set; } = null!;

        // Зв'язок один-до-багатьох з RouteModel
        public ICollection<RouteModel> Routes { get; set; } = new List<RouteModel>();
    }
}
