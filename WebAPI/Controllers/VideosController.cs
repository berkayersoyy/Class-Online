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
    public class VideosController : ControllerBase
    {
        private IVideoService _videoService;

        public VideosController(IVideoService videoService)
        {
            _videoService = videoService;
        }
        [HttpGet("getall")]
        public IActionResult GetList()
        {
            var result = _videoService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int videoId)
        {
            var result = _videoService.Get(videoId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("add")]
        public IActionResult Add([FromBody]Video video)
        {
            var result = _videoService.Add(video);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete([FromBody] Video video)
        {
            var result = _videoService.Delete(video);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody] Video video)
        {
            var result = _videoService.Update(video);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
