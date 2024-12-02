using Microsoft.AspNetCore.Mvc;
using MoviesRepositoryLib;

namespace RESTmovies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private IMoviesRepository _moviesRepository;

        public MoviesController(IMoviesRepository repo)
        {
            _moviesRepository = repo;
        }

        // GET: api/<MoviesController>
        [HttpGet]
        public IEnumerable<Movie> Get([FromQuery] int? yearAfter, [FromQuery] string? titleIncludes, [FromQuery] string? orderBy)
        {
            return _moviesRepository.Get(yearAfter, titleIncludes, orderBy);
        }

        // GET api/<MoviesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Movie> Get(int id)
        {
            Movie? movie = _moviesRepository.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(movie);
            }
        }

        // POST api/<MoviesController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Movie> Post([FromBody] Movie mov)
        {
            Movie movie = _moviesRepository.Add(mov);
            return Created(
                Url.ActionContext.HttpContext.Request.Path + "/" + movie.Id,
                movie);
        }

        // PUT api/<MoviesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Movie> Put(int id, [FromBody] Movie value)
        {
            Movie? movie = _moviesRepository.Update(id, value);
            if (movie == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(movie);
            }
        }

        // DELETE api/<MoviesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Movie> Delete(int id)
        {
            Movie? movie = _moviesRepository.Remove(id);
            if (movie == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(movie);
            }
        }
    }
}
