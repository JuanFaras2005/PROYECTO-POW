using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CrearTablaCursos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Profesores_ProfesorId",
                table: "Cursos");

            migrationBuilder.AlterColumn<string>(
                name: "ProfesorId",
                table: "Cursos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfesorId1",
                table: "Cursos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_ProfesorId1",
                table: "Cursos",
                column: "ProfesorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_AspNetUsers_ProfesorId",
                table: "Cursos",
                column: "ProfesorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Profesores_ProfesorId1",
                table: "Cursos",
                column: "ProfesorId1",
                principalTable: "Profesores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_AspNetUsers_ProfesorId",
                table: "Cursos");

            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Profesores_ProfesorId1",
                table: "Cursos");

            migrationBuilder.DropIndex(
                name: "IX_Cursos_ProfesorId1",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "ProfesorId1",
                table: "Cursos");

            migrationBuilder.AlterColumn<int>(
                name: "ProfesorId",
                table: "Cursos",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Profesores_ProfesorId",
                table: "Cursos",
                column: "ProfesorId",
                principalTable: "Profesores",
                principalColumn: "Id");
        }
    }
}
