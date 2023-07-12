using Microsoft.AspNetCore.Mvc;
using Auto_API_REST;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Auto_API_REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcessionarioController : ControllerBase
    {
        private static List<CarDealer> carDealers = new List<CarDealer>();

        // GET: api/CarDealer
        [HttpGet]
        public List<Car> Get()
        {
            List<Car> Supra = new List<Car>();
            Supra.Add(new Car(0001, "Toyota", 350, 200, 250, 55));

            return Supra;
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
