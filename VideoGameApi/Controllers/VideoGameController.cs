using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VideoGameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
        static private List<VideoGame> videoGames = new List<VideoGame>()
        {
            new VideoGame
            {
                Id = 1,
                Title = "Spider-Man 2",
                Platform = "PS5",
                Developer = "Insomniac Games",
                Publisher = "Sony Interactive Entertainement"
            },
            new VideoGame
            {
                Id = 2,
                Title = "Zelda",
                Platform = "Nintendo Switch",
                Developer = "Nintendo EPD",
                Publisher = "Nintendo"
            },
            new VideoGame
            {
                Id = 3,
                Title = "Cyberpunk 2077",
                Platform = "PC",
                Developer = "CD Project Red",
                Publisher = "CD Project"
            },
        };
        [HttpGet] 
        public ActionResult<List<VideoGame>> GetVideoGames()
        {
            return Ok(videoGames);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<VideoGame> GetVideoGameById(int id) {
            var videoGame = videoGames.FirstOrDefault(v => v.Id == id);
            if (videoGame is null)
            {
                return NotFound();
            }
            return Ok(videoGame);
        }

        [HttpPost]
        public ActionResult<VideoGame> AddVideoGame(VideoGame videoGame) {
            if (videoGame is null)
                return BadRequest();
            videoGame.Id = videoGames.Max(v=> v.Id) + 1;
            videoGames.Add(videoGame);
            return CreatedAtAction(nameof(GetVideoGameById) ,
                new { id = videoGame.Id} , videoGame
            );
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVideoGame(int id, VideoGame newVideoGame)
        {
            var videoGame = videoGames.FirstOrDefault(v => v.Id == id);
            if (videoGame is null)
            {
                return NotFound();
            }
            videoGame.Title = newVideoGame.Title;
            videoGame.Publisher = newVideoGame.Publisher;
            videoGame.Developer  = newVideoGame.Developer;
            videoGame.Platform = newVideoGame.Platform;
            return NoContent();
        }

        [HttpDelete("{id}")]
        private IActionResult DeleteVideoGame(int id)
        {
            var videoGame = videoGames.FirstOrDefault(videoGame => videoGame.Id == id);
            if (videoGame is null)
            {
                return NotFound();
            }
            videoGames.Remove(videoGame);
            return NoContent();
        }

    }
}
