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

        //private List<CarDealer> GenCarDealers()
        //{
            //List<Car> myCars = new List<Car>();
            //List<Car> XCars = new List<Car>();

            //myCars.Add(new Car(1, "AB001DZ", "Supra", "Toyota", 350, 200, 250, 55));
            //myCars.Add(new Car(2, "AB002DZ", "Renegade", "Jeep", 370, 250, 320, 60));
            //myCars.Add(new Car(3, "AB003DZ", "Cavallier", "Bugatti", 310, 300, 450, 100)); 
            //XCars.Add(new Car(3, "AB004DZ", "Forte", "Maseratti", 390, 310, 550, 120));
            //XCars.Add(new Car(4, "AB005DZ", "Patriot", "Ford", 400, 230, 250, 40));
            //XCars.Add(new Car(5, "AB006DZ", "Gallardo", "Lamborghini", 380, 400, 550, 110));

            //List<CarDealer> carDealers = new List<CarDealer>();

            //carDealers.Add(new CarDealer(1,"SuperMacchine", myCars));
            //carDealers.Add(new CarDealer(2, "MonsterCars", XCars));

        //    return carDealers;
        //}

        // GET: api/CarDealer
        [HttpGet]
        public ActionResult<IEnumerable<CarDealer>> Get() => carDealers;

        // GET api/CarDealer/5
        [HttpGet("{dealerId}")]
        public ActionResult<CarDealer> Get(int dealerId)
        {
            CarDealer carDealer = carDealers.FirstOrDefault(d => d.DealerId == dealerId);
            if (carDealer == null)
            {
                return NotFound();
            }
            return Ok(carDealer);
        }

        // POST api/CarDealer
        [HttpPost]
        public ActionResult<CarDealer> Post(CarDealer carDealer)
        {
            carDealers.Add(carDealer);
            return CreatedAtAction(nameof(Get), new { id = carDealer.DealerId }, carDealer);
        }

        [HttpPost("{dealerId}")]
        public string CreateCar(int id, [FromBody] Car car)
        {
            var carDealer = carDealers.FirstOrDefault(c => c.DealerId == id);
            if (carDealer == null)
            {
                return "Not Found!";
            }

            carDealer.ListofCars.Add(car);

            return "Car Added!";
        }

        [HttpPut]
        public ActionResult<CarDealer> Put(int id, CarDealer updatedCarDealer)
        {
            var carDealer = carDealers.FirstOrDefault(d => d.DealerId == id);
            if (carDealer == null)
            {
                return NotFound();
            }

            carDealer.DealerId = updatedCarDealer.DealerId;
            carDealer.DealerName = updatedCarDealer.DealerName;

            return carDealer;
        }

        // PUT api/<ConcessionarioController>/5
        [HttpPut("{CarId}")]
        public IActionResult EditCar(int CarId, [FromBody] Car updatedCar)
        {
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
                    carToUpdate.Displacement = updatedCar.Displacement;
                    return Ok("Car attributes updated successfully.");
                }
            }
            return NotFound("Car not found.");
        }

        // DELETE api/CarDealer/5
        [HttpDelete("{CarId}")]
        public ActionResult DeleteCarById(int CarId)
        {
            foreach (var carDealer in carDealers)
            {
                var carToRemove = carDealer.ListofCars?.FirstOrDefault(car => car.Id == CarId);

                if (carToRemove != null)
                {
                    carDealer.ListofCars?.Remove(carToRemove);
                    return NoContent();
                }
            }
            return Ok();
        }
    }
}
