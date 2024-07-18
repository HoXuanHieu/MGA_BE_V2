using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service;
using Web_API.ResponseModel;

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
        public async Task<IActionResult> GetAllMangaAsync()
        {
            var response = await _service.GetAllMangaAsync();
            return StatusCode(response.Status, response);
        }
        [HttpGet]
        [Route("getallapprove")]
        public async Task<IActionResult> GetAllApprovalMangaAsync()
        {
            var response = await _service.GetAllApproveAsync();
            return StatusCode(response.Status, response);
        }

        [HttpGet]
        [Route("getbyid/{mangaId}")]
        public async Task<IActionResult> GetMangaInfomationByIdAsync(String mangaId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateMangaAsync(CreateMangaRequest request)
        {
            var response = await _service.CreateMangaAsync(request);
            return StatusCode(response.Status, response);
        }

        [HttpDelete]
        [Route("delete/{mangaId}")]
        public async Task<IActionResult> DeleteMangaAsync(String mangaId)
        {
            var response = await _service.DeleteMangaAsync(mangaId);
            return StatusCode(response.Status, response);
        }
    }
}
