namespace Auto_API_REST
{
    public class CarDealer
    {

        public int DealerId { get; set; }
        public List<Car>? ListofCars { get; set; }

    }

    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public double Weight { get; set; }
        public double Speed { get; set; }
        public double MaxSpeed { get; set; }
        public int Displacement { get; set; }

        public Car(int id, string brand, double weight, double speed, double maxSpeed, int displacement)
        {
            Id = id;
            Brand = brand;
            Weight = weight;
            Speed = speed;
            MaxSpeed = maxSpeed;
            Displacement = displacement;
        }

    }

}
