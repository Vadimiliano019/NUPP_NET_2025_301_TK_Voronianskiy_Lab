namespace Transport.Infrastructure.Models
{
    public class RouteModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int BusId { get; set; }
        public BusModel Bus { get; set; } = null!;
    }
}
