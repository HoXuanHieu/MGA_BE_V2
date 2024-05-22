using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSuspension = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsVerify = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mangas",
                columns: table => new
                {
                    MangaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MangaName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MangaImage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 5, 22, 16, 31, 4, 327, DateTimeKind.Local).AddTicks(911)),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsApproval = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PostedBy = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mangas", x => x.MangaId);
                    table.ForeignKey(
                        name: "FK_Mangas_Users_PostedBy",
                        column: x => x.PostedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chapters",
                columns: table => new
                {
                    ChapterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChapterName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 5, 22, 16, 31, 4, 327, DateTimeKind.Local).AddTicks(9440)),
                    ChapterImage = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    MangaId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapters", x => x.ChapterId);
                    table.ForeignKey(
                        name: "FK_Chapters_Mangas_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Mangas",
                        principalColumn: "MangaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_MangaId",
                table: "Chapters",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mangas_PostedBy",
                table: "Mangas",
                column: "PostedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chapters");

            migrationBuilder.DropTable(
                name: "Mangas");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
