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
        public ActionResult<IEnumerable<CarDealer>> Get() => carDealers;

        // GET api/CarDealer/5
        [HttpGet("{dealerId}")]
        public ActionResult<CarDealer> Get(int dealerId)
        {
            var carDealer = carDealers.FirstOrDefault(d => d.DealerId == dealerId);
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
        public IActionResult CreateCar(int id, [FromBody] Car car)
        {
            var carDealer = carDealers.FirstOrDefault(c => c.DealerId == id);
            if (carDealer == null)
            {
                return BadRequest();
            }

            carDealer.ListofCars.Add(car);

            return Ok("Car Added!");
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

                if (carToUpdate == null) throw new Exception("Id doesn't exist");

                    carToUpdate.Id = updatedCar.Id;
                    carToUpdate.Plate = updatedCar.Plate;
                    carToUpdate.Name = updatedCar.Name;
                    carToUpdate.Brand = updatedCar.Brand;
                    carToUpdate.Speed = updatedCar.Speed;
                    carToUpdate.MaxSpeed = updatedCar.MaxSpeed;
                    carToUpdate.Weight = updatedCar.Weight;
                    carToUpdate.Displacement = updatedCar.Displacement;

            }
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
                    return NoContent();
                }
            }
            return Ok();
        }
    }
}
