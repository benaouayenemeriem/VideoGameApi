using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGameApi.Data;

namespace VideoGameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
        private readonly VideoGameDbContext _context;

        public VideoGameController(VideoGameDbContext context)
        {
            _context = context;
        }

        [HttpGet] 
        public async Task<ActionResult<List<VideoGame>>> GetVideoGames()
        {
            return Ok(await _context.VideoGames.ToListAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<VideoGame>> GetVideoGameById(int id)
        {
            var videoGame = await _context.VideoGames.FindAsync(id);
            if (videoGame is null)
            {
                return NotFound();
            }
            return Ok(videoGame);
        }

        [HttpPost]
        public async Task<ActionResult<VideoGame>> AddVideoGame(VideoGame videoGame)
        {
            if (videoGame is null)
                return BadRequest();
            _context.VideoGames.Add(videoGame);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVideoGameById),
                new { id = videoGame.Id }, videoGame
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVideoGame(int id, VideoGame newVideoGame)
        {
            var videoGame = await _context.VideoGames.FindAsync(id);
            if (videoGame is null)
            {
                return NotFound();
            }
            videoGame.Title = newVideoGame.Title;
            videoGame.Publisher = newVideoGame.Publisher;
            videoGame.Developer = newVideoGame.Developer;
            videoGame.Platform = newVideoGame.Platform;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete ("{id}")]
        private async Task<IActionResult> DeleteVideoGame(int id)
        {
            var videoGame = await _context.VideoGames.FindAsync(id);
            if (videoGame is null)
            {
                return NotFound();
            }
            _context.VideoGames.Remove(videoGame);  
            await _context.SaveChangesAsync();  
            return NoContent();
        }

    }
}
