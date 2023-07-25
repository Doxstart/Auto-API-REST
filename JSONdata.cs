using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Auto_API_REST;
using System.Net.Http.Json;

namespace JSONdata
{
    public class CarDealer
    {
        public int DealerId { get; set; }
        public string DealerName { get; set; }
        public List<Car> ListofCars { get; set; }

        public CarDealer(int dealerId, string dealerName, List<Car> listofcars)
        {
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

    public class Program
    {
        public static void Main()
        {
            List<Car> myCars = new List<Car>();
            List<Car> XCars = new List<Car>();

            myCars.Add(new Car(1, "AB001DZ", "Supra", "Toyota", 350, 200, 250, 55));
            myCars.Add(new Car(2, "AB002DZ", "Renegade", "Jeep", 370, 250, 320, 60));
            myCars.Add(new Car(3, "AB003DZ", "Cavallier", "Bugatti", 310, 300, 450, 100)); 
            XCars.Add(new Car(3, "AB004DZ", "Forte", "Maseratti", 390, 310, 550, 120));
            XCars.Add(new Car(4, "AB005DZ", "Patriot", "Ford", 400, 230, 250, 40));
            XCars.Add(new Car(5, "AB006DZ", "Gallardo", "Lamborghini", 380, 400, 550, 110));

            List<CarDealer> carDealers = new List<CarDealer>();

            carDealers.Add(new CarDealer(1,"SuperMacchine", myCars));
            carDealers.Add(new CarDealer(2, "MonsterCars", XCars));

            string json = JsonSerializer.Serialize(carDealers);
            File.WriteAllText(@"C:\JSONdata.json", json);
            Console.WriteLine(json);
        }
    }

}
