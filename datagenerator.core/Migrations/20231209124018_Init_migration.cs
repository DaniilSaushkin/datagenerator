using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace datagenerator.core.Migrations
{
    /// <inheritdoc />
    public partial class Init_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TEMPLATES",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Идентификатор"),
                    Name = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false, comment: "Имя шаблона"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленного шаблона")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEMPLATES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USERDATA",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Идентификатор"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Хешированный пароль")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERDATA", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ITEMS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Идентификатор"),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false, comment: "Имя предмета"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленного предмета"),
                    TemplateID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Ссылка на шаблон")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITEMS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ITEMS_TEMPLATES_TemplateID",
                        column: x => x.TemplateID,
                        principalTable: "TEMPLATES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Идентификатор"),
                    Nickname = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "Псевдоним пользователя"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленного пользователя"),
                    UserDataID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Ссылка на пользовательские данные")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USERS_USERDATA_UserDataID",
                        column: x => x.UserDataID,
                        principalTable: "USERDATA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ITEMS_Name",
                table: "ITEMS",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ITEMS_TemplateID",
                table: "ITEMS",
                column: "TemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_TEMPLATES_Name",
                table: "TEMPLATES",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USERS_Nickname",
                table: "USERS",
                column: "Nickname",
                unique: true,
                filter: "[Nickname] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_USERS_UserDataID",
                table: "USERS",
                column: "UserDataID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ITEMS");

            migrationBuilder.DropTable(
                name: "USERS");

            migrationBuilder.DropTable(
                name: "TEMPLATES");

            migrationBuilder.DropTable(
                name: "USERDATA");
        }
    }
}
