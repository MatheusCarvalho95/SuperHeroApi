using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new()
        {
        };
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {


          return  Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetOne(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero not found");
            }
            return Ok(hero);
        }

        [HttpPost]
        public async  Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SuperHero>> UpdateHero(int id, SuperHero EditRequest
            )
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero not found");
            }

            hero.Name = EditRequest.Name;
            hero.FirstName = EditRequest.FirstName;
            hero.LastName = EditRequest.LastName;
            hero.Place = EditRequest.Place;
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.FindAsync(id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero not found");
            }
            _context.Remove(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.FindAsync(id));

        }
    }
}
