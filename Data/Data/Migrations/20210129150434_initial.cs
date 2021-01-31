using Microsoft.EntityFrameworkCore.Migrations;

namespace Simple_Online_Voitng_System.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MemberManList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePicPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SymbolPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalVote = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberManList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VoterList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVotedChairman = table.Column<bool>(type: "bit", nullable: false),
                    IsVotedMemberMan = table.Column<bool>(type: "bit", nullable: false),
                    IvotedMemberWomen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoterList", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberManList");

            migrationBuilder.DropTable(
                name: "VoterList");
        }
    }
}
