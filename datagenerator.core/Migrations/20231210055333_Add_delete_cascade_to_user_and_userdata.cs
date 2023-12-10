using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace datagenerator.core.Migrations
{
    /// <inheritdoc />
    public partial class Add_delete_cascade_to_user_and_userdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USERS_USERDATA_UserDataID",
                table: "USERS");

            migrationBuilder.DropIndex(
                name: "IX_USERS_Nickname",
                table: "USERS");

            migrationBuilder.DropIndex(
                name: "IX_USERS_UserDataID",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "UserDataID",
                table: "USERS");

            migrationBuilder.AlterColumn<string>(
                name: "Nickname",
                table: "USERS",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "",
                comment: "Псевдоним пользователя",
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32,
                oldNullable: true,
                oldComment: "Псевдоним пользователя");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "USERDATA",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Хешированный пароль",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Хешированный пароль");

            migrationBuilder.AddColumn<Guid>(
                name: "UserID",
                table: "USERDATA",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "Ссылка на пользователя");

            migrationBuilder.CreateIndex(
                name: "IX_USERS_Nickname",
                table: "USERS",
                column: "Nickname",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USERDATA_UserID",
                table: "USERDATA",
                column: "UserID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_USERDATA_USERS_UserID",
                table: "USERDATA",
                column: "UserID",
                principalTable: "USERS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USERDATA_USERS_UserID",
                table: "USERDATA");

            migrationBuilder.DropIndex(
                name: "IX_USERS_Nickname",
                table: "USERS");

            migrationBuilder.DropIndex(
                name: "IX_USERDATA_UserID",
                table: "USERDATA");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "USERDATA");

            migrationBuilder.AlterColumn<string>(
                name: "Nickname",
                table: "USERS",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true,
                comment: "Псевдоним пользователя",
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32,
                oldComment: "Псевдоним пользователя");

            migrationBuilder.AddColumn<Guid>(
                name: "UserDataID",
                table: "USERS",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "Ссылка на пользовательские данные");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "USERDATA",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Хешированный пароль",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "Хешированный пароль");

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

            migrationBuilder.AddForeignKey(
                name: "FK_USERS_USERDATA_UserDataID",
                table: "USERS",
                column: "UserDataID",
                principalTable: "USERDATA",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
