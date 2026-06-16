using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApi.Migrations
{
    /// <inheritdoc />
    public partial class MovieActorRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieActor",
                columns: table => new
                {
                    ActorsId = table.Column<int>(type: "int", nullable: false),
                    MoviesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieActor", x => new { x.ActorsId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_MovieActor_Actors_ActorsId",
                        column: x => x.ActorsId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieActor_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieActor_MoviesId",
                table: "MovieActor",
                column: "MoviesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieActor");
        }
    }
}
