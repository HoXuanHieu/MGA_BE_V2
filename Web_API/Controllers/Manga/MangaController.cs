using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service;

namespace Web_API.Controllers.Manga
{
    [Route("[controller]")]
    [ApiController]
    public class MangaController : ControllerBase
    {
        private readonly IMangaService _service;
        public MangaController(IMangaService service)
        {
            _service = service;
        }

        // add pagination
        [HttpGet]
        [Route("getall")]
        public async Task<List<MangaResponse>> getAllManga()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("getbyid/{mangaId}")]
        public async Task<IActionResult> getMangaInfomationById(String mangaId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateManga(CreateMangaRequest request)
        {
            var response = await _service.CreateMangaAsync(request);
            return StatusCode(response.Status, response);
        }
    }
}
