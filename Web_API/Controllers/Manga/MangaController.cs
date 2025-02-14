﻿using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "admin")]
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
            var result = await _service.GetMangaByIdAsync(mangaId);
            return StatusCode(result.Status, result);
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "poster")]
        public async Task<IActionResult> CreateMangaAsync(CreateMangaRequest request)
        {
            var response = await _service.CreateMangaAsync(request);
            return StatusCode(response.Status, response);
        }

        [HttpDelete]
        [Route("delete/{mangaId}")]
        [Authorize(Roles = "admin,poster")]
        public async Task<IActionResult> DeleteMangaAsync(String mangaId)
        {
            var response = await _service.DeleteMangaAsync(mangaId);
            return StatusCode(response.Status, response);
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Roles = "admin,poster")]
        public async Task<IActionResult> UpdateMangaAsync(UpdateMangaRequest request)
        {
            var response = await _service.UpdateMangaAsync(request);
            return StatusCode(response.Status, response);
        }

        [HttpPut]
        [Route("approve")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ApproveMangaAsync(String mangaId, String modifiedBy)
        {
            var response = await _service.ApproveMangaAsync(mangaId, modifiedBy);
            return StatusCode(response.Status, response);
        }

        [HttpGet]
        [Route("getbyposter/{userId}")]
        public async Task<IActionResult> GetMangaByPosterAsync(String userId)
        {
            var response = await _service.GetAllMangaByUserAsync(userId);
            return StatusCode(response.Status, response);
        }

        [HttpGet]
        [Route("getbyauthor/{authorId}")]
        public async Task<IActionResult> GetMangaByAuthorAsync(String authorId)
        {
            var response = await _service.GetAllMangaByAuthorAsync(authorId);
            return StatusCode(response.Status, response);
        }
    }
}
