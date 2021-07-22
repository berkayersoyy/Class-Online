using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private IPlaylistService _playlistService;

        public PlaylistsController(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }

        [HttpGet("getall")]
        public IActionResult GetList()
        {
            var result = _playlistService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int playlistId)
        {
            var result = _playlistService.Get(playlistId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] Playlist playlist)
        {
            var result = _playlistService.Add(playlist);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete([FromBody] Playlist playlist)
        {
            var result = _playlistService.Delete(playlist);
            if (result.Success)
            {
                return Ok(result);  
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody] Playlist playlist)
        {
            var result = _playlistService.Update(playlist);
            if (result.Success)
            {
                return Ok(result);  
            }

            return BadRequest(result);
        }
    }
}
