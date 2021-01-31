using Microsoft.EntityFrameworkCore.Migrations;

namespace Simple_Online_Voitng_System.Data.Migrations
{
    public partial class updatesomething : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "MemberManList");

            migrationBuilder.CreateTable(
                name: "ChairmanList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePicPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SymbolPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalVote = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChairmanList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemberWomenList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePicPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SymbolPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalVote = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberWomenList", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChairmanList");

            migrationBuilder.DropTable(
                name: "MemberWomenList");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "MemberManList",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
