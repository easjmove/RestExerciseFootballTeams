using FootballTeamLib;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestExercise1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FootballTeamsController : ControllerBase
    {
        private FootballRepo _repository;

        public FootballTeamsController(FootballRepo repo)
        {
            _repository = repo;
        }

        // GET: api/<FootballTeamsController>
        [HttpGet]
        public IEnumerable<FootballTeam> Get()
        {
            return _repository.GetAllTeams();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<FootballTeam> Get(int id)
        {
            FootballTeam? team = _repository.GetTeamById(id);
            if (team == null)
            {
                return NotFound();
            }
            return Ok(team);
        }

        // POST api/<FootballTeamsController>
        [HttpPost]
        public FootballTeam Post([FromBody] FootballTeam newTeam)
        {
            return _repository.AddTeam(newTeam);
        }


        [HttpPut("{id}")]
        public FootballTeam Put(int id, [FromBody] FootballTeam value)
        {
            return _repository.UpdateTeam(id, value);
        }

        // DELETE api/<FootballTeamsController>/5
        [HttpDelete("{id}")]
        public FootballTeam Delete(int id)
        {
            return _repository.DeleteTeam(id);
        }
    }
}
