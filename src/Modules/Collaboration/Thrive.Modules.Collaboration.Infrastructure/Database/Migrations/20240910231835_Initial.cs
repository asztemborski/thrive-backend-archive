using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thrive.Modules.Collaboration.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "collaboration");

            migrationBuilder.CreateTable(
                name: "Workspaces",
                schema: "collaboration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workspaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                schema: "collaboration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkspaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => new { x.Id, x.WorkspaceId });
                    table.ForeignKey(
                        name: "FK_Members_Workspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalSchema: "collaboration",
                        principalTable: "Workspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "collaboration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    WorkspaceId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Workspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalSchema: "collaboration",
                        principalTable: "Workspaces",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Thread",
                schema: "collaboration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkspaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thread", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Thread_Workspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalSchema: "collaboration",
                        principalTable: "Workspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThreadCategory",
                schema: "collaboration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Index = table.Column<int>(type: "integer", nullable: false),
                    WorkspaceId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreadCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThreadCategory_Workspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalSchema: "collaboration",
                        principalTable: "Workspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "collaboration",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Permissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "collaboration",
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Members_WorkspaceId",
                schema: "collaboration",
                table: "Members",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_RoleId",
                schema: "collaboration",
                table: "Permissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_WorkspaceId",
                schema: "collaboration",
                table: "Roles",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Thread_WorkspaceId",
                schema: "collaboration",
                table: "Thread",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadCategory_WorkspaceId",
                schema: "collaboration",
                table: "ThreadCategory",
                column: "WorkspaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members",
                schema: "collaboration");

            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "collaboration");

            migrationBuilder.DropTable(
                name: "Thread",
                schema: "collaboration");

            migrationBuilder.DropTable(
                name: "ThreadCategory",
                schema: "collaboration");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "collaboration");

            migrationBuilder.DropTable(
                name: "Workspaces",
                schema: "collaboration");
        }
    }
}
