using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleAnaliseDesembolso.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAtivoValidacaoRegistro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ATIVO",
                table: "CAD_TB006_VALIDACAO_REGISTRO",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ATIVO",
                table: "CAD_TB006_VALIDACAO_REGISTRO");
        }
    }
}
