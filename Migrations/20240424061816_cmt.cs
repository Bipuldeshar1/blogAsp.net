using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blogg.Migrations
{
    /// <inheritdoc />
    public partial class cmt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommentModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    blogModelId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    appUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentModels_AspNetUsers_appUserId",
                        column: x => x.appUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CommentModels_blogModels_blogModelId",
                        column: x => x.blogModelId,
                        principalTable: "blogModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentModels_appUserId",
                table: "CommentModels",
                column: "appUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentModels_blogModelId",
                table: "CommentModels",
                column: "blogModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentModels");
        }
    }
}
