using System;

namespace BusStation.REST.Models
{
    public class BusModel
    {
        public Guid Id { get; set; }
        public string NumberPlate { get; set; }
        public string Route { get; set; }
        public int Capacity { get; set; }
    }
}
