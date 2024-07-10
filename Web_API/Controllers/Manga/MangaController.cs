using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service;

namespace Web_API.Controllers.Manga
{
    [Route("api/[controller]")]
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
        public async Task<List<MangaResponse>> getAllManga()
        {
            throw new NotImplementedException();
        }

        [HttpGet("/{mangaId}")]
        public async Task<IActionResult> getMangaInfomationById(String mangaId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> CreateManga()
        {
            throw new NotImplementedException();
        }
    }
}
