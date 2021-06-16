using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NovaVidaApplication.Migrations
{
    public partial class Atualizacao_valores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Mensalidade",
                table: "Alunos",
                type: "decimal(30,20)",
                precision: 30,
                scale: 20,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,10)",
                oldPrecision: 12,
                oldScale: 10);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataVencimentoMensalidade",
                table: "Alunos",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Mensalidade",
                table: "Alunos",
                type: "decimal(12,10)",
                precision: 12,
                scale: 10,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(30,20)",
                oldPrecision: 30,
                oldScale: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataVencimentoMensalidade",
                table: "Alunos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
