using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaGastos.API.Migrations
{
    /// <inheritdoc />
    public partial class SyncModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Tipo",
                table: "Transacoes",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_DataDeCriacao",
                table: "Transacoes",
                column: "DataDeCriacao");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_PessoaId_DataDeCriacao",
                table: "Transacoes",
                columns: new[] { "PessoaId", "DataDeCriacao" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transacoes_DataDeCriacao",
                table: "Transacoes");

            migrationBuilder.DropIndex(
                name: "IX_Transacoes_PessoaId_DataDeCriacao",
                table: "Transacoes");

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "Transacoes",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
