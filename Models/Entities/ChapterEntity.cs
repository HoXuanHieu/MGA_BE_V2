using System.ComponentModel.DataAnnotations;
using System;

namespace Models.Entities
{
    public class ChapterEntity
    {
        [Key]
        public String ChapterId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(100)]
        public String ChapterName { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public String LastActivity { get; set; } = "";

        [Required]
        public String MangaId { get; set; }

        public MangaEntity manga { get; set; }

        public List<ChapterImageEntity> ChapterImage { get; set; }

    }
}
