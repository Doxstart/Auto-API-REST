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

        // GET: api/CarDealer
        [HttpGet]
        public ActionResult<IEnumerable<CarDealer>> Get()
        {
            List<Car> myCars = new List<Car>();
            List<Car> XCars = new List<Car>();  
            List<Car> ZCars = new List<Car>();  
            List<Car> NCars = new List<Car>();
            List<Car> SCars = new List<Car>();

            List<CarDealer> carDealers = new List<CarDealer>();

            myCars.Add(new Car(1, "AB001DZ", "Supra", "Toyota", 350, 200, 250, 55));
            XCars.Add(new Car(2, "AB002DZ", "Renegade", "Jeep", 370, 250, 320, 60));
            ZCars.Add(new Car(3, "AB003DZ", "Cavallier", "Bugatti", 310, 300, 450, 100));
            NCars.Add(new Car(4, "AB004DZ", "Patriot", "Ford", 400, 230, 250, 40));
            SCars.Add(new Car(5, "AB005DZ", "Gallardo", "Lamborghini", 380, 400, 550, 110));

            CarDealer Concs1 = new CarDealer(1, myCars);
            CarDealer Concs2 = new CarDealer(2, XCars);
            CarDealer Concs3 = new CarDealer(3, ZCars);
            CarDealer Concs4 = new CarDealer(4, NCars);
            CarDealer Concs5 = new CarDealer(5, SCars);

            carDealers.Add(Concs1);
            carDealers.Add(Concs2);
            carDealers.Add(Concs3);
            carDealers.Add(Concs4);
            carDealers.Add(Concs5); 

            return carDealers;
        }

        // GET api/CarDealer/5
        [HttpGet("{id}")]
        public ActionResult<CarDealer> Get(int id)
        {
            var carDealer = carDealers.FirstOrDefault(d => d.DealerId == id);
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
            return CreatedAtAction(nameof(Get), new { id = carDealer.DealerId }, carDealer);
        }

        // PUT api/<ConcessionarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/CarDealer/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            foreach (var carDealer in carDealers)
            {
                var carToRemove = carDealer.ListofCars?.FirstOrDefault(car => car.Id == id);

                if (carToRemove != null)
                {
                    carDealer.ListofCars?.Remove(carToRemove);
                    return NoContent();
                }
            }
            return NotFound();
        }
    }
}
