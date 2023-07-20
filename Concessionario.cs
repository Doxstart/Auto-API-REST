namespace Auto_API_REST
{
    public class CarDealer
    {

        public int DealerId { get; set; }
        public string DealerName { get; set; }
        public List<Car> ListofCars { get; set; }
        public CarDealer(int dealerId, string dealerName, List<Car> listofcars) {
            DealerId = dealerId;
            DealerName = dealerName;
            ListofCars = listofcars;
        }

    }

    public class Car
    {
        public int Id { get; set; }
        public string Plate { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public double Weight { get; set; }
        public double Speed { get; set; }
        public double MaxSpeed { get; set; }
        public int Displacement { get; set; }

        public Car(int id, string plate, string name, string brand, double weight, double speed, double maxSpeed, int displacement)
        {
            Id = id;
            Plate = plate;
            Name = name;
            Brand = brand;
            Weight = weight;
            Speed = speed;
            MaxSpeed = maxSpeed;
            Displacement = displacement;
        }

    }

}
