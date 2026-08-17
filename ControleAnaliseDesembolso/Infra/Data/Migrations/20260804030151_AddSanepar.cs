using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleAnaliseDesembolso.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSanepar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SANEPAR",
                table: "CAD_TB002_FICHA_PEDIDO_DESEMBOLSO",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SANEPAR",
                table: "CAD_TB002_FICHA_PEDIDO_DESEMBOLSO");
        }
    }
}
