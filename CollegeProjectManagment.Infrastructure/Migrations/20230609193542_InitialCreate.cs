using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeProjectManagment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "team",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_team", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "member",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrestigePoints = table.Column<int>(type: "int", nullable: true),
                    KnownTechnologies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SkillRatings = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_member", x => x.MemberId);
                    table.ForeignKey(
                        name: "FK_member_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "role",
                        principalColumn: "RoleId");
                    table.ForeignKey(
                        name: "FK_member_team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "team",
                        principalColumn: "TeamId");
                });

            migrationBuilder.CreateTable(
                name: "project",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requirements = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfMembers = table.Column<int>(type: "int", nullable: false),
                    TechnologyStack = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgrammingLanguages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DifficultyLevel = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    PlannedEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RepositoryLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignedTeamId = table.Column<int>(type: "int", nullable: false),
                    LeaderId = table.Column<int>(type: "int", nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_project_member_LeaderId",
                        column: x => x.LeaderId,
                        principalTable: "member",
                        principalColumn: "MemberId");
                    table.ForeignKey(
                        name: "FK_project_team_AssignedTeamId",
                        column: x => x.AssignedTeamId,
                        principalTable: "team",
                        principalColumn: "TeamId");
                    table.ForeignKey(
                        name: "FK_project_team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "team",
                        principalColumn: "TeamId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_member_RoleId",
                table: "member",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_member_TeamId",
                table: "member",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_project_AssignedTeamId",
                table: "project",
                column: "AssignedTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_project_LeaderId",
                table: "project",
                column: "LeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_project_TeamId",
                table: "project",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "project");

            migrationBuilder.DropTable(
                name: "member");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "team");
        }
    }
}
