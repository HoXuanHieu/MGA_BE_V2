using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service;

namespace Web_API.Controllers.Chapter
{
    [Route("[controller]")]
    [ApiController]
    public class ChapterController : ControllerBase
    {
        private readonly IChapterService _service;
        public ChapterController(IChapterService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "poster")]
        public async Task<IActionResult> CreateChapterAsync(CreateChapterRequest request)
        {
            var response = await _service.CreateChapterAsync(request);
            return StatusCode(response.Status, response);
        }

        [HttpDelete]
        [Route("delete/{chapterId}")]
        [Authorize(Roles = "admin,poster")]
        public async Task<IActionResult> DeleteChapterAsync(String chapterId)
        {
            var response = await _service.RemoveChapterAsync(chapterId);
            return StatusCode(response.Status, response);
        }

        [HttpGet]
        [Route("getbyid/{chapterId}")]
        public async Task<IActionResult> GetChapterByIdAsync(String chapterId)
        {
            var response = await _service.GetChapterDetailAsync(chapterId);
            return StatusCode(response.Status, response);
        }

        [HttpGet]
        [Route("getall/{mangaId}")]
        public async Task<IActionResult> GetAllChapterByMangaIdAsync(String mangaId)
        {
            var response = await _service.GetAllChapterByMangaIdAsync(mangaId);
            return StatusCode(response.Status, response);
        }
    }
}
