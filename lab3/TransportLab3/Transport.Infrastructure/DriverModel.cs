namespace Transport.Infrastructure.Models
{
    public class DriverModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;

        // Зв'язок один-до-одного
        public BusModel Bus { get; set; } = null!;
    }
}
