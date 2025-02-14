﻿using Models;
using Web_API.ResponseModel;

namespace Service;

public interface IMangaService
{
    Task<ApiResponse<MangaResponse>> CreateMangaAsync(CreateMangaRequest request);
    Task<ApiResponse<AllMangaResponse>> GetAllMangaAsync();
    Task<ApiResponse<List<MangaResponse>>> GetAllApproveAsync();
    Task<ApiResponse<List<MangaResponse>>> GetAllMangaByUserAsync(String userId);
    Task<ApiResponse<MangaResponse>> GetMangaByIdAsync(String mangaId);
    Task<ApiResponse<Boolean>> DeleteMangaAsync(String mangaId);
    Task<ApiResponse<MangaResponse>> UpdateMangaAsync(UpdateMangaRequest request);
    Task<ApiResponse<MangaResponse>> ApproveMangaAsync(String mangaId, string modifiedBy);
    Task<ApiResponse<MangaResponse>> GetAllMangaByAuthorAsync(String authorId);
}
