using System;

namespace Library.Common
{
    public class Bus
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public double Mileage { get; set; }

        public static Bus CreateNew()
        {
            var rand = new Random();
            return new Bus
            {
                Id = Guid.NewGuid(),
                Model = "Model_" + rand.Next(1, 100),
                Mileage = rand.Next(1000, 100000)
            };
        }
    }
}
