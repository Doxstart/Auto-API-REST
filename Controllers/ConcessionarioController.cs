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

        private List<CarDealer> GenCarDealers()
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

            return carDealers;
        }

        // GET: api/CarDealer
        [HttpGet]
        public ActionResult<IEnumerable<CarDealer>> Get() => GenCarDealers();

        // GET api/CarDealer/5
        [HttpGet("{dealerId}")]
        public ActionResult<CarDealer> Get(int dealerId)
        {
            List<CarDealer> carDealers = GenCarDealers();
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
            List<CarDealer> carDealers = GenCarDealers();
            carDealers.Add(carDealer);
            return CreatedAtAction(nameof(Get), new { id = carDealer.DealerId }, carDealer);
        }

        // PUT api/<ConcessionarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/CarDealer/5
        [HttpDelete("{dealerId}")]
        public ActionResult Delete(int dealerId)
        {
            //List<CarDealer> carDealers = GenCarDealers();
            //foreach (var carDealer in carDealers)
            //{
            //    var carToRemove = carDealer.ListofCars?.FirstOrDefault(car => car.Id == id);

            //    if (carToRemove != null)
            //    {
            //        carDealer.ListofCars?.Remove(carToRemove);
            //        return NoContent();
            //    }
            //}
            //return NotFound();
            List<CarDealer> carDealers = GenCarDealers();

            CarDealer dealer = carDealers.FirstOrDefault(d => d.DealerId == dealerId);

            if (dealer != null)
            {
                carDealers.Remove(dealer);
                return Ok();
            }

            return NotFound();
        }
    }
}
