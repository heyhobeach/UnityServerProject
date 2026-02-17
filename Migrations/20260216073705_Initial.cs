using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnityServerProject.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "userplaydata",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsNormal = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    StartTime = table.Column<long>(type: "bigint", nullable: false),
                    EndTime = table.Column<long>(type: "bigint", nullable: false),
                    Duration = table.Column<long>(type: "bigint", nullable: false),
                    DeadCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userplaydata", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PlayerChapterInfo",
                columns: table => new
                {
                    ChapterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PlayresultId = table.Column<int>(type: "int", nullable: false),
                    ChapterName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChapterDuration = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerChapterInfo", x => x.ChapterId);
                    table.ForeignKey(
                        name: "FK_PlayerChapterInfo_userplaydata_PlayresultId",
                        column: x => x.PlayresultId,
                        principalTable: "userplaydata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StageDeathInfo",
                columns: table => new
                {
                    StageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ChapterId = table.Column<int>(type: "int", nullable: false),
                    PlayresultId = table.Column<int>(type: "int", nullable: false),
                    StageName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeathCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageDeathInfo", x => x.StageId);
                    table.ForeignKey(
                        name: "FK_StageDeathInfo_PlayerChapterInfo_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "PlayerChapterInfo",
                        principalColumn: "ChapterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StageDeathInfo_userplaydata_PlayresultId",
                        column: x => x.PlayresultId,
                        principalTable: "userplaydata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DeathInfo",
                columns: table => new
                {
                    DeathId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StageId = table.Column<int>(type: "int", nullable: false),
                    EnemyName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeathPositionX = table.Column<float>(type: "float", nullable: false),
                    DeathPositionY = table.Column<float>(type: "float", nullable: false),
                    EnemyPositionX = table.Column<float>(type: "float", nullable: false),
                    EnemyPositionY = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeathInfo", x => x.DeathId);
                    table.ForeignKey(
                        name: "FK_DeathInfo_StageDeathInfo_StageId",
                        column: x => x.StageId,
                        principalTable: "StageDeathInfo",
                        principalColumn: "StageId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DeathInfo_StageId",
                table: "DeathInfo",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerChapterInfo_PlayresultId",
                table: "PlayerChapterInfo",
                column: "PlayresultId");

            migrationBuilder.CreateIndex(
                name: "IX_StageDeathInfo_ChapterId",
                table: "StageDeathInfo",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_StageDeathInfo_PlayresultId",
                table: "StageDeathInfo",
                column: "PlayresultId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeathInfo");

            migrationBuilder.DropTable(
                name: "StageDeathInfo");

            migrationBuilder.DropTable(
                name: "PlayerChapterInfo");

            migrationBuilder.DropTable(
                name: "userplaydata");
        }
    }
}
