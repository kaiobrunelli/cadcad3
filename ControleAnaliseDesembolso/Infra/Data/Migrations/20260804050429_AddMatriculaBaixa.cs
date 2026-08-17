using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleAnaliseDesembolso.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMatriculaBaixa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MATRICULA_BAIXA",
                table: "CAD_TB003_DESEMBOLSO",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MATRICULA_BAIXA",
                table: "CAD_TB003_DESEMBOLSO");
        }
    }
}
