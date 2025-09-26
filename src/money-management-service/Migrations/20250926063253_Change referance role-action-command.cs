using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace money_management_service.Migrations
{
    /// <inheritdoc />
    public partial class Changereferanceroleactioncommand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandFunction");

            migrationBuilder.DropTable(
                name: "FunctionRole");

            migrationBuilder.CreateTable(
                name: "ROLE_FUNCTION_COMMAND",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FunctionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLE_FUNCTION_COMMAND", x => new { x.RoleId, x.FunctionId, x.CommandId });
                    table.ForeignKey(
                        name: "FK_ROLE_FUNCTION_COMMAND_COMMANDS_CommandId",
                        column: x => x.CommandId,
                        principalTable: "COMMANDS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ROLE_FUNCTION_COMMAND_FUNCTIONS_FunctionId",
                        column: x => x.FunctionId,
                        principalTable: "FUNCTIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ROLE_FUNCTION_COMMAND_ROLES_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ROLES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ROLE_FUNCTION_COMMAND_CommandId",
                table: "ROLE_FUNCTION_COMMAND",
                column: "CommandId");

            migrationBuilder.CreateIndex(
                name: "IX_ROLE_FUNCTION_COMMAND_FunctionId",
                table: "ROLE_FUNCTION_COMMAND",
                column: "FunctionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ROLE_FUNCTION_COMMAND");

            migrationBuilder.CreateTable(
                name: "CommandFunction",
                columns: table => new
                {
                    CommandsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FunctionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandFunction", x => new { x.CommandsId, x.FunctionsId });
                    table.ForeignKey(
                        name: "FK_CommandFunction_COMMANDS_CommandsId",
                        column: x => x.CommandsId,
                        principalTable: "COMMANDS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandFunction_FUNCTIONS_FunctionsId",
                        column: x => x.FunctionsId,
                        principalTable: "FUNCTIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FunctionRole",
                columns: table => new
                {
                    FunctionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    rolesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionRole", x => new { x.FunctionsId, x.rolesId });
                    table.ForeignKey(
                        name: "FK_FunctionRole_FUNCTIONS_FunctionsId",
                        column: x => x.FunctionsId,
                        principalTable: "FUNCTIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FunctionRole_ROLES_rolesId",
                        column: x => x.rolesId,
                        principalTable: "ROLES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommandFunction_FunctionsId",
                table: "CommandFunction",
                column: "FunctionsId");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionRole_rolesId",
                table: "FunctionRole",
                column: "rolesId");
        }
    }
}
