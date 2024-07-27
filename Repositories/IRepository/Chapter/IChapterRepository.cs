﻿using Models;
using Models.Entities;

namespace Repositories;

public interface IChapterRepository
{
    Task<List<ChapterEntity>> GetAllChapterAsync();
    Task<ChapterEntity> CreateChapterAsync(ChapterEntity request);
    Task<Boolean> RemoveChapterAsync(String chapterId);
}
