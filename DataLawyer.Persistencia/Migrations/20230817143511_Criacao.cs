using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLawyer.Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class Criacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogDeErro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Mensagem = table.Column<string>(type: "TEXT", nullable: true),
                    DataHoraNumerica = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogDeErro", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Processo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Numero = table.Column<string>(type: "TEXT", nullable: true),
                    Grau = table.Column<int>(type: "INTEGER", nullable: false),
                    Classe = table.Column<string>(type: "TEXT", nullable: true),
                    Area = table.Column<string>(type: "TEXT", nullable: true),
                    Assunto = table.Column<string>(type: "TEXT", nullable: true),
                    Origem = table.Column<string>(type: "TEXT", nullable: true),
                    Distribuicao = table.Column<string>(type: "TEXT", nullable: true),
                    Relator = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovimentacaoDeProcesso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProcessoId = table.Column<int>(type: "INTEGER", nullable: true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    DataHoraNumerica = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentacaoDeProcesso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimentacaoDeProcesso_Processo_ProcessoId",
                        column: x => x.ProcessoId,
                        principalTable: "Processo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacaoDeProcesso_ProcessoId",
                table: "MovimentacaoDeProcesso",
                column: "ProcessoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogDeErro");

            migrationBuilder.DropTable(
                name: "MovimentacaoDeProcesso");

            migrationBuilder.DropTable(
                name: "Processo");
        }
    }
}
