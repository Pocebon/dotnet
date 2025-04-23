using System.Collections.Generic;
using System.Linq;
using APICsv.DataBase;
using APICsv.DataBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace APICsv.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimaisControllers : ControllerBase
    {

        private readonly DBContext _dbContext;
        public AnimaisControllers(DBContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        [HttpGet]
        public ActionResult<List<Animal>> GetAll()
        {
            return Ok(_dbContext.Animals);
        }

        [HttpGet("{id}")]
        public ActionResult<Animal> GetById(int id)
        {
            try
              {
                Animal animal =
                                _dbContext
                                .Animals.Find(a => a.Id == id);

                if (animal == null)
                    return NotFound();

                return Ok(animal);
            }

            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteById(int id)
        {
            try
            {
                Animal animal = _dbContext
                                    .Animals.Find(a => a.Id == id);

                if (animal == null)
                    return NotFound();

                _dbContext.Animals.Remove(animal);
                return NoContent();
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

            [HttpPatch("AlterarNome")]

            public ActionResult<Animal> AlterarNome(
               [FromBody] Animal body)
            {
                if ((body == null) ||
                   (string.IsNullOrEmpty(body.Name)))
                    return BadRequest();

                Animal animal =
                    _dbContext
                    .Animals.Find(a => a.Id == body.Id);

                if (animal == null)
                    return NotFound();

                animal.Name = body.Name;
                return Ok(animal);
            }

        [HttpPost]
        public ActionResult<Animal> Post([FromBody] Animal novoAnimal)
        {
            if (novoAnimal == null || string.IsNullOrEmpty(novoAnimal.Name))
                return BadRequest("Dados inválidos.");

            int novoId = 1;
            if (_dbContext.Animals.Count > 0)
            {
                novoId = _dbContext.Animals.Max(a => a.Id) + 1;
            }
            novoAnimal.Id = novoId;

            _dbContext.Animals.Add(novoAnimal);
            return CreatedAtAction(nameof(GetById), new { id = novoAnimal.Id }, novoAnimal);
        }

        [HttpPut("{id}")]
        public ActionResult<Animal> Put(int id, [FromBody] Animal animalAtualizado)
        {
            if (animalAtualizado == null || id != animalAtualizado.Id)
                return BadRequest("Dados inconsistentes.");

            Animal animalExistente = _dbContext.Animals.Find(a => a.Id == id);

            if (animalExistente == null)
                return NotFound();
          
            animalExistente.Name = animalAtualizado.Name;

            return Ok(animalExistente);
        }

    }           
}
