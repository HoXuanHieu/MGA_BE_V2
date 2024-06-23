using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class UserEntity
    {
        [Key]
        public Guid UserId { get; set; } = Guid.NewGuid();
        [Required]
        [StringLength(100)]
        public String UserName { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        [Required]
        [RegularExpression(@"^(admin|reader|poster)$")]
        public String Role { get; set; }

        [Required]
        public DateTime DateCreate { get; set; } = DateTime.Now;

        [Required]
        public DateTime DateUpdate { get; set; } = DateTime.Now;

        [Required]
        public Boolean IsSuspension { get; set; } = false;

        [Required]
        public Boolean IsVerify { get; set; } = false;

        [Required]
        public Boolean IsDelete { get; set; } = false;

        //------------------
        public List<MangaEntity> MangaPost { get; set; }
    }
}
