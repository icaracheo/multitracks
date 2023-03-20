using api.multitracks.com.Interfaces;
using api.multitracks.com.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace api.multitracks.com.Controllers
{
    [ApiController]
    public class ArtistDetailController : ControllerBase
    {
        private readonly IMultitracksProvider multitracksProvider;

        public ArtistDetailController(IMultitracksProvider multitracksProvider)
        {
            this.multitracksProvider = multitracksProvider;
        }

        [HttpGet]
        [Route("/artist/search")]
        public async Task<ActionResult> GetArtistByName([FromQuery] string artistName)
        {
            var result = await multitracksProvider.SearchAsync(artistName);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("/song/list")]
        public async Task<ActionResult> GetAllSongs([FromHeader] PagingParams parameters)
        {
            ICollection<Song> results;
            if (parameters.Items == 0 || parameters.Page == 0)
                results = await multitracksProvider.GetAll();
            else 
                results = await multitracksProvider.ListAsync(parameters);

            if (results != null)
            {
                Response.Headers.Add("X-Items", parameters.Items.ToString());
                Response.Headers.Add("X-Page", parameters.Page.ToString());
                Response.Headers.Add("X-Order", parameters.Order);
                var countSongs = await multitracksProvider.CountSongsAsync();
                Response.Headers.Add("X-Total-Items", countSongs.ToString());
                return Ok(results);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("/artist/add")]
        public async Task<ActionResult> AddArtist(Artist artist) 
        {
            var result = await multitracksProvider.AddAsync(artist);
            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
