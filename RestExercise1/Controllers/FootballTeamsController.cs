using FootballTeamLib;
using Microsoft.AspNetCore.Cors;
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
        public IEnumerable<FootballTeam> GetAll()
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<FootballTeam> Post([FromBody] FootballTeamDTO newTeam)
        {
            try
            {
                FootballTeam team = ConvertDTOToFootballTeam(newTeam);
                _repository.AddTeam(team);
                
                //return Created("/" + team.Id, team); 
                return CreatedAtAction(nameof(Get), new { id = team.Id }, team);

            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.ToString());
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpOptions]
        public ActionResult Options()
        {
            //Response.Headers.Add("Allow", "GET,POST,PUT,DELETE,OPTIONS");
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult<FootballTeam> Put(int id, [FromBody] FootballTeamDTO value)
        {
            try
            {
                FootballTeam team = ConvertDTOToFootballTeam(value);
                return _repository.UpdateTeam(id, team);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // DELETE api/<FootballTeamsController>/5
        [HttpDelete("{id}")]
        public FootballTeam Delete(int id)
        {
            return _repository.DeleteTeam(id);
        }

        private FootballTeam ConvertDTOToFootballTeam(FootballTeamDTO dto) {      
            if (dto.Year == null) throw new ArgumentOutOfRangeException("Year cannot be null");
            FootballTeam team = new FootballTeam() { League = dto.League, Name = dto.Name, Year = dto.Year.Value};
            return team;
        }
    }
}
