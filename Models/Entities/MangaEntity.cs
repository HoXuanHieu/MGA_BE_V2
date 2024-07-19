using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class MangaEntity
    {
        [Key]
        public String MangaId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(100)]
        public String MangaName { get; set; }

        [Required]
        [StringLength(100)]
        public String MangaImage { get; set; }

        [Required]
        [StringLength(500)]
        public String Description { get; set; }

        [Required]
        public String Categories { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Required]
        public DateTime DateUpdated { get; set; } = DateTime.Now;

        public Boolean IsApproval { get; set; } = false;

        public Boolean IsDelete { get; set; } = false;

        public String LastActivity { get; set; } = "";

        [Required]
        public String PostedBy { get; set; }
        [Required]
        public String AuthorId { get; set; }


        //relationship
        public AuthorEntity Author { get; set; }
        public UserEntity User { get; set; }
        public List<ChapterEntity> Chapters { get; set; }

    }
}
