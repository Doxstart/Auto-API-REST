using Microsoft.AspNetCore.Mvc;
using Auto_API_REST;
using Microsoft.OpenApi.Any;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Auto_API_REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcessionarioController : ControllerBase
    {
        public static List<CarDealer> carDealers = new List<CarDealer>();
        private static string filePath = "data.txt";

        private static void ReadRecords()
        {
            if (System.IO.File.Exists(filePath))
            {
                string[] lines = System.IO.File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] recordData = line.Split(";");
                    int dealerId = Convert.ToInt32(recordData[0]);
                    string dealerName = recordData[1];
                    List<Car> listofCars = new List<Car>();

                    for (int i = 4; i < recordData.Length; i += 8)
                    {
                        int id = Convert.ToInt32(recordData[i]);
                        string plate = recordData[i + 1];
                        string name = recordData[i + 2];
                        string brand = recordData[i + 3];
                        double weight = Convert.ToDouble(recordData[i + 4]);
                        double speed = Convert.ToDouble(recordData[i + 5]);
                        double maxSpeed = Convert.ToDouble(recordData[i + 6]);
                        double displacement = Convert.ToDouble(recordData[i + 7]);

                        Car car = new Car(id, plate, name, brand, weight, speed, maxSpeed, displacement);

                        listofCars.Add(car);
                    }

                    CarDealer carDealer = new CarDealer(dealerId, dealerName, listofCars);

                    carDealers.Add(carDealer);
                }
            }
        }

        private static void WriteRecords()
        {
            List<string> lines = new List<string>();
            foreach (CarDealer carDealer in carDealers)
            {
                string recordData = $"{carDealer.DealerId};{carDealer.DealerName}";
                foreach (Car car in carDealer.ListofCars)
                {
                    recordData += $";{car.Id};{car.Plate};{car.Name};{car.Brand};{car.Speed};{car.MaxSpeed};{car.Weight}{car.Displacement}";
                }

                lines.Add(recordData);
            }
            System.IO.File.WriteAllLines(filePath, lines);
        }

        // GET: api/CarDealer
        [HttpGet]
        public ActionResult<IEnumerable<CarDealer>> Get()
        {
            carDealers = new List<CarDealer>();
            //CODICE PER READ FILE
            ReadRecords();
            //FINE CODICE READ FILE
            return carDealers;
        }    
            

        // GET api/CarDealer/5
        [HttpGet("{dealerId}")]
        public ActionResult<CarDealer> Get(int dealerId)
        {
            var carDealer = carDealers.FirstOrDefault(d => d.DealerId == dealerId);
            if (carDealer == null)
            {
                return NotFound();
            }
            return carDealer;
        }

        // POST api/CarDealer
        [HttpPost]
        public ActionResult<CarDealer> Post(CarDealer carDealer)
        {
            carDealers.Add(carDealer);
            WriteRecords();
            return CreatedAtAction(nameof(Get), new { id = carDealer.DealerId }, carDealer);
        }

        [HttpPost("{dealerId}")]
        public IActionResult CreateCar(int id, [FromBody] Car car)
        {
            var carDealer = carDealers.FirstOrDefault(c => c.DealerId == id);
            if (carDealer == null)
            {
                return BadRequest();
            }

            carDealer.ListofCars.Add(car);
            WriteRecords();
            return Ok("Car Added!");
        }

        [HttpPut]
        public ActionResult<CarDealer> EditDealer(int id, [FromBody] CarDealer updatedCarDealer)
        {
            var carDealer = carDealers.FirstOrDefault(d => d.DealerId == id);

            if (carDealer == null) throw new Exception("CarDealer doesn't exist");

            carDealer.DealerId = updatedCarDealer.DealerId;
            carDealer.DealerName = updatedCarDealer.DealerName;

            WriteRecords();

            return Ok("CarDealer Successfully edited!");
        }

        // PUT api/<ConcessionarioController>/5
        [HttpPut("{CarId}")]
        public IActionResult EditCar(int CarId, [FromBody] Car updatedCar)
        {
            bool carFound = false;

            foreach (var carDealer in carDealers)
            {
                var carToUpdate = carDealer.ListofCars.FirstOrDefault(car => car.Id == CarId);

                if (carToUpdate != null)
                {
                    carToUpdate.Id = updatedCar.Id;
                    carToUpdate.Plate = updatedCar.Plate;
                    carToUpdate.Name = updatedCar.Name;
                    carToUpdate.Brand = updatedCar.Brand;
                    carToUpdate.Speed = updatedCar.Speed;
                    carToUpdate.MaxSpeed = updatedCar.MaxSpeed;
                    carToUpdate.Weight = updatedCar.Weight;
                    carToUpdate.Displacement = updatedCar.Displacement;

                    carFound = true;
                    break;
                }

            }

                if (!carFound)
                {
                    throw new Exception("Id doesn't exist");
                }

                WriteRecords();
                    
            return Ok();
        }

        // DELETE api/CarDealer/5
        [HttpDelete("{CarId}")]
        public ActionResult Delete(int CarId)
        {
            foreach (var carDealer in carDealers)
            {
                var carToRemove = carDealer.ListofCars?.FirstOrDefault(car => car.Id == CarId);

                if (carToRemove != null)
                {
                    carDealer.ListofCars?.Remove(carToRemove);
                    WriteRecords();
                    return NoContent();
                }
            }
            return Ok("Car successfully deleted!");
        }
    }
}
